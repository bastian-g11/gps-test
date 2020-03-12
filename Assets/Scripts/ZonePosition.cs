using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonePosition : MonoBehaviour
{
    public float reachRange;
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().EnterZone(transform.position);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ExitZone();
        }
    }

}
