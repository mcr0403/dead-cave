using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

    public float timeToExplore;
    public GameObject explore;

    public float blastRange;
    public LayerMask whatIsDestructible;

    public int damageAmount;
    public LayerMask whatIsDamageable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToExplore -= Time.deltaTime;
        if (timeToExplore <= 0)
        {
            // animation explore
            if (explore != null)
            {
                Instantiate(explore, transform.position, transform.rotation);
            }

            Destroy(gameObject);

            Collider2D[] objectsToRemove =  Physics2D.OverlapCircleAll(transform.position, blastRange, whatIsDestructible);
            
            if (objectsToRemove.Length > 0)
            {
                foreach(Collider2D col in objectsToRemove)
                {
                    Destroy(col.gameObject);
                }
            }

            Collider2D[] objectToDamage = Physics2D.OverlapCircleAll(transform.position, blastRange, whatIsDamageable);

            if (objectToDamage.Length > 0)
            {
                foreach(Collider2D col in objectToDamage)
                {
                    EnemyHealthController enemyHealth = col.GetComponent<EnemyHealthController>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.DamageEnemy(damageAmount);
                    }
                }

            }



        }

        
    }
}
