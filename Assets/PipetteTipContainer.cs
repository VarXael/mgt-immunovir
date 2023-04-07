using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipetteTipContainer : DeskInteractableObject, PipetteInteractable
{
    public void InteractWithPipette(Pipette pipetteRef)
    {
        pipetteRef.RemoveTipFromPipette();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
