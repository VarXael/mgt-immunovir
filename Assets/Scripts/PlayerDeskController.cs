using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeskController : MonoBehaviour
{
    public LayerMask deskInteractablesLayer;
    public LayerMask whileInteractingLayer;
    public float camMoveSpeed;
    private Vector3 smoothDumpVelocity;
    public float x;
    public float y;
    private IInteractableDeskObject currentInteractedObject;
    private bool isInteracting;
    private RaycastHit hit;
    private Vector3 lastHitPosition;
    private Vector3 camStartPos;
    private Camera cam;
    private Rigidbody camRB;

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
        cam = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>();
        camRB = cam.GetComponent<Rigidbody>();
        camStartPos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        InteractWithMouse();
        MantainInteraction();
        WhileInteractingAction();
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3.SmoothDamp(cam.transform.position, camStartPos, ref smoothDumpVelocity, camMoveSpeed);
        Debug.LogError("Fai muovere la camera idiota");
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
                currentInteractedObject = hit.collider.GetComponent<IInteractableDeskObject>();
                currentInteractedObject.Interact();
                isInteracting = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(currentInteractedObject != null)
            {
                currentInteractedObject.StopInteraction();
            }
            currentInteractedObject = null;
            isInteracting = false;
        }
    }

    private void WhileInteractingAction()
    {
        if (!isInteracting) return;
        if (Input.GetMouseButtonDown(1))
        {
            currentInteractedObject.WhileInteractingAction();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            currentInteractedObject.StopInteractingAction();
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
