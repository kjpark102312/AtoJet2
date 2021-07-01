using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Transform dashPanel;
    public GameObject dashImage;

    public enum ItemState
    {
        fuel,
        addDashCount
    }

    public ItemState itemState = ItemState.fuel;
    public AudioSource audioSource;
    public AudioClip itemEat;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void CheckItem()
    {
        switch(itemState)
        {
            case ItemState.fuel:
            Fuel();
            break;
            case ItemState.addDashCount:
            AddDashCount();
            break;
        }
        this.gameObject.SetActive(false);
    }

    public void Fuel()
    {
        GameManager.Instance.jetpackGage += 20;
    }

    public void AddDashCount()
    {
        GameManager.Instance.dashCount++;
        GameObject dashimage = Instantiate(dashImage);
        dashimage.transform.parent = dashPanel.transform;
        GameManager.Instance.dashImageList.Add(dashimage);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player")) 
        {
            audioSource.PlayOneShot(itemEat);
            CheckItem();
        }
    }
}
