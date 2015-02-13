using UnityEngine;
using System.Collections;

public class NewEquipMenu : MonoBehaviour
{
    public KeyCode key_equipment;
    public GameObject equipmentCanvas;

    private InteractionControls playerCtrl;
    private BlockCTRL blocker;

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
    }

    public void Disable()
    {
        Time.timeScale = 1;
        blocker.BlockMovement(false);
        playerCtrl.enabled = true;
        blocker.BlockLookingaround(false, false);
        equipmentCanvas.SetActive(false);
    }
}
