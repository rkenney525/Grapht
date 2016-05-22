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
    /// Load all references/external data on component creation
    /// </summary>
	void Start () {
        canvas = GameObject.Find(CANVAS_OBJECT_NAME);
        watcher = FindObjectOfType<VictoryWatcherScript>();
        // TODO remove this line after testing and when the stage is initially clean
        ClearStage();
        ////////////
        levels = LoadLevels();
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
    /// Load the next Level
    /// </summary>
    public void NextLevel() {

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
