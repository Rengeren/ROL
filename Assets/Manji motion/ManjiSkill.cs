using UnityEngine;
using System.Collections;

public class ManjiSkill : MonoBehaviour {
    GameObject ManjiX;
    GameObject ManjiX_hit;
    GameObject Head;
    [HideInInspector]
    public GameObject SwoonedMark;
    private GameObject Ground;
    private bool Delayend = true;
    private float Ctrllength = 120;
    private CharacterStatus Status;
    private move move;
    private LadderforPlayer Ladder;
    Vector2 scale;
    float tmp;
    float tmp2;
    float tmp3;
    float tmp4;
    //通常攻撃(X)
    int pattern;
    [HideInInspector]
    public GameObject ManjiX1;
    [HideInInspector]
    public GameObject ManjiX2;
    [HideInInspector]
    public GameObject ManjiX_hit1;
    [HideInInspector]
    public GameObject ManjiX_hit2;
    [HideInInspector]
    public AudioClip Normal_SE;
    [HideInInspector]
    public AudioClip Normal_HitSE;
    //三段切り(Shift)
    [HideInInspector]
    public GameObject ManjiShift;
    [HideInInspector]
    public GameObject ManjiShift_hit;
    [HideInInspector]
    public AudioClip Shift_SE;
    [HideInInspector]
    public AudioClip Shift_HitSE;
    [HideInInspector]
    public float Shiftct = 5;
    bool Shift_cooltimeend = true;
    //足首落とし(Z)
    [HideInInspector]
    public GameObject ManjiZ;
    [HideInInspector]
    public GameObject ManjiZ_hit;
    [HideInInspector]
    public AudioClip Z_SE;
    [HideInInspector]
    public AudioClip Z_HitSE;
    [HideInInspector]
    public float Zct = 7;
    bool Z_cooltimeend = true;
    //蒼空斬り(ctrl)
    [HideInInspector]
    public GameObject ManjiCtrl;
    [HideInInspector]
    public GameObject ManjiCtrlBody;
    [HideInInspector]
    public GameObject ManjiCtrl_hit;
    [HideInInspector]
    public AudioClip Ctrl_SE;
    [HideInInspector]
    public AudioClip Ctrl_HitSE;
    [HideInInspector]
    public float Ctrlct = 8;
    float Ctrl_distance = 240;
    float ErrorCorrection = 23;
    Vector3 pos = Vector3.zero;
    Vector3 pos2 = Vector3.zero;
    RaycastHit2D hit;
    bool Ctrl_cooltimeend = true;
    bool CtrlDelayend = true;
    //剣体一致(space)
    [HideInInspector]
    public GameObject ManjiSpace;
    [HideInInspector]
    public GameObject ManjiSpaceAura;
    [HideInInspector]
    public AudioClip ManjiSpaceSE;
    [HideInInspector]
    public float SpaceCorrection = 1;
    [HideInInspector]
    public float Spacect = 70;
    bool Space_cooltimeend = true;

