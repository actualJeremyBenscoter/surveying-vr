using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SurveyStick : MonoBehaviour {
    public Material SourceMat;
    public bool StickToGround = false;

	// Use this for initialization
	void Start () {
        SourceMat = transform.Find("Renderer").GetComponent<Renderer>().sharedMaterial;
	}
	
	// Update is called once per frame
	void Update () {
        SourceMat.mainTextureScale = new Vector2(0.4f, transform.localScale.y * 0.1f);

        if (StickToGround)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + transform.up, -transform.up, out hit, 100f))
            {
                transform.position = hit.point;
            }
        }
    }
}
