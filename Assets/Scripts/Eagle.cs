using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    [SerializeField] private float upperCap; // Limite superior para o movimento vertical
    [SerializeField] private float lowerCap; // Limite inferior para o movimento vertical

    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private bool movingUp = true;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movimento vertical
        if (movingUp)
        {
            rb.velocity = new Vector2(0, moveSpeed);

            if (transform.position.y >= upperCap)
            {
                movingUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, -moveSpeed);

            if (transform.position.y <= lowerCap)
            {
                movingUp = true;
            }
        }
    }
}
