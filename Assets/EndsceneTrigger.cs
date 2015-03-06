using UnityEngine;
using System.Collections;

public class EndsceneTrigger : MonoBehaviour {

    public ScreenFadeInOut fader;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Tags.player))
        {
            fader.StartEndScene(4); // fade to cutscene
        }
    }
}
