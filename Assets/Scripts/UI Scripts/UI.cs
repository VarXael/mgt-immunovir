using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    private UIManager uIManager;
    public string myName;
    public GameObject activatorReference;
    // Start is called before the first frame update
    void Start()
    {
        uIManager = GetComponentInParent<UIManager>();
        uIManager.AddUIToList(this);
        gameObject.SetActive(false);
    }

    public virtual void OnChangeActivationState()
    {

    }
    public virtual void UpdateUI(GameObject updaterObject)
    {

    }
}
