using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject John;
    void Update()
    {
        bool action = Input.GetKey(KeyCode.F);
        if (action && John == null)
        {
            Debug.Log("Restart Game");
            SceneManager.LoadScene("SampleScene");
        } 
    }
}
