using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class TreeNodeScript : MonoBehaviour {

    private const float LINE_DISTANCE = 5;

    public GameObject BindingRef;

    public TreeNodeScript ParentNode { get; private set; }

    private IDictionary<TreeNodeScript, Binding> children = new Dictionary<TreeNodeScript, Binding>();

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
    }

    public void ChangeParent(TreeNodeScript parentNode) {
        // Tell previous parent
        if (this.ParentNode != null) {
            this.ParentNode.LoseChild(this);
        }

        // Change parent reference
        this.ParentNode = parentNode;
    }

    public Vector2 GetChildOffset() {
        // TODO make this more dynamic
        float xVal = (float) Math.Cos(Math.PI * 1.25) * LINE_DISTANCE;
        float yVal = (float) Math.Sin(Math.PI * 1.25) * LINE_DISTANCE;
        return new Vector2(xVal, yVal);
    }

    public void LoseChild(TreeNodeScript childNode) {
        this.children.Remove(childNode);
    }
}
