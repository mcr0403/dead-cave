using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D theRB;

    public int damageAmount;
    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        //identify direction off bullet follow player
        Vector3 direction = transform.position - PlayerHeathController.instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        AudioManager.instance.PlaySFX(2);

    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = -transform.right * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHeathController.instance.DamagePlayer(damageAmount);
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        AudioManager.instance.PlaySFX(3);
    }
}
