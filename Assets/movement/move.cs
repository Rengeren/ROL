using UnityEngine;
using System.Collections;
//animatorでtriggerを作ってください

public class move : MonoBehaviour
{
    private PhotonView photonView;
    private PhotonTransformView photonTransformView;
    [HideInInspector]
    public Vector2 scale;
    private CharacterStatus Status;
    Animator animator;
    private bool ready = true;  //ready to jump
    float speedx;
    //移動速度
    [HideInInspector]
    public float walkForce = 1000f;
    [HideInInspector]
    public float maxwalkspeed = 100;
    private float airForce = 5f;
    //ポータルワープ
    [HideInInspector]
    public Vector3 warppoint;
    [HideInInspector]
    public bool canwarp = true;
    [HideInInspector]
    public bool onportal;

    //jump
    private GameObject Ground;
    [HideInInspector]
    public bool CanJump;
    [HideInInspector]
    public AudioClip JumpSE;
    [HideInInspector]
    public float LayDistance = 0.1f;
    //ジャンプの種類判定
    [HideInInspector]
    public bool downjump = false;
    //接地判定用
    public LayerMask GroundLayer;
    [HideInInspector]
    public bool IsGround = false;
    [HideInInspector]
    public bool CanDown = false;
    //ジャンプ力
    [HideInInspector]
    public float jumpforce = 5600;
    [HideInInspector]
    public float jumpforce2 = 2000; //下ジャンプ時
    //y軸方向の最大速度
    [HideInInspector]
    public float maxSpeedY = 900;

    //Inclination
    public static float angle = 38.7f;
    private float gravity = 0.1f * 180 * 9.81f;
    [HideInInspector]
    public bool onslope;
    [HideInInspector]
    public bool which;   //true=Left,false=Right

    void Start()
    {
        angle *= Mathf.Deg2Rad;
        Ground = transform.FindChild("Ground").gameObject;
        Status = GetComponent<CharacterStatus>();
        animator = GetComponent<Animator>();
        photonTransformView = GetComponent<PhotonTransformView>();
        photonView = PhotonView.Get(this);
    }

