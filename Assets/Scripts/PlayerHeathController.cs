using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeathController : MonoBehaviour
{

    public static PlayerHeathController instance;

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

    //[HideInInspector]
    public int currentHealth;
    public int maxHealth;

    public float invincibilityLenght;
    private float invinCounter;

    public float flashLenght;
    private float flashCounter;

    public SpriteRenderer[] playerSprites;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {

        //touch to enemy then invisible in a few second
        if (invinCounter > 0)
        {
            invinCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                foreach (SpriteRenderer sp in playerSprites)
                {
                    sp.enabled = !sp.enabled;
                }
                flashCounter = flashLenght;
            }

            if (invinCounter <= 0)
            {
                foreach (SpriteRenderer sp in playerSprites)
                {
                    sp.enabled = true;
                }
                flashCounter = 0f;


            }
        }
    }


    //damage to player when touch to enemies when invisible out of time 
    public void DamagePlayer (int damageAmount)
    {
        if (invinCounter <= 0)
        {

            currentHealth -= damageAmount;

            AudioManager.instance.PlaySFX(11);

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                AudioManager.instance.PlaySFX(8);

                //gameObject.SetActive(false);

                RespawnController.instance.Respawn();


            }
            else
            {
                invinCounter = invincibilityLenght;


            }

            UIController.instance.UpdateHealth(currentHealth, maxHealth);
        }   
    }

    //fill health after respawn
    public void FillHealth()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdateHealth(currentHealth, maxHealth);

    }

    public void HealPlayer(int healthAmount)
    {
        currentHealth += healthAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealth(currentHealth, maxHealth);

    }

    
}
