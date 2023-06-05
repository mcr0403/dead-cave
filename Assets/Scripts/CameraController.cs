using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    

    private PlayerController player;

    public BoxCollider2D boundBox;

    private float haftWeight, haftHeight;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        haftHeight = Camera.main.orthographicSize;
        haftWeight = haftHeight * Camera.main.aspect;

        AudioManager.instance.PlayLevelMusic();
    }

    // Update is called once per frame
    void Update()
    {

        //Camera controller
        if (player != null)
        {
            transform.position = new Vector3( 
                Mathf.Clamp(player.transform.position.x, boundBox.bounds.min.x + haftWeight, boundBox.bounds.max.x - haftWeight),
                Mathf.Clamp(player.transform.position.y, boundBox.bounds.min.y + haftHeight, boundBox.bounds.max.y - haftHeight),
                transform.position.z);
        }
        else
        {
            player = FindObjectOfType<PlayerController>();
        }
    }
}
