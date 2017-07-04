using UnityEngine;
using System.Collections;

public class BukoSkills : MonoBehaviour {
    private GameObject Ground;
    private bool Delayend = true;
    private CharacterStatus Status;
    private LadderforPlayer Ladder;
    Vector2 scale;
    GameObject CharaHead;
    [HideInInspector]
    public GameObject SlowMark;
    [HideInInspector]
    public bool Ztime=false;
    //通常攻撃(X)
    int pattern;
    private GameObject Attack;
    [HideInInspector]
    public GameObject BukoX1;
    [HideInInspector]
    public GameObject BukoX2;
    [HideInInspector]
    public GameObject BukoX_hit;
    [HideInInspector]
    public AudioClip X_SE;
    [HideInInspector]
    public AudioClip X_HitSE;
    //万斤槌(Shift)
    [HideInInspector]
    public GameObject BukoShift;
    [HideInInspector]
    public GameObject BukoShift_hit;
    [HideInInspector]
    public AudioClip Shift_SE;
    [HideInInspector]
    public AudioClip Shift_HitSE;
    [HideInInspector]
    public float Shiftct = 5;
    bool Shift_cooltimeend = true;
    //旋風脚(Z)
    [HideInInspector]
    public GameObject BukoZ;
    [HideInInspector]
    public GameObject BukoZ_hit;
    [HideInInspector]
    public AudioClip Z_HitSE;
    [HideInInspector]
    public float Zct = 10;
    bool Z_cooltimeend = true;
    //半月蹴り(Ctrl)
    [HideInInspector]
    public GameObject BukoCtrl;
    [HideInInspector]
    public GameObject BukoCtrlEff;
    [HideInInspector]
    public GameObject BukoCtrl_hit;
    [HideInInspector]
    public AudioClip Ctrl_SE;
    [HideInInspector]
    public AudioClip Ctrl_HitSE;
    [HideInInspector]
    public float Ctrlct = 7;
    bool Ctrl_cooltimeend = true;
    bool Second=false;
    //パンダ出撃(Space)
    [HideInInspector]
    public GameObject BukoSpace;
    [HideInInspector]
    public AudioClip SpaceStart_SE1;
    [HideInInspector]
    public AudioClip SpaceStart_SE2;
    [HideInInspector]
    public AudioClip Space_HitSE;
    [HideInInspector]
    public float Spacect = 50;
    bool Space_cooltimeend = true;

