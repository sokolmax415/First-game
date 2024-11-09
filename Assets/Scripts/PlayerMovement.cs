using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        } else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed * 1.5f);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
