using UnityEngine;
using System.Collections;

public class WiggleLetter : MonoBehaviour
{
    public float maxY = 1f;
    public float minY = -1f;
    public float wiggleSpeed = 1f;

    private Vector3 maxTarget;
    private Vector3 minTarget;
    private Vector3 target;

    void Awake()
    {
        maxTarget = new Vector3(transform.position.x, transform.position.y + maxY, transform.position.z);
        minTarget = new Vector3(transform.position.x, transform.position.y + minY, transform.position.z);

        target = minTarget;
    }

    void Update()
    {
        MoveUpDown();
    }

    void MoveUpDown()
    {
        transform.position = Vector3.Lerp(transform.position, target, wiggleSpeed * Time.deltaTime);
        Vector3 diff = transform.position - target;
        if (diff.magnitude < 0.05f)
        {
            if (target == minTarget)
                target = maxTarget;
            else
                target = minTarget;
        }
    }
}
