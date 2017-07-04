using UnityEngine;
using System.Collections.Generic;
using System.Collections;
//スキルの範囲を決めるCollider(Trigger)に張る

public class SkillDetail : MonoBehaviour {
    [HideInInspector]
    public List<GameObject> HitTarget = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> HitList = new List<GameObject>();
    [HideInInspector]
    public GameObject WhoseSkill;
    [HideInInspector]
    public GameObject SoundManager;
    [HideInInspector]
    public GameObject HitEffect;
    [HideInInspector]
    public int level;
    [HideInInspector]
    public int MinATK;
    [HideInInspector]
    public int MaxATK;
    [HideInInspector]
    public float skillpercentage;
    [HideInInspector]
    public int criticalpercentage;
    [HideInInspector]
    public int HitNum = 0;
    [HideInInspector]
    public int PlayerNumber;
    [HideInInspector]
    public int Hitlimit;
    [HideInInspector]
    public AudioClip HitSE;
    [HideInInspector]
    public int i;
    //debuffType 0:無し 1:DarkLoadZ 2:万斤槌 3:BukoCtrl 4:HelenaX 5:HelenaCtrl 6:気絶
    [HideInInspector]
    public float debuffType=0;
    [HideInInspector]
    public GameObject debuff;
    [HideInInspector]
    public float IceForce=0;
    [HideInInspector]
    public bool MageHeal=false;
    [HideInInspector]
    public bool HelenaX;
    [HideInInspector]
    public float PlaceCorrection=0;
    //ダメージ計算用
    int k;
    float damage;
    int critical;
    move move;
    CharacterStatus Status1;
    MobStatus Status2;

