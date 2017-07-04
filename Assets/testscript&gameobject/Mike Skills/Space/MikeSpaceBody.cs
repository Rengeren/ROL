using UnityEngine;
using System.Collections;

public class MikeSpaceBody : MonoBehaviour {
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GameObject SpaceBreak;
    [HideInInspector]
    public AudioClip SpaceHeal;
    [HideInInspector]
    public CharacterStatus Status;
    [HideInInspector]
    private SkillDetail Skill;
    float time = 0;
    float time2 = 1;

    void Start()
    {
        GetComponent<Animator>().SetTrigger("Start");
        Skill = GetComponent<SkillDetail>();
        Skill.level = Status.Level;
        if (Status.Level <= 10) Skill.skillpercentage = -2.5f;
        else if (Status.Level > 10) Skill.skillpercentage = -4;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && time >= 0.5f)
        {
            CreateBreak();
        }
        else if (time > 10)
        {
            CreateBreak();
        }
        time2 -= Time.deltaTime;
        if (time2 <= 0.0)
        {
            time2 = 1.0f;
            StartCoroutine("Heal");
        }
    }
    void CreateBreak()
    {
        GameObject Attack = Instantiate(SpaceBreak, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
        Attack.GetComponent<SkillDetail>().level = Skill.level;
        Attack.GetComponent<MikeSpaceBreak>().player = player;
        Attack.GetComponent<horming>().player = player;
        Destroy(gameObject);
    }
    public IEnumerator Heal()
    {
        Skill.MinATK = Status.minATK;
        Skill.MaxATK = Status.maxATK;
        Skill.criticalpercentage = Status.criticalpercentage;
        Skill.HitSE = SpaceHeal;
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<BoxCollider2D>().enabled = false;
        Skill.HitTarget.Clear();
        Skill.HitList.Clear();
    }
}
