using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject player;

    public Transform originTr;
    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        GameManager.Instance.mapIndex++;
        GameManager.Instance.LoadScene();
    }
}
