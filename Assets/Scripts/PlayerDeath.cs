using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    // if bumps into enemy, then isAlive = false
    public bool playerIsAlive = true;

    // player bounces after death
    PolygonCollider2D deathBounceCollider;
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    [SerializeField] float deathJump = 5f;

    // AUDIO
    [SerializeField] AudioClip deathSFX;

    void checkIfDead()
    {
        if (myRigidbody.IsTouchingLayers(LayerMask.GetMask("Enemies")) ||
            myRigidbody.IsTouchingLayers(LayerMask.GetMask("Spikes")) ||
            myRigidbody.IsTouchingLayers(LayerMask.GetMask("CustomWater")))
        {
            playerIsAlive = false;
            myAnimator.SetTrigger("isDying");
            deathBounceCollider.enabled = true;
            myRigidbody.velocity += new Vector2(0f, deathJump);

            FindObjectOfType<GameSession>().managePlayerLives();
        }
    }

    public void playDeathSFX()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
    }

    void Start()
    {
        deathBounceCollider = GetComponent<PolygonCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        checkIfDead();
    }
}
