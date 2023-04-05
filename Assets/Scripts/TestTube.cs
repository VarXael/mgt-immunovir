using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TubeSolutionType
{
    Salina,
    Soluzione_2,
    Soluzione_3
}

public enum TubeSolutionNumber
{
    A,
    A1,
    A2,
    A3,
    A4,
    B1,
    B2,
    B3,
    B4
}

public class TestTube : DeskInteractableObject, PipetteInteractable
{
    public GameObject testTubeLid;
    public float solutionQuantity_ul;
    public TubeSolutionType solutionType;
    public TubeSolutionNumber solutionNumber;
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

        pipetteRef.AddSolutionInPipette(solutionType, pipetteRef.pipetteLiquidPerTick);
        //if (test)
        //{
        //    RemoveSolutionFromPipette(pipetteRef);
        //}
        //else
        //{
        //    AddSolutionInPipette(pipetteRef);
        //}
        //test = !test;
    }

    public override void Interact()
    {
        base.Interact();
        OpenTubeLid();
        UIManager.Instance.ChangeUIActiveState(gameObject,"TestTubeUI", true);
    }
    public override void StopInteraction()
    {
        base.StopInteraction();
        UIManager.Instance.ChangeUIActiveState(gameObject,"TestTubeUI", false);
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

    

    public override void StopInteractingAction()
    {
        base.StopInteractingAction();

    }
}
