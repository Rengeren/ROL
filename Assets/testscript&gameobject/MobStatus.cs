using UnityEngine;
using System.Collections;

public class MobStatus : MonoBehaviour {
    [HideInInspector]
    public GameObject LastAttack;
    public int HP=1000;
    public int EXP = 6;
    public int Gold = 9;
    //float damage;
    //int critical;
    private float mLength;
    private Animator animOne;
    [HideInInspector]
    public AudioClip DeathSE;
    float time=0;
    int i;
    bool end=false;

    void Start()
    {
        animOne = GetComponent<Animator>();
    }
    public IEnumerator Deathtime()
    {
        yield return new WaitForSeconds(mLength*2);
        Destroy(gameObject);
    }
    public void CreateDamage(int damage)
    {
        StartCoroutine(GetComponent<DamageUI>().Damage(damage, this.transform.position.x, this.transform.position.y));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Skill")
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
                    if (Skill.HitList[i] != gameObject && i == Skill.HitList.Count-1)
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
            if (HP <= GetComponent<Debuff>().PoisonDamage)
            {
                StartCoroutine(GetComponent<DamageUI>().Damage(((int)GetComponent<Debuff>().PoisonDamage), this.transform.position.x, this.transform.position.y));
                HP = 1;
            }
            else
            {
                StartCoroutine(GetComponent<DamageUI>().Damage(((int)GetComponent<Debuff>().PoisonDamage), this.transform.position.x, this.transform.position.y));
                HP -= (int)GetComponent<Debuff>().PoisonDamage;
            }
            time = 0;
        }
        if (HP <= 0&&end!=true)
        {
            end = true;
            GetComponent<AudioSource>().PlayOneShot(DeathSE);
            LastAttack.GetComponent<CharacterStatus>().HaveGold += Gold * 5;
            LastAttack.GetComponent<CharacterStatus>().Exp += EXP * 5;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Kinoko>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Animator>().SetTrigger("Death");
            AnimatorStateInfo infAnim = animOne.GetCurrentAnimatorStateInfo(0);
            mLength = infAnim.length;
            StartCoroutine("Deathtime");
        }
    }
}
