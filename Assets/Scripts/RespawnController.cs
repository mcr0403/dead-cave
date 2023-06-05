using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{

    public static RespawnController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Vector3 respawnPoint;
    public float waitToRespawn;

    private GameObject thePlayer;

    public GameObject deathEffect;

    public PlayerAbilityTracker player;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHeathController.instance.gameObject;

        respawnPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //position Respawn
    public void SetSpawn(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());


    }

    IEnumerator RespawnCo()
    {
        thePlayer.SetActive(false);
        if (deathEffect != null)
        {
            Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);
        }


        yield return new WaitForSeconds(waitToRespawn);
    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        thePlayer.transform.position = respawnPoint;
        thePlayer.SetActive(true);

        if (PlayerPrefs.HasKey("DoubleJumpUnlocked"))
        {
            if (PlayerPrefs.GetInt("DoubleJumpUnlocked") == 1)
            {
                player.canDoubleJump = false;
            }
        }

        if (PlayerPrefs.HasKey("DashUnlocked"))
        {
            if (PlayerPrefs.GetInt("DashUnlocked") == 1)
            {
                player.canDash = false;
            }
        }

        if (PlayerPrefs.HasKey("BallUnlocked"))
        {
            if (PlayerPrefs.GetInt("BallUnlocked") == 1)
            {
                player.canBecomeBall = false;
            }
        }

        if (PlayerPrefs.HasKey("BombUnlocked"))
        {
            if (PlayerPrefs.GetInt("BombUnlocked") == 1)
            {
                player.canDropBall = false;
            }
        }

        PlayerHeathController.instance.FillHealth();
    
    }

}
