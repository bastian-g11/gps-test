using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float followSpeed;
    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = target.position - offset;
        transform.position = Vector3.Lerp(transform.position, target.position, followSpeed);
    }
}
