using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource deathSound;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        deathSound = GetComponent<AudioSource>();
    }

    public void jumpedOn()
    {
        // Play death sound
        if (deathSound != null)
        {
            deathSound.Play();
        }

        // Trigger death animation
        anim.SetTrigger("death");

    }

    private void death()
    {
        Destroy(this.gameObject);
    }
}
