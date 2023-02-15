using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskInteractable : Interactable
{
    [SerializeField] private GameObject playerPos;
    [SerializeField] private GameObject cameraPos;
    private bool playerIsCloseToDesk;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool Interact()
    {
        MovePlayerInPosition();
        return base.Interact();
    }
    private void MovePlayerInPosition()
    {
        GameManager.Instance.playerRef.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.Instance.playerRef.transform.position = new Vector3(playerPos.transform.position.x, playerPos.transform.position.y, GameManager.Instance.playerRef.transform.position.z);
        GameManager.Instance.cameraRef.transform.position = cameraPos.transform.position;
        GameManager.Instance.cameraRef.transform.rotation = cameraPos.transform.rotation;
        GameManager.Instance.playerRef.gameObject.AddComponent<PlayerPipetteController>();
        GameManager.Instance.playerRef.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
