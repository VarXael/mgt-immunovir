using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public SUPERCharacterAIO playerRef;
    [HideInInspector]
    public Camera cameraRef;
    [HideInInspector]
    public UIManager UIManager;
    
    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<SUPERCharacterAIO>();
        cameraRef = playerRef.gameObject.GetComponentInChildren<Camera>();
        UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

}
