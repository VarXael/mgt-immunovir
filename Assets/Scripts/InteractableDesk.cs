using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDesk : Interactable
{
    [SerializeField] private GameObject playerPos;
    [SerializeField] private GameObject cameraPos;
    private bool playerIsCloseToDesk;
    [SerializeField] private GameObject deskObjectCollider;
    // Start is called before the first frame update
    void Start()
    {

    }

    public override bool Interact()
    {
        MovePlayerInDesk();
        return base.Interact();
    }
    private void MovePlayerInDesk()
    {
        GameManager.Instance.playerRef.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.Instance.playerRef.transform.position = new Vector3(playerPos.transform.position.x, playerPos.transform.position.y, GameManager.Instance.playerRef.transform.position.z);
        GameManager.Instance.cameraRef.transform.position = cameraPos.transform.position;
        GameManager.Instance.cameraRef.transform.rotation = cameraPos.transform.rotation;
        GameManager.Instance.playerRef.gameObject.GetComponent<PlayerDeskController>().enabled = true;
        GameManager.Instance.playerRef.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void ExitPlayerOutOfDesk()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsCloseToDesk = true;
        }
        else playerIsCloseToDesk = false;

    }

}

public interface IInteractableDeskObject
{
    public void Interact();
    public void StopInteraction();

    public void WhileInteractingAction();

    public void StopInteractingAction();

    public void MoveInteractableObject(Vector3 v);
}
