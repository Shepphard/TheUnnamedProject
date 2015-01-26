using UnityEngine;
using System.Collections;

public class PreGameScript : MonoBehaviour {

    public int sceneToExitTo = 1;

    public void ExitToScene()
    {
        Application.LoadLevel(sceneToExitTo);
    }
}
