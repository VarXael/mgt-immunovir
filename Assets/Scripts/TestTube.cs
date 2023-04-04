using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TubeSolutionType
{
    Salina,
    Soluzione_2,
    Soluzione_3
}

public class TestTube : DeskInteractableObject, PipetteInteractable
{
    public GameObject testTubeLid;
    public float solutionQuantity_ul;
    public TubeSolutionType solutionType;
    private bool isLidOpen;
    private bool test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InteractWithPipette(Pipette pipetteRef)
    {
        if (!isLidOpen) return;
        if (!pipetteRef.HasPipetteTip) return;
        if (pipetteRef.IsPipetteFull) return;
        if (test)
        {
            RemoveSolutionFromPipette(pipetteRef);
        }
        else
        {
            AddSolutionInPipette(pipetteRef);
        }
        test = !test;
    }

    public override void Interact()
    {
        base.Interact();
        OpenTubeLid();
    }

    private void OpenTubeLid()
    {
        testTubeLid.transform.Rotate(new Vector3(0, GetLidRotationBasedOnOpenState(), 0), Space.Self);
        isLidOpen = !isLidOpen;
    }
    private float GetLidRotationBasedOnOpenState()
    {
        if (isLidOpen) return -90;
        else return 90;
    }

    private void AddSolutionInPipette(Pipette pipetteRef)
    {
        GameManager.Instance.pipetteUI.AddSolutionToPipetteContainer(solutionQuantity_ul, solutionType);
    }
    private void RemoveSolutionFromPipette(Pipette pipetteRef)
    {
        GameManager.Instance.pipetteUI.RemoveSolutionToPipetteContainer(solutionQuantity_ul);
    }
}
