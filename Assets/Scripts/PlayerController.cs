using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //public static PlayerController instance;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);

    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public Rigidbody2D theRB;

    public float moveSpeed;
    public float jumbForce;

    public Transform groundPoint;
    private bool isOnGround;
    public LayerMask whatIsGround;

    public Animator anim, ballAnim;

    public BulletController shotToFire;
    public Transform shotPoint;

    private bool canDoubleJump = true;

    public float dashSpeed, dashTime;
    private float dashCount;

    public SpriteRenderer sr, afterImage;
    public float afterImageLifetime, timeBetweenAffterImages;
    private float afterImageCounter;
    public Color afterImageColor;

    public float timeWaitAfterDasing;
    private float dashRechargeCounter;

    public GameObject standing, ball;
    public float timeToTransform;
    private float transformCounter;

    public Transform bombPoint;
    public GameObject bomb;

    private PlayerAbilityTracker abilities;

    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        abilities = GetComponent<PlayerAbilityTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && Time.timeScale != 0)
        {
            //count to dash again
            if (dashRechargeCounter > 0)
            {
                dashRechargeCounter -= Time.deltaTime;
            } else
            {
                //create dasing
                if (Input.GetButtonDown("Fire2") && standing.activeSelf && abilities.canDash)
                {
                    dashCount = dashTime;

                    AudioManager.instance.PlaySFX(7);
                }
            }


            if (dashCount > 0)
            {
                dashCount = dashCount - Time.deltaTime;

                theRB.velocity = new Vector2(dashSpeed * transform.localScale.x, theRB.velocity.y);

                //time appear of image dashing
                afterImageCounter -= Time.deltaTime;
                if (afterImageCounter < 0)
                {
                    ShowAfterImage();
                }

                dashRechargeCounter = timeWaitAfterDasing;

            }
            else
            {
                //move sideway
                theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);



                //flip player
                if (theRB.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else if (theRB.velocity.x > 0)
                    transform.localScale = Vector3.one;
            }


            //checking sprite on groud
            isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

            //Jump
            if (Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump && abilities.canDoubleJump)))
            {
                if (isOnGround)
                {
                    canDoubleJump = true;

                    AudioManager.instance.PlaySFX(12);
                }
                else
                {
                    canDoubleJump = false;

                    anim.SetTrigger("doubleJump");

                    AudioManager.instance.PlaySFX(9);
                }

                theRB.velocity = new Vector2(theRB.velocity.x, jumbForce);
            }

            //checking sprite on groud
            isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

            //shot & bomb
            if (Input.GetButtonDown("Fire1"))
            {
                if (standing.activeSelf)
                {
                    Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);

                    anim.SetTrigger("shotFired");

                    AudioManager.instance.PlaySFX(14);
                }

                if (ball.activeSelf && abilities.canDropBall)
                {
                    Instantiate(bomb, bombPoint.position, bombPoint.rotation);

                    AudioManager.instance.PlaySFX(13);
                }

            }

            //transform 
            if (!ball.activeSelf)
            {
                //ball mode
                if (Input.GetAxisRaw("Vertical") < -0.5f && abilities.canBecomeBall)
                {
                    transformCounter -= Time.deltaTime;
                    if (transformCounter < 0)
                    {
                        ball.SetActive(true);
                        standing.SetActive(false);

                        AudioManager.instance.PlaySFX(6);
                    }
                } else
                {
                    transformCounter = timeToTransform;
                }
            } else
            {
                //human mode
                if (Input.GetAxisRaw("Vertical") > 0.5f)
                {
                    transformCounter -= Time.deltaTime;
                    if (transformCounter < 0)
                    {
                        ball.SetActive(false);
                        standing.SetActive(true);

                        AudioManager.instance.PlaySFX(10);
                    }
                }
                else
                {
                    transformCounter = timeToTransform;
                }
            }

        } else
        {
            theRB.velocity = Vector2.zero;
        }


    


        //change action
        if (standing.activeSelf)
        {
            anim.SetBool("isOnGround", isOnGround);
            anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        }

        if (ball.activeSelf)
        {
            ballAnim.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));
        }
        
    }

    // create image after dashing
    public void ShowAfterImage()
    {
        SpriteRenderer image =  Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = sr.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        Destroy(image.gameObject, afterImageLifetime);

        afterImageCounter = timeBetweenAffterImages;
    }
}
