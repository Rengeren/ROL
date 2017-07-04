using UnityEngine;
using System.Collections;

public class MageSkills : MonoBehaviour {
    private bool Delayend = true;
    private CharacterStatus Status;
    private LadderforPlayer Ladder;
    private SkillDetail Skill;
    Vector2 scale;
    //通常攻撃(X)
    [HideInInspector]
    public GameObject MageX;
    [HideInInspector]
    public GameObject MageX_hit;
    [HideInInspector]
    public AudioClip X_SE;
    [HideInInspector]
    public AudioClip X_HitSE;
    //アイスニードル(Shift)
    [HideInInspector]
    public GameObject MageShift;
    [HideInInspector]
    public GameObject MageShift_hit;
    [HideInInspector]
    public AudioClip Shift_SE;
    [HideInInspector]
    public AudioClip Shift_HitSE;
    [HideInInspector]
    public float Shiftct = 6;
    bool Shift_cooltimeend = true;
    //ヒーリングオーブ(Z)
    [HideInInspector]
    public GameObject MageZ;
    [HideInInspector]
    public GameObject MageZ_hit;
    [HideInInspector]
    public AudioClip Z_SE;
    [HideInInspector]
    public AudioClip Z_HitSE;
    [HideInInspector]
    public float Zct = 12;
    bool Z_cooltimeend = true;
    //アイスオーブ(Ctrl)
    [HideInInspector]
    public GameObject MageCtrl;
    [HideInInspector]
    public GameObject MageCtrl_hit;
    [HideInInspector]
    public AudioClip Ctrl_SE;
    [HideInInspector]
    public AudioClip Ctrl_HitSE;
    [HideInInspector]
    public float Ctrlct = 10;
    bool Ctrl_cooltimeend = true;
    //デスメテオ(Space)
    [HideInInspector]
    public GameObject MageSpace;
    [HideInInspector]
    public GameObject MageSpace_hit;
    [HideInInspector]
    public AudioClip Space_SE;
    [HideInInspector]
    public AudioClip Space_HitSE;
    [HideInInspector]
    public float Spacect = 60;
    bool Space_cooltimeend = true;

    void Start ()
    {
        Status = GetComponent<CharacterStatus>();
        Ladder = GetComponent<LadderforPlayer>();
    }
    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.6f);
        Status.canmove = true;
        Delayend = true;
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

    void Update () {
            //通常攻撃(X)
            if (Input.GetKey(KeyCode.X) && Delayend && Ladder.Skilltime)
            {
                GetComponent<AudioSource>().PlayOneShot(X_SE);
                GameObject Attack = Instantiate(MageX, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
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
                Skill.HitEffect = MageX_hit;
                Skill.HitSE = X_HitSE;
                Skill.skillpercentage = 1;
                Skill.Hitlimit = 1;
                Delayend = false;
                StartCoroutine("Delay");
            }
        //アイスニードル(shift)
        if (Input.GetKey(KeyCode.LeftShift) && Delayend && Shift_cooltimeend && Ladder.Skilltime || Input.GetKey(KeyCode.RightShift) && Delayend && Shift_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Shift_SE);
            GameObject Attack = Instantiate(MageShift, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            SkillDetail Skill = Attack.GetComponent<SkillDetail>();
            if (transform.localScale.x == 1)
            {
                scale = Attack.transform.localScale;
                scale.x = 1;
                Attack.transform.localScale = scale;
                Skill.IceForce = -6000;
            }
            else
            {
                scale = Attack.transform.localScale;
                scale.x = -1;
                Attack.transform.localScale = scale;
                Skill.IceForce = 6000;
            }
            Status.canmove = false;
            //値渡し
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = MageShift_hit;
            Skill.HitSE = Shift_HitSE;
            Skill.skillpercentage = 2.5f;
            Skill.Hitlimit = 6;
            Delayend = false;

            Shift_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Shift_cooltime");
        }
        //ヒーリングオーブ(z)
        if (Input.GetKey(KeyCode.Z) && Delayend && Z_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Z_SE);
            GameObject Attack = Instantiate(MageZ, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
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
            Skill.HitEffect = MageZ_hit;
            Skill.HitSE = Z_HitSE;
            Skill.skillpercentage = -1;
            Skill.Hitlimit = 1000;
            Skill.MageHeal = true;

            Z_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Z_cooltime");
        }
        //アイスオーブ(Ctrl)
        if (Input.GetKey(KeyCode.LeftControl) && Delayend && Ctrl_cooltimeend && Ladder.Skilltime || Input.GetKey(KeyCode.RightControl) && Delayend && Ctrl_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Ctrl_SE);
            if (transform.localScale.x == 1)
            {
                GameObject Attack = Instantiate(MageCtrl, new Vector3(transform.position.x - 20, transform.position.y + 30, -20), Quaternion.identity) as GameObject;
                Skill = Attack.GetComponent<SkillDetail>();
                scale = Attack.transform.localScale;
                scale.x = 1;
                Attack.transform.localScale = scale;
                Attack.GetComponent<MageCtrl>().distance = -200;
            }
            else
            {
                GameObject Attack = Instantiate(MageCtrl, new Vector3(transform.position.x + 20, transform.position.y + 30, -20), Quaternion.identity) as GameObject;
                Skill = Attack.GetComponent<SkillDetail>();
                scale = Attack.transform.localScale;
                scale.x = -1;
                Attack.transform.localScale = scale;
                Attack.GetComponent<MageCtrl>().distance = 200;
            }
            Status.canmove = false;
            //値渡し
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = MageCtrl_hit;
            Skill.HitSE = Ctrl_HitSE;
            Skill.skillpercentage = 0.5f;
            Skill.Hitlimit = 1000;
            Delayend = false;

            Ctrl_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Ctrl_cooltime");
        }
        //デスメテオ(Space)
        if (Input.GetKey(KeyCode.Space) && Delayend && Space_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Space_SE);
            GameObject Attack = Instantiate(MageSpace, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            Skill = Attack.GetComponent<SkillDetail>();
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
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = MageSpace_hit;
            Skill.HitSE = Space_HitSE;
            if (Status.Level <= 10) Skill.skillpercentage = 6f;
            else Skill.skillpercentage = 8.5f;
            Skill.Hitlimit = 6;
            Delayend = false;

            Space_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Space_cooltime");
        }
    }
}