    //坂接地中
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Leftslope")
        {
            LayDistance = 10.0f;
            onslope = true;
            which = true;
        }
        if (other.gameObject.tag == "Rightslope")
        {
            LayDistance = 10.0f;
            onslope = true;
            which = false;
        }
    }
    //坂と離れた時
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Leftslope" || other.gameObject.tag == "Rightslope")
        {
            LayDistance = 0.1f;
            onslope = false;
        }
    }

    void Update()
    {
        IsGround = Physics2D.Linecast(transform.position + transform.up * 1.0f, transform.position - transform.up * LayDistance, GroundLayer);
        CanDown = Physics2D.Linecast(transform.position - transform.up * 1.0f, transform.position - transform.up * 599f, GroundLayer);
        if (Input.GetKey(KeyCode.UpArrow) && onportal && canwarp)
        {
            transform.position = warppoint;
        }
        if (IsGround != true && ready)
        {
            GetComponent<Animator>().SetBool("jump", true);
            ready = false;
        }
        if (IsGround && ready != true)
        {
            GetComponent<Animator>().SetBool("jump", false);
            ready = true;
        }
        //通常->移動
        if ((ready && Input.GetKey(KeyCode.RightArrow)) || (ready && Input.GetKey(KeyCode.LeftArrow)))
        {
            GetComponent<Animator>().SetBool("move", true);
            animator.speed = speedx / 50.0f;
            if (animator.speed < 1) animator.speed = 1;
        }
        //移動->通常
        else if (ready)
        {
            GetComponent<Animator>().SetBool("move", false);
        }
    }

    void FixedUpdate()
    {
        speedx = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x);
        //移動
        if (Input.GetKey(KeyCode.RightArrow) && Status.canmove)
        {
            scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
            if (GetComponent<Rigidbody2D>().velocity.x < maxwalkspeed)
            {
                if (IsGround && CanJump)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * walkForce);
                    if (GetComponent<Rigidbody2D>().velocity.x > maxwalkspeed) GetComponent<Rigidbody2D>().velocity = new Vector2(maxwalkspeed, GetComponent<Rigidbody2D>().velocity.y);
                    if (GetComponent<Rigidbody2D>().velocity.x < maxwalkspeed * -1) GetComponent<Rigidbody2D>().velocity = new Vector2(maxwalkspeed * -1, GetComponent<Rigidbody2D>().velocity.y);
                }
                else if (downjump != true)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * airForce);
                }
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Status.canmove)
        {
            Vector2 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
            if (GetComponent<Rigidbody2D>().velocity.x > maxwalkspeed * -1)
            {
                if (IsGround && CanJump)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.left * walkForce);
                    if (GetComponent<Rigidbody2D>().velocity.x > maxwalkspeed) GetComponent<Rigidbody2D>().velocity = new Vector2(maxwalkspeed, GetComponent<Rigidbody2D>().velocity.y);
                    if (GetComponent<Rigidbody2D>().velocity.x < maxwalkspeed * -1) GetComponent<Rigidbody2D>().velocity = new Vector2(maxwalkspeed * -1, GetComponent<Rigidbody2D>().velocity.y);
                }
                else if (downjump != true)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.left * airForce);
                }
            }
        }

        //jump
        //地面のすり抜け判定
        if (Input.GetKey(KeyCode.C) && GetComponent<Rigidbody2D>().velocity.y > 0) Ground.layer = LayerMask.NameToLayer("OffGround");
        if (downjump && GetComponent<Rigidbody2D>().velocity.y < -430)
        {
            Ground.layer = LayerMask.NameToLayer("OnGround");
            CanJump = true;
            downjump = false;
        }
        else if (downjump != true && GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            Ground.layer = LayerMask.NameToLayer("OnGround");
            CanJump = true;
        }

        //ジャンプさせる
        //下ジャンプ
        if (GetComponent<Debuff>().infirmed != true && Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.DownArrow) && IsGround && CanDown && Status.canmove && CanJump)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<AudioSource>().PlayOneShot(JumpSE);
            if (onslope || GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce2);
                downjump = true;
                CanJump = false;
                IsGround = false;
            }
        }
        //通常ジャンプ
        else if (GetComponent<Debuff>().infirmed != true && Input.GetKey(KeyCode.C) && IsGround && Status.canmove && Input.GetKey(KeyCode.DownArrow) != true && CanJump)
        {
            GetComponent<AudioSource>().PlayOneShot(JumpSE);
            if (onslope && GetComponent<Rigidbody2D>().velocity.y < 200)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce);
                if (which)
                {
                    if (Input.GetKey(KeyCode.RightArrow) && Status.canmove)
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.up * walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                        GetComponent<Rigidbody2D>().AddForce(Vector2.right * walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
                    }
                    if (Input.GetKey(KeyCode.LeftArrow) && Status.canmove)
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.down * walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                        GetComponent<Rigidbody2D>().AddForce(Vector2.right * walkForce);
                    }
                }
                if (which != true)
                {
                    if (Input.GetKey(KeyCode.LeftArrow) && Status.canmove)
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.up * walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                        GetComponent<Rigidbody2D>().AddForce(Vector2.left * walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
                    }
                    if (Input.GetKey(KeyCode.RightArrow) && Status.canmove)
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.down * walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                        GetComponent<Rigidbody2D>().AddForce(Vector2.left * walkForce);
                    }
                }
                IsGround = false;
                CanJump = false;
                onslope = false;
            }
            else if (GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce);
                if (GetComponent<Rigidbody2D>().velocity.x > 0)
                {
                    GetComponent<Rigidbody2D>().isKinematic = true;
                    GetComponent<Rigidbody2D>().isKinematic = false;
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(maxwalkspeed + 20, GetComponent<Rigidbody2D>().velocity.y);
                }
                if (GetComponent<Rigidbody2D>().velocity.x < 0)
                {
                    GetComponent<Rigidbody2D>().isKinematic = true;
                    GetComponent<Rigidbody2D>().isKinematic = false;
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(maxwalkspeed * -1 - 20, GetComponent<Rigidbody2D>().velocity.y);
                }
                CanJump = false;
                IsGround = false;
            }
        }
        //落下速度制限
        if (GetComponent<Rigidbody2D>().velocity.y < -maxSpeedY)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -maxSpeedY);
        }

        //Leftslope
        if (onslope && which && IsGround)
        {
            if (Input.GetKey(KeyCode.C) != true)
            {
                GetComponent<Rigidbody2D>().AddForce((Vector2.up * gravity * Mathf.Sin(angle)) * Mathf.Sin(angle));
                GetComponent<Rigidbody2D>().AddForce((Vector2.left * gravity * Mathf.Sin(angle)) * Mathf.Cos(angle));
            }
            if (Input.GetKey(KeyCode.RightArrow) && Status.canmove)
            {
                if (GetComponent<Rigidbody2D>().velocity.x < maxwalkspeed)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.down * walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                    GetComponent<Rigidbody2D>().AddForce(Vector2.left * walkForce * 2 * Mathf.Sin(angle) * Mathf.Sin(angle));
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow) && Status.canmove)
            {
                if (GetComponent<Rigidbody2D>().velocity.x > maxwalkspeed * -1f)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
                }
            }
        }
        //Rightslope
        if (onslope && which != true && IsGround)
        {
            if (Input.GetKey(KeyCode.C) != true)
            {
                GetComponent<Rigidbody2D>().AddForce((Vector2.up * gravity * Mathf.Sin(angle)) * Mathf.Sin(angle));
                GetComponent<Rigidbody2D>().AddForce((Vector2.right * gravity * Mathf.Sin(angle)) * Mathf.Cos(angle));
            }
            if (Input.GetKey(KeyCode.RightArrow) && Status.canmove)
            {
                if (GetComponent<Rigidbody2D>().velocity.x < maxwalkspeed)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                    GetComponent<Rigidbody2D>().AddForce(Vector2.left * walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow) && Status.canmove)
            {
                if (GetComponent<Rigidbody2D>().velocity.x > maxwalkspeed * -1f)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.down * walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
                }
            }
        }

        //PUN
        if (photonView.isMine)
        {
            //現在の移動速度
            Vector3 velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            //移動速度を指定
            photonTransformView.SetSynchronizedValues(velocity, 0);
        }
    }
}