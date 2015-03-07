using UnityEngine;
using System.Collections;

public class NPCMoonCutscene : MonoBehaviour {

    public GameObject normal;
    public GameObject switched;

    bool started = false;

    void Update()
    {
        if (started)
        {
            if (!audio.isPlaying)
                Application.LoadLevel(5);
        }
    }

    public void StartMusic()
    {
        audio.Play();
        started = true;
    }

    public void EnterShip()
    {
        transform.parent.GetComponent<SpaceCutscene>().EnterShip();
    }

    public void Switch()
    {
        normal.SetActive(false);
        switched.SetActive(true);
    }
}