    void Update()
    {
        if (0 < HitNum)
        {
            GameObject SEdummy = Instantiate(SoundManager, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            SEdummy.GetComponent<SoundManager>().HitSE = HitSE;
            for (k=0;k<HitTarget.Count;k++)
            {
                if (HitTarget[k].tag == "RedPlayer" || HitTarget[k].tag == "BluePlayer" || HitTarget[k].tag == "Player")
                {
                    move = HitTarget[k].GetComponent<move>();
                    Status1 = HitTarget[k].GetComponent<CharacterStatus>();
                }
                else if (HitTarget[k].tag == "mob") Status2 = HitTarget[k].GetComponent<MobStatus>();
                Instantiate(HitEffect, new Vector3(HitTarget[k].transform.position.x, HitTarget[k].transform.position.y+PlaceCorrection, -20), Quaternion.identity);
                if (HitEffect.GetComponent<horming>()) HitEffect.GetComponent<horming>().player = HitTarget[k];
                //ダメージ計算
                damage = skillpercentage * Random.Range(MinATK, MaxATK);
                critical = Random.Range(1, 101);
                if (critical <= criticalpercentage)
                {
                    damage *= 2;
                }
                if (HitTarget[k].GetComponent<Debuff>().Targeted)
                {
                    damage *= 1.1f;
                }
                if (HitTarget[k].GetComponent<HelenaSkills>() && HitTarget[k].GetComponent<HelenaSkills>().HelenaBuff)
                {
                    damage *= 0.8f;
                }
                //デバフ
                if (debuffType == 1)
                {
                    HitTarget[k].GetComponent<Debuff>().infirmed = true;
                    GameObject Infirmedummy = Instantiate(debuff, new Vector3(HitTarget[k].transform.position.x, HitTarget[k].transform.position.y + 20, -20), Quaternion.identity) as GameObject;
                    HitTarget[k].GetComponent<Debuff>().InfirmeMark = Infirmedummy;
                    Infirmedummy.GetComponent<horming>().player = HitTarget[k];
                }
                if (debuffType == 3)
                {
                    HitTarget[k].GetComponent<Debuff>().Slowed = true;
                    GameObject Slowdummy = Instantiate(debuff, new Vector3(HitTarget[k].transform.position.x, HitTarget[k].transform.position.y, -20), Quaternion.identity) as GameObject;
                    GetComponent<Debuff>().SlowMark = Slowdummy;
                    Slowdummy.GetComponent<horming>().player = HitTarget[k];
                }
                if (debuffType == 4)
                {
                    if (HitTarget[k].GetComponent<Debuff>().Targeted != true)
                    {
                        HitTarget[k].GetComponent<Debuff>().Targeted = true;
                        GameObject Targetdummy = Instantiate(debuff, new Vector3(HitTarget[k].transform.position.x, HitTarget[k].transform.position.y, -20), Quaternion.identity) as GameObject;
                        HitTarget[k].GetComponent<Debuff>().TargetMark = Targetdummy;
                        Targetdummy.GetComponent<horming>().player = HitTarget[k];
                        Targetdummy.GetComponent<horming>().Correction = 20;
                    }
                    else HitTarget[k].GetComponent<Debuff>().TargetTime = 0;
                }
                if (debuffType == 5)
                {
                    if (HitTarget[k].GetComponent<Debuff>().Iced)   HitTarget[k].GetComponent<Debuff>().IceTime = 0;
                    else
                    {
                        HitTarget[k].GetComponent<Debuff>().Iced = true;
                        GameObject Icedummy = Instantiate(debuff, new Vector3(HitTarget[k].transform.position.x, HitTarget[k].transform.position.y, -20), Quaternion.identity) as GameObject;
                        HitTarget[k].GetComponent<Debuff>().IceMark = Icedummy;
                        Icedummy.GetComponent<horming>().player = HitTarget[k];
                        Icedummy.GetComponent<horming>().Correction = 40;
                    }
                }
                if (debuffType == 6)
                {
                    if (HitTarget[k].GetComponent<Debuff>().Swooned) HitTarget[k].GetComponent<Debuff>().SwoonTime = 0;
                    else
                    {
                        HitTarget[k].GetComponent<Debuff>().Swooned = true;
                        GameObject Swoondummy = Instantiate(debuff, new Vector3(HitTarget[k].transform.position.x, HitTarget[k].transform.position.y, -20), Quaternion.identity) as GameObject;
                        HitTarget[k].GetComponent<Debuff>().SwoonMark = Swoondummy;
                        Swoondummy.GetComponent<horming>().player = HitTarget[k];
                        Swoondummy.GetComponent<horming>().Correction = 40;
                    }
                }
                if (IceForce !=0)
                {
                    HitTarget[k].GetComponent<Rigidbody2D>().AddForce(new Vector2(IceForce,0));
                }
                //StartCoroutine(HitTarget[k].GetComponent<DamageUI>().Damage(((int)damage), HitTarget[k].transform.position.x, HitTarget[k].transform.position.y));
                if (k == 0 && HelenaX) skillpercentage = 0.5f;
                //player
                if (HitTarget[k].tag == "RedPlayer" || HitTarget[k].tag == "BluePlayer" || HitTarget[k].tag == "Player")
                {
                    if (debuffType == 2 && move.IsGround && move.CanDown)
                    {
                        HitTarget[k].layer=LayerMask.NameToLayer("CharacterHook");
                        HitTarget[k].GetComponent<Rigidbody2D>().AddForce(Vector2.up * move.jumpforce2);
                        move.downjump = true;
                        move.CanJump = false;
                        move.IsGround = false;
                    }
                    if (damage>0) {
                        if (HitTarget[k].GetComponent<DamageCal>().Ctrl != true)
                        {
                            Status1.NowHP -= (int)damage;
                        }
                        else if (HitTarget[k].GetComponent<DamageCal>().MikeCtrl.Durability > (int)damage)
                        {
                            HitTarget[k].GetComponent<DamageCal>().MikeCtrl.Durability -= (int)damage;
                            Status1.NowHP -= 1;
                        }
                        else if (HitTarget[k].GetComponent<DamageCal>().MikeCtrl.Durability <= (int)damage)
                        {
                            HitTarget[k].GetComponent<DamageCal>().MikeCtrl.Durability = 0;
                            Status1.NowHP -= (int)damage + HitTarget[k].GetComponent<DamageCal>().MikeCtrl.Durability;
                            HitTarget[k].GetComponent<DamageCal>().Ctrl = false;
                        }
                    }
                    else if (damage < 0)
                    {
                        if (MageHeal) damage = -150;
                        if (Status1.MaxHP - Status1.NowHP > -damage) Status1.NowHP -= (int)damage;
                        else
                        {
                            Status1.NowHP = (int)Status1.MaxHP;
                        }
                    }
                }
                //mob
                if (HitTarget[k].tag == "mob")
                {
                    Status2.CreateDamage((int)damage);
                    Status2.LastAttack = WhoseSkill;
                    Status2.HP -= (int)damage;
                }
            }
            Hitlimit-=HitTarget.Count;
            HitNum = 0;
            HitTarget.Clear();
        }

        if (HitNum >= Hitlimit && GetComponent<BoxCollider2D>() != null)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
