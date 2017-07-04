using UnityEngine;
using System.Collections;

public class jump : MonoBehaviour
{
    private GameObject Ground;
    private CharacterStatus Status;
    [HideInInspector]
    public bool CanJump;
    [HideInInspector]
    public AudioClip JumpSE;
    move move;
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
    //Laycast距離調整用
    [HideInInspector]
    public float LayDistance = 0.1f;
    //for ladder
    [HideInInspector]
    public bool onladder=false;

    //Inclination
    public static float angle = 38.7f;
    private float gravity = 0.1f * 180 * 9.81f;
    [HideInInspector]
    public bool onslope;
    [HideInInspector]
    public bool which;   //true=Left,false=Right

    void Awake()
    {
        angle *= Mathf.Deg2Rad;
    }

    void Start()
    {
        Ground= transform.FindChild("Ground").gameObject;
        Status = GetComponent<CharacterStatus>();
        move = GetComponent<move>();
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
        IsGround = Physics2D.Linecast(
            transform.position + transform.up * 1.0f, transform.position - transform.up * LayDistance, GroundLayer);
        CanDown = Physics2D.Linecast(transform.position - transform.up * 1.0f, transform.position - transform.up * 599f, GroundLayer);
    }

    void FixedUpdate()
    {
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
        if (GetComponent<Debuff>().infirmed!=true&&Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.DownArrow) && IsGround && CanDown && Status.canmove&&CanJump)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<AudioSource>().PlayOneShot(JumpSE);
            if (onslope||GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce2);
                downjump = true;
                CanJump = false;
                IsGround = false;
            }
        }
        //通常ジャンプ
        else if (GetComponent<Debuff>().infirmed!=true && Input.GetKey(KeyCode.C) && IsGround && Status.canmove&& Input.GetKey(KeyCode.DownArrow)!=true&&CanJump)
        {
            GetComponent<AudioSource>().PlayOneShot(JumpSE);
            if (onslope && GetComponent<Rigidbody2D>().velocity.y < 200)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce);
                if (which)
                {
                    if (Input.GetKey(KeyCode.RightArrow) && Status.canmove)
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.up * move.walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                        GetComponent<Rigidbody2D>().AddForce(Vector2.right * move.walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
                    }
                    if (Input.GetKey(KeyCode.LeftArrow) && Status.canmove) GetComponent<Rigidbody2D>().AddForce(Vector2.down * move.walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                }
                if (which != true)
                {
                    if (Input.GetKey(KeyCode.LeftArrow) && Status.canmove)
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.up * move.walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                        GetComponent<Rigidbody2D>().AddForce(Vector2.left * move.walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
                    }
                    if (Input.GetKey(KeyCode.RightArrow) && Status.canmove) GetComponent<Rigidbody2D>().AddForce(Vector2.down * move.walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                }
                IsGround = false;
                CanJump = false;
                onslope = false;
            }
            else if (GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                if (Input.GetKey(KeyCode.RightArrow)) GetComponent<Rigidbody2D>().velocity =new Vector2(1,0)*move.maxwalkspeed;
                if (Input.GetKey(KeyCode.LeftArrow)) GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * move.maxwalkspeed;
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce);
                CanJump = false;
                IsGround = false;
                if (Input.GetKey(KeyCode.RightArrow)) GetComponent<Rigidbody2D>().AddForce(Vector2.right * 18 * 0.7f);
                if (Input.GetKey(KeyCode.LeftArrow)) GetComponent<Rigidbody2D>().AddForce(Vector2.left * 18 * 0.7f);
            }
        }
        //落下速度制限
        if (GetComponent<Rigidbody2D>().velocity.y< -maxSpeedY)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -maxSpeedY);
        }


        //Leftslope
        if (onslope && which &&IsGround)
        {
            if (Input.GetKey(KeyCode.C) != true)
            {
                GetComponent<Rigidbody2D>().AddForce((Vector2.up * gravity * Mathf.Sin(angle)) * Mathf.Sin(angle));
                GetComponent<Rigidbody2D>().AddForce((Vector2.left * gravity * Mathf.Sin(angle)) * Mathf.Cos(angle));
            }
            if (Input.GetKey(KeyCode.RightArrow) && Status.canmove)
            {
                if (GetComponent<Rigidbody2D>().velocity.x < move.maxwalkspeed)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.down * move.walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                    GetComponent<Rigidbody2D>().AddForce(Vector2.left * move.walkForce * 2 * Mathf.Sin(angle) * Mathf.Sin(angle));
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow) && Status.canmove)
            {
                if (GetComponent<Rigidbody2D>().velocity.x > move.maxwalkspeed * -1f)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * move.walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * move.walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
                }
            }
        }
        //Rightslope
        if (onslope && which != true &&IsGround)
        {
            if (Input.GetKey(KeyCode.C) != true)
            {
                GetComponent<Rigidbody2D>().AddForce((Vector2.up * gravity * Mathf.Sin(angle)) * Mathf.Sin(angle));
                GetComponent<Rigidbody2D>().AddForce((Vector2.right * gravity * Mathf.Sin(angle)) * Mathf.Cos(angle));
            }
            if (Input.GetKey(KeyCode.RightArrow) && Status.canmove)
            {
                if (GetComponent<Rigidbody2D>().velocity.x < move.maxwalkspeed)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * move.walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                    GetComponent<Rigidbody2D>().AddForce(Vector2.left * move.walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow) && Status.canmove)
            {
                if (GetComponent<Rigidbody2D>().velocity.x > move.maxwalkspeed * -1f)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.down * move.walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * move.walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
                }
            }
        }
    }
}