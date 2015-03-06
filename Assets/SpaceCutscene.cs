using UnityEngine;
using System.Collections;

public class SpaceCutscene : MonoBehaviour {

    public void EnterShip()
    {
        GetComponent<Animator>().SetTrigger("open");
    }
}
