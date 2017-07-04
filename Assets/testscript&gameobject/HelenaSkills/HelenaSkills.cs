using UnityEngine;
using System.Collections;

public class HelenaSkills : MonoBehaviour {
    [HideInInspector]
    public bool Delayend = true;
    private CharacterStatus Status;
    private LadderforPlayer Ladder;
    Vector2 scale;
    bool IsGround;
    //ピアシングアロー(X)
    [HideInInspector]
    public GameObject TargetMark;
    [HideInInspector]
    public GameObject HelenaX;
    [HideInInspector]
    public GameObject XEff;
    [HideInInspector]
    public GameObject X_hit;
    [HideInInspector]
    public AudioClip X_SE;
    [HideInInspector]
    public AudioClip X_HitSE;
    //ウィンドスピリット(Shift)
    [HideInInspector]
    public GameObject HelenaShift;
    [HideInInspector]
    public GameObject ShiftEff;
    [HideInInspector]
    public AudioClip Shift_SE;
    [HideInInspector]
    public bool HelenaBuff;
    [HideInInspector]
    public float Shiftct = 8;
    bool Shift_cooltimeend = true;
    //スタップショット(Z)
    [HideInInspector]
    public GameObject fire;
    [HideInInspector]
    public GameObject Z_hit;
    [HideInInspector]
    public AudioClip Z_SE;
    [HideInInspector]
    public AudioClip Z_HitSE;
    [HideInInspector]
    public float Zct = 10;
    bool Z_cooltimeend = true;
    float movepower = 650000; //移動パワー
    float movepower2 = 400000;
    float movepower3 = 12000;
    [HideInInspector]
    public float efdelay = 0.36f;
    [HideInInspector]
    public bool afteruse_l, afteruse_r, afend;
    [HideInInspector]
    public bool useZ;
    [HideInInspector]
    public bool endZ;
    [HideInInspector]
    public float useZtime;
    [HideInInspector]
    public float jumptime;
    [HideInInspector]
    public float jumppower;
    //アイスアロー(Ctrl)
    int i;
    [HideInInspector]
    public GameObject Ice;
    [HideInInspector]
    public GameObject HelenaCtrl;
    [HideInInspector]
    public GameObject CtrlEff;
    [HideInInspector]
    public GameObject Ctrl_hit;
    [HideInInspector]
    public AudioClip Ctrl_SE;
    [HideInInspector]
    public AudioClip Ctrl_HitSE;
    [HideInInspector]
    public float Ctrlct = 10;
    bool Ctrl_cooltimeend = true;
    //集中射撃(Space)
    [HideInInspector]
    public GameObject HelenaSpace;
    [HideInInspector]
    public GameObject SpaceEff;
    [HideInInspector]
    public GameObject Space_hit;
    [HideInInspector]
    public AudioClip Space_SE;
    [HideInInspector]
    public AudioClip Space_SE2;
    [HideInInspector]
    public float Spacect = 45;
    bool Space_cooltimeend = true;

