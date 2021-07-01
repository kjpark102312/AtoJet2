using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlock : MonoBehaviour
{

    Rigidbody2D rb;
    Player player;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            rb = other.GetComponent<Rigidbody2D>();
            player = other.GetComponent<Player>();
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            rb.gravityScale = 0.01f;
            player.speed = 2f;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            rb.gravityScale = 1f;
            player.speed = 5f;
        }
    }
}
