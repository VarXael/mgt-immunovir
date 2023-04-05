using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public List<UI> UI;

    public void AddUIToList(UI UItoAdd)
    {
        UI.Add(UItoAdd);
    }
    public void ChangeUIActiveState(GameObject whoIsActivatingMe, string UIToChange, bool isActive)
    {
        UI ui = GetUIFromName(UIToChange);
        if (ui)
        {
            ui.gameObject.SetActive(isActive);
            ui.activatorReference = whoIsActivatingMe;
            ui.OnChangeActivationState();
        }
    }


    public UI GetUIFromName(string NameToCompare)
    {
        for (int i = 0; i < UI.Count; i++)
        {
            if (UI[i].myName == NameToCompare)
            {
                return UI[i];
            }
        }
        return null;
    }
}
