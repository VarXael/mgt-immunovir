using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract;

    public virtual bool Interact(){
        OnInteract.Invoke();
        return true;
    }

    public void DestroySelf(){
        Destroy(gameObject);
    }
}