    void Start()
    {
        Ground = transform.FindChild("Ground").gameObject;
        Status = GetComponent<CharacterStatus>();
        CharaHead = GetComponent<LadderforPlayer>().CharaHead;
        Ladder = GetComponent<LadderforPlayer>();
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        Status.canmove = true;
        Delayend = true;
    }
    public IEnumerator NormalDelay()
    {
        yield return new WaitForSeconds(0.45f);
        Status.canmove = true;
        Delayend = true;
    }
    public IEnumerator CtrlDelay()
    {
        yield return new WaitForSeconds(0.3f);
        Status.canmove = true;
        Delayend = true;
        StartCoroutine("SecondCtrl");
    }
    public IEnumerator SecondCtrl()
    {
        yield return new WaitForSeconds(0.5f);
        if (Second)
        {
            Second = false;
            StartCoroutine("Ctrl_cooltime");
        }
    }
    public IEnumerator ForZ(GameObject Z)
    {
        yield return new WaitForSeconds(4);
        Destroy(Z);
        Ztime = false;
        GetComponent<LadderforPlayer>().CanWork = true;
        GetComponent<move>().canwarp = true;
        Status.WalkSpeedCorrection -= 0.2f;
        Status.SpeedCal();
    }
    public IEnumerator ForSpace()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<AudioSource>().PlayOneShot(SpaceStart_SE2);
    }
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
        Ctrl_cooltimeend = true;
    }

    void Update()
    {
        //通常攻撃(X)
        if (Input.GetKey(KeyCode.X) && Delayend&&Ztime!=true&& Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(X_SE);
            pattern = Random.Range(0, 2);
            if (pattern == 0)
            {
                Attack = Instantiate(BukoX1, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            }
            if (pattern == 1)
            {
                Attack = Instantiate(BukoX2, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            }
            if (transform.localScale.x == 1)
            {
                scale = Attack.transform.localScale;
                scale.x = 1;
                Attack.transform.localScale = scale;
            }
            else
            {
                scale = Attack.transform.localScale;
                scale.x = -1;
                Attack.transform.localScale = scale;
            }
            Status.canmove = false;
            //値渡し
            SkillDetail Skill = Attack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = BukoX_hit;
            Skill.HitSE = X_HitSE;
            Skill.skillpercentage = 1;
            Skill.Hitlimit = 3;
            Delayend = false;
            StartCoroutine("NormalDelay");
        }
        //万斤槌(shift)
        if (Input.GetKey(KeyCode.LeftShift) && Delayend && Shift_cooltimeend&&Ztime!= true&& Ladder.Skilltime || Input.GetKey(KeyCode.RightShift) && Delayend && Shift_cooltimeend&&Ztime!=true && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Shift_SE);
            
            GameObject Attack = Instantiate(BukoShift, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            if (transform.localScale.x == 1)
            {
                scale = Attack.transform.localScale;
                scale.x = 1;
                Attack.transform.localScale = scale;
            }
            else
            {
                scale = Attack.transform.localScale;
                scale.x = -1;
                Attack.transform.localScale = scale;
            }
            Status.canmove = false;
            //値渡し
            SkillDetail Skill = Attack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = BukoShift_hit;
            Skill.HitSE = Shift_HitSE;
            Skill.skillpercentage = 2.5f;
            Skill.Hitlimit = 3;
            Skill.debuffType = 2;
            Delayend = false;

            Shift_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Shift_cooltime");
        }
        //旋風脚(Z)
        if (Input.GetKey(KeyCode.Z) && Delayend&&Z_cooltimeend && Ladder.Skilltime)
        {
            Attack = Instantiate(BukoZ, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            
            if (transform.localScale.x == 1)
            {
                scale = Attack.transform.localScale;
                scale.x = 1;
                Attack.transform.localScale = scale;
            }
            else
            {
                scale = Attack.transform.localScale;
                scale.x = -1;
                Attack.transform.localScale = scale;
            }
            GetComponent<LadderforPlayer>().CanWork = false;
            GetComponent<move>().canwarp = false;
            Status.WalkSpeedCorrection += 0.2f;
            Status.SpeedCal();
            Attack.GetComponent<horming>().player = gameObject;
            Ztime = true;
            //値渡し
            SkillDetail Skill = Attack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = BukoZ_hit;
            Skill.HitSE = Z_HitSE;
            Skill.skillpercentage = 1.2f;
            Skill.Hitlimit = 1000;

            Z_cooltimeend = false;
            StartCoroutine("Z_cooltime");
            StartCoroutine("ForZ",Attack);
        }
        //半月蹴り一の段(Ctrl)
        if (Input.GetKeyDown(KeyCode.LeftControl) && Delayend && Ctrl_cooltimeend && Ztime != true && Ladder.Skilltime || Input.GetKeyDown(KeyCode.RightControl) && Delayend && Ctrl_cooltimeend && Ztime != true && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Ctrl_SE);
            GameObject Attack = Instantiate(BukoCtrl, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            GameObject Eff=Instantiate(BukoCtrlEff, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity)as GameObject;
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
            Status.canmove = false;
            Second = true;
            //値渡し
            SkillDetail Skill = Attack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = BukoCtrl_hit;
            Skill.HitSE = Ctrl_HitSE;
            Skill.skillpercentage = 2.5f;
            Skill.Hitlimit = 1;
            Delayend = false;

            Ctrl_cooltimeend = false;
            StartCoroutine("CtrlDelay");
        }
        //半月蹴り二の段(Ctrl)
        if (Input.GetKeyDown(KeyCode.LeftControl) && Delayend && Ztime != true&& Second && Ladder.Skilltime || Input.GetKeyDown(KeyCode.RightControl) && Delayend && Ztime != true&&Second && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Ctrl_SE);
            if (GetComponent<LadderforPlayer>().Skilltime)
            {
                GetComponent<Rigidbody2D>().isKinematic = false;
                GetComponent<LadderforPlayer>().onladder = false;
                GetComponent<LadderforPlayer>().Skilltime = false;
                GetComponent<Animator>().SetBool("OnRope", false);
                CharaHead.GetComponent<Animator>().SetBool("OnRope", false);
            }
            GameObject Attack = Instantiate(BukoCtrl, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            GameObject Eff = Instantiate(BukoCtrlEff, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            if (transform.localScale.x == 1)
            {
                scale = Attack.transform.localScale;
                scale.x = 1;
                Attack.transform.localScale = scale;
                Eff.transform.localScale = scale;
                if (GetComponent<Rigidbody2D>().velocity.x>-100) GetComponent<Rigidbody2D>().velocity=Vector2.left * 100;
            }
            else
            {
                scale = Attack.transform.localScale;
                scale.x = -1;
                Attack.transform.localScale = scale;
                Eff.transform.localScale = scale;
                if (GetComponent<Rigidbody2D>().velocity.x < 100) GetComponent<Rigidbody2D>().velocity = Vector2.right * 100;
            }
            Status.canmove = false;
            Second = false;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7000);
            Ground.layer = LayerMask.NameToLayer("OffGround");
            //値渡し
            SkillDetail Skill = Attack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = BukoCtrl_hit;
            Skill.HitSE = Ctrl_HitSE;
            Skill.skillpercentage = 2.5f;
            Skill.Hitlimit = 1;
            Skill.debuffType = 3;
            //Skill.debuff = SlowMark;
            Delayend = false;

            Ctrl_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Ctrl_cooltime");
        }
        //パンダ出撃(Space)
        if (Input.GetKey(KeyCode.Space) && Delayend && Space_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(SpaceStart_SE1);
            StartCoroutine("ForSpace");
            Attack = Instantiate(BukoSpace, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            if (transform.localScale.x == 1)
            {
                scale = Attack.transform.localScale;
                scale.x = 1;
                Attack.transform.localScale = scale;
            }
            else
            {
                scale = Attack.transform.localScale;
                scale.x = -1;
                Attack.transform.localScale = scale;
            }
            Attack.GetComponent<horming>().player = gameObject;
            //値渡し
            SkillDetail Skill = Attack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitSE = Space_HitSE;
            if(Status.Level<=10)Skill.skillpercentage = 1.0f;
            else Skill.skillpercentage = 1.5f;
            Skill.Hitlimit = 1000;
            Delayend = false;

            Space_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Space_cooltime");
        }
    }

}
