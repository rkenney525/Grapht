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
    /// TODO make this a value calculated from the width of the sprite
    /// The width a single node with no children needs
    /// </summary>
    private const float SINGLE_NODE_WIDTH = 3.25f;

    /// <summary>
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
    /// The width of the node
    /// </summary>
    private float width;

    /// <summary>
    /// Get the NumericValueScript and other properties when the component is created
    /// </summary>
    void Awake() {
        this.value = this.GetComponent<NumericValueScript>();
        this.width = SINGLE_NODE_WIDTH;
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
    /// Update the width of the node and its children
    /// </summary>
    private void UpdateWidth() {
        if (this.IsLeaf()) {
            this.width = SINGLE_NODE_WIDTH;
        } else {
            this.width = this.children.Select(set => set.Key).Sum(node => {
                node.UpdateWidth();
                return node.width;
            });
        }
    }

    /// <summary>
    /// Set the angles of all children
    /// </summary>
    private void SetChildAngles() {
        // Get the width of the node
        float width = this.width;

        // Set the angles for each child
        float position = -width / 2.0f;
        this.children.All(set => {
            // Get the position and set the node there
            float offset = set.Key.width / 2.0f;
            position += offset;
            Vector2 angle = new Vector2(position, -LINE_HEIGHT);
            set.Value.UpdateAngle(angle);
            position += offset;
            // Update the child
            set.Key.SetChildAngles();
            // return success
            return true;
        });
    }

    /// <summary>
    /// Update all angles starting from the top of the tree
    /// </summary>
    private void UpdateAnglesFromRoot() {
        // Update the root and its children's widths
        // Note that this is slightly wasteful since we have to traverse the tree later anyway, but this is more explicit at least.
        TreeNodeScript root = this.Root();
        root.UpdateWidth();

        // Update angles recursively
        root.SetChildAngles();
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
    /// Check if a node is equal to or has as a parent a particular node
    /// </summary>
    /// <param name="parent">The node to check</param>
    /// <returns>True if the node is equal to the current node or any of its ancestors, false otherwise</returns>
    public bool HasParent(TreeNodeScript parent) {
        return this.Equals(parent) ? true : (IsRoot() ? false : ParentNode.HasParent(parent));
    }

    /// <summary>
    /// Gets the number of layers in the graph
    /// </summary>
    /// <returns>The number of layers deep the graph has</returns>
    public int Depth() {
        int max = 1;
        if (this.children.Count > 0) {
            max += this.children.Max(child => child.Key.Depth());
        }
        return max;
    }
}
