using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chronometer : MonoBehaviour
{
    public Text v1,lastLap, bestTime;
    private float seg = 0f, recordTime = 100000000000000000000000000000f;
    private bool primeraVuelta = true;




    // Start is called before the first frame update
    void Start()
    {
        v1.text = "Vuelta actual: 0";
        bestTime.text = "Mejor vuelta: 0";
    }

    // Update is called once per frame
    void Update()
    {

        seg += Time.deltaTime;
        v1.text = "Vuelta actual: " + (int)(seg * 1000.0f) / 1000.0f;
    }

    public void ReiniciarCrono()
    {
        

        if (seg < recordTime && !primeraVuelta)
        {
            GameObject.FindWithTag("Player").GetComponent<ThirdPersonMovement>().NewRecordSound();
            recordTime = seg;
            lastLap.text = "Ultima vuelta: " + (int)(seg * 1000.0f) / 1000.0f;
            bestTime.text = "Mejor vuelta: " + (int)(recordTime * 1000.0f) / 1000.0f;
            GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovement>().CopiarNuevoRecord();
            ThirdPersonMovement.comienzoAReproducir = true;  
        }
        else
        {
            if (!primeraVuelta)
            {
                lastLap.text = "Ultima vuelta: " + (int)(seg * 1000.0f) / 1000.0f;
                GameObject.FindWithTag("Player").GetComponent<ThirdPersonMovement>().LapSound();
                GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovement>().ResetearTemporal();
            }
        }

        if (primeraVuelta)
        {
            primeraVuelta = false;
        }
        seg = 0f;

    }
}
