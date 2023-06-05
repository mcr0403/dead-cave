using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    public Transform[] padtrolPoints;
    private int currentPoint;

    public float moveSpeed, waitAtPoints;
    private float waitCounter;

    public float jumpForce;

    public Rigidbody2D RB;

    public Animator crabAnim;
    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoints;

        foreach(Transform point in padtrolPoints)
        {
            point.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - padtrolPoints[currentPoint].position.x) > .2f) 
        {
            if (transform.position.x < padtrolPoints[currentPoint].position.x)
            {
                RB.velocity = new Vector2(moveSpeed, RB.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            } else
            {
                RB.velocity = new Vector2(-moveSpeed, RB.velocity.y);
                transform.localScale =  Vector3.one;
            }

            //if (transform.position.y < padtrolPoints[currentPoint].position.y -.5f && RB.velocity.y < .1f)
            //{
            //    RB.velocity = new Vector2(RB.velocity.x, jumpForce);
            //}

        } else
        {
            RB.velocity = new Vector2(0f, RB.velocity.y);

            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                waitCounter = waitAtPoints;

                currentPoint++;

                if (currentPoint >= padtrolPoints.Length)
                {
                    currentPoint = 0;
                }
            }
        }

        crabAnim.SetFloat("speed", Mathf.Abs(RB.velocity.x));
    }
}
