using UnityEngine;
using System.Collections;

public class PreGameScript : MonoBehaviour {

    public int sceneToExitTo = 0;

    public void ExitToScene()
    {
        Application.LoadLevel(sceneToExitTo);
    }
}
