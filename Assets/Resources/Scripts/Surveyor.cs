using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Surveyor : MonoBehaviour {

    public SurveyorStand Stand;
    public Transform BaseTransform;
    private Rigidbody _rb;
	// Use this for initialization
	void Start () {
        Stand = GetComponentInChildren<SurveyorStand>();
        BaseTransform = GameObject.Find("SurveyorBase").transform;
        _rb = GetComponent<Rigidbody>();

        if(Stand)
        {
            Stand.HeightChanged += Stand_HeightChanged;
        }
	}

    private void Stand_HeightChanged(float newHeight)
    {
        //update height
        var position = BaseTransform.position;
        position.y = newHeight;
        BaseTransform.position = position;
    }

}
