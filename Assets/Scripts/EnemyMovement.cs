using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    BoxCollider2D collisionDetector;
    [SerializeField] float moveSpeed = 1f;

    void flipSpriteOnCollision()
    {
        if (collisionDetector.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            // "*-1" --> change facing direction, change moving direction
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x * -1, myRigidbody.velocity.y);
        }
    }

    void Start()
    {
        collisionDetector = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
    }

    void Update()
    {
        flipSpriteOnCollision();
    }
}
