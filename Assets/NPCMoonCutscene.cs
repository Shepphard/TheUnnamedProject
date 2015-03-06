using UnityEngine;
using System.Collections;

public class NPCMoonCutscene : MonoBehaviour {

    public void StartMusic()
    {
        audio.Play();
    }

    public void EnterShip()
    {
        transform.parent.GetComponent<SpaceCutscene>().EnterShip();
    }
}
