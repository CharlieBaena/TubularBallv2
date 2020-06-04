using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLap : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas myCanvas;
    public GameObject barrera, barreraPostMeta;

    public static bool comienzoAGrabar = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            myCanvas.GetComponent<Chronometer>().ReiniciarCrono();
            barrera.SetActive(false);
            barreraPostMeta.SetActive(false);
            StartCoroutine(Esperar(0.5f));
            comienzoAGrabar = true;
            GameObject.FindGameObjectWithTag("Fantasma").GetComponent<BolaFantasma>().Reset();
        }
    }

    IEnumerator Esperar(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        barreraPostMeta.SetActive(true);
    }
}
