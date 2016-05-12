using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NumericValueScript : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start () {
        this.text = this.GetComponentInChildren<Text>();
	}

    public int Value() {
        // TODO Traverse the graph
        return System.Int32.Parse(this.text.text);
    }
}
