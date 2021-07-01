using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnoffTile : MonoBehaviour
{
    public GameObject onOffTile;
    //public GameObject fadeTile;

    private float coolTime;

    private void Start() 
    {
        StartCoroutine(OnOff());
    }

    public IEnumerator OnOff()
    {
        while(true)
        {
            coolTime = Random.Range(0.5f, 1f); 

            yield return new WaitForSeconds(coolTime);
            
            onOffTile.SetActive(false);

            yield return new WaitForSeconds(coolTime);

            onOffTile.SetActive(true);

            Debug.Log(coolTime);
        }
    }
}
