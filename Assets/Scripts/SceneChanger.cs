using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{   
    [SerializeField] private string SceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name == "end")
            {
                // Se for a última cena, reinicie o contador de cerejas
                PlayerPrefs.SetInt("CollectedCherries", 0);
                PlayerPrefs.Save();
            }
            SceneManager.LoadScene(SceneName);
        }
    }
}
