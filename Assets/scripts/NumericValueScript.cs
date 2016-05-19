using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Captures the numeric value of a single node
/// </summary>
public class NumericValueScript : MonoBehaviour {

    /// <summary>
    /// The Text field containing the numeric value
    /// </summary>
    private Text text;

    /// <summary>
    /// Get the Text field when this component is created
    /// </summary>
	void Start () {
        this.text = this.GetComponentInChildren<Text>();
	}

    /// <summary>
    /// Get the numeric value of this node
    /// </summary>
    /// <returns>The numeric value stored in the Text field</returns>
    public int Value() {
        return System.Int32.Parse(this.text.text);
    }
}
