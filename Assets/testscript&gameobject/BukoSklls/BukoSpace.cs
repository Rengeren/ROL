using UnityEngine;
using System.Collections;

public class BukoSpace : MonoBehaviour {
    [HideInInspector]
    public GameObject BukoSpace_hit1;
    [HideInInspector]
    public GameObject BukoSpace_hit2;
    [HideInInspector]
    public GameObject BukoSpace_hit3;
    [HideInInspector]
    public GameObject BukoSpace_hit4;
    [HideInInspector]
    public GameObject BukoSpace_hit5;
    [HideInInspector]
    public GameObject BukoSpace_hit6;
    private SkillDetail Skill;
    int pattern;
    float time;

    public IEnumerator HitVanish()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<BoxCollider2D>().enabled = false;
        Skill.HitNum = 0;
        Skill.i = 0;
        Skill.HitTarget.Clear();
        Skill.HitList.Clear();
    }
    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    void Start () {
        GetComponent<Animator>().SetTrigger("Start");
        StartCoroutine("HitVanish");
        StartCoroutine("Destroy");
        Skill = GetComponent<SkillDetail>();
        pattern = Random.Range(1, 7);
        switch (pattern){
            case 1:
                Skill.HitEffect = BukoSpace_hit1;
                break;
            case 2:
                Skill.HitEffect = BukoSpace_hit2;
                break;
            case 3:
                Skill.HitEffect = BukoSpace_hit3;
                break;
            case 4:
                Skill.HitEffect = BukoSpace_hit4;
                break;
            case 5:
                Skill.HitEffect = BukoSpace_hit5;
                break;
            case 6:
                Skill.HitEffect = BukoSpace_hit6;
                break;
        }
    }

	void Update () {
        time += Time.deltaTime;
        if (time >= 0.7f)
        {
            time = 0;
            GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine("HitVanish");
        }
        pattern = Random.Range(1, 7);
        switch (pattern)
        {
            case 1:
                Skill.HitEffect = BukoSpace_hit1;
                break;
            case 2:
                Skill.HitEffect = BukoSpace_hit2;
                break;
            case 3:
                Skill.HitEffect = BukoSpace_hit3;
                break;
            case 4:
                Skill.HitEffect = BukoSpace_hit4;
                break;
            case 5:
                Skill.HitEffect = BukoSpace_hit5;
                break;
            case 6:
                Skill.HitEffect = BukoSpace_hit6;
                break;
        }
    }
}
