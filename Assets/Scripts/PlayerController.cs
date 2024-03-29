using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    protected AudioSource[] audioSources;

    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask ground;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] private float hurtforce = 5f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private Text cherryText;
    [SerializeField] private int health = 3;
    [SerializeField] private Text healthText;

    public float yPosition;

    [SerializeField] private bool hasPowerUp = false;
    [SerializeField] private int jumpsRemaining = 1;
    [SerializeField] private bool canDoubleJump = false;


    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "level_1")
        {
            cherries = 0;
            PlayerPrefs.SetInt("CollectedCherries", cherries);
            PlayerPrefs.Save();
        }
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        audioSources = GetComponents<AudioSource>();
        healthText.text = health.ToString();

        cherries = PlayerPrefs.GetInt("CollectedCherries", 0);
        cherryText.text = cherries.ToString();
        ResetPowerUp();
    }

    private void Update()
    {
        if (state != State.hurt)
        {
            movement();
        }

        animationState();
        anim.SetInteger("state", (int)state);

        yPosition = transform.position.y;
        if (yPosition < -10)
        {
            audioSources[3].Play();
            float delay = Mathf.Max(0, audioSources[3].clip.length - 0.8f);
            Invoke("LoadSceneAfterDelay", delay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            audioSources[0].Play();
            cherries += 1;
            cherryText.text = cherries.ToString();

            PlayerPrefs.SetInt("CollectedCherries", cherries);
            PlayerPrefs.Save();
        }

        if (collision.tag == "powerUp")
        {
            Destroy(collision.gameObject);
            audioSources[1].Play();
            hasPowerUp = true;
            canDoubleJump = true;
}
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.jumpedOn();
                jump();
            }
            else
            {
                state = State.hurt;
                audioSources[2].Play();
                health -= 1;
                healthText.text = health.ToString();
                if (health <= 0)
                {
                    audioSources[3].Play();
                    float delay = Mathf.Max(0, audioSources[3].clip.length - 0.8f);
                    Invoke("LoadSceneAfterDelay", delay);
                }
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtforce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtforce, rb.velocity.y);
                }
            }
        }
    }

    private void movement()
    {
        if (coll.IsTouchingLayers(ground))
        {
            if(hasPowerUp == true)
            {
                canDoubleJump = true;
                jumpsRemaining = 1;

            }
        }
        float hDirection = Input.GetAxis("Horizontal");

        // Moving left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true; // Flip the sprite when moving left.

        }
        // Moving right
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = false; // Don't flip the sprite when moving right.

        }

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (coll.IsTouchingLayers(ground))
            {
                jump();
            }
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                jump();
            }
        }
       
    }

    private void jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
        if (Input.GetButtonDown("Jump") && hasPowerUp == true && jumpsRemaining == 1)
        {
            jumpsRemaining--;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void animationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }


        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
    /*private void Footstep()
    {
        footstep.Play();
    }*/
    public void ResetDoubleJump()
    {
        canDoubleJump = false;
        jumpsRemaining = 0;
    }

    /*public void ResetPowerUp()
    {
        hasPowerUp = false;
        canDoubleJump = false;
    }*/

    private void LoadSceneAfterDelay()
    {
        ResetPowerUp();
        cherries = 0;
        PlayerPrefs.SetInt("CollectedCherries", cherries);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetPowerUp()
    {
        hasPowerUp = false;
        canDoubleJump = false;
        jumpsRemaining = 1; // Reset jumps remaining as well
    }
}