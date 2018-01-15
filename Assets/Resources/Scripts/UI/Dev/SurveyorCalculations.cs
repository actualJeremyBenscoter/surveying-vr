using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SurveyorCalculations : MonoBehaviour
{
    public SurveyorBase Base;
    private Text _uiText;
    // Use this for initialization
    void Start()
    {
        if (Base == null)
        {
            Debug.LogError("No surveyor base has been set! Please set the surveyor base in the inspector for the UI to update!");
        }

        _uiText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Base)
        {
            _uiText.text = string.Concat("D: ", Base.Distance.ToString("0.000"), "m\n",
                "H: ", Base.StickHeight.ToString("0.000"), "m\n",
                "Is Level: ", (Base.IsLevel) ? "Yes" : "No");
        }
    }
}
