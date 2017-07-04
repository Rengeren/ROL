using UnityEngine;
using System.Collections;
//Layer:mob,mobHook

public class Kinoko : MonoBehaviour
{
    private GameObject Ground;
    Debuff debuff;
    private float gravity = 0.1f * 180 * 9.81f;
    //移動用
    private float walkForce = 150f;
    private float maxwalkspeed = 100f;
    private float targetPosition;
    private float time = 0;
    private bool onslope;
    private bool which;   //true=Left,false=Right
    private float angle = 38.7f;
    private bool stopmove = true;
    //ジャンプ用
    private float time2 = 0;
    private int JumpDecition=2;
    private float jumpforce = 5000f;
    private float jumpforce2 = 1000f;
    private bool downjump = false;
    //設置判定
    public LayerMask GroundLayer;
    [HideInInspector]
    public bool IsGround = false;
    private float LayDistance = 2f;
    //移動範囲の制限
    private float MaxX;
    private float MinX;
    private float MaxX1 = 3070;
    private float MinX1 = 851;
    private float MaxX2 = 2908;
    private float MinX2 = 1118;
    private float MaxX3 = 2679;
    private float MinX3 = 1327;
    private float MaxX4 = 3006;
    private float MinX4 = 974;

    void Start()
    {
        angle *= Mathf.Deg2Rad;
        targetPosition = transform.position.x;
        Ground= transform.FindChild("Ground").gameObject;
        debuff = GetComponent<Debuff>();
    }

    //坂接地中
    void OnCollisionStay2D(Collision2D other)
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
            LayDistance = 2f;
            onslope = false;
        }
    }

    void Update()
    {
        IsGround = Physics2D.Linecast(
            transform.position + transform.up * 1.0f, transform.position - transform.up * LayDistance, GroundLayer);
    }

    void FixedUpdate()
    {
        time = time + Time.deltaTime;   //目的地変更時間
        time2 = time2 + Time.deltaTime; //ジャンプ判定時間
        if (time >= 4f)
        {
            time = 0;
            stopmove = false;
            targetPosition = transform.position.x + Random.Range(-600, 600);
            if (transform.position.y >= 86.10983)
            {
                MinX = MinX4;
                MaxX = MaxX4;
            }
            else if (transform.position.y >= 29.70994)
            {
                MinX = MinX3;
                MaxX = MaxX3;
            }
            else if (transform.position.y >= -31.29005)
            {
                MinX = MinX2;
                MaxX = MaxX2;
            }
            else
            {
                MinX = MinX1;
                MaxX = MaxX1;
            }
            if (targetPosition > MaxX) targetPosition=2*MaxX-targetPosition;
            else if (targetPosition < MinX) targetPosition = 2*MinX-targetPosition;
        }
        if (time2 >= 3f)
        {
            time2 = 0;
            JumpDecition = Random.Range(0, 10);
        }

        //移動
        if (stopmove != true && GetComponent<Transform>().position.x > targetPosition && GetComponent<Rigidbody2D>().velocity.x > maxwalkspeed * -1 && debuff.Swooned != true && debuff.infirmed != true&&debuff.Slowed!=true&&debuff.Iced!=true)
        {
            Vector2 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * walkForce);
            if (GetComponent<Transform>().position.x <= targetPosition + 10f) stopmove = true;
        }
        if (stopmove != true && GetComponent<Transform>().position.x < targetPosition && GetComponent<Rigidbody2D>().velocity.x < maxwalkspeed && debuff.Swooned != true && debuff.infirmed != true && debuff.Slowed != true && debuff.Iced != true)
        {
            Vector2 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * walkForce);
            if (GetComponent<Transform>().position.x >= targetPosition - 10f) stopmove = true;
        }
        //ジャンプ
        if (JumpDecition == 0 && transform.position.y <= 86.10983&&debuff.Swooned!=true&&debuff.infirmed!=true)  //通常ジャンプ
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce);
            Ground.layer = LayerMask.NameToLayer("OffGround");
            IsGround = false;
            JumpDecition = 2;
        }
        if (JumpDecition == 1 && transform.position.y >= -31.29005 && debuff.Swooned != true && debuff.infirmed != true)  //下ジャンプ
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpforce2);
            Ground.layer = LayerMask.NameToLayer("OffGround");
            downjump = true;
            IsGround = false;
            JumpDecition = 2;
        }
        if (downjump && GetComponent<Rigidbody2D>().velocity.y < -360)
        {
            Ground.layer = LayerMask.NameToLayer("MobOnGround");
            downjump = false;
        }
        else if (downjump != true && GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            Ground.layer = LayerMask.NameToLayer("MobOnGround");
        }

        //坂での動き
        //Leftslope
        if (onslope && which && IsGround)
        {
            GetComponent<Rigidbody2D>().AddForce((Vector2.up * gravity * Mathf.Sin(angle)) * Mathf.Sin(angle));
            GetComponent<Rigidbody2D>().AddForce((Vector2.left * gravity * Mathf.Sin(angle)) * Mathf.Cos(angle));

            if (GetComponent<Rigidbody2D>().velocity.x > 0 && GetComponent<Rigidbody2D>().velocity.x < maxwalkspeed)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.down * walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
            }
            if (GetComponent<Rigidbody2D>().velocity.x < 0 && GetComponent<Rigidbody2D>().velocity.x > maxwalkspeed * -1f)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
            }
        }
        //Rightslope
        if (onslope && which != true && IsGround)
        {
            GetComponent<Rigidbody2D>().AddForce((Vector2.up * gravity * Mathf.Sin(angle)) * Mathf.Sin(angle));
            GetComponent<Rigidbody2D>().AddForce((Vector2.right * gravity * Mathf.Sin(angle)) * Mathf.Cos(angle));

            if (GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                if (GetComponent<Rigidbody2D>().velocity.x < maxwalkspeed)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                    GetComponent<Rigidbody2D>().AddForce(Vector2.left * walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
                }
                if (Input.GetKey(KeyCode.C)) GetComponent<Rigidbody2D>().AddForce(Vector2.down * walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
            }
            if (GetComponent<Rigidbody2D>().velocity.x < 0 && GetComponent<Rigidbody2D>().velocity.x > maxwalkspeed * -1f)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.down * walkForce * Mathf.Sin(angle) * Mathf.Cos(angle));
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * walkForce * Mathf.Sin(angle) * Mathf.Sin(angle));
            }
        }

        //アニメーション
        if (GetComponent<Rigidbody2D>().velocity.x != 0)
        {
            GetComponent<Animator>().SetBool("move", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("move", false);
        }
        if (IsGround)
        {
            GetComponent<Animator>().SetBool("jump", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("jump", true);
        }
    }
}
