using UnityEngine;
using System.Collections;

public class TurnOffForce : MonoBehaviour
{
    public float timer = 1f;
	
	// Update is called once per frame
	void Update () {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                GetComponent<ConstantForce>().enabled = false;
        }
	}
}