    void Start()
    {
        tmp2 = Ctrllength;
        tmp3 = Ctrl_distance;
        tmp4 = ErrorCorrection;
        Ground = transform.FindChild("Ground").gameObject;
        Status = GetComponent<CharacterStatus>();
        move = GetComponent<move>();
        Head = transform.FindChild("Head").gameObject;
        Ladder = GetComponent<LadderforPlayer>();
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        Status.canmove = true;
        Delayend = true;
    }
    public IEnumerator AnimationTime(string WhichSkill, float mx, float my)
    {
        GetComponent<Animator>().SetBool(WhichSkill, true);
        Head.transform.Translate(mx, my, Head.transform.position.z);
        yield return new WaitForSeconds(0.5f);
        GetComponent<Animator>().SetBool(WhichSkill, false);
        Head.transform.Translate(-mx, -my, Head.transform.position.z);
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
    public IEnumerator CtrlDelay()
    {
        yield return new WaitForSeconds(0.1f);
        CtrlDelayend = true;
    }
    public IEnumerator Space_cooltime()
    {
        Status.SpaceCT = true; //Bar用
        yield return new WaitForSeconds(Spacect);
        Space_cooltimeend = true;
    }
    public IEnumerator SpaceBaff(int Level)
    {
        yield return new WaitForSeconds(15);
        if (Status.Level < 11)
        {
            SpaceCorrection = 1;
            Status.JumpForceCorrection -= 0.2f;
            Status.WalkSpeedCorrection -= 0.2f;
            move.maxwalkspeed = 100 * Status.WalkSpeedCorrection;
            move.jumpforce = 5600 * Status.JumpForceCorrection;
        }
        else
        {
            SpaceCorrection = 1;
            Status.JumpForceCorrection -= 0.4f;
            Status.WalkSpeedCorrection -= 0.4f;
            move.maxwalkspeed = 100 * Status.WalkSpeedCorrection;
            move.jumpforce = 5600 * Status.JumpForceCorrection;
        }
    }

    void Update()
    {
        if (CtrlDelayend != true) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //通常攻撃(X)
        if (Input.GetKey(KeyCode.X) && Delayend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Normal_SE);
            pattern = Random.Range(0, 2);
            if (pattern == 0)
            {
                ManjiX = ManjiX1;
                ManjiX_hit = ManjiX_hit1;
                StartCoroutine(AnimationTime("X1", -8.93799f, -5.8857f));
            }
            else if (pattern == 1)
            {
                ManjiX = ManjiX2;
                ManjiX_hit = ManjiX_hit2;
                StartCoroutine(AnimationTime("X2", -8.93799f, -5.8857f));
            }
            GameObject xAttack = Instantiate(ManjiX, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
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
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = ManjiX_hit;
            Skill.HitSE = Normal_HitSE;
            Skill.skillpercentage = 1 * SpaceCorrection;
            Skill.Hitlimit = 3;
            Delayend = false;
            StartCoroutine("Delay");
        }

        //三段切り(shift)
        if (Input.GetKey(KeyCode.LeftShift) && Delayend && Shift_cooltimeend && Ladder.Skilltime || Input.GetKey(KeyCode.RightShift) && Delayend && Shift_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Shift_SE);

            GameObject shiftAttack = Instantiate(ManjiShift, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
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
            StartCoroutine(AnimationTime("Shift", 2.93799f, -4.8857f));
            //値渡し
            SkillDetail Skill = shiftAttack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = ManjiShift_hit;
            Skill.HitSE = Shift_HitSE;
            Skill.skillpercentage = 2.5f * SpaceCorrection;
            Skill.Hitlimit = 6;
            Delayend = false;

            Shift_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Shift_cooltime");
        }

        //足首落とし(z)
        if (Input.GetKey(KeyCode.Z) && Delayend && Z_cooltimeend && Ladder.Skilltime)
        {

            GetComponent<AudioSource>().PlayOneShot(Z_SE);

            GameObject ZAttack = Instantiate(ManjiZ, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
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
            StartCoroutine(AnimationTime("Z", -14.93799f, -11.8857f));
            //値渡し
            SkillDetail Skill = ZAttack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = ManjiZ_hit;
            Skill.HitSE = Z_HitSE;
            Skill.skillpercentage = 2 * SpaceCorrection;
            Skill.Hitlimit = 1;
            Skill.debuffType = 6;
            Skill.debuff = SwoonedMark;
            Delayend = false;

            Z_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Z_cooltime");
        }

        //左右方向の蒼空斬り(ctrl)
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightArrow) && Delayend && Ctrl_cooltimeend && Ladder.Skilltime || Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftArrow) && Delayend && Ctrl_cooltimeend && Ladder.Skilltime
            || Input.GetKey(KeyCode.RightControl) && Input.GetKey(KeyCode.RightArrow) && Delayend && Ctrl_cooltimeend && Ladder.Skilltime || Input.GetKey(KeyCode.RightControl) && Input.GetKey(KeyCode.LeftArrow) && Delayend && Ctrl_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Ctrl_SE);
            if (Input.GetKey(KeyCode.RightArrow)) Ctrllength = tmp2;
            else if (Input.GetKey(KeyCode.LeftArrow)) Ctrllength = -tmp2;
            GameObject CtrlAttack = Instantiate(ManjiCtrl, new Vector3(transform.position.x + Ctrllength, transform.position.y, -20), Quaternion.identity) as GameObject;
            GameObject CtrlBody = Instantiate(ManjiCtrlBody, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            Ground.layer = LayerMask.NameToLayer("OnGround");
            move.CanJump = true;
            move.downjump = false;
            if (transform.localScale.x == 1)
            {
                scale = CtrlAttack.transform.localScale;
                scale.x = 1;
                CtrlAttack.transform.localScale = scale;
                scale = CtrlBody.transform.localScale;
                scale.x = 1;
                CtrlBody.transform.localScale = scale;
            }
            else
            {
                scale = CtrlAttack.transform.localScale;
                scale.x = -1;
                CtrlAttack.transform.localScale = scale;
                scale = CtrlBody.transform.localScale;
                scale.x = -1;
                CtrlBody.transform.localScale = scale;
            }
            if (Input.GetKey(KeyCode.RightArrow)) Ctrl_distance = tmp3;
            else if (Input.GetKey(KeyCode.LeftArrow)) Ctrl_distance = tmp3 * -1;
            Status.canmove = false;
            StartCoroutine(AnimationTime("Ctrl", -12.93799f, -6.8857f));
            //値渡し
            SkillDetail Skill = CtrlAttack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = ManjiCtrl_hit;
            Skill.HitSE = Ctrl_HitSE;
            Skill.skillpercentage = 2.5f * SpaceCorrection;
            Skill.Hitlimit = 6;
            Delayend = false;

            Vector3 distance_tmp = transform.position;
            distance_tmp.x += Ctrl_distance;
            transform.position = distance_tmp;
            CtrlDelayend = false;
            Ctrl_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Ctrl_cooltime");
            StartCoroutine("CtrlDelay");
        }
        //上下方向の蒼空斬り(ctrl)
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.UpArrow) && Delayend && Ctrl_cooltimeend && Ladder.Skilltime || Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.DownArrow) && Delayend && Ctrl_cooltimeend && move.CanDown && Ladder.Skilltime
            || Input.GetKey(KeyCode.RightControl) && Input.GetKey(KeyCode.UpArrow) && Delayend && Ctrl_cooltimeend && Ladder.Skilltime || Input.GetKey(KeyCode.RightControl) && Input.GetKey(KeyCode.DownArrow) && Delayend && Ctrl_cooltimeend && move.CanDown && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(Ctrl_SE);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Ctrl_distance = tmp3;
                Ctrllength = tmp2;
                ErrorCorrection = tmp4;
                pos.y = Ctrl_distance;
                pos.z = -20;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Ctrl_distance = tmp3 * -1;
                Ctrllength = -tmp2;
                ErrorCorrection = -1 * tmp4;
                pos.y = Ctrl_distance;
                pos.z = -20;
            }
            GameObject CtrlAttack = Instantiate(ManjiCtrl, new Vector3(transform.position.x + ErrorCorrection, transform.position.y + Ctrllength, -20), Quaternion.identity) as GameObject;
            GameObject CtrlBody = Instantiate(ManjiCtrlBody, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            if (Input.GetKey(KeyCode.UpArrow)) CtrlAttack.transform.rotation = Quaternion.Euler(0, 0, 90);
            else if (Input.GetKey(KeyCode.DownArrow)) CtrlAttack.transform.rotation = Quaternion.Euler(0, 0, -90);
            if (transform.localScale.x == 1)
            {
                scale = CtrlAttack.transform.localScale;
                scale.x = 1;
                CtrlAttack.transform.localScale = scale;
                scale = CtrlBody.transform.localScale;
                scale.x = 1;
                CtrlBody.transform.localScale = scale;
            }
            else
            {
                scale = CtrlAttack.transform.localScale;
                scale.x = -1;
                CtrlAttack.transform.localScale = scale;
                scale = CtrlBody.transform.localScale;
                scale.x = -1;
                CtrlBody.transform.localScale = scale;
            }
            Status.canmove = false;
            StartCoroutine(AnimationTime("Ctrl", -12.93799f, -6.8857f));
            //値渡し
            SkillDetail Skill = CtrlAttack.GetComponent<SkillDetail>();
            Skill.WhoseSkill = gameObject;
            Skill.level = Status.Level;
            Skill.MinATK = Status.minATK;
            Skill.MaxATK = Status.maxATK;
            Skill.criticalpercentage = Status.criticalpercentage;
            //スキルの固有値
            Skill.HitEffect = ManjiCtrl_hit;
            Skill.HitSE = Ctrl_HitSE;
            Skill.skillpercentage = 2.5f * SpaceCorrection;
            Skill.Hitlimit = 6;
            Delayend = false;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                hit = Physics2D.Raycast(transform.position + pos, Vector2.down, Ctrl_distance - 1f, move.GroundLayer);
                if (Physics2D.Raycast(transform.position + pos, Vector2.down, Ctrl_distance - 1f, move.GroundLayer))
                {
                    Ground.layer = LayerMask.NameToLayer("OnGround");
                    move.CanJump = false;
                    move.downjump = false;
                    pos2 = hit.point;
                    pos2.z = -20;
                    transform.position = pos2;
                }
                else
                {
                    Ground.layer = LayerMask.NameToLayer("OnGround");
                    move.CanJump = false;
                    move.downjump = false;
                    Vector3 distance_tmp = transform.position;
                    distance_tmp.y += Ctrl_distance;
                    transform.position = distance_tmp;
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                hit = Physics2D.Raycast(transform.position + pos, Vector2.down, Ctrl_distance - 1f, move.GroundLayer);
                if (Physics2D.Raycast(transform.position + pos, Vector2.up, -1 * Ctrl_distance - 1f, move.GroundLayer))
                {
                    Ground.layer = LayerMask.NameToLayer("OnGround");
                    move.CanJump = true;
                    move.downjump = false;
                    pos2 = hit.point;
                    pos2.z = -20;
                    transform.position = pos2;
                }
                else
                {
                    Ground.layer = LayerMask.NameToLayer("OnGround");
                    move.CanJump = true;
                    move.downjump = false;
                    Vector3 distance_tmp = transform.position;
                    distance_tmp.y += Ctrl_distance;
                    transform.position = distance_tmp;
                }
            }
            CtrlDelayend = false;
            Ctrl_cooltimeend = false;
            StartCoroutine("Delay");
            StartCoroutine("Ctrl_cooltime");
            StartCoroutine("CtrlDelay");
        }

        //剣体一致(space)
        if (Input.GetKey(KeyCode.Space) && Delayend && Space_cooltimeend && Ladder.Skilltime)
        {
            GetComponent<AudioSource>().PlayOneShot(ManjiSpaceSE);
            GameObject SpaceEffect = Instantiate(ManjiSpace, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
            GameObject SpaceAura = Instantiate(ManjiSpaceAura, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            if (transform.localScale.x == 1)
            {
                scale = SpaceAura.transform.localScale;
                scale.x = 1;
                SpaceAura.transform.localScale = scale;
            }
            else
            {
                scale = SpaceAura.transform.localScale;
                scale.x = -1;
                SpaceAura.transform.localScale = scale;
            }
            SpaceEffect.GetComponent<horming>().player = gameObject;
            SpaceAura.GetComponent<horming>().player = gameObject;
            SpaceAura.GetComponent<ManjiSpaceDestroy>().player = gameObject;
            if (Status.Level < 11)
            {
                SpaceCorrection = 1.5f;
                Status.JumpForceCorrection += 0.2f;
                Status.WalkSpeedCorrection += 0.2f;
                move.maxwalkspeed = 100 * Status.WalkSpeedCorrection;
                move.jumpforce = 5600 * Status.JumpForceCorrection;
            }
            else
            {
                SpaceCorrection = 2;
                Status.JumpForceCorrection += 0.4f;
                Status.WalkSpeedCorrection += 0.4f;
                move.maxwalkspeed = 100 * Status.WalkSpeedCorrection;
                move.jumpforce = 5600 * Status.JumpForceCorrection;
            }
            Space_cooltimeend = false;
            StartCoroutine("Space_cooltime");
            StartCoroutine("SpaceBaff", Status.Level);
        }
    }
}
