using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SurveyStick : MonoBehaviour {
    public Material SourceMat;

	// Use this for initialization
	void Start () {
        SourceMat = transform.Find("Renderer").GetComponent<Renderer>().sharedMaterial;
	}
	
	// Update is called once per frame
	void Update () {
        SourceMat.mainTextureScale = new Vector2(0.4f, transform.localScale.y * 0.1f);
	}
}
