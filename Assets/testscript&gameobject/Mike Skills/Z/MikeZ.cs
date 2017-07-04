using UnityEngine;
using System.Collections;

public class MikeZ : MonoBehaviour {
    [HideInInspector]
    public GameObject MikeZ_end;
    [HideInInspector]
    public AudioClip Z_EndSE;
    bool start=true;
    float time=0;

    public IEnumerator Judge()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        if (GetComponent<BoxCollider2D>() != null) GetComponent<BoxCollider2D>().enabled = false;
        SkillDetail Skill = GetComponent<SkillDetail>();
        Skill.HitTarget.Clear();
        Skill.HitList.Clear();

        yield return new WaitForSeconds(0.5f);

        start = true;
    }
	void Update () {
        time += Time.deltaTime;
	    if (start)
        {
            start = false;
            StartCoroutine("Judge");
        }
        if(time>6)
        {
            GetComponent<AudioSource>().PlayOneShot(Z_EndSE);
            GameObject Zend= Instantiate(MikeZ_end, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity)as GameObject;
            Zend.GetComponent<horming>().player = GetComponent<horming>().player;
            Destroy(gameObject);
        }
	}
}
