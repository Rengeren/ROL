using UnityEngine;
using System.Collections;

public class DarkLoadSkills : MonoBehaviour {
    public GameObject Head;
    private bool Delayend = true;
    private CharacterStatus Status;
    private LadderforPlayer Ladder;
    Vector2 scale;
    [HideInInspector]
    public GameObject InfirmeMark;
    //手裏剣投げ(X)
    private GameObject xAttack;
    private GameObject xEff;
    int pattern;
    [HideInInspector]
    public GameObject DarkLoadX;
    [HideInInspector]
    public GameObject XEff1;
    [HideInInspector]
    public GameObject XEff2;
    [HideInInspector]
    public GameObject X2Eff1;
    [HideInInspector]
    public GameObject X2Eff2;
    [HideInInspector]
    public GameObject X_hit;
    [HideInInspector]
    public AudioClip X_SE;
    [HideInInspector]
    public AudioClip X_HitSE;
    [HideInInspector]
    public GameObject DarkLoadX2;
    [HideInInspector]
    public GameObject X2_hit;
    [HideInInspector]
    public AudioClip X2_SE;
    [HideInInspector]
    public AudioClip X2_HitSE;
    //コウモリの墓
    [HideInInspector]
    public GameObject DarkLoadZ;
    [HideInInspector]
    public GameObject Z_hit;
    [HideInInspector]
    public AudioClip Z_SE;
    [HideInInspector]
    public AudioClip Z_HitSE;
    [HideInInspector]
    public float Zct = 7;
    bool Z_cooltimeend = true;
    //巨大手裏剣
    [HideInInspector]
    public GameObject DarkLoadCtrl;
    [HideInInspector]
    public GameObject CtrlEff;
    [HideInInspector]
    public GameObject Ctrl_hit;
    [HideInInspector]
    public AudioClip Ctrl_SE;
    [HideInInspector]
    public AudioClip Ctrl_HitSE;
    [HideInInspector]
    public float Ctrlct = 12;
    bool Ctrl_cooltimeend = true;
    //闇の落雷
    [HideInInspector]
    public GameObject DarkLoadSpace_Start;
    [HideInInspector]
    public GameObject DarkLoadSpace;
    [HideInInspector]
    public GameObject Space_hit;
    [HideInInspector]
    public AudioClip SpaceStart_SE;
    [HideInInspector]
    public AudioClip Space_HitSE;
    [HideInInspector]
    public float Spacect = 60;
    bool Space_cooltimeend = true;
    Vector3 PositionTmp;

