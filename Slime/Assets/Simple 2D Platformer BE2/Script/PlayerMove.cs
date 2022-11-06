using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    SpriteRenderer sr;
    float refVelocity;

    public float moveDir;
    public float moveSpeed = 4500f;
    public float maxSpeed = 5.5f;
    public float jumpPower = 17f;

    public float slideRate = 0.35f;
    public float AttackSlideRate = 0.25f;

    public LayerMask whatisGround;
    public Animator Anim;

    public bool isGround;
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
                rb.AddForce(new Vector2(moveDir * Time.fixedDeltaTime * moveSpeed,0));
        }
        
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
        moveDir = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGround && !IsPlayingAnime("Attack"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        if(!isGround)
        {
            MyAnimSetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.X) && !IsPlayingAnime("Attack"))
        {
            MyAnimSetTrigger("Attack");
        }
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
            if((Mathf.Abs(moveDir)<=0.01f||Mathf.Abs(rb.velocity.x)<=0.01f) && Mathf.Abs(rb.velocity.y) <= 0.01f)
            {
                MyAnimSetTrigger("Idle");
            }
            else if( (Mathf.Abs(rb.velocity.x) > 0.01f) && Mathf.Abs(rb.velocity.y) <= 0.01f)
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




}
