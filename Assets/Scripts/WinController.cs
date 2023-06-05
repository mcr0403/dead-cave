using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    
    public PlayerController player;
    public GameObject winScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.canMove = false;

            winScreen.SetActive(true);

        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        Destroy(UIController.instance.gameObject);
        UIController.instance = null;

        Destroy(player.gameObject);

        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;
        
        Destroy(gameObject);

        SceneManager.LoadScene("Main Menu");
    }
}
