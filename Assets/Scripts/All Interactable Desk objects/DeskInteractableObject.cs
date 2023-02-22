using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskInteractableObject : MonoBehaviour, IInteractableDeskObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void MoveInteractableObject(Vector3 pos)
    {
        
    }

    public virtual void Interact()
    {
        Debug.Log("Started interacting with" + name);
    }

    public virtual void StopInteraction()
    {
        StopInteractingAction();
        Debug.Log("Stopped interaction with " + name);
    }

    public virtual void WhileInteractingAction()
    {
        Debug.Log("Started Special Interaction with " + name);
    }
    public virtual void StopInteractingAction()
    {
        Debug.Log("Stopped Special Interaction with " + name);
    }
}
