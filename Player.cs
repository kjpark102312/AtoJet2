using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using Cinemachine;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        idle,
        walk,
        dash,
        onJetpack
    }

    PlayerState state = PlayerState.idle;

    [SerializeField]
    private float dashCoolTime  = 0.4f;    
    [SerializeField]
    private float dashPower     = 10f;
    [SerializeField]
    public float jetpackGage;

    public float speed          = 5f;
    public float jetpackForce   = 5f;
    public float dir;

    public float StartDashTimer;
    float CurrentDashTimer;

    private float dashDir       = 1f;

    private Animator anim;

    public bool isUseSkill = true;
    public bool isDash     = false;
    public bool isMove     = true;

    public GameObject afterImage;
    public GameObject backGround;

    private readonly int hashWalk = Animator.StringToHash("IsRun");
    private readonly int hashDash = Animator.StringToHash("Dash");

    public SpriteRenderer rend;
    Rigidbody2D rb;
    CinemachineImpulseSource cinemachineImpulseSource;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    

    void Start()
    {
        rb   = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        cinemachineVirtualCamera.Follow = this.gameObject.transform;

        GameManager.Instance.jetpackGage = jetpackGage;
        GameManager.Instance.dashCount = 3;
    }

    void Update()
    {
        if(!isDash)
        {
            Move();
            Jetpack();
        }
        PlayerAnimControl();
        if(isUseSkill && Input.GetButtonDown("Jump")&& GameManager.Instance.dashCount > 0)
        {
            StartCoroutine(Dash());
            CurrentDashTimer = StartDashTimer;
        }
    }

    void Move()
    {
        if(isMove)
        {
            dir = Input.GetAxisRaw("Horizontal");

            transform.Translate(new Vector3(dir * speed * Time.deltaTime, 0, 0));

            if(dir > 0) 
            {
                rend.flipX = false;
                dashDir = dir;
            }
            else if(dir < 0)
            {
                rend.flipX = true;
                dashDir = dir;
            } 
        }
    }

    void Jetpack()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(new Vector2(0, jetpackForce) ,ForceMode2D.Force);

            state = PlayerState.onJetpack;

            GameManager.Instance.jetpackGage -= 0.5f;
        }
    }

    void PlayerAnimControl()
    {
        if(state == PlayerState.walk) anim.SetBool(hashWalk, true);

        if(isDash) 
        {
            anim.SetBool(hashWalk, false);
            anim.SetTrigger(hashDash);
        }

        if(dir != 0) state = PlayerState.walk;
        else if (dir == 0) anim.SetBool(hashWalk, false);
        
    }

    IEnumerator Dash()
    {
        GameManager.Instance.dashCount--;
        Destroy(GameManager.Instance.dashImageList[GameManager.Instance.dashImageList.Count - 1]);
        GameManager.Instance.dashImageList.Remove(GameManager.Instance.dashImageList[GameManager.Instance.dashImageList.Count -1]);


        afterImage.gameObject.SetActive(true);

        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        rb.AddForce(new Vector3(dashDir * dashPower, 0, 0), ForceMode2D.Impulse);

        cinemachineImpulseSource.GenerateImpulse();

        isUseSkill  = false;
        isMove      = false;
        isDash      = true;

        Debug.Log("DASH!!");

        state = PlayerState.dash;

        yield return new WaitForSeconds(dashCoolTime);

        afterImage.gameObject.SetActive(false);

        rb.velocity = Vector2.zero;
        rb.gravityScale = 1;

        isUseSkill = true;
        isMove     = true;
        isDash     = false;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(isDash)
        {
            rb.AddForce(new Vector2(-dashDir * 150, 100), ForceMode2D.Force);
        }    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
            
    }
}
