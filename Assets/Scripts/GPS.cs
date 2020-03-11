using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPS : MonoBehaviour
{
    public Text coordenadas;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Start2());
    }
    IEnumerator Start2()
    {
        // Start service before querying location
        while (true)
        {
            Input.location.Start();

            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                coordenadas.text = ("Timed out");
                yield break;
            }
            yield return new WaitForSeconds(3);
            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                coordenadas.text = ("Unable to determine device location");
                yield break;
            }
            else
            {
                // Access granted and location value could be retrieved
                coordenadas.text = ("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            }

            // Stop service if there is no need to query location updates continuously
            Input.location.Stop();
        }
    }
}
