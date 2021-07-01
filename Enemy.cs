using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public float sawDis = 5f;
    public float maceDis = 5f;

    private float rotateSpeed = 50f;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    public enum EnemyType
    {
        saw,
        spike,
        mace
        
    }

    public EnemyType enemyType;

    private void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Move();
    }

    void Update()
    {
        Rotation();
    }

    void Move()
    {
        if(enemyType == EnemyType.saw)
        {
            transform.DOMoveX(this.transform.position.x + sawDis, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else if(enemyType == EnemyType.mace)
        {
            transform.DOMoveY(this.transform.position.y +   maceDis, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }

    void Rotation()
    {
        if(enemyType == EnemyType.saw)
        {
            spriteRenderer.transform.Rotate(new Vector3(0,0,rotateSpeed) * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.jetpackGage = 100f;
            GameManager.Instance.LoadScene(); 
        }          
    }
}
