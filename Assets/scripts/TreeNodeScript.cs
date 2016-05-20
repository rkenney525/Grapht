using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages children, parents, and all graph related functions
/// </summary>
[RequireComponent(typeof(NumericValueScript))]
public class TreeNodeScript : MonoBehaviour {

    /// <summary>
    /// The vertical distance bindings should have
    /// </summary>
    private const float LINE_HEIGHT = 4;

    /// <summary>
    /// The maximum number of children a parent node can have
    /// </summary>
    private const int CONNECTION_LIMIT = 4;

    /// <summary>
    /// The horizontal space that child nodes will be distributed in
    /// </summary>
    private const int CHILD_WIDTH = 11;

    /// <summary>
    /// TODO find a way to set this outside of the editor
    /// A reference to the Binding prefab
    /// </summary>
    public GameObject BindingRef;

    /// <summary>
    /// The parent of this node
    /// </summary>
    public TreeNodeScript ParentNode { get; private set; }

    /// <summary>
    /// The NumericValueScript responsible for returning this nodes value
    /// </summary>
    private NumericValueScript value;

    /// <summary>
    /// A Map of children and their respective Binding
    /// </summary>
    private IDictionary<TreeNodeScript, Binding> children = new Dictionary<TreeNodeScript, Binding>();

    /// <summary>
    /// Get the NumericValueScript when the component is created
    /// </summary>
    void Start() {
        this.value = this.GetComponent<NumericValueScript>();
    }

    /// <summary>
    /// Add a new child to this node
    /// </summary>
    /// <param name="childNode">The child to add</param>
    public void AddNewChild(TreeNodeScript childNode) {
        // Create Binding and add to the parent
        GameObject bindingObj = Instantiate(BindingRef);
        bindingObj.transform.parent = this.transform;
        Binding binding = bindingObj.GetComponent<Binding>();
        binding.SetUp(childNode);

        // Add to map
        this.children.Add(childNode, binding);

        // Update child
        childNode.ChangeParent(this);

        // Update angles
        this.UpdateAnglesFromRoot();
    }

    /// <summary>
    /// Make this node an orphan
    /// </summary>
    public void Detach() {
        this.ChangeParent(null);
    }

    /// <summary>
    /// Change the parent node of this node to be "parentNode"
    /// </summary>
    /// <param name="parentNode">The new parent node</param>
    public void ChangeParent(TreeNodeScript parentNode) {
        // Tell previous parent
        if (this.ParentNode != null) {
            this.ParentNode.LoseChild(this);
        }

        // Change parent reference
        this.ParentNode = parentNode;
    }

    /// <summary>
    /// Remove the specified child
    /// </summary>
    /// <param name="childNode">The child to lose</param>
    public void LoseChild(TreeNodeScript childNode) {
        Destroy(this.children[childNode].gameObject);
        this.children.Remove(childNode);
        this.UpdateAnglesFromRoot();
    }

    /// <summary>
    /// Check if this node can have another child
    /// </summary>
    /// <returns>True if this node can add another child, false otherwise</returns>
    public bool CanAcceptConnection() {
        return this.children.Values.Count < CONNECTION_LIMIT;
    }

    /// <summary>
    /// Set the angles of all children for the current level
    /// </summary>
    /// <param name="remainingDepth">The layers of children left in the graph</param>
    private void SetChildAngles(int remainingDepth) {
        // Get the bindings
        Binding[] bindings = this.children.Select(set => set.Value).OrderBy(binding => binding.Offset()).ToArray<Binding>();

        // Calculate the distances between nodes
        float layerWidth = ((float) CHILD_WIDTH * remainingDepth) * 1.5f;
        float space = layerWidth / (float)(bindings.Length + 1);
        float start = -layerWidth / 2;

        // Manipulate the angle of the nodes
        Vector2 angle;
        for (int i = 0; i < bindings.Length; i++) {
            float x = start + (((float)i + 1) * space);
            angle = new Vector2(x, -LINE_HEIGHT);
            bindings[i].UpdateAngle(angle);
        }

        // Update the grandchildren
        foreach (TreeNodeScript child in this.children.Select(set => set.Key).ToArray()) {
            child.SetChildAngles(remainingDepth - 1);
        }
    }

    private void UpdateAnglesFromRoot() {
        // Get root's max depth
        TreeNodeScript root = this.Root();
        int depth = root.MaxDepth();

        // Update angles recursively
        root.SetChildAngles(depth - 1);
    }

    /// <summary>
    /// Get the numeric value of this node and the sum of all the parent nodes
    /// </summary>
    /// <returns>The numeric value of this node and its parents</returns>
    public int BranchValue() {
        return this.value.Value() + (this.ParentNode == null ? 0 : this.ParentNode.BranchValue());
    }

    /// <summary>
    /// Check if the node is a leaf, which is to say that the node has no children
    /// </summary>
    /// <returns>True if the node is a leaf, false otherwise</returns>
    public bool IsLeaf() {
        return this.children.Count == 0;
    }

    /// <summary>
    /// Check if the current node is the root of its tree
    /// </summary>
    /// <returns>True if the node is a root node, false otherwise</returns>
    public bool IsRoot() {
        return this.ParentNode == null;
    }

    /// <summary>
    /// Get the root parent for this node. This is the node furthest up (parent direction) of the graph
    /// </summary>
    /// <returns>The root parent for this node</returns>
    public TreeNodeScript Root() {
        return (this.IsRoot()) ? this : this.ParentNode.Root();
    }
    /// <summary>
    /// Gets the number of layers in the graph
    /// </summary>
    /// <returns>The number of layers deep the graph has</returns>
    private int MaxDepth() {
        int max = 1;
        if (this.children.Count > 0) {
            max += this.children.Max(child => child.Key.MaxDepth());
        }
        return max;
    }
}
