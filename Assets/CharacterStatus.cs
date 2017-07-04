using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStatus : MonoBehaviour {
    private GameObject player;
    [HideInInspector]
    public float MaxHP = 3000;
    public GameObject LevelUI;
    public GameObject LevelUPef;
    public GameObject GoldText;
    public GameObject StatusQ,StatusW,StatusE,StatusR,StatusT;
    public GameObject SBX, SBShift, SBZ, SBCtrl, SBSpace;
    [HideInInspector]
    public Sprite SBManji1, SBManji2, SBManji3, SBManji4, SBManji5, SBMike1, SBMike2, SBMike3, SBMike4, SBMike5, SBDL1, SBDL2, SBDL3, SBDL4, SBDL5, SBMage1, SBMage2, SBMage3, SBMage4, SBMage5, SBBuko1, SBBuko2, SBBuko3, SBBuko4, SBBuko5, SBHelena1, SBHelena2, SBHelena3, SBHelena4, SBHelena5;
    [HideInInspector]
    public Sprite imageQ1, imageQ2, imageQ3, imageQ4, imageQ5, imageW1, imageW2, imageW3, imageW4, imageW5, imageE1, imageE2, imageE3, imageE4, imageE5, imageR1, imageR2, imageR3, imageR4, imageR5, imageT1, imageT2, imageT3, imageT4, imageT5;
    [HideInInspector]
    public int NowHP = 2000;
    float[] JobMaxHP = new float[6] { 3000, 3900, 2400, 2100, 3150, 1800 }; //職業ごとの初期HP
    float[] CoreQrate = new float[6] { 1, 1.02f, 1.05f, 1.10f, 1.15f, 1.2f };
    float[] CoreWrate = new float[6] { 1, 1.02f, 1.07f, 1.15f, 1.2f, 1.3f };
    static public int[] Exptable = new int[15]{60,90,125,175,245,320,415,540,650,780,935,1029,1132,1245,0};
    int[,] JobATK = new int[3, 6] { { 180, 126, 216, 234, 189, 280 }, { 200, 140, 240, 260, 210, 310 }, {16,11,19,21,17,24} };//最低攻撃力、最大攻撃力、攻撃力増加率
    [HideInInspector]
    public int Level=1;
    [HideInInspector]
    public int Exp=0;
    [HideInInspector]
    public int minATK;
    [HideInInspector]
    public int maxATK;
    [HideInInspector]
    public int Avator=1; //1=manji 2=maiku 3=da-kuro-do 4=hainzu 5=buko 6=herena
    [HideInInspector]
    public int HeadAvator=1;
    [HideInInspector]
    public int HaveGold = 9999;
    [HideInInspector]
    public int CoreQ = 0;
    [HideInInspector]
    public int CoreW = 0;
    [HideInInspector]
    public int CoreE = 0;
    [HideInInspector]
    public int CoreR = 0;
    [HideInInspector]
    public int CoreT = 0;
    public string Charaname = "見習い聖騎士";
    [HideInInspector]
    public AudioClip CoreSuccess;
    [HideInInspector]
    public AudioClip CoreAlert;
    [HideInInspector]
    public AudioClip Levelup;
    //追加
    [HideInInspector]
    public int PlayerNumber = 1;
    [HideInInspector]
    public int criticalpercentage=0;
    [HideInInspector]
    public bool canmove = true;
    [HideInInspector]
    public GameObject CoreQEff;
    [HideInInspector]
    public GameObject CoreWEff;
    [HideInInspector]
    public GameObject CoreEEff;
    [HideInInspector]
    public GameObject CoreREff;
    [HideInInspector]
    public GameObject CoreTEff;
    [HideInInspector]
    public float WalkSpeedCorrection=1;
    [HideInInspector]
    public float JumpForceCorrection = 1;
    [HideInInspector]
    public GameObject Heal;
    [HideInInspector]
    public AudioClip HealSE;
    int Healvalue;
    bool Heal_cooltimeend = true;

    //SkillUITest
    [HideInInspector]
    public bool ShiftCT;
    [HideInInspector]
    public bool ZCT;
    [HideInInspector]
    public bool CtrlCT;
    [HideInInspector]
    public bool SpaceCT;
    [HideInInspector]
    public bool AltCT;

    // Use this for initialization
    void Start () {
        ATKcal();
        player = gameObject;
    }
    public void SpeedCal()
    {
        GetComponent<LadderforPlayer>().MoveSpeed = 150 * GetComponent<CharacterStatus>().WalkSpeedCorrection;
        GetComponent<move>().maxwalkspeed = 100 * WalkSpeedCorrection;
        GetComponent<move>().jumpforce = 5600 * JumpForceCorrection;
    }
    void HPcal()
    {
        MaxHP = JobMaxHP[Avator - 1] * (1f + ((float)Level - 1f) * 0.1f) * CoreWrate[CoreW];
    }
    void ATKcal()
    {
        if (Level < 13) {
            minATK = (int)((JobATK[0, Avator - 1] + Level * JobATK[2, Avator - 1]) * CoreQrate[CoreQ]);
            maxATK = (int)((JobATK[1, Avator - 1] + Level * JobATK[2, Avator - 1]) * CoreQrate[CoreQ]);
            Debug.Log(minATK + "～" + maxATK);
                }
        else {
            minATK = (int)((JobATK[0, Avator - 1] + Level * JobATK[2, Avator - 1]+(Level - 12) * JobATK[2, Avator - 1]) * CoreQrate[CoreQ]);
            maxATK = (int)((JobATK[1, Avator - 1] + Level * JobATK[2, Avator - 1]+(Level - 12) * JobATK[2, Avator - 1]) * CoreQrate[CoreQ]);
            Debug.Log(minATK + "～" + maxATK);
        }
    }
    void JobChange()
    {
        switch (Avator)
        {
            case 1:
                player.GetComponent<ManjiSkill>().enabled = true;
                player.GetComponent<MikeSkills>().enabled = false;
                player.GetComponent<DarkLoadSkills>().enabled = false;
                player.GetComponent<BukoSkills>().enabled = false;
                player.GetComponent<MageSkills>().enabled = false;
                player.GetComponent<HelenaSkills>().enabled = false;
                SBX.GetComponent<SpriteRenderer>().sprite = SBManji1;
                SBShift.GetComponent<SpriteRenderer>().sprite = SBManji2;
                SBZ.GetComponent<SpriteRenderer>().sprite = SBManji3;
                SBCtrl.GetComponent<SpriteRenderer>().sprite = SBManji4;
                SBSpace.GetComponent<SpriteRenderer>().sprite = SBManji5;
                break;

            case 2:
                player.GetComponent<ManjiSkill>().enabled = false;
                player.GetComponent<MikeSkills>().enabled = true;
                player.GetComponent<DarkLoadSkills>().enabled = false;
                player.GetComponent<BukoSkills>().enabled = false;
                player.GetComponent<MageSkills>().enabled = false;
                player.GetComponent<HelenaSkills>().enabled = false;
                SBX.GetComponent<SpriteRenderer>().sprite = SBMike1;
                SBShift.GetComponent<SpriteRenderer>().sprite = SBMike2;
                SBZ.GetComponent<SpriteRenderer>().sprite = SBMike3;
                SBCtrl.GetComponent<SpriteRenderer>().sprite = SBMike4;
                SBSpace.GetComponent<SpriteRenderer>().sprite = SBMike5;
                break;

            case 3:
                player.GetComponent<ManjiSkill>().enabled = false;
                player.GetComponent<MikeSkills>().enabled = false;
                player.GetComponent<DarkLoadSkills>().enabled = true;
                player.GetComponent<BukoSkills>().enabled = false;
                player.GetComponent<MageSkills>().enabled = false;
                player.GetComponent<HelenaSkills>().enabled = false;
                SBX.GetComponent<SpriteRenderer>().sprite = SBDL1;
                SBShift.GetComponent<SpriteRenderer>().sprite = SBDL2;
                SBZ.GetComponent<SpriteRenderer>().sprite = SBDL3;
                SBCtrl.GetComponent<SpriteRenderer>().sprite = SBDL4;
                SBSpace.GetComponent<SpriteRenderer>().sprite = SBDL5;
                break;

            case 4:
                player.GetComponent<ManjiSkill>().enabled = false;
                player.GetComponent<MikeSkills>().enabled = false;
                player.GetComponent<DarkLoadSkills>().enabled = false;
                player.GetComponent<BukoSkills>().enabled = false;
                player.GetComponent<MageSkills>().enabled = true;
                player.GetComponent<HelenaSkills>().enabled = false;
                SBX.GetComponent<SpriteRenderer>().sprite = SBMage1;
                SBShift.GetComponent<SpriteRenderer>().sprite = SBMage2;
                SBZ.GetComponent<SpriteRenderer>().sprite = SBMage3;
                SBCtrl.GetComponent<SpriteRenderer>().sprite = SBMage4;
                SBSpace.GetComponent<SpriteRenderer>().sprite = SBMage5;
                break;

            case 5:
                player.GetComponent<ManjiSkill>().enabled = false;
                player.GetComponent<MikeSkills>().enabled = false;
                player.GetComponent<DarkLoadSkills>().enabled = false;
                player.GetComponent<BukoSkills>().enabled = true;
                player.GetComponent<MageSkills>().enabled = false;
                player.GetComponent<HelenaSkills>().enabled = false;
                SBX.GetComponent<SpriteRenderer>().sprite = SBBuko1;
                SBShift.GetComponent<SpriteRenderer>().sprite = SBBuko2;
                SBZ.GetComponent<SpriteRenderer>().sprite = SBBuko3;
                SBCtrl.GetComponent<SpriteRenderer>().sprite = SBBuko4;
                SBSpace.GetComponent<SpriteRenderer>().sprite = SBBuko5;
                break;

            case 6:
                player.GetComponent<ManjiSkill>().enabled = false;
                player.GetComponent<MikeSkills>().enabled = false;
                player.GetComponent<DarkLoadSkills>().enabled = false;
                player.GetComponent<BukoSkills>().enabled = false;
                player.GetComponent<MageSkills>().enabled = false;
                player.GetComponent<HelenaSkills>().enabled = true;
                SBX.GetComponent<SpriteRenderer>().sprite = SBHelena1;
                SBShift.GetComponent<SpriteRenderer>().sprite = SBHelena2;
                SBZ.GetComponent<SpriteRenderer>().sprite = SBHelena3;
                SBCtrl.GetComponent<SpriteRenderer>().sprite = SBHelena4;
                SBSpace.GetComponent<SpriteRenderer>().sprite = SBHelena5;
                break;
        }
    }

    public IEnumerator LevelUpEffect()
    {
        
        GameObject Lvup = Instantiate(LevelUPef, new Vector3(transform.position.x, transform.position.y , -22), Quaternion.identity) as GameObject;
        Lvup.GetComponent<horming>().player=player;
        yield return new WaitForSeconds(1.2f);
        Destroy(Lvup);
    }
    // Update is called once per frame
    void Update() {
        GoldText.GetComponent<Text>().text = HaveGold.ToString();
        //コアアップグレードシステムここから
        if (Input.GetKeyDown(KeyCode.Q)) {
            switch (CoreQ) {
                case 0:
                    if (HaveGold >= 540)
                    {
                        CoreQ = 1;
                        HaveGold -= 540;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        ATKcal();
                        StatusQ.GetComponent<SpriteRenderer>().sprite = imageQ1;
                        GameObject Effect = Instantiate(CoreQEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 1:
                    if (HaveGold >= 990)
                    {
                        CoreQ = 2;
                        HaveGold -= 990;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        ATKcal();
                        StatusQ.GetComponent<SpriteRenderer>().sprite = imageQ2;
                        GameObject Effect = Instantiate(CoreQEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 2:
                    if (HaveGold >= 1620)
                    {
                        CoreQ = 3;
                        HaveGold -= 1620;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusQ.GetComponent<SpriteRenderer>().sprite = imageQ3;
                        ATKcal();
                        GameObject Effect = Instantiate(CoreQEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 3:
                    if (HaveGold >= 2340)
                    {
                        CoreQ = 4;
                        HaveGold -= 2340;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusQ.GetComponent<SpriteRenderer>().sprite = imageQ4;
                        ATKcal();
                        GameObject Effect = Instantiate(CoreQEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 4:
                    if (HaveGold >= 3300)
                    {
                        CoreQ = 5;
                        HaveGold -= 3300;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusQ.GetComponent<SpriteRenderer>().sprite = imageQ5;
                        ATKcal();
                        GameObject Effect = Instantiate(CoreQEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 5:
                   
                        Debug.Log(HaveGold + "and" + CoreQ);
                    
                    break;
            } } 

        if (Input.GetKeyDown(KeyCode.W)){

            switch (CoreW)
            {
                case 0:
                    if (HaveGold >= 540)
                    {
                        CoreW = 1;
                        HaveGold -= 540;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        HPcal();
                        StatusW.GetComponent<SpriteRenderer>().sprite = imageW1;
                        GameObject Effect = Instantiate(CoreWEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 1:
                    if (HaveGold >= 990)
                    {
                        CoreW = 2;
                        HaveGold -= 990;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        HPcal();
                        StatusW.GetComponent<SpriteRenderer>().sprite = imageW2;
                        GameObject Effect = Instantiate(CoreWEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 2:
                    if (HaveGold >= 1620)
                    {
                        CoreW = 3;
                        HaveGold -= 1620;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        HPcal();
                        StatusW.GetComponent<SpriteRenderer>().sprite = imageW3;
                        GameObject Effect = Instantiate(CoreWEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 3:
                    if (HaveGold >= 2340)
                    {
                        CoreW = 4;
                        HaveGold -= 2340;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        HPcal();
                        StatusW.GetComponent<SpriteRenderer>().sprite = imageW4;
                        GameObject Effect = Instantiate(CoreWEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 4:
                    if (HaveGold >= 3300)
                    {
                        CoreW = 5;
                        HaveGold -= 3300;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        HPcal();
                        StatusW.GetComponent<SpriteRenderer>().sprite = imageW5;
                        GameObject Effect = Instantiate(CoreWEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 5:

                    Debug.Log(HaveGold + "and" + CoreW);

                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.E)){

            switch (CoreE)
            {
                case 0:
                    if (HaveGold >= 540)
                    {
                        CoreE = 1;
                        HaveGold -= 540;
                        criticalpercentage = 3;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusE.GetComponent<SpriteRenderer>().sprite = imageE1;
                        GameObject Effect = Instantiate(CoreEEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 1:
                    if (HaveGold >= 990)
                    {
                        CoreE = 2;
                        HaveGold -= 990;
                        criticalpercentage = 6;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusE.GetComponent<SpriteRenderer>().sprite = imageE2;
                        GameObject Effect = Instantiate(CoreEEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 2:
                    if (HaveGold >= 1620)
                    {
                        CoreE = 3;
                        HaveGold -= 1620;
                        criticalpercentage = 12;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusE.GetComponent<SpriteRenderer>().sprite = imageE3;
                        GameObject Effect = Instantiate(CoreEEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 3:
                    if (HaveGold >= 2340)
                    {
                        CoreE = 4;
                        HaveGold -= 2340;
                        criticalpercentage = 18;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusE.GetComponent<SpriteRenderer>().sprite = imageE4;
                        GameObject Effect = Instantiate(CoreEEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 4:
                    if (HaveGold >= 3300)
                    {
                        CoreE = 5;
                        HaveGold -= 3300;
                        criticalpercentage = 25;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusE.GetComponent<SpriteRenderer>().sprite = imageE5;
                        GameObject Effect = Instantiate(CoreEEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 5:

                    Debug.Log(HaveGold + "and" + CoreE);

                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.R)){
            switch (CoreR)
            {
                case 0:
                    if (HaveGold >= 540)
                    {
                        CoreR = 1;
                        WalkSpeedCorrection +=0.06f;
                        JumpForceCorrection += 0.02f;
                        if (GetComponent<Debuff>().infirmed != true)
                        {
                            SpeedCal();
                        }
                        HaveGold -= 540;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusR.GetComponent<SpriteRenderer>().sprite = imageR1;
                        GameObject Effect = Instantiate(CoreREff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 1:
                    if (HaveGold >= 990)
                    {
                        CoreR = 2;
                        WalkSpeedCorrection += 0.06f;
                        JumpForceCorrection += 0.04f;
                        if (GetComponent<Debuff>().infirmed != true)
                        {
                            SpeedCal();
                        }
                        HaveGold -= 990;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusR.GetComponent<SpriteRenderer>().sprite = imageR2;
                        GameObject Effect = Instantiate(CoreREff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 2:
                    if (HaveGold >= 1620)
                    {
                        CoreR = 3;
                        WalkSpeedCorrection += 0.12f;
                        JumpForceCorrection += 0.02f;
                        if (GetComponent<Debuff>().infirmed != true)
                        {
                            SpeedCal();
                        }
                        HaveGold -= 1620;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusR.GetComponent<SpriteRenderer>().sprite = imageR3;
                        GameObject Effect = Instantiate(CoreREff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 3:
                    if (HaveGold >= 2340)
                    {
                        CoreR = 4;
                        WalkSpeedCorrection += 0.12f;
                        JumpForceCorrection += 0.04f;
                        if (GetComponent<Debuff>().infirmed != true)
                        {
                            SpeedCal();
                        }
                        HaveGold -= 2340;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusR.GetComponent<SpriteRenderer>().sprite = imageR4;
                        GameObject Effect = Instantiate(CoreREff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 4:
                    if (HaveGold >= 3300)
                    {
                        CoreR = 5;
                        WalkSpeedCorrection += 0.12f;
                        JumpForceCorrection += 0.03f;
                        if (GetComponent<Debuff>().infirmed != true)
                        {
                            SpeedCal();
                        }
                        HaveGold -= 3300;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusR.GetComponent<SpriteRenderer>().sprite = imageR5;
                        GameObject Effect = Instantiate(CoreREff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 5:

                    Debug.Log(HaveGold + "and" + CoreR);

                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.T)){

            switch (CoreT)
            {
                case 0:
                    if (HaveGold >= 540)
                    {
                        CoreT = 1;
                        HaveGold -= 540;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusT.GetComponent<SpriteRenderer>().sprite = imageT1;
                        GameObject Effect = Instantiate(CoreTEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 1:
                    if (HaveGold >= 990)
                    {
                        CoreT = 2;
                        HaveGold -= 990;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusT.GetComponent<SpriteRenderer>().sprite = imageT2;
                        GameObject Effect = Instantiate(CoreTEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 2:
                    if (HaveGold >= 1620)
                    {
                        CoreT = 3;
                        HaveGold -= 1620;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusT.GetComponent<SpriteRenderer>().sprite = imageT3;
                        GameObject Effect = Instantiate(CoreTEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 3:
                    if (HaveGold >= 2340)
                    {
                        CoreT = 4;
                        HaveGold -= 2340;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusT.GetComponent<SpriteRenderer>().sprite = imageT4;
                        GameObject Effect = Instantiate(CoreTEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 4:
                    if (HaveGold >= 3300)
                    {
                        CoreT = 5;
                        HaveGold -= 3300;
                        GetComponent<AudioSource>().PlayOneShot(CoreSuccess);
                        StatusT.GetComponent<SpriteRenderer>().sprite = imageT5;
                        GameObject Effect = Instantiate(CoreTEff, new Vector3(transform.position.x, transform.position.y + 50, -20), Quaternion.identity) as GameObject;
                        Effect.GetComponent<horming>().player = player;
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(CoreAlert);
                    }
                    break;
                case 5:

                    Debug.Log(HaveGold + "and" + CoreT);

                    break;
            }
        }
        //コアアップグレードシステムここまで
        //レベルシステム

        if (Level <15 && Exp>=Exptable[Level-1]){
            Level++;
            GetComponent<AudioSource>().PlayOneShot(Levelup);
            StartCoroutine("LevelUpEffect");
            LevelUI.GetComponent<Animator>().SetInteger("NowLevel", Level);
            HPcal();
            ATKcal();
            Exp = 0;
        }

        if (Input.GetKeyDown(KeyCode.L)) Exp+=1000; //デバッグLvUP用
        if (Input.GetKeyDown(KeyCode.Tab))//デバッグ職変更用
        {
            if (Avator < 6) Avator++;
            else Avator = 1;

            JobChange();
        }

        
        if (Input.GetKey(KeyCode.LeftAlt)&&Heal_cooltimeend|| Input.GetKey(KeyCode.RightAlt) && Heal_cooltimeend)
        {
            GetComponent<AudioSource>().PlayOneShot(HealSE);
            GameObject HealEff = Instantiate(Heal, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            HealEff.GetComponent<horming>().player = gameObject;
            Healvalue = (int)MaxHP / 10 * 3;
            if (Healvalue > MaxHP - NowHP) NowHP = (int)MaxHP;
            else NowHP += Healvalue;
            Heal_cooltimeend = false;
            StartCoroutine("HealCooltime");
        }
    }
    public IEnumerator HealCooltime()
    {
        AltCT = true; //Bar用
        yield return new WaitForSeconds(60);
        Heal_cooltimeend = true;
    }
}
