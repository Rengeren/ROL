using UnityEngine;
using System.Collections;

public class BukoZ : MonoBehaviour {
    float time;

    public IEnumerator HitVanish()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<BoxCollider2D>().enabled = false;
        SkillDetail Skill = GetComponent<SkillDetail>();
        Skill.HitTarget.Clear();
        Skill.HitList.Clear();
    }

    void Start()
    {
        StartCoroutine("HitVanish");
    }

    void Update () {
        time += Time.deltaTime;
        if (time >= 0.7f)
        {
            time = 0;
            GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine("HitVanish");
        }
    }
}
