using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    public GameObject John;
    public GameObject GameOver;

    void Update()
    {
        if (John != null)
        {
            Vector3 posicion = transform.position;
            posicion.x = John.transform.position.x;
            transform.position = posicion;
        } else
        {
            GameOver.SetActive(true);
        }
    }
}
