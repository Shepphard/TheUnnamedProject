using UnityEngine;
using System.Collections;

public class NewEquipMenu : MonoBehaviour
{
    public KeyCode key_equipment;
    public GameObject equipmentCanvas;
    public EquipPanel headPanel;
    public EquipPanel lefthandPanel;

    private InteractionControls playerCtrl;
    private BlockCTRL blocker;
    private ArrayList itemlist = new ArrayList();

    void Awake()
    {
        playerCtrl = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<InteractionControls>();
        blocker = playerCtrl.GetComponent<BlockCTRL>();
    }

    void Update()
    {
        if (Input.GetKeyDown(key_equipment))
        {
            if (equipmentCanvas.activeSelf)
            {
                Disable();
            }
            else
            {
                Enable();
            }
        }

        if (equipmentCanvas.activeSelf)
        {
            Time.timeScale = 0;
        }

    }

    public void Enable()
    {
        Time.timeScale = 0;
        playerCtrl.enabled = false;
        blocker.BlockMovement(true);
        blocker.BlockLookingaround(true, true);
        equipmentCanvas.SetActive(true);

        FlushListIntoMenu();
    }

    public void Disable()
    {
        Time.timeScale = 1;
        blocker.BlockMovement(false);
        playerCtrl.enabled = true;
        blocker.BlockLookingaround(false, false);
        equipmentCanvas.SetActive(false);
    }

    // flushes the lists of added items and
    // creates corresponding buttons to the new menu
    void FlushListIntoMenu()
    {
        for (int i = 0; i < itemlist.Count; i++)
        {
            item it = (item)itemlist[i];
            if (it.belongsToEquipmentBar == 0)
            {
                headPanel.AddButton(it);
            }
            else if (it.belongsToEquipmentBar == 1)
            {
                lefthandPanel.AddButton(it);
            }
            else
            {
                Debug.LogError("WRONG ITEM belongsToEquipmentBar SET!");
            }
        }

        itemlist.Clear();
    }

    public void AddItem(item i)
    {
        itemlist.Add(i);
    }
}