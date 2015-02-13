using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Select : MonoBehaviour
{
    private EquipPanel equipPanel;
    private Button button;

    void Awake()
    {
        equipPanel = transform.parent.GetComponent<EquipPanel>();
        button = GetComponent<Button>();
    }

    public void SelectThis()
    {
        equipPanel.ActivateButtons();
        button.interactable = false;
    }
}
