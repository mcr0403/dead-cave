using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int totalHealth = 3;

    public GameObject deathEffect;

    public void DamageEnemy(int damegeAmount)
    {
        totalHealth -= damegeAmount;

        if (totalHealth <= 0)
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            AudioManager.instance.PlaySFX(4);

            Destroy(gameObject);
        }
    }
}
