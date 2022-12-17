using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    SpriteRenderer sr;
    GameManager gameManager;
    float refVelocity;

    public float moveDir;
    public float moveSpeed = 4.6f;
    public float maxSpeed = 5.5f;
    public float jumpPower = 17f;

    public float slideRate = 0.35f;
    public float AttackSlideRate = 0.25f;

    public LayerMask whatisGround;
    public Animator Anim;
    Vector3 dirVec;

    GameObject scanObject;
    public bool isGround;
    public Vector2 boxSize;// 공격범위 
    public Transform[] AttackPos;// 공격위치 \
    int i = 0; // 공격방향

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        GroundCheck();
        PlayerAnim();
        GroundFriction();
    }
    void FixedUpdate()
    {
        if (!IsPlayingAnime("Attack"))
        {
            if(PlayerFlip() || Mathf.Abs(moveDir * rb.velocity.x) < maxSpeed)

                 transform.Translate(moveDir * (moveSpeed /*달리기 고민중*/) * Time.deltaTime, 0, 0);
                //rb.AddForce(new Vector2(moveDir * Time.fixedDeltaTime * moveSpeed,0));
        }
            Ray();

    }

    private void Ray()
    {
        Vector3 test = dirVec;
        if (moveDir > 0)
            test = Vector3.right;
        else if( moveDir < 0)
            test = Vector3.left;
        dirVec = test;
        Debug.DrawRay(rb.position, dirVec * 1.5f, Color.red);
        Debug.DrawRay(rb.position, dirVec * 1.5f, Color.red);
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, dirVec, 1.5f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;


    }
        
    bool IsPlayingAnime(string AnimName)
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName(AnimName))
        {
            return true;
        }
        return false;
    }
    void PlayerInput()
    {
        // Attack Dir
        if (moveDir == -1)
            i = 0;
        else if (moveDir == 1)
            i = 1;
        // --
        if (Input.GetKeyDown(KeyCode.Z) && scanObject != null )
        {
            GameManager.Instance.Action(scanObject); //z 조사 액션
        }
        moveDir = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGround && !IsPlayingAnime("Attack"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            rb.AddForce(transform.up * jumpPower * Time.fixedDeltaTime);
        }
        if (!isGround)
        {
            MyAnimSetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.X) && !IsPlayingAnime("Attack"))
        {
            MyAnimSetTrigger("Attack");

            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(AttackPos[i].position , boxSize, 0);
        
            foreach(Collider2D collider in collider2Ds)
            {
                if (collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<Enemy>().TakeDamage(1);
                }
            }
        }
    }
    private void OnDrawGizmos() // 공격범위 그리기
    {
        Gizmos.color = Color.blue;
  
        Gizmos.DrawWireCube(AttackPos[i].position, boxSize);

    }
    bool PlayerFlip()
    {
        bool flipSprite = (sr.flipX ? (moveDir > 0f) : (moveDir < 0f));
        if (flipSprite)
        {
            sr.flipX = !sr.flipX;
            GroundFriction();
        }
        return flipSprite;
    }
    void GroundCheck()
    {
        if (Physics2D.CircleCast(circleCollider.bounds.center, circleCollider.radius,Vector2.down,0.01f,whatisGround))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
    void PlayerAnim()
    {
        if(isGround && !IsPlayingAnime("Attack"))
        {
            if(moveDir == 0 && Mathf.Abs(rb.velocity.y) <= 0.01f)
            {
                MyAnimSetTrigger("Idle");
            }
            else if((moveDir == -1 || moveDir == 1) && Mathf.Abs(rb.velocity.y) <= 0.01f)
            {
                MyAnimSetTrigger("Walk");
            }
        }
    }
    void GroundFriction()
    {
        if (isGround)
        {
            if (IsPlayingAnime("Attack"))
            {
                rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, 0f, ref refVelocity, slideRate + AttackSlideRate), rb.velocity.y);
            }
            else if (Mathf.Abs(moveDir)<=0.01f)
            {
                rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, 0f, ref refVelocity, slideRate), rb.velocity.y);
            }
        }
    }
    void MyAnimSetTrigger(string t)
    {
        if (!IsPlayingAnime(t))
        {
            Anim.SetTrigger(t);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
    }


}
