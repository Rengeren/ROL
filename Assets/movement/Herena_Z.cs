using UnityEngine;
using System.Collections;

public class Herena_Z : MonoBehaviour {
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public float movepower = 700000; //移動パワー
    [HideInInspector]
    public float movepower2 = 400000;
    [HideInInspector]
    public float movepower3 = 12000;
    [HideInInspector]
    public float ct = 1f; //クールタイムの秒数
    [HideInInspector]
    public float efdelay = 0.36f; //エフェクトのディレイ
    [HideInInspector]
    public bool cooltimeend;
    [HideInInspector]
    public bool IsGround = false;
    [HideInInspector]
    public bool afteruse_l,afteruse_r,afend;
    [HideInInspector]
    public bool useZ;
    [HideInInspector]
    public bool endZ;
    public float useZtime;
    public float jumptime;
    public float jumppower;
    private float Arrow;
    public GameObject fireprefab;

    void Start () {
        cooltimeend = true;
        player = gameObject;
	}
    public IEnumerator firecreate()
    {
        GameObject fire = Instantiate(fireprefab, new Vector3(transform.position.x, transform.position.y - 0.5f, -21), Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(efdelay);
        Destroy(fire);
        GetComponent<Animator>().SetTrigger("end Z");
        yield return new WaitForSeconds(0.24f);
        endZ = true;
    }
    public IEnumerator cooltime()
    {
        yield return new WaitForSeconds(ct);
        cooltimeend = true;
        afteruse_l = false;
        afteruse_r = false;
    }
    public IEnumerator Zpower()
    {
        yield return new WaitForSeconds(0.7f);
        afend = true;
    }

    void Update () {
        IsGround = GetComponent<jump>().IsGround;
        Arrow = transform.localScale.x;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Arrow < 0 && IsGround == true && cooltimeend)
        {
            GetComponent<Animator>().SetTrigger("start Z");
            useZ = true;
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * movepower * Time.deltaTime);
            afteruse_l = true;
            useZtime = Time.time;
            StartCoroutine("firecreate");
            cooltimeend = false;
            StartCoroutine("cooltime");
            StartCoroutine("Zpower");
        }
        else if (Input.GetKeyDown(KeyCode.Z) && Arrow > 0 && IsGround == true && cooltimeend)
        {
            this.GetComponent<Animator>().SetTrigger("start Z");
            useZ = true;
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * movepower*Time.deltaTime);
            afteruse_r = true;
            useZtime = Time.time;
            StartCoroutine("firecreate");
            cooltimeend = false;
            StartCoroutine("cooltime");
            StartCoroutine("Zpower");
        }
        if(afteruse_l == true && IsGround == false)
        {
            jumptime = Time.time - useZtime;
            Debug.Log(jumptime);
            if (jumptime < 0.14f)
            {
                //jumppower = 0.7f / jumptime;
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * movepower2 * Time.deltaTime);
            }
            else if (jumptime >= 0.14f && jumptime < 0.7f)
            {
                jumppower = 0.7f / jumptime;
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * movepower3 * jumppower * Time.deltaTime);
            }
            afteruse_l = false;
            afteruse_r = false;
            afend = false;
        }
        else if (afteruse_r == true && IsGround == false)
        {
            jumptime = Time.time - useZtime;
            Debug.Log(jumptime);
            if (jumptime < 0.14f)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * movepower2 * Time.deltaTime);
            }
            else if (jumptime >= 0.14f && jumptime < 0.7f)
            {
                jumppower = 0.7f / jumptime;
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * movepower3 * jumppower * Time.deltaTime);
            }
            afteruse_l = false;
            afteruse_r = false;
            afend = false;
        }
        else if (afend==true && IsGround == true)
        {
            afteruse_l = false;
                afteruse_r = false;
            afend = false;
            
        }

    }

}
