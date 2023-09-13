using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

// it is a script for only one sound effect
public class MushroomSFX : MonoBehaviour
{
    AudioSource audioSource;
    TilemapCollider2D mushroomCollider1;
    CompositeCollider2D mushroomCollider2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mushroomCollider1 = GetComponent<TilemapCollider2D>();
        mushroomCollider2 = GetComponent<CompositeCollider2D>();
    }

    void playBounceSFX()
    {
        if (mushroomCollider1.IsTouchingLayers(LayerMask.GetMask("Player")) || 
            mushroomCollider2.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            audioSource.Play();
        }
    }
    void Update()
    {
        playBounceSFX();
    }
}
