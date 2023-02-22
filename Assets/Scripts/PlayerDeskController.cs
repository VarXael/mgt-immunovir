using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeskController : MonoBehaviour
{
    private DeskInteractableObject currentInteractedObject;
    public LayerMask deskInteractablesLayer;
    public LayerMask whileInteractingLayer;
    private bool isInteracting;
    private RaycastHit hit;
    private Vector3 lastHitPosition;

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        InteractWithMouse();
        MantainInteraction();
    }

    private RaycastHit MouseRayCast(LayerMask layerMask)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = GameManager.Instance.cameraRef.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(GameManager.Instance.cameraRef.transform.position, mousePos- GameManager.Instance.cameraRef.transform.position, Color.blue);
        RaycastHit hit;
        Physics.Raycast(GameManager.Instance.cameraRef.transform.position, mousePos - GameManager.Instance.cameraRef.transform.position, out hit, Mathf.Infinity, layerMask);
        return hit;
    }

    private void InteractWithMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = MouseRayCast(deskInteractablesLayer);
            if (hit.collider)
            {
                currentInteractedObject = hit.collider.GetComponent<DeskInteractableObject>();
                currentInteractedObject.GetComponent<IInteractableDeskObject>()?.Interact();
                isInteracting = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(currentInteractedObject != null)
            {
                currentInteractedObject.GetComponent<IInteractableDeskObject>()?.StopInteraction();
            }
            currentInteractedObject = null;
            isInteracting = false;
        }
    }

    private void MantainInteraction()
    {
        if (!isInteracting) return;

        hit = MouseRayCast(whileInteractingLayer);
        Vector3 pos = Vector3.zero;

        //hit.collider? pos = hit.point : pos = lastHitPosition;

        if (hit.collider != null)
        {
            pos = hit.point;
            lastHitPosition = pos;
            currentInteractedObject.MoveInteractableObject(pos);
        }
        else
        {
            pos = lastHitPosition;
            lastHitPosition = pos;
            currentInteractedObject.MoveInteractableObject(pos);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(hit.point, 0.25f);
    }
}