    void Start()
    {
        Status = GetComponent<CharacterStatus>();
        Ladder = GetComponent<LadderforPlayer>();
    }
    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        Status.canmove = true;
        Delayend = true;
    }
    public IEnumerator SpaceDelay()
    {
        yield return new WaitForSeconds(2);
        GetComponent<Renderer>().material.color += new Color(0, 0, 0, 1);
        Head.GetComponent<Renderer>().material.color += new Color(0, 0, 0, 1);
        gameObject.layer = LayerMask.NameToLayer("Character");
        Status.canmove = true;
        Delayend = true;
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
        Space_cooltimeend = true;
    }

    public IEnumerator CreateSpace()
    {
        yield return new WaitForSeconds(0.7f);
        GameObject Space = Instantiate(DarkLoadSpace, new Vector3(PositionTmp.x, PositionTmp.y, -20), Quaternion.identity) as GameObject;
        if (transform.localScale.x == 1)
        {
            scale = Space.transform.localScale;
            scale.x = 1;
            Space.transform.localScale = scale;
        }
        else
        {
            scale = Space.transform.localScale;
            scale.x = -1;
            Space.transform.localScale = scale;
        }
        //値渡し
        SkillDetail Skill = Space.GetComponent<SkillDetail>();
        Skill.WhoseSkill = gameObject;
        Skill.level = Status.Level;
        Skill.PlayerNumber = Status.PlayerNumber;
        Skill.MinATK = Status.minATK;
        Skill.MaxATK = Status.maxATK;
        Skill.criticalpercentage = Status.criticalpercentage;
        //スキルの固有値
        if (Status.Level < 11) Skill.skillpercentage = 1;
        else
        {
            Skill.skillpercentage = 1.5f;
        }
        Skill.HitEffect = Space_hit;
        Skill.HitSE = Space_HitSE;
        Skill.Hitlimit = 1000;
    }

    void Update()
    {
        //通常攻撃(X)
        if (Input.GetKey(KeyCode.X) && Delayend && Ladder.Skilltime)
        {
            pattern = Random.Range(1, 3);
            if (Status.Level < 3)
            {
                xAttack = Instantiate(DarkLoadX, new Vector3(transform.position.x, transform.position.y+30, -20), Quaternion.identity) as GameObject;
                GetComponent<AudioSource>().PlayOneShot(X_SE);
                if(pattern==1) xEff = Instantiate(XEff1, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
                if (pattern == 2) xEff = Instantiate(XEff2, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            }
            else
            {
                xAttack = Instantiate(DarkLoadX2, new Vector3(transform.position.x, transform.position.y+30, -20), Quaternion.identity) as GameObject;
                GetComponent<AudioSource>().PlayOneShot(X2_SE);
                if (pattern == 1) xEff = Instantiate(X2Eff1, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
                if (pattern == 2) xEff = Instantiate(X2Eff2, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            }
            if (transform.localScale.x == 1)
            {
                scale = xAttack.transform.localScale;
                scale.x = 1;
                xAttack.transform.localScale = scale;
                xEff.transform.localScale = scale;
                xAttack.GetComponent<DarkLoadX>().distance = -280;
            }
            else
            {
                scale = xAttack.transform.localScale;
                scale.x = -1;
                xAttack.transform.localScale = scale;
                xEff.transform.localScale = scale;
                xAttack.GetComponent<DarkLoadX>().distance = 280;
            }
            Status.canmove = false;
            //値渡し
            SkillDetail Skill = xAttack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.PlayerNumber = Status.PlayerNumber;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            if (Status.Level < 3)
            {
                Skill.HitEffect = X_hit;
                Skill.HitSE = X_HitSE;
            }
            else
            {
                Skill.HitEffect = X2_hit;
                Skill.HitSE = X2_HitSE;
                xAttack.GetComponent<DarkLoadX>().PoisonDamage = 20+((Status.Level-3)/2)*10;
                xAttack.GetComponent<DarkLoadX>().poison = true;
            }
            Skill.skillpercentage = 1;
            Skill.Hitlimit = 1;
            Delayend = false;
            StartCoroutine("Delay");
        }

        //コウモリの墓(Z)
        if (Input.GetKey(KeyCode.Z) && Delayend && Z_cooltimeend && Ladder.Skilltime)
        {

            GetComponent<AudioSource>().PlayOneShot(Z_SE);
            GameObject ZAttack = Instantiate(DarkLoadZ, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            if (transform.localScale.x == 1)
            {
                scale = ZAttack.transform.localScale;
                scale.x = 1;
                ZAttack.transform.localScale = scale;
            }
            else
            {
                scale = ZAttack.transform.localScale;
                scale.x = -1;
                ZAttack.transform.localScale = scale;
            }
            Status.canmove = false;
            //値渡し
            SkillDetail Skill = ZAttack.GetComponent<SkillDetail>();
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
            Skill.Hitlimit = 100;
            Skill.debuffType = 1;
            Skill.debuff = InfirmeMark;
            Delayend = false;

            Z_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Z_cooltime");
        }

        //巨大手裏剣(Ctrl)
        if (Input.GetKey(KeyCode.LeftControl) && Delayend && Ctrl_cooltimeend && Ladder.Skilltime || Input.GetKey(KeyCode.RightControl) && Delayend && Ctrl_cooltimeend && Ladder.Skilltime)
        {

            GetComponent<AudioSource>().PlayOneShot(Ctrl_SE);
            GameObject CtrlAttack = Instantiate(DarkLoadCtrl, new Vector3(transform.position.x, transform.position.y+60, -20), Quaternion.identity) as GameObject;
            GameObject Eff = Instantiate(CtrlEff, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity)as GameObject;
            if (transform.localScale.x == 1)
            {
                scale = CtrlAttack.transform.localScale;
                scale.x = 1;
                CtrlAttack.transform.localScale = scale;
                Eff.transform.localScale = scale;
                CtrlAttack.GetComponent<DarkLoadCtrl>().distance = -2000;
            }
            else
            {
                scale = CtrlAttack.transform.localScale;
                scale.x = -1;
                CtrlAttack.transform.localScale = scale;
                Eff.transform.localScale = scale;
                CtrlAttack.GetComponent<DarkLoadCtrl>().distance = 2000;
            }
            Status.canmove = false;
            //値渡し
            SkillDetail Skill = CtrlAttack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.PlayerNumber = Status.PlayerNumber;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.skillpercentage = 4;
            Skill.HitEffect = Ctrl_hit;
            Skill.HitSE = Ctrl_HitSE;
            Skill.Hitlimit = 3;
            Delayend = false;

            Ctrl_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Ctrl_cooltime");
        }

        //闇の落雷(Space)
        if (Input.GetKey(KeyCode.Space) && Delayend && Space_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(SpaceStart_SE);
            GameObject Eff = Instantiate(DarkLoadSpace_Start, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            PositionTmp = new Vector3(transform.position.x, transform.position.y, -20);
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
            GetComponent<Renderer>().material.color -= new Color(0,0,0,1);
            Head.GetComponent<Renderer>().material.color -= new Color(0, 0, 0, 1);
            gameObject.layer = LayerMask.NameToLayer("Hide");
            Delayend = false;
            Space_cooltimeend = false;
            StartCoroutine("CreateSpace");
            StartCoroutine("SpaceDelay");
            StartCoroutine("Space_cooltime");
        }
    }
}
