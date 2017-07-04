using UnityEngine;
using System.Collections;

public class DarkLoadX : MonoBehaviour {
    [HideInInspector]
    public GameObject PoisonMark;
    [HideInInspector]
    public GameObject SoundManager;
    [HideInInspector]
    public float distance;
    [HideInInspector]
    public bool poison=false;
    [HideInInspector]
    public float PoisonDamage;
    float length;
    float Firstposition;
    SkillDetail Skill;
    bool destroy=false;

    void Start()
    {
        Firstposition = transform.position.x;
        if(distance>0)  GetComponent<Rigidbody2D>().velocity = new Vector2(800, 0);
        else GetComponent<Rigidbody2D>().velocity = new Vector2(-800, 0);
        length = Mathf.Abs(distance);
        Skill=GetComponent<SkillDetail>();
    }

    void Update()
    {
        if (destroy) Destroy(gameObject);
        if (Skill.HitNum >= 1&&Skill.HitTarget!=null)
        {
            if (poison&&Skill.HitTarget[0].GetComponent<Debuff>().poisoned!=true)
            {
                GameObject Poisondummy = Instantiate(PoisonMark, new Vector3(Skill.HitTarget[0].transform.position.x, Skill.HitTarget[0].transform.position.y, -20), Quaternion.identity) as GameObject;
                Poisondummy.GetComponent<horming>().player = Skill.HitTarget[0];
                Skill.HitTarget[0].GetComponent<Debuff>().PoisonMark = Poisondummy;
                Skill.HitTarget[0].GetComponent<Debuff>().PoisonDamage = PoisonDamage;
                Skill.HitTarget[0].GetComponent<Debuff>().poisoned = true;
            }
            else if(poison && Skill.HitTarget[0].GetComponent<Debuff>().poisoned)
            {
                Skill.HitTarget[0].GetComponent<Debuff>().PoisonDamage = PoisonDamage;
                Skill.HitTarget[0].GetComponent<Debuff>().PoisonTime = 0;
            }
            destroy = true;
        }
    }
    void FixedUpdate ()
    {
        if (Mathf.Abs(transform.position.x - Firstposition) >= length) Destroy(gameObject); 
    }
}
