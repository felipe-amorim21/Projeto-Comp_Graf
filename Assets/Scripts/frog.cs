using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private LayerMask ground;

    private Collider2D coll;
    private Rigidbody2D rb;

    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
   
    }

    private void Update()
    {
        //Transition from Jump to Fall
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < -0.1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }
        //Transition from Fall to Idle
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    private void Move()
    {
        if (facingLeft && transform.position.x > leftCap)
        {
            Jump(-jumpLength, jumpHeight);
            anim.SetBool("Jumping", true);
        }
        else if (!facingLeft && transform.position.x < rightCap)
        {
            Jump(jumpLength, jumpHeight);
            anim.SetBool("Jumping", true);
        }

        FlipDirectionIfNeeded();
    }

    private void Jump(float xVelocity, float yVelocity)
    {
        if (coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(xVelocity, yVelocity);
        }
    }

    private void FlipDirectionIfNeeded()
    {
        if ((facingLeft && transform.position.x <= leftCap) || (!facingLeft && transform.position.x >= rightCap))
        {
            FlipCharacterScale();
        }
    }

    private void FlipCharacterScale()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1);
        facingLeft = !facingLeft;
    }
}
