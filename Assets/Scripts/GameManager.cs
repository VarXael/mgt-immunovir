using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public SUPERCharacterAIO playerRef;
    [HideInInspector]
    public Camera cameraRef;
    [SerializeField]
    private UIManager UIManager;
    
    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<SUPERCharacterAIO>();
        cameraRef = playerRef.gameObject.GetComponentInChildren<Camera>();
        UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    public void ChangeUIActiveState(GameObject whoIsActivatingMe,string UIToChange, bool isActive)
    {
        UI ui = UIManager.GetUIFromName(UIToChange);
        if (ui)
        {
            ui.gameObject.SetActive(isActive);
            ui.activatorReference = whoIsActivatingMe;
        }
    }

}
