using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipetteTip : DeskInteractableObject, PipetteInteractable 
{
    public bool hasBeenUsed;
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
        pipetteRef.PutTipOnPipette(this);
    }

    
}
