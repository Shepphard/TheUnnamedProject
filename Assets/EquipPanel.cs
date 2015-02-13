using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipPanel : MonoBehaviour
{
    public void ActivateButtons()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button b in buttons)
        {
            b.interactable = true;
        }
    }

}
