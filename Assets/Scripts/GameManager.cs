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
    public PipetteUI pipetteUI;
    [SerializeField]
    private GameObject pipetteUI_GO;
    
    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<SUPERCharacterAIO>();
        cameraRef = playerRef.gameObject.GetComponentInChildren<Camera>();
        pipetteUI = SpawnPipetteUI().GetComponent<PipetteUI>();
    }

    private GameObject SpawnPipetteUI()
    {
        return Instantiate(pipetteUI_GO);
    }

}
