using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestTubeUI : UI
{
    public TextMeshProUGUI SolutionTypeText;

    private void OnEnable()
    {
        
    }

    public override void OnChangeActivationState()
    {
        base.OnChangeActivationState();
        if (activatorReference == null) return;
        SolutionTypeText.text = activatorReference.GetComponent<TestTube>().solutionType.ToString();
    }

}
