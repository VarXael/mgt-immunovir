using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractablePipette : DeskInteractableObject, IInteractableDeskObject
{
    public float PickedUpPipetteHeight;
    public Vector3 PickedUpPipetteRotation;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        transform.position = new Vector3(transform.position.x, PickedUpPipetteHeight,transform.position.z);
        transform.eulerAngles = PickedUpPipetteRotation;
    }

    public void StopInteraction()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    public override void MoveInteractableObject(Vector3 pos)
    {
        base.MoveInteractableObject(pos);
        rb.MovePosition(pos);
    }

}
