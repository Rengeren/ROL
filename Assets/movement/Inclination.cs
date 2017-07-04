using UnityEngine;
using System.Collections;

public class Inclination : MonoBehaviour {
    public static float angle=38.7f;
    private float walkForce;
    private float gravity=0.1f*180*9.81f;
    private float maxwalkspeed;
    [HideInInspector]
    public bool onslope;
    [HideInInspector]
    public bool which;   //true=Left,false=Right
    private CharacterStatus Status;

    void Awake()
    {
        angle *= Mathf.Deg2Rad;
    }
    void Start()
    {
        Status = GetComponent<CharacterStatus>();
    }
    //坂接地中
    void OnCollisionStay2D(Collision2D other)
    {
        maxwalkspeed=GetComponent<move>().maxwalkspeed;
        if (other.gameObject.tag == "Leftslope")
        {
            GetComponent<jump>().LayDistance = 10.0f;
            walkForce = GetComponent<move>().walkForce;
            onslope = true;
            which = true;
        }
        if (other.gameObject.tag == "Rightslope")
        {
            GetComponent<jump>().LayDistance = 10.0f;
            walkForce = GetComponent<move>().walkForce;
            onslope = true;
            which = false;
        }
    }
    //坂と離れた時
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Leftslope"|| other.gameObject.tag == "Rightslope")
        {
            GetComponent<jump>().LayDistance = 0.1f;
            onslope = false;
        }
    }

    //坂での動き
    void FixedUpdate()
    {
        //Leftslope
        if (onslope && which && GetComponent<jump>().IsGround)
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
                    GetComponent<Rigidbody2D>().AddForce(Vector2.down * walkForce * Mathf.Sin(angle)*Mathf.Cos(angle));
                    GetComponent<Rigidbody2D>().AddForce(Vector2.left * walkForce*2 * Mathf.Sin(angle)* Mathf.Sin(angle));
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow) && Status.canmove)
            {
                if (GetComponent<Rigidbody2D>().velocity.x > maxwalkspeed * -1f)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * walkForce * Mathf.Sin(angle)*Mathf.Cos(angle));
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * walkForce* Mathf.Sin(angle)* Mathf.Sin(angle));
                }
            }
        }

        //Rightslope
        if (onslope &&which != true && GetComponent<jump>().IsGround)
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
    }
}
