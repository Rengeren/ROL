using UnityEngine;
using System.Collections;

public class Manji_Ctrl : MonoBehaviour
{
    public GameObject player;
    public LayerMask GroundLayer;
    public bool Manji = true;  //職業フラグ
    public float movepower = 3000f; //移動パワー
    public float ct = 1f; //クールタイムの秒数
    public float efdelay = 0.36f; //エフェクトのディレイ
    public bool cooltimeend;
    public bool IsGround = false;
    public bool afteruse_l, afteruse_r, afend;
    public static bool useZ;
    public static bool endZ;
    public static float useZtime;
    public static float jumptime;
    public float jumppower;
    private float Arrow;
    public GameObject fireprefab;

    void Start()
    {
        cooltimeend = true;
    }
    public IEnumerator firecreate()
    {
        GameObject fire = Instantiate(fireprefab, new Vector3(transform.position.x, transform.position.y - 0.5f, -21), Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(efdelay);
        Destroy(fire);
       // this.GetComponent<Animator>().SetTrigger("end Z");
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
    public IEnumerator StopCtrl()
    {
        yield return new WaitForSeconds(0.01f*Time.deltaTime);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

    }
    // Update is called once per frame
    void Update()
    {
        IsGround = Physics2D.Linecast(
   transform.position + transform.up * 1.0f, transform.position - transform.up * 0.1f, GroundLayer);

        Arrow = transform.localScale.x;


    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.LeftControl)  && cooltimeend)
        {
         //   this.GetComponent<Animator>().SetTrigger("start Z");
            useZ = true;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * movepower, ForceMode2D.Impulse);
            StartCoroutine("StopCtrl");
            afteruse_r = true;
            useZtime = Time.time;
            //   StartCoroutine("firecreate");
            cooltimeend = false;
            StartCoroutine("cooltime");
            //    StartCoroutine("Zpower");
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.LeftControl) && Arrow < 0  && cooltimeend)
        {
         //   this.GetComponent<Animator>().SetTrigger("start Z");
            useZ = true;
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * movepower,ForceMode2D.Impulse);
            StartCoroutine("StopCtrl");
            afteruse_l = true;
            useZtime = Time.time;
          //  StartCoroutine("firecreate");
            cooltimeend = false;
            StartCoroutine("cooltime");
           
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.LeftControl) && Arrow > 0  && cooltimeend)
        {
         //   this.GetComponent<Animator>().SetTrigger("start Z");
            useZ = true;
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * movepower, ForceMode2D.Impulse);
            StartCoroutine("StopCtrl");
            afteruse_r = true;
            useZtime = Time.time;
         //   StartCoroutine("firecreate");
            cooltimeend = false;
            StartCoroutine("cooltime");
        //    StartCoroutine("Zpower");
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.LeftControl) && Arrow > 0 && cooltimeend)
        {
         //   this.GetComponent<Animator>().SetTrigger("start Z");
            useZ = true;
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * movepower, ForceMode2D.Impulse);
            StartCoroutine("StopCtrl");
            afteruse_r = true;
            useZtime = Time.time;
            //   StartCoroutine("firecreate");
            cooltimeend = false;
            StartCoroutine("cooltime");
            //    StartCoroutine("Zpower");
        }


    }

}
