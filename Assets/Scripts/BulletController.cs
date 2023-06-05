using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D rb;

    public Vector2 moveDir;

    public GameObject impactEffect;

    public int damageAmount = 1;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveDir * bulletSpeed;
    }
    
    //touch other
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
        }

        if(other.tag == "Boss")
        {
            BossHealthController.instance.TakeDamage(damageAmount);
        }
        //effect bound
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
             
        }

        AudioManager.instance.PlaySFX(3);

        Destroy(gameObject);
    }

    //out camera
    private void OnBecameInvisible()
    {
        Destroy (gameObject);
    }
}
