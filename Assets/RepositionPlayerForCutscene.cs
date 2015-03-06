using UnityEngine;
using System.Collections;

public class RepositionPlayerForCutscene : MonoBehaviour {

    public GameObject player;
    public Vector3 position;

    void Start()
    {
        player.transform.position = position;
    }
}
