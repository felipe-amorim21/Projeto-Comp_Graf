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
            if (SceneManager.GetActiveScene().name == "level_1") // Change "Level1" to the actual name of your level 1 scene
            {
                // If the current scene is level 1, reset the amount of cherries to 0
                PlayerPrefs.SetInt("CollectedCherries", 0);
                PlayerPrefs.Save();
            }
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ResetPowerUp();
                playerController.ResetDoubleJump();
            }
            
            SceneManager.LoadScene(SceneName);
        }
    }
}
