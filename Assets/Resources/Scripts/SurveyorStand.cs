using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public delegate void OnHeightChanged(float newHeight);
public class SurveyorStand : SerializedMonoBehaviour
{
    public event OnHeightChanged HeightChanged;

    [ShowInInspector]
    public float Height
    {
        get { return _height; }
        set
        {
            _height = value;
            OnHeightChanged(_height);
            UpdateRenderer();
        }
    }

    private float _height = 1f;
    private Quaternion _rotation = Quaternion.identity;

    public Transform RendererTransform; //used for adjusting the visual height of the object

    // Use this for initialization
    void Start()
    {
        RendererTransform = transform.Find("Renderer").transform;
    }

    void UpdateRenderer()
    {
        if (RendererTransform)
        {
            var scale = RendererTransform.localScale;
            scale.y = Height;
            RendererTransform.localScale = scale;

            var position = RendererTransform.position;
            position.y = scale.y * 0.5f;
            RendererTransform.position = position;
        }
    }

    private void OnHeightChanged(float newHeight)
    {
        HeightChanged?.Invoke(newHeight);
    }
}
