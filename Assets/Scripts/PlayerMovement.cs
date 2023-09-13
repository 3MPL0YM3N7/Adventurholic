using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveValue;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;

    [SerializeField] float horizontalSpeed = 7f;
    [SerializeField] float verticalSpeed = 7f;
    [SerializeField] float jumpSpeed = 7f;

    //AUDIO
    [SerializeField] AudioClip jumpSFX;

    // if user enters "up" or "down" while touching ladder, then isClimbing = true
    bool isClimbing = false;

    PlayerDeath PlayerDeath;

    void OnMove(InputValue value)
    {
        if (!PlayerDeath.playerIsAlive) { return; }
        moveValue = value.Get<Vector2>();
        Debug.Log(moveValue);
    }

    void Move()
    {
        // RUNNING
        // only changes velocity.x (horizontal speed)
        myRigidbody.velocity = new Vector2(moveValue.x * horizontalSpeed, myRigidbody.velocity.y);

        // RUN ANIMATION
        if (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon)
        {
            myAnimator.SetBool("isRunning", true);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }

        // CLIMBING
        // changes velocity.y (vertical speed) when touching ladder
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            if (Mathf.Abs(moveValue.y) > Mathf.Epsilon)
            {
                myRigidbody.velocity = new Vector2(moveValue.x * horizontalSpeed, moveValue.y * verticalSpeed);
                // gravity change when climbing ladder
                myRigidbody.gravityScale = 0f;
                isClimbing = true;

                // CLIMB ANIMATION
                myAnimator.SetBool("isClimbing", true);
            }

            if (moveValue.y == 0f && isClimbing == true)
            {
                myRigidbody.velocity = new Vector2(moveValue.x * horizontalSpeed, 0f);
            }
        }
        // if not touching ladder
        else if (!(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))))
        {
            // 6f = gravityScale at start
            myRigidbody.gravityScale = 6f;
            isClimbing = false;

            // ANIMATION
            myAnimator.SetBool("isClimbing", false);
        }
    }

    void flipSprite()
    {
        // if player is moving (NOT idling) --> otherwise when idling Mathf.Abs(0f) = 1
        if ((Mathf.Abs(moveValue.x) > Mathf.Epsilon))
        {
            transform.localScale = new Vector2(Mathf.Sign(moveValue.x), transform.localScale.y);
        }
    }

    void OnJump(InputValue value)
    {
        if (!PlayerDeath.playerIsAlive) { return; }
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (value.isPressed)
            {
                myRigidbody.velocity += new Vector2(0f, jumpSpeed);
                AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position, 0.7f);
            }
        }
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();

        PlayerDeath = FindObjectOfType<PlayerDeath>();
    }

    void Update()
    {
        if (!PlayerDeath.playerIsAlive) { return; }
        Move();
        flipSprite();
    }
}

