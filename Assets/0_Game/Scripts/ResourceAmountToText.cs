using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceAmountToText : MonoBehaviour {

    public Resource resource;
    private Text text;
    // Use this for initialization
    void Awake () {
        text = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = resource.ResourceCapacity + "";
	}
}
