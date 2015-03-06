using UnityEngine;
using System.Collections;

public class Levitate : MonoBehaviour {

    public float strength = 1;

    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            rigidbody.AddForce(new Vector3(0, strength * Time.deltaTime, 0));
        }
    }
}
