using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour
{
    public UnityEngine.UI.Text coordenadas;
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
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                coordenadas.text = ("Unable to determine device location");
                yield break;
            }
            else
            {
                // Access granted and location value could be retrieved
                coordenadas.text = ("Latitud: " + Input.location.lastData.latitude + "\nLongitud:" + Input.location.lastData.longitude);
            }

            // Stop service if there is no need to query location updates continuously
            Input.location.Stop();
        }
    }
}
