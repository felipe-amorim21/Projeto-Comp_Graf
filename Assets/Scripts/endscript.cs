using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endscript : MonoBehaviour
{
    void Update()
    {
        // Verifica se a tecla Enter foi pressionada
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Carrega a cena do Menu Inicial
            SceneManager.LoadScene("main menu");
        }
    }
}
