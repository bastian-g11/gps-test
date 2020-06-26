using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public List<int> teamsPopulation;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            ZoneControlController.instance.SetControlBar(this);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            ZoneControlController.instance.currentZone = null;
            ZoneControlController.instance.uiElement.gameObject.SetActive(false);
        }
    }
}
