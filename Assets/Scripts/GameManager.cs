using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public SUPERCharacterAIO playerRef;
    public Camera cameraRef;
    
    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<SUPERCharacterAIO>();
        cameraRef = playerRef.gameObject.GetComponentInChildren<Camera>();
    }


}
