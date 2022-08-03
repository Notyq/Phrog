using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimMovement : MonoBehaviour
{
    // movement in water
    [HideInInspector] public bool swimming;
    [HideInInspector] public Vector2 swimDir;
    [SerializeField] private float swimSpeed;
    private float dirX;
    private float dirY;
    bool inWater;

    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private PlayerMovement playerMove;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerMove = GetComponent<PlayerMovement>();
    }

    /*private void Update()
    {
        //flips the sprite
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * swimSpeed, body.velocity.y);
    }*/
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!swimming && collision.CompareTag("Water"))
        {
            swimming = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (swimming && collision.CompareTag("Water"))
        {
            swimming = false;
        }
    }

    private void Swim()
    {
        inWater = true;
        body.velocity *= 0.9f;
        body.gravityScale = 0;
        swimDir = new Vector2(dirX, dirY).normalized;
        body.AddForce(swimDir * swimSpeed, ForceMode2D.Impulse);

        // vertical movement
        if (Input.GetKey(KeyCode.W))
        {
            dirY = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dirY = -1;
        }
        else
        {
            dirY = 0;
        }

        // horizontal movement
        if (Input.GetKey(KeyCode.D))
        {
            dirX = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dirX = -1;
        }
        else
        {
            dirX = 0;
        }
    }
}
