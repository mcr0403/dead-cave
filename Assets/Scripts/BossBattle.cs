using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    private CameraController theCam;
    public Transform camPosition;
    public float camSpeed;

    public int threshold1, threshold2;

    public float activeTime, fadeOutTime, inActiveTime, failTime;
    private float activeCounter, fadeCounter, inActiveCounter;

    public Transform[] spawnPoints;
    private Transform targetPoint;
    public float moveSpeed;

    public Animator anim;

    public Transform theBoss;

    public float timeBetweenShots1, timeBetweenShots2;
    private float shotCounter;
    public GameObject bullet;
    public Transform shotPoint;

    public GameObject winObject;

    private bool battleEnded;


    // Start is called before the first frame update
    void Start()
    {
        theCam = FindObjectOfType<CameraController>();
        theCam.enabled = false;

        activeCounter = activeTime;

        shotCounter = timeBetweenShots1;

        AudioManager.instance.PlayBossMusic();
    }

    // Update is called once per frame
    void Update()
    {  
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position, camSpeed * Time.deltaTime);
        
        if (!battleEnded)
        {
            
            //boss health upper 75%
            if (BossHealthController.instance.curretnHealth > threshold1)
            {
                if (activeCounter > 0)
                {
                
                    activeCounter -= Time.deltaTime;
                    if (activeCounter <= 0)
                    {
                        fadeCounter = fadeOutTime;
                        anim.SetTrigger("vanish");
                    }

                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        shotCounter = timeBetweenShots1;

                        Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    }

                }
                else if (fadeCounter > 0)
                {
                    fadeCounter -= Time.deltaTime;
                    if (fadeCounter <= 0)
                    {
                        theBoss.gameObject.SetActive(false);
                        inActiveCounter = inActiveTime;
                    }
                }
                else if (inActiveCounter > 0)
                {
                    inActiveCounter -= Time.deltaTime;
                    if (inActiveCounter <= 0)
                    {
                        theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                        theBoss.gameObject.SetActive(true);
                        activeCounter = activeTime;

                        shotCounter = timeBetweenShots2;
                    }
                }
            }
            //boss health: 50%< <75% 
            else 
            {
                if (targetPoint == null) 
                {
                    targetPoint = theBoss;
                    fadeCounter = fadeOutTime;
                    anim.SetTrigger("vanish");

                }else
                {
                    if (Vector3.Distance(theBoss.position, targetPoint.position) > .02f)
                    {
                        theBoss.position = Vector3.MoveTowards(theBoss.position, targetPoint.position, moveSpeed * Time.deltaTime);

                        activeCounter -= Time.deltaTime;
                        if (Vector3.Distance(theBoss.position, targetPoint.position) <= .02f)
                        {
                            fadeCounter = fadeOutTime;
                            anim.SetTrigger("vanish");
                        }

                        shotCounter -= Time.deltaTime;
                        if (shotCounter <= 0)
                        {
                            if (PlayerHeathController.instance.currentHealth > threshold2)
                            {
                                shotCounter = timeBetweenShots1;
                            } else
                            {
                                shotCounter = timeBetweenShots2;
                            }

                            Instantiate(bullet, shotPoint.position, Quaternion.identity);
                        }
                    }
                    else if (fadeCounter > 0)
                    {
                        fadeCounter -= Time.deltaTime;
                        if (fadeCounter <= 0)
                        {
                            theBoss.gameObject.SetActive(false);
                            inActiveCounter = inActiveTime;
                        }
                    }
                    else if (inActiveCounter > 0)
                    {
                        inActiveCounter -= Time.deltaTime;
                        if (inActiveCounter <= 0)
                        {
                            theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                            targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                            int whileBreaker = 0;
                            while(targetPoint.position == theBoss.position && whileBreaker <100)
                            {
                                targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                                whileBreaker++;
                            }

                            theBoss.gameObject.SetActive(true);


                            if (PlayerHeathController.instance.currentHealth > threshold2)
                            {
                                shotCounter = timeBetweenShots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShots2;
                            }
                        }
                    }
                }
            }
        } else
        {
            
            fadeCounter -= Time.deltaTime;
            if (fadeCounter <= 0)
            {
                Debug.Log(fadeCounter);
                if (winObject != null)
                {
                    winObject.transform.SetParent(null);
                    winObject.SetActive(true);
                }

                theCam.enabled = true;


                gameObject.SetActive(false);
            }

        }

    }

    public void EndBattle()
    {
        battleEnded = true;

        fadeCounter = failTime;
        anim.SetTrigger("faild");
        theBoss.GetComponent<Collider2D>().enabled = false;


        BossBullet[] bullets = FindObjectsOfType<BossBullet>();
        if (bullets.Length > 0)
        {
            foreach(BossBullet bb in bullets)
            {
                Destroy(bb.gameObject);
            }
        }
               

    }
}
