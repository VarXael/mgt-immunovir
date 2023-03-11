using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipetteTip : PipetteInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InteractWithPipette(Pipette pipetteRef)
    {
        base.InteractWithPipette(pipetteRef);
        PutTipOnPipette(pipetteRef);
    }

    private void PutTipOnPipette(Pipette pipetteRef)
    {
        if (pipetteRef.currentlyHeldPipetteTip != null) return;
        pipetteRef.currentlyHeldPipetteTip = gameObject;
        gameObject.transform.position = pipetteRef.PipetteTipPositionObject.transform.position;
        transform.parent = pipetteRef.transform;
    }
}
