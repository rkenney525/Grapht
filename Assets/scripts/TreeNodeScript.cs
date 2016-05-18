using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(NumericValueScript))]
public class TreeNodeScript : MonoBehaviour {

    private const float LINE_HEIGHT = 4;

    private const int CONNECTION_LIMIT = 4;

    private const int CHILD_WIDTH = 10;

    public GameObject BindingRef;

    public TreeNodeScript ParentNode { get; private set; }

    private NumericValueScript value;

    private IDictionary<TreeNodeScript, Binding> children = new Dictionary<TreeNodeScript, Binding>();

    void Start() {
        this.value = this.GetComponent<NumericValueScript>();
    }

    public void AddNewChild(TreeNodeScript childNode) {
        // Create Binding and add to the parent
        GameObject bindingObj = Instantiate(BindingRef);
        bindingObj.transform.parent = this.transform;
        Binding binding = bindingObj.GetComponent<Binding>();
        binding.SetUp(childNode);

        // Add to map
        this.children.Add(childNode, binding);

        // Update angles
        this.SetChildAngles();

        // Update child
        childNode.ChangeParent(this);
    }

    public void Detach() {
        this.ChangeParent(null);
    }

    public void ChangeParent(TreeNodeScript parentNode) {
        // Tell previous parent
        if (this.ParentNode != null) {
            this.ParentNode.LoseChild(this);
        }

        // Change parent reference
        this.ParentNode = parentNode;
    }

    public void LoseChild(TreeNodeScript childNode) {
        Destroy(this.children[childNode].gameObject);
        this.children.Remove(childNode);
        this.SetChildAngles();
    }

    public bool CanAcceptConnection() {
        return this.children.Values.Count < CONNECTION_LIMIT;
    }

    private void SetChildAngles() {
        // TODO sort the list
        Binding[] bindings = this.children.Values.ToArray<Binding>();
        float space = CHILD_WIDTH / (float)(bindings.Length + 1);
        float start = -CHILD_WIDTH / 2;
        Vector2 angle;
        for (int i = 0; i < bindings.Length; i++) {
            float x = start + (((float)i + 1) * space);
            angle = new Vector2(x, -LINE_HEIGHT);
            bindings[i].UpdateAngle(angle);
        }
    }

    public int BranchValue() {
        return this.value.Value() + (this.ParentNode == null ? 0 : this.ParentNode.BranchValue());
    }

    public bool IsLeaf() {
        return this.children.Count == 0;
    }

    public TreeNodeScript Root() {
        return (this.ParentNode == null) ? this : this.ParentNode.Root();
    }
}