    void Start()
    {
        Status = GetComponent<CharacterStatus>();
        Ladder = GetComponent<LadderforPlayer>();
    }
    //Delay
    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        Status.canmove = true;
        Delayend = true;
    }
    public IEnumerator ShiftDelay()
    {
        yield return new WaitForSeconds(0.3f);
        Delayend = true;
    }
    public IEnumerator ZDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Delayend = false;
        Status.canmove = false;
        yield return new WaitForSeconds(0.2f);
        Delayend = true;
        Status.canmove = true;
    }
    //CT
    public IEnumerator Shift_cooltime()
    {
        Status.ShiftCT = true; //Bar用
        yield return new WaitForSeconds(Shiftct);
        Shift_cooltimeend = true;
    }
    public IEnumerator Z_cooltime()
    {
        Status.ZCT = true; //Bar用
        yield return new WaitForSeconds(Zct);
        Z_cooltimeend = true;
        afteruse_l = false;
        afteruse_r = false;
    }
    public IEnumerator Ctrl_cooltime()
    {
        Status.CtrlCT = true; //Bar用
        yield return new WaitForSeconds(Ctrlct);
        Ctrl_cooltimeend = true;
    }
    public IEnumerator Space_cooltime()
    {
        Status.SpaceCT = true; //Bar用
        yield return new WaitForSeconds(Spacect);
        Space_cooltimeend = true;
    }
    //Z用
    public IEnumerator firecreate()
    {
        yield return new WaitForSeconds(efdelay);
        //GetComponent<Animator>().SetTrigger("end Z");
        yield return new WaitForSeconds(0.24f);
        endZ = true;
    }
    public IEnumerator Zpower()
    {
        yield return new WaitForSeconds(0.7f);
        afend = true;
    }

    void Update()
    {
        IsGround = GetComponent<move>().IsGround;
        //ピアシングアロー(X)
        if (Input.GetKey(KeyCode.X) && Delayend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(X_SE);
            GameObject Attack = Instantiate(HelenaX, new Vector3(transform.position.x, transform.position.y + 30, -20), Quaternion.identity) as GameObject;
            GameObject Eff = Instantiate(XEff, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            if (transform.localScale.x == 1)
            {
                scale = Attack.transform.localScale;
                scale.x = 1;
                Attack.transform.localScale = scale;
                Eff.transform.localScale = scale;
                X_hit.transform.localScale = scale;
                Attack.GetComponent<HelenaX>().distance = -280;
            }
            else
            {
                scale = Attack.transform.localScale;
                scale.x = -1;
                Attack.transform.localScale = scale;
                Eff.transform.localScale = scale;
                X_hit.transform.localScale = scale;
                Attack.GetComponent<HelenaX>().distance = 280;
            }
            Status.canmove = false;
            //値渡し
            SkillDetail Skill = Attack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.PlayerNumber = Status.PlayerNumber;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.skillpercentage = 1;
            Skill.HitEffect = X_hit;
            Skill.HitSE = X_HitSE;
            Skill.Hitlimit = 2;
            Skill.debuffType = 4;
            Skill.PlaceCorrection = 40;
            Skill.debuff = TargetMark;
            Skill.HelenaX = true;
            Delayend = false;

            StartCoroutine("Delay");
        }
        //ウィンドスピリット(Shift)
        if (Input.GetKey(KeyCode.LeftShift) && Delayend && Shift_cooltimeend && Ladder.Skilltime || Input.GetKey(KeyCode.RightShift) && Delayend && Shift_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Shift_SE);
            GameObject Attack = Instantiate(HelenaShift, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            GameObject Eff = Instantiate(ShiftEff, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            if (transform.localScale.x == 1)
            {
                scale = Attack.transform.localScale;
                scale.x = 1;
                Attack.transform.localScale = scale;
                Eff.transform.localScale = scale;
            }
            else
            {
                scale = Attack.transform.localScale;
                scale.x = -1;
                Attack.transform.localScale = scale;
                Eff.transform.localScale = scale;
            }
            Attack.GetComponent<HelenaShift>().player = gameObject;
            Attack.GetComponent<horming>().player = gameObject;
            ShiftEff.GetComponent<horming>().player = gameObject;
            GetComponent<CharacterStatus>().WalkSpeedCorrection += 0.2f;
            GetComponent<CharacterStatus>().SpeedCal();
            HelenaBuff = true;
            Delayend = false;
            Shift_cooltimeend = false;

            StartCoroutine("ShiftDelay");
            StartCoroutine("Shift_cooltime");
        }
        //アイスアロー(Ctrl)
        if (Input.GetKey(KeyCode.LeftControl) && Delayend&&Ctrl_cooltimeend && Ladder.Skilltime || Input.GetKey(KeyCode.RightControl) && Delayend && Ctrl_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Ctrl_SE);
            for (i= -30; i <= 30; i += 15)
            {
                GameObject Attack = Instantiate(HelenaCtrl, new Vector3(transform.position.x, transform.position.y+20, -20), Quaternion.identity) as GameObject;
                Attack.GetComponent<HelenaCtrl>().angle = i;
                Attack.GetComponent<Renderer>().material.color -= new Color(0, 0, 0, 1);
                if (transform.localScale.x == 1)
                {
                    Attack.transform.position += new Vector3(-20,0,0);
                    scale = Attack.transform.localScale;
                    scale.x = 1;
                    Attack.transform.localScale = scale;
                    X_hit.transform.localScale = scale;
                }
                else
                {
                    Attack.transform.position += new Vector3(20, 0, 0);
                    scale = Attack.transform.localScale;
                    scale.x = -1;
                    Attack.transform.localScale = scale;
                    X_hit.transform.localScale = scale;
                }
                //値渡し
                SkillDetail Skill = Attack.GetComponent<SkillDetail>();
                Skill.WhoseSkill = gameObject;
                Skill.level = Status.Level;
                Skill.PlayerNumber = Status.PlayerNumber;
                Skill.MinATK = Status.minATK;
                Skill.MaxATK = Status.maxATK;
                Skill.criticalpercentage = Status.criticalpercentage;
                //スキルの固有値
                Skill.skillpercentage = 0.7f;
                Skill.HitEffect = Ctrl_hit;
                Skill.HitSE = Ctrl_HitSE;
                Skill.Hitlimit = 1;
                Skill.debuffType = 5;
                Skill.debuff = Ice;
            }
            GameObject Eff = Instantiate(CtrlEff, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            if (transform.localScale.x == 1)
            {
                scale = Eff.transform.localScale;
                scale.x = 1;
                Eff.transform.localScale = scale;
            }
            else
            {
                scale = Eff.transform.localScale;
                scale.x = -1;
                Eff.transform.localScale = scale;
            }
            Status.canmove = false;
            Delayend = false;
            Ctrl_cooltimeend = false;

            StartCoroutine("Delay");
            StartCoroutine("Ctrl_cooltime");
        }
        //集中射撃
        if (Input.GetKeyDown(KeyCode.Space) && Delayend && Space_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Space_SE);
            GetComponent<AudioSource>().PlayOneShot(Space_SE2);
            GameObject Attack = Instantiate(HelenaSpace, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            GameObject Eff = Instantiate(SpaceEff, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            Attack.GetComponent<HelenaSpace>().Attack = Attack;
            Attack.GetComponent<HelenaSpace>().Eff = Eff;
            Eff.GetComponent<horming>().player = gameObject;
            if (transform.localScale.x == 1)
            {
                scale = Attack.transform.localScale;
                scale.x = -1;
                Attack.transform.localScale = scale;
                Eff.transform.localScale = scale;
            }
            else
            {
                scale = Attack.transform.localScale;
                scale.x = 1;
                Attack.transform.localScale = scale;
                Eff.transform.localScale = scale;
            }
            Status.canmove = false;
            //値渡し
            SkillDetail Skill = Attack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.PlayerNumber = Status.PlayerNumber;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            if (Status.Level <= 10) Skill.skillpercentage = 0.7f;
            else Skill.skillpercentage = 1.3f;
            Skill.HitEffect = Space_hit;
            Skill.Hitlimit = 1000;
            Delayend = false;
            Space_cooltimeend = false;

            StartCoroutine("Space_cooltime");
        }
    }
    void FixedUpdate()
    {
        //スタップショット(Z)
        if (Input.GetKey(KeyCode.Z) && Delayend&& Z_cooltimeend&&IsGround && Ladder.Skilltime)
        {
            //GetComponent<Animator>().SetTrigger("start Z");
            GetComponent<AudioSource>().PlayOneShot(X_SE);
            GameObject Attack = Instantiate(fire, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            useZ = true;
            useZtime = Time.time;
            StartCoroutine("firecreate");
            StartCoroutine("Zpower");
            if (transform.localScale.x == 1)
            {
                scale = Attack.transform.localScale;
                scale.x = 1;
                Attack.transform.localScale = scale;
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * movepower * Time.deltaTime);
                afteruse_r = true;
            }
            else
            {
                scale = Attack.transform.localScale;
                scale.x = -1;
                Attack.transform.localScale = scale;
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * movepower * Time.deltaTime);
                afteruse_l = true;
            }
            //値渡し
            SkillDetail Skill = Attack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.PlayerNumber = Status.PlayerNumber;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.skillpercentage = 2;
            Skill.HitEffect = Z_hit;
            Skill.HitSE = Z_HitSE;
            Skill.Hitlimit = 1000;
            Z_cooltimeend = false;

            StartCoroutine("ZDelay");
            StartCoroutine("Z_cooltime");

            if (afteruse_l == true && GetComponent<move>().IsGround == false)
            {
                jumptime = Time.time - useZtime;
                if (jumptime < 0.14f)
                {
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
            else if (afteruse_r == true && GetComponent<move>().IsGround == false)
            {
                jumptime = Time.time - useZtime;
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
            else if (afend == true && GetComponent<move>().IsGround == true)
            {
                afteruse_l = false;
                afteruse_r = false;
                afend = false;
            }
        }
    }
}
