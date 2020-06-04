using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFantasma : MonoBehaviour
{
    private int index =0;


    // Update is called once per frame
    void Update()
    {
        if (index == ThirdPersonMovement.posicionesAlmacenadas.Count)
            index = 0;
        if (ThirdPersonMovement.comienzoAReproducir)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            transform.position = ThirdPersonMovement.posicionesAlmacenadas[index];
            index++;
        }
    }

    public void Reset()
    {
        index = 0;
    }
}
