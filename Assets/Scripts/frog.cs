using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : MonoBehaviour

{
    public float jumpForce = 2f;
    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isJumpingLeft = true;
    private enum State { Idle, Jumping }
    private State state = State.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Jump();
    }

    void Update()
    {
        // Check if the frog has landed to allow for the next jump
        if (!isJumping && rb.velocity.magnitude == 0)
        {
            SetState(State.Jumping);
            isJumping = true;
            Invoke("Jump", 2f); // Adjust the delay as needed
        }
    }

    void Jump()
    {
        if (isJumpingLeft)
        {
            // Jump to the left
            rb.velocity = new Vector2(-jumpForce, jumpForce);
        }
        else
        {
            // Jump to the right
            rb.velocity = new Vector2(jumpForce, jumpForce);
        }

        isJumpingLeft = !isJumpingLeft; // Switch the jump direction

        // Reset the isJumping flag after a short delay to allow the frog to jump again
        Invoke("ResetIsJumping", 0.1f);
    }

    void ResetIsJumping()
    {
        isJumping = false;
        SetState(State.Idle);
    }

    void SetState(State newState)
    {
        // Handle state changes here (you can add more logic as needed)
        switch (newState)
        {
            case State.Idle:
                Debug.Log("State: Idle");
                // Add idle state behavior if necessary
                break;

            case State.Jumping:
                Debug.Log("State: Jumping");
                // Add jumping state behavior if necessary
                break;
        }

        state = newState;
    }
}

