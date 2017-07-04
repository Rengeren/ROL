//player用
using UnityEngine;
using System.Collections;

public class DamageCal : MonoBehaviour {
    int i;
    int Damage;
    float damage;
    int critical;
    bool immortal = false;
    private CharacterStatus Status;
    //マイク用
    [HideInInspector]
    public bool Ctrl = false;
    [HideInInspector]
    public MikeCtrlBody MikeCtrl;
    float time;

    void Awake()
    {
        Status = GetComponent<CharacterStatus>();
    }
    public IEnumerator ImmmortalTime()
    {
        immortal = true;
        yield return new WaitForSeconds(1);
        immortal = false;
    }

    //mobとの接触ダメージ
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "mob" && immortal != true)
        {
            Damage = Random.Range(other.GetComponent<Mobdamage>().MinATK, other.GetComponent<Mobdamage>().MaxATK);
            if (GetComponent<HelenaSkills>() && GetComponent<HelenaSkills>().HelenaBuff)
            {
                damage = 0.8f*Damage;
                Status.NowHP -= (int)damage;
            }
            else if (Ctrl != true)
            {
                Status.NowHP -= Damage;
            }
            else if (MikeCtrl.Durability > Damage)
            {
                MikeCtrl.Durability -= Damage;
                Status.NowHP -= 1;
            }
            else if (MikeCtrl.Durability <= Damage)
            {
                MikeCtrl.Durability = 0;
                Status.NowHP -= Damage + MikeCtrl.Durability;
                Ctrl = false;
            }
            StartCoroutine("ImmmortalTime");
        }
    }

    //スキルのダメージ
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Skill"&&other.gameObject.GetComponent<SkillDetail>().WhoseSkill.tag!=gameObject.tag|| other.gameObject.tag == "Skill" && other.gameObject.GetComponent<SkillDetail>().WhoseSkill.tag == gameObject.tag&& other.gameObject.GetComponent<SkillDetail>().skillpercentage<0)
        {
            SkillDetail Skill = other.gameObject.GetComponent<SkillDetail>();
            if (Skill.HitList.Count == 0)
            {
                Skill.HitNum += 1;
                Skill.HitTarget.Insert(0, gameObject);
                Skill.HitList.Insert(0, gameObject);
            }
            else
            {
                for (i = 0; i < Skill.HitList.Count; i++)
                {
                    if (Skill.HitList[i] != gameObject && i == Skill.HitList.Count - 1)
                    {
                        Skill.HitNum += 1;
                        if (Skill.HitNum <= Skill.Hitlimit)
                        {
                            Skill.HitTarget.Insert(Skill.HitNum - 1, gameObject);
                            Skill.HitList.Insert(0, gameObject);
                        }
                        else if (Skill.HitNum > Skill.Hitlimit)
                        {
                            Skill.HitTarget.Sort((a, b) => a.GetComponent<Number>().No - b.GetComponent<Number>().No);
                            i = 0;
                            while (GetComponent<Number>().No > Skill.HitTarget[i].GetComponent<Number>().No)
                            {
                                i++;
                                if (i == Skill.Hitlimit) break;
                            }
                            if (i == Skill.Hitlimit)
                            {
                                if (Skill.HitTarget[i - 1].GetComponent<Number>().No > GetComponent<Number>().No)
                                {
                                    Skill.HitTarget.Insert(i, gameObject);
                                    Skill.HitTarget.RemoveAt(Skill.Hitlimit);
                                    Skill.HitList.Insert(0, gameObject);
                                }
                            }
                            else if (Skill.HitTarget[i].GetComponent<Number>().No > GetComponent<Number>().No)
                            {
                                Skill.HitTarget.Insert(i, gameObject);
                                Skill.HitTarget.RemoveAt(Skill.Hitlimit);
                                Skill.HitList.Insert(0, gameObject);
                            }
                        }
                        break;
                    }
                    if (Skill.HitList[i] == gameObject) break;
                }
            }
        }
    }

    void Update()
    {
        if (GetComponent<Debuff>().poisoned) time += Time.deltaTime;
        if (GetComponent<Debuff>().poisoned && time >= 1)
        {
            if (Status.NowHP <= GetComponent<Debuff>().PoisonDamage)
            {
                StartCoroutine(GetComponent<DamageUI>().Damage(((int)GetComponent<Debuff>().PoisonDamage), this.transform.position.x, this.transform.position.y));
                Status.NowHP = 1;
            }
            else
            {
                StartCoroutine(GetComponent<DamageUI>().Damage(((int)GetComponent<Debuff>().PoisonDamage), this.transform.position.x, this.transform.position.y));
                Status.NowHP -= (int)GetComponent<Debuff>().PoisonDamage;
            }
            time = 0;
        }
    }
}
