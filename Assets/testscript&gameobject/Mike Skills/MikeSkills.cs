using UnityEngine;
using System.Collections;

public class MikeSkills : MonoBehaviour {
    GameObject MikeX;
    private bool Delayend = true;
    private CharacterStatus Status;
    private LadderforPlayer Ladder;
    Vector2 scale;
    //通常攻撃(X)
    int pattern;
    [HideInInspector]
    public GameObject MikeX1;
    [HideInInspector]
    public GameObject MikeX2;
    [HideInInspector]
    public GameObject MikeX_hit;
    [HideInInspector]
    public AudioClip Normal_SE;
    [HideInInspector]
    public AudioClip Normal_HitSE;
    //ジャイアントモール(Shift)
    [HideInInspector]
    public GameObject MikeShift;
    [HideInInspector]
    public GameObject MikeShift_hit;
    [HideInInspector]
    public AudioClip Shift_SE;
    [HideInInspector]
    public AudioClip Shift_HitSE;
    [HideInInspector]
    public float Shiftct=6;
    bool Shift_cooltimeend = true;
    private float ShiftDelayLength;
    //鉄槌鎧(Z)
    [HideInInspector]
    public GameObject MikeZ;
    [HideInInspector]
    public GameObject MikeZ_hit;
    [HideInInspector]
    public GameObject MikeZ_start;
    [HideInInspector]
    public AudioClip Z_SE;
    [HideInInspector]
    public AudioClip Z_HitSE;
    [HideInInspector]
    public float Zct=9;
    bool Z_cooltimeend = true;
    //戦場の盾(ctrl)
    [HideInInspector]
    public GameObject MikeCtrlShield;
    [HideInInspector]
    public AudioClip CtrlStart_SE;
    [HideInInspector]
    public float Ctrlct=25;
    bool Ctrl_cooltimeend = true;
    //犠牲のラッパ(space)
    [HideInInspector]
    public GameObject MikeSpace;
    [HideInInspector]
    public GameObject Space_Hit;
    [HideInInspector]
    public AudioClip SpaceStart_SE;
    [HideInInspector]
    public float Spacect=60;
    bool Space_cooltimeend = true;

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
    public IEnumerator ShiftDelay()
    {
        yield return new WaitForSeconds(ShiftDelayLength);
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
        Space_cooltimeend = true;
    }

    void Update()
    {
        //通常攻撃(X)
        if (Input.GetKey(KeyCode.X) && Delayend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Normal_SE);
            pattern = Random.Range(0, 2);
            if (pattern == 0)
            {
                MikeX = MikeX1;
            }
            else if (pattern == 1)
            {
                MikeX = MikeX2;
            }
            GameObject xAttack = Instantiate(MikeX, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            if (transform.localScale.x == 1)
            {
                scale = xAttack.transform.localScale;
                scale.x = 1;
                xAttack.transform.localScale = scale;
            }
            else
            {
                scale = xAttack.transform.localScale;
                scale.x = -1;
                xAttack.transform.localScale = scale;
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
            Skill.HitEffect = MikeX_hit;
            Skill.HitSE = Normal_HitSE;
            Skill.skillpercentage = 1;
            Skill.Hitlimit = 4;
            
            Delayend = false;
            StartCoroutine("Delay");
        }

        //ジャイアントモール(shift)
        if (Input.GetKey(KeyCode.LeftShift) && Delayend && Shift_cooltimeend && Ladder.Skilltime || Input.GetKey(KeyCode.RightShift) && Delayend && Shift_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Shift_SE);
            GameObject shiftAttack = Instantiate(MikeShift, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            Animator animOne = shiftAttack.GetComponent<Animator>();
            AnimatorStateInfo infAnim = animOne.GetCurrentAnimatorStateInfo(0);
            ShiftDelayLength = infAnim.length;
            if (transform.localScale.x == 1)
            {
                scale = shiftAttack.transform.localScale;
                scale.x = 1;
                shiftAttack.transform.localScale = scale;
            }
            else
            {
                scale = shiftAttack.transform.localScale;
                scale.x = -1;
                shiftAttack.transform.localScale = scale;
            }
            Status.canmove = false;
            //値渡し
            SkillDetail Skill = shiftAttack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.PlayerNumber = Status.PlayerNumber;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = MikeShift_hit;
            Skill.HitSE = Shift_HitSE;
            Skill.skillpercentage = 2;
            Skill.Hitlimit = 3;
            Delayend = false;

            Shift_cooltimeend = false;
            StartCoroutine("ShiftDelay");
            StartCoroutine("Shift_cooltime");
        }

        //鉄槌鎧(z)
        if (Input.GetKey(KeyCode.Z) && Delayend && Z_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Z_SE);
            GameObject ZStart=Instantiate(MikeZ_start, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity)as GameObject;
            GameObject ZAttack = Instantiate(MikeZ, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
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
            ZStart.GetComponent<horming>().player = gameObject;
            ZAttack.GetComponent<horming>().player = gameObject;
            //値渡し
            SkillDetail Skill = ZAttack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.PlayerNumber = Status.PlayerNumber;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect =MikeZ_hit;
            Skill.HitSE = Z_HitSE;
            Skill.skillpercentage = 0.5f;
            Skill.Hitlimit = 1000;

            Z_cooltimeend = false;
            StartCoroutine("Z_cooltime");
        }

        //戦場の盾(ctrl)
        if (Input.GetKeyDown(KeyCode.LeftControl)&& Delayend && Ctrl_cooltimeend && Ladder.Skilltime || Input.GetKeyDown(KeyCode.RightControl) && Delayend && Ctrl_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(CtrlStart_SE);
            GameObject CtrlShield = Instantiate(MikeCtrlShield, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            if (transform.localScale.x == 1)
            {
                scale = CtrlShield.transform.localScale;
                scale.x = 1;
                CtrlShield.transform.localScale = scale;
            }
            else
            {
                scale = CtrlShield.transform.localScale;
                scale.x = -1;
                CtrlShield.transform.localScale = scale;
            }
            Status.canmove = false;
            CtrlShield.GetComponent<horming>().player = gameObject;
            CtrlShield.GetComponent<MikeCtrlBody>().player = gameObject;
            CtrlShield.GetComponent<MikeCtrlBody>().damage = GetComponent<DamageCal>();
            GetComponent<DamageCal>().Ctrl = true;
            GetComponent<DamageCal>().MikeCtrl = CtrlShield.GetComponent<MikeCtrlBody>();
            Delayend = false;
            Ctrl_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Ctrl_cooltime");
        }
        //犠牲のラッパ(Space)
        if (Input.GetKeyDown(KeyCode.Space) && Delayend && Space_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(SpaceStart_SE);
            GameObject Space = Instantiate(MikeSpace, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
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
            Status.canmove = false;
            Space.GetComponent<horming>().player = gameObject;
            Space.GetComponent<MikeSpaceBody>().player = gameObject;
            Space.GetComponent<MikeSpaceBody>().Status = GetComponent<CharacterStatus>();
            //値渡し
            SkillDetail Skill = Space.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.PlayerNumber = Status.PlayerNumber;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = Space_Hit;
            Skill.Hitlimit = 100;

            Delayend = false;
            Space_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Space_cooltime");
        }
    }
}
