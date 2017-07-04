using UnityEngine;
using System.Collections;

public class Debuff : MonoBehaviour {
    //Mark
    [HideInInspector]
    public GameObject PoisonMark;
    [HideInInspector]
    public GameObject SwoonMark;
    [HideInInspector]
    public GameObject InfirmeMark;
    [HideInInspector]
    public GameObject SlowMark;
    [HideInInspector]
    public GameObject TargetMark;
    [HideInInspector]
    public GameObject IceMark;
    //Bool
    [HideInInspector]
    public bool poisoned;
    [HideInInspector]
    public bool Swooned;
    [HideInInspector]
    public bool infirmed;
    [HideInInspector]
    public bool Slowed;
    [HideInInspector]
    public bool Targeted;
    [HideInInspector]
    public bool Iced;
    [HideInInspector]
    public float PoisonDamage;
    //Time
    [HideInInspector]
    public float PoisonTime;
    [HideInInspector]
    public float InfirmeTime;
    [HideInInspector]
    public float SwoonTime;
    [HideInInspector]
    public float SlowTime;
    [HideInInspector]
    public float IceTime;
    [HideInInspector]
    public float TargetTime=0;

    void Update()
    {
        if(poisoned)    PoisonTime += Time.deltaTime;
        if (Swooned)    SwoonTime += Time.deltaTime;
        if (infirmed)   InfirmeTime += Time.deltaTime;
        if (Targeted) TargetTime += Time.deltaTime;
        if (Slowed) SlowTime += Time.deltaTime;
        if (Iced) IceTime += Time.deltaTime;

        if (gameObject.tag != "mob"&&infirmed|| gameObject.tag != "mob"&&Slowed || gameObject.tag != "mob" && Iced)
        {
            GetComponent<move>().maxwalkspeed = 10;
            GetComponent<LadderforPlayer>().MoveSpeed = 10;
        }
        if (PoisonMark!=null&&PoisonTime >= 8)
        {
            poisoned = false;
            PoisonTime = 0;
            Destroy(PoisonMark);
        }
        if (SwoonMark != null && SwoonTime >= 1)
        {
            Swooned = false;
            SwoonTime = 0;
            Destroy(SwoonMark);
        }
        if (InfirmeMark != null && InfirmeTime >= 2)
        {
            infirmed = false;
            InfirmeTime = 0;
            if (gameObject.tag != "mob")
            {
                GetComponent<LadderforPlayer>().MoveSpeed = 150 * GetComponent<CharacterStatus>().WalkSpeedCorrection;
                GetComponent<move>().maxwalkspeed = 100 * GetComponent<CharacterStatus>().WalkSpeedCorrection;
            }
            Destroy(InfirmeMark);
        }
        if (TargetMark != null && TargetTime >= 3)
        {
            Targeted = false;
            TargetTime = 0;
            Destroy(TargetMark);
        }
        if (SlowMark != null && SlowTime >= 2)
        {
            Slowed = false;
            SlowTime = 0;
            if (gameObject.tag != "mob")
            {
                GetComponent<LadderforPlayer>().MoveSpeed = 150 * GetComponent<CharacterStatus>().WalkSpeedCorrection;
                GetComponent<move>().maxwalkspeed = 100 * GetComponent<CharacterStatus>().WalkSpeedCorrection;
            }
            Destroy(SlowMark);
        }
        if (IceMark != null && IceTime >= 2)
        {
            Iced = false;
            IceTime = 0;
            if (gameObject.tag != "mob")
            {
                GetComponent<LadderforPlayer>().MoveSpeed = 150 * GetComponent<CharacterStatus>().WalkSpeedCorrection;
                GetComponent<move>().maxwalkspeed = 100 * GetComponent<CharacterStatus>().WalkSpeedCorrection;
            }
            Destroy(IceMark);
        }

        if (GetComponent<BukoSkills>())
        {
            if (GetComponent<BukoSkills>().Ztime)
            {
                if (Swooned)
                {
                    Swooned = false;
                    SwoonTime = 0;
                    Destroy(SwoonMark);
                }
                if (infirmed)
                {
                    infirmed = false;
                    InfirmeTime = 0;
                    if (gameObject.tag != "mob")
                    {
                        GetComponent<LadderforPlayer>().MoveSpeed = 150 * GetComponent<CharacterStatus>().WalkSpeedCorrection;
                        GetComponent<move>().maxwalkspeed = 100 * GetComponent<CharacterStatus>().WalkSpeedCorrection;
                    }
                    Destroy(InfirmeMark);
                }
                if (Slowed)
                {
                    Slowed = false;
                    SlowTime = 0;
                    if (gameObject.tag != "mob")
                    {
                        GetComponent<LadderforPlayer>().MoveSpeed = 150 * GetComponent<CharacterStatus>().WalkSpeedCorrection;
                        GetComponent<move>().maxwalkspeed = 100 * GetComponent<CharacterStatus>().WalkSpeedCorrection;
                    }
                    Destroy(SlowMark);
                }
                if (Iced)
                {
                    Iced = false;
                    IceTime = 0;
                    if (gameObject.tag != "mob")
                    {
                        GetComponent<LadderforPlayer>().MoveSpeed = 150 * GetComponent<CharacterStatus>().WalkSpeedCorrection;
                        GetComponent<move>().maxwalkspeed = 100 * GetComponent<CharacterStatus>().WalkSpeedCorrection;
                    }
                    Destroy(IceMark);
                }
            }
        }
    }
}
