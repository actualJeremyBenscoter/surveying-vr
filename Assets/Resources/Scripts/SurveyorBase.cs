using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyorBase : MonoBehaviour
{
    public List<Transform> Legs;
    public LayerMask GroundHitMask;
    public float LevelTolerance = 0.05f;

    [HideInInspector]
    public float StickHeight = 0f;
    [HideInInspector]
    public float Distance = 0f;
    [HideInInspector]
    public bool IsLevel = false;

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

        var oldLocalYEuler = transform.localEulerAngles.y;

        var normal = GetNormal(Legs[0].position, Legs[1].position, Legs[2].position);

        var center = GetCenterPoint(Legs[0].position, Legs[1].position, Legs[2].position);
        Debug.DrawRay(center, normal);

        transform.up = normal;

        var localEulers = transform.localEulerAngles;
        localEulers.y = oldLocalYEuler;

        transform.localEulerAngles = localEulers;


        UpdateDistanceAndHeight();
        CheckIfLevel();
    }

    private Vector3 GetCenterPoint(params Vector3[] points)
    {
        Vector3 minVector = points[0];
        Vector3 maxVector = points[0];

        for(int i = 1; i < points.Length; i++)
        {
            if(minVector.x > points[i].x)
            {
                minVector.x = points[i].x;
            }
            if (minVector.y > points[i].y)
            {
                minVector.y = points[i].y;
            }
            if (minVector.z > points[i].z)
            {
                minVector.z = points[i].z;
            }
            if (maxVector.x < points[i].x)
            {
                maxVector.x = points[i].x;
            }
            if (maxVector.y < points[i].y)
            {
                maxVector.y = points[i].y;
            }
            if (maxVector.z < points[i].z)
            {
                maxVector.z = points[i].z;
            }
        }

        return new Vector3((minVector.x + maxVector.x) * 0.5f,
                                      (minVector.y + maxVector.y) * 0.5f,
                                      (minVector.z + maxVector.z) * 0.5f);
    }
    private void CheckIfLevel()
    {
        //TODO check if surveyor is level here, this code doesn't work

        Vector3 eulers = transform.eulerAngles;
        float xRot, zRot;
        Vector3 axis = Vector3.right;
        transform.rotation.ToAngleAxis(out xRot, out axis);
        axis = Vector3.forward;
        transform.rotation.ToAngleAxis(out zRot, out axis);
        //ignore Y rotation
        eulers.y = 0f;

        IsLevel = (eulers.x < LevelTolerance || eulers.x > 360f - LevelTolerance) && (eulers.z < LevelTolerance || eulers.z > 360f - LevelTolerance);
    }

    private void UpdateDistanceAndHeight()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.forward * 1000f, Color.yellow);
        if (Physics.Linecast(transform.position, transform.forward * 1000f, out hit))
        {
            if (hit.collider.gameObject.tag == "SurveyStick")
            {
                Distance = hit.distance;
                if (Physics.Linecast(hit.point, hit.point - Vector3.up * 1000f, out hit, GroundHitMask))
                {
                    StickHeight = hit.distance;
                }
            }
            else
            {
                Distance = -1f; //did not hit stick, no distance to set
                StickHeight = -1f;
            }
        }
        else
        {
            Distance = -1f; //did not hit stick, no distance to set
            StickHeight = -1f;
        }
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 100f))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);
        }

        var normal = GetNormal(Legs[0].position, Legs[1].position, Legs[2].position);

        var center = GetCenterPoint(Legs[0].position, Legs[1].position, Legs[2].position);
        Debug.DrawRay(center, normal);
    }

    private Vector3 GetNormal(Vector3 coor1, Vector3 coor2, Vector3 coor3)
    {
        Vector3 v = coor2 - coor1;
        Vector3 w = coor3 - coor1;

        return Vector3.Cross(v, w);
    }
}
