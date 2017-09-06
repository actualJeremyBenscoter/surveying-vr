using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyorBase : MonoBehaviour
{
    public List<Transform> Legs;

    // Use this for initialization
    void Awake()
    {
        if (Legs.Count != 3)
        {
            Debug.LogError("Surveyor base must have 3 legs.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            var normal = GetNormal(Legs[0].position, Legs[1].position, Legs[2].position);
            transform.up = normal;

            transform.RotateAround(transform.position, transform.up, transform.localEulerAngles.y);
        }
    }

    private Vector3 GetNormal(Vector3 coor1, Vector3 coor2, Vector3 coor3)
    {
        Vector3 v = coor2 - coor1;
        Vector3 w = coor3 - coor1;

        return Vector3.Cross(v, w);
    }
}
