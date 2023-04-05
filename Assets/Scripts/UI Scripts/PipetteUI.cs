using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PipetteUI : UI
{
    public Slider SolutionContainer;
    public TextMeshProUGUI SolutionTypeText;
    public TextMeshProUGUI SolutionTypeQuantity;

    public override void UpdateUI(GameObject updaterObject)
    {
        base.UpdateUI(updaterObject);
        UpdateLiquidQuantity(updaterObject.GetComponent<Pipette>().currentPipetteLiquid);
    }

    public void UpdateLiquidQuantity(float currentLiquidinPipette)
    {
        SolutionTypeQuantity.text = currentLiquidinPipette.ToString();
    }


    public void AddSolutionToPipetteContainer(float valueToSum, TubeSolutionType solutionType)
    {
        SolutionContainer.value =+ valueToSum;
        SolutionTypeText.text = solutionType.ToString();
        SolutionTypeQuantity.text = valueToSum.ToString();
    }
    public void RemoveSolutionToPipetteContainer(float valueToSum)
    {
        SolutionContainer.value =- valueToSum;
        SolutionTypeText.text = "SolutionType";
        SolutionTypeQuantity.text = "0";
    }


}
