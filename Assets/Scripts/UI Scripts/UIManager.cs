using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<UI> UI;

    public void AddUIToList(UI UItoAdd)
    {
        UI.Add(UItoAdd);
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
