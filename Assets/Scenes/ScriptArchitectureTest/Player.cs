using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask hittableObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private RaycastHit MouseRayCast(LayerMask layerMask)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = GameManager.Instance.cameraRef.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(GameManager.Instance.cameraRef.transform.position, mousePos - GameManager.Instance.cameraRef.transform.position, Color.blue);
        RaycastHit hit;
        Physics.Raycast(GameManager.Instance.cameraRef.transform.position, mousePos - GameManager.Instance.cameraRef.transform.position, out hit, Mathf.Infinity, layerMask);
        return hit;
    }
}
