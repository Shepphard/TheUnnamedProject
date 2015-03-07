using UnityEngine;
using System.Collections;

public class cutsceneTrigger : MonoBehaviour {

    private CutsceneScript scener;

    void Awake()
    {
        scener = transform.parent.GetComponent<CutsceneScript>();
    }

    void OnTriggerEnter()
    {
        scener.PlayAll();
    }
}
