using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour
{
    public UnityEngine.UI.Text coordenadas;
    public Vector2 zeroPos;
    public Vector2 currentPos;
    public Vector2 traslation;
    public float disFactorX;
    public float disFactorY;
    public UnityEngine.UI.InputField inputX;
    public UnityEngine.UI.InputField inputY;

    public PlayerController playerController;

    public Vector2 destination;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Start2());
        currentPos = zeroPos;
    }

    void Update()
    {
    }

    public void ChangeFactor()
    {
        disFactorX = int.Parse(inputX.text);
        disFactorY = int.Parse(inputY.text);
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
                currentPos = new Vector2(Input.location.lastData.longitude, Input.location.lastData.latitude);
                coordenadas.text = ("LATITUD: " + currentPos.y + "\nLONGITUD:" + currentPos.x);
                traslation = currentPos - zeroPos;
                traslation.x *= disFactorX;
                traslation.y *= disFactorY;
                playerController.SetTargetPos(traslation.x, traslation.y);
                Debug.Log(currentPos);
            }

            // Stop service if there is no need to query location updates continuously
            Input.location.Stop();
        }
    }
}
