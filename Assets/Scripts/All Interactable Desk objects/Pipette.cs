using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pipette : DeskInteractableObject
{
    
    public float PickedUpPipetteHeight;
    public float PickedUpPipetteInteractionHeight;
    public GameObject PipetteTipPositionObject;
    [HideInInspector]
    public GameObject currentlyHeldPipetteTip;
    public Quaternion PickedUpPipetteRotation;
    public Quaternion PickedUpPipetteInteractionRotation;
    public LayerMask pipetteInteractable;
    public Material PipetteIndicatorColor;
    public LineRenderer LineVisualizer;
    private Rigidbody rb;
    private bool changeMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LineVisualizer.enabled = false;
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
        LineVisualizer.enabled = true;
    }

    public override void StopInteraction()
    {
        base.StopInteraction();
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        LineVisualizer.enabled = false;
    }

    public override void WhileInteractingAction()
    {
        base.WhileInteractingAction();
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        changeMovement = true;
        WhileInteractingPipette();
    }
    private void WhileInteractingPipette()
    {
        RaycastHit hit = RayCast(Vector3.down, pipetteInteractable);
        if (hit.collider)
        {
            PipetteInteractable p = hit.collider.gameObject.GetComponent<PipetteInteractable>();
            p.InteractWithPipette(this);
        }
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
