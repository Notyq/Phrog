using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();
        }

        // water check that didnt work
        /*if (inWater == true)
        {
            body.velocity = new Vector2(Input.GetAxis("Vertical") * swim, body.velocity.x);
            body.velocity = new Vector2(body.velocity.y, swim);
        }*/ 

    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision) //for animation - if is on ground (idle)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded() = true;
        }
    }*/

        private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

}
