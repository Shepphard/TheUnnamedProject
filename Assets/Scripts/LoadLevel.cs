using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{
    public int Scene;

    void Start()
    {
        Application.LoadLevel(Scene);
    }
}
