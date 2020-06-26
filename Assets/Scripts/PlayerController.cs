using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 destinationPos;
    public float speed;

    public GameObject posIndicator;

    public void SetTargetPos(float x, float z)
    {
        destinationPos = new Vector3(x, 3, z);
    }

    public void EnterZone(Vector3 newPos)
    {
        posIndicator.SetActive(true);
        posIndicator.transform.position = new Vector3(newPos.x, posIndicator.transform.position.y, newPos.z);
    }

    public void ExitZone()
    {
        posIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destinationPos, speed);
    }
}
