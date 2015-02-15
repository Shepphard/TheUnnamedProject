using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Select : MonoBehaviour
{
    private Equipment equipment;
    private EquipPanel equipPanel;
    private Button button;
    private bool init = false;

    void Update()
    {
        if (!init)
        {
            equipment = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Equipment>();
            equipPanel = transform.parent.GetComponent<EquipPanel>();
            button = GetComponent<Button>();
            init = true;
        }
    }

    public void SelectThis()
    {
        equipPanel.ActivateButtons();
        button.interactable = false;

        int bar = 0;
        if (equipPanel.name == "LeftHand")
            bar = 1;

        equipment.wearEquipment(name, bar);
        Debug.Log("Selecting: " + name + " from Bar: " + bar);
    }
}
