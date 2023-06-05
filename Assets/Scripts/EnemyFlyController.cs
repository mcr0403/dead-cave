using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyController : MonoBehaviour
{
    public float rangeToChasing;
    private bool isChasing;

    public float moveSpeed, turnSpeed;

    private Transform player;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHeathController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //check player in chasing area or not
        if (!isChasing)
        {
            if (Vector3.Distance(transform.position, player.position) < rangeToChasing)
            {
                isChasing = true;

                anim.SetBool("isChasing", isChasing);
            }
        }
        else
        {
            if (player.gameObject.activeSelf)
            {
                //rotation follow player
                Vector3 direction = transform.position - player.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);

                //move follow the player
                //transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                transform.position += -transform.right * moveSpeed * Time.deltaTime;
                
                
            }
        }
    }
}
