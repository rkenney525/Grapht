using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Grapht.Config;
using SimpleJSON;

/// <summary>
/// Loads level data from a json file, converts it into C# objects, and adapts the Scene
/// </summary>
public class LevelLoaderScript : MonoBehaviour {
    /// <summary>
    /// The name of the GameObject that contains the Canvas
    /// </summary>
    private const string CANVAS_OBJECT_NAME = "Canvas";

    /// <summary>
    /// Reference to the moveable node prefab
    /// </summary>
    public GameObject MoveableNodeRef;

    /// <summary>
    /// Reference to the immoveable node prefab
    /// </summary>
    public GameObject ImmoveableNodeRef;

    /// <summary>
    /// The Canvas GameObject which contains all nodes
    /// </summary>
    private GameObject canvas;

    /// <summary>
    /// The watcher for end of game conditions, configurable by level
    /// </summary>
    private VictoryWatcherScript watcher;

    /// <summary>
    /// List of levels loaded by the game
    /// </summary>
    private IList<Level> levels;

    /// <summary>
    /// 
    /// </summary>
    private int currentLevel;
    
    /// <summary>
    /// Load all references/external data on component creation
    /// </summary>
	void Start () {
        canvas = GameObject.Find(CANVAS_OBJECT_NAME);
        watcher = FindObjectOfType<VictoryWatcherScript>();
        // TODO load currentLevel from save data
        currentLevel = 1;
        levels = LoadLevels();
        LoadLevel(currentLevel);
    }

    /// <summary>
    /// Loads level data from Levels.json and puts it into a List.
    /// </summary>
    /// <returns>A List of Level objects</returns>
    private IList<Level> LoadLevels() {
        TextAsset targetFile = Resources.Load<TextAsset>("Levels");
        JSONNode data = JSONNode.Parse(targetFile.text);
        return data.AsArray.Childs.Select(level => (Level.Parse(level))).ToList();
    }

    /// <summary>
    /// Load the specified level id
    /// </summary>
    /// <param name="levelId">The id tag of the Level to load</param>
    private void LoadLevel(int levelId) {
        // Get the appropriate level
        Level level = levels.Where(lev => lev.Id == levelId).First();

        // Add the specified nodes and their children
        PlaceNodesInWorld(level.Nodes);

        // Configure the victory conditions
        watcher.LoadLevel(level);
    }

    /// <summary>
    /// Place the list of nodes in the Scene
    /// </summary>
    /// <param name="nodes">The nodes to place in the Scene</param>
    private IList<GameObject> PlaceNodesInWorld(IList<Node> nodes) {
        return nodes.Select(node => {
            // Get the prefab
            GameObject nodePrefab = Instantiate(
                (node.NodeType == Node.Type.IMMOVEABLE) ? ImmoveableNodeRef : MoveableNodeRef);
            SetUpPrefab(nodePrefab);

            // Add to the canvas
            nodePrefab.transform.parent = canvas.transform;
            nodePrefab.transform.position = node.Position;

            // Set the value
            NumericValueScript valueScript = nodePrefab.GetComponent<NumericValueScript>();
            valueScript.SetValue(node.Value);

            // Add the children to the world and graph
            TreeNodeScript nodeScript = nodePrefab.GetComponent<TreeNodeScript>();
            PlaceNodesInWorld(node.Children).All(childPrefab => {
                TreeNodeScript childScript = childPrefab.GetComponent<TreeNodeScript>();
                nodeScript.AddNewChild(childScript);
                return true;
            });

            // Return the prefab
            return nodePrefab;
        }).ToList();
    }

    /// <summary>
    /// Load components that might not have been loaded during instantiate
    /// TODO replace this with a more streamlined system
    /// </summary>
    /// <param name="nodePrefab">The prefab to instantiate</param>
    private void SetUpPrefab(GameObject nodePrefab) {
        nodePrefab.GetComponent<NumericValueScript>().SetUp();
    }

    /// <summary>
    /// Load the next Level
    /// </summary>
    public void NextLevel() {
        LoadLevel(++currentLevel);
    }

    /// <summary>
    /// Remove all nodes from the Scene
    /// </summary>
    private void ClearStage() {
        for (int i = 0; i < canvas.transform.childCount; i++) {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }
}
