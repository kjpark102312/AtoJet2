using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class offTile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Offco());
        }
    }

    public IEnumerator Offco()
    {
        yield return new WaitForSeconds(1f);

        this.gameObject.SetActive(false);
    }
}
