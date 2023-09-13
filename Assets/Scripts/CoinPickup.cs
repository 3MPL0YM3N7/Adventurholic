using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    CircleCollider2D coinCollider;
    [SerializeField] AudioClip coinSFX;
    [SerializeField] int coinScoreAmount = 100;

    void coinPickup()
    {
        if (coinCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position, 0.6f);
            Destroy(gameObject);
            FindObjectOfType<GameSession>().increaseScoreAmount(coinScoreAmount);
            FindObjectOfType<GameSession>().increaseLives();
        }
    }

    void Start()
    {
        coinCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        coinPickup();
    }
}
