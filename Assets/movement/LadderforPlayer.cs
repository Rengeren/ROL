using UnityEngine;
using System.Collections;

public class LadderforPlayer : MonoBehaviour
{
    private GameObject Ground;
    public GameObject CharaHead; //アニメ用キャラ頭
    private CharacterStatus Status;
    private float center;    //梯子の中心のx座標
    private float upperY;		//梯子のx軸の上端
    private float lowerY;        //梯子のx軸の下端
    private float xForce=2000;
    private bool Isground = false;
    [HideInInspector]
    public bool onladder = false;
    private int RopeAnime = 0; //ロープアニメーション用
    [HideInInspector]
    public bool CanWork=true;
    [HideInInspector]
    public float MoveSpeed = 150f;
    [HideInInspector]
    public bool Skilltime=true;

    void Start()
    {
        Status = GetComponent<CharacterStatus>();
        Ground = transform.FindChild("Ground").gameObject;
    }

    public IEnumerator delay()
    {
        yield return new WaitForSeconds(0.1f);
        Skilltime = false;
    }

    //梯子判定;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "ladder")
        {
            Vector3 ladderpoint = transform.position;
            ladderpoint.x = other.GetComponent<ladder>().center;
            ladderpoint.z = -20;
            Isground = GetComponent<move>().IsGround;
            upperY = other.GetComponent<ladder>().upperY;
            lowerY = other.GetComponent<ladder>().lowerY;

            //梯子に乗る条件
            if (onladder != true && Input.GetKey(KeyCode.DownArrow) && Isground && transform.position.y >= upperY&&CanWork)
            {
                GetComponent<Rigidbody2D>().isKinematic = true;
                onladder = true;
                GetComponent<Animator>().SetBool("OnRope", true);
                CharaHead.GetComponent<Animator>().SetBool("OnRope", true);
                ladderpoint.y -= 5f;
                transform.position = ladderpoint;
                StartCoroutine("delay");
            }
            if (onladder != true && Input.GetKey(KeyCode.UpArrow) && transform.position.y < upperY - 1f&&CanWork)
            {
                GetComponent<Rigidbody2D>().isKinematic = true;
                onladder = true;
                GetComponent<Animator>().SetBool("OnRope", true);
                CharaHead.GetComponent<Animator>().SetBool("OnRope", true);
                if(transform.position.y < lowerY+10f) ladderpoint.y += 5f;
                transform.position = ladderpoint;
                StartCoroutine("delay");
            }
        }
    }

    //梯子上での動き
    void Update()
    {
        //梯子の両端での処理
        if (onladder && GetComponent<Rigidbody2D>().position.y > upperY)
        {
            onladder = false;
            Skilltime = true;
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Animator>().SetBool("OnRope", false);
            CharaHead.GetComponent<Animator>().SetBool("OnRope", false);
        }
        if (onladder && GetComponent<Rigidbody2D>().position.y < lowerY)
        {
            onladder = false;
            Skilltime = true;
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Animator>().SetBool("OnRope", false);
            CharaHead.GetComponent<Animator>().SetBool("OnRope", false);
        }

        //上下移動
        if (onladder && Input.GetKey(KeyCode.UpArrow)&&Status.canmove)
        {
            Vector3 Qpos = transform.localPosition;
            Qpos.z = -20;
            Qpos.y += (MoveSpeed * Time.deltaTime);
            transform.localPosition = Qpos;
            RopeAnime++;
            GetComponent<Animator>().SetInteger("Rope", RopeAnime % 2);
            CharaHead.GetComponent<Animator>().SetInteger("Rope", RopeAnime % 2);
        }
        else if (onladder && Input.GetKey(KeyCode.DownArrow)&&Status.canmove)
        {
            Vector3 Qpos = transform.localPosition;
            Qpos.y += (-MoveSpeed * Time.deltaTime);
            Qpos.z = -20;
            RopeAnime++;
            GetComponent<Animator>().SetInteger("Rope", RopeAnime % 2);
            CharaHead.GetComponent<Animator>().SetInteger("Rope", RopeAnime % 2);
            transform.localPosition = Qpos;
        }
    }

    void FixedUpdate()
    {
        //梯子ジャンプ
        if (GetComponent<Debuff>().infirmed!=true&&onladder && Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.LeftArrow)&&Status.canmove)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * xForce);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * GetComponent<move>().jumpforce/2);
            onladder = false;
            Skilltime = true;
            GetComponent<Animator>().SetBool("OnRope", false);
            CharaHead.GetComponent<Animator>().SetBool("OnRope", false);
            Ground.layer = LayerMask.NameToLayer("OffGround");
        }
        if (GetComponent<Debuff>().infirmed != true && onladder && Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.RightArrow)&&Status.canmove)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * xForce);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * GetComponent<move>().jumpforce/2);
            onladder = false;
            Skilltime = true;
            GetComponent<Animator>().SetBool("OnRope", false);
            CharaHead.GetComponent<Animator>().SetBool("OnRope", false);
            Ground.layer = LayerMask.NameToLayer("OffGround");
        }
    }
}
