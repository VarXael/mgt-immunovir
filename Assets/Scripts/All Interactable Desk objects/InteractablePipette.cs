using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractablePipette : DeskInteractableObject
{
    public float PickedUpPipetteHeight;
    public float PickedUpPipetteInteractionHeight;
    public Quaternion PickedUpPipetteRotation;
    public Quaternion PickedUpPipetteInteractionRotation;
    public Material PipetteIndicatorColor;
    public GameObject PipettePlunger;
    private Rigidbody rb;
    private bool changeMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        base.Interact();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.rotation = PickedUpPipetteRotation;
    }

    public override void StopInteraction()
    {
        base.StopInteraction();
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
    }

    public override void WhileInteractingAction()
    {
        base.WhileInteractingAction();
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        changeMovement = true;
    }

    public override void StopInteractingAction()
    {
        base.StopInteractingAction();
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        changeMovement = false;
    }

    public override void MoveInteractableObject(Vector3 pos)
    {
        base.MoveInteractableObject(pos);
        if (!changeMovement)
        {
            rb.MovePosition(new Vector3(pos.x, PickedUpPipetteHeight, pos.z));
            rb.MoveRotation(PickedUpPipetteRotation);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, PickedUpPipetteInteractionHeight, transform.position.z);
            rb.MoveRotation(PickedUpPipetteInteractionRotation);

        }
    }

}
