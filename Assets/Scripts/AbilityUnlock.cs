using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUnlock : MonoBehaviour
{

    public bool unlockDoubleJump, unlockDash, unlockBall, unlockDropBomb;

    public GameObject pickupEffect;

    public string unlockMessage;

    public TMP_Text unlockText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerAbilityTracker player = collision.GetComponentInParent<PlayerAbilityTracker>();

            if (unlockDoubleJump)
            {
                player.canDoubleJump = true;

                PlayerPrefs.SetInt("DoubleJumpUnlocked", 1);

            }

            if (unlockDash)
            {
                player.canDash = true;

                PlayerPrefs.SetInt("DashUnlocked", 1);
            }

            if (unlockBall)
            {
                player.canBecomeBall = true;

                PlayerPrefs.SetInt("BallUnlocked", 1);
            }
             
            if (unlockDropBomb)
            {
                player.canDropBall = true;

                PlayerPrefs.SetInt("BombUnlocked", 1);
            }

            Instantiate(pickupEffect, transform.position, transform.rotation);

            unlockText.transform.parent.SetParent(null);
            unlockText.transform.parent.position = transform.position;

            unlockText.text = unlockMessage;
            unlockText.gameObject.SetActive(true);

            AudioManager.instance.PlaySFX(5);

            Destroy(unlockText.gameObject, 1.5f);


            Destroy(gameObject);
        }
    }
}
