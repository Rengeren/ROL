using UnityEngine;
using System.Collections;

public class MikeShift : MonoBehaviour {
    SkillDetail Skill;
	void Start () {
        Skill = GetComponent<SkillDetail>();
        StartCoroutine("SetActive");
	}
    public IEnumerator SetActive()
    {
        yield return new WaitForSeconds(0.4f);
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        if (GetComponent<BoxCollider2D>() != null) GetComponent<BoxCollider2D>().enabled = false;
        Skill.HitNum = 0;
        Skill.Hitlimit = 3;
        Skill.HitTarget.Clear();
        Skill.HitList.Clear();
        Skill.skillpercentage = 4;
        yield return new WaitForSeconds(0.9f);
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        if (GetComponent<BoxCollider2D>() != null) GetComponent<BoxCollider2D>().enabled = false;
    }
}
