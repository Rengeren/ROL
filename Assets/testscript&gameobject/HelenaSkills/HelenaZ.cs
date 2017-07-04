using UnityEngine;
using System.Collections;

public class HelenaZ : MonoBehaviour {
    private SkillDetail Skill;
    float time=0;

    public IEnumerator DamageCorrection()
    {
        yield return new WaitForSeconds(0.05f);
        GetComponent<SkillDetail>().skillpercentage = 0.2f;
    }
    public IEnumerator HitVanish()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = false;
        Skill.HitNum = 0;
        Skill.i = 0;
        Skill.HitTarget.Clear();
        Skill.HitList.Clear();
    }
    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }

    void Start () {
        StartCoroutine("DamageCorrection");
        StartCoroutine("HitVanish");
        StartCoroutine("Destroy");
        Skill = GetComponent<SkillDetail>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 0.5f)
        {
            time = 0;
            GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine("HitVanish");
        }
    }
}
