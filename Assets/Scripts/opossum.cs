using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opossum : Enemy
{
    [SerializeField] private float leftCap; // Limite esquerdo para o movimento horizontal
    [SerializeField] private float rightCap; // Limite direito para o movimento horizontal

    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private bool movingRight = false;
    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movimento horizontal
        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, 0);

            if (transform.position.x >= rightCap)
            {
                movingRight = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0);

            if (transform.position.x <= leftCap)
            {
                movingRight = true;
            }
        }

        // Flip the sprite based on the direction
        if (movingRight && facingLeft)
        {
            Flip();
        }
        else if (!movingRight && !facingLeft)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingLeft = !facingLeft;

        // Multiply the object's scale by -1 to flip it along the x-axis
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
