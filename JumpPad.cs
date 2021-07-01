using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPower = 50f;
    public Rigidbody2D rb;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("ASDAS");
            rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * jumpPower);
        }
    }
}
