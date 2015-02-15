using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipPanel : MonoBehaviour
{
    public GameObject equipButton;

    public void ActivateButtons()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button b in buttons)
        {
            b.interactable = true;
        }
    }

    public void AddButton(item i)
    {
        GameObject button = (GameObject)Instantiate(equipButton);
        button.name = i.itemName; // set the name
        button.transform.SetParent(this.transform); // set the parent
        button.transform.GetChild(0).GetComponent<Image>().sprite = i.icon; // set the image
        if (button.name != "empty")
        {
            button.GetComponent<Button>().interactable = true;
        }
    }
}
