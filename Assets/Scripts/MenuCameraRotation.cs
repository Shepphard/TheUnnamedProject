using UnityEngine;
using System.Collections;

public class MenuCameraRotation : MonoBehaviour
{
    public float rotationSpeed = 3f;

	void Update ()
    {
        transform.RotateAround(Vector3.up, rotationSpeed * Time.deltaTime);
	}
}
