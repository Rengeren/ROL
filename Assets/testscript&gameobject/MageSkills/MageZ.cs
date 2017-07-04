using UnityEngine;
using System.Collections;

public class MageZ : MonoBehaviour {
    [HideInInspector]
    public GameObject MageZ_end;
    [HideInInspector]
    public AudioClip Z_EndSE;
    float time=1;
    float Endtime=0;
    private SkillDetail Skill;
    Vector2 scale;
    void Start()
    {
        GetComponent<Animator>().SetTrigger("Start");
        
    }

    void Update()
    {
        time -= Time.deltaTime;
        Endtime += Time.deltaTime;
        if (Endtime >= 6)
        {
            GetComponent<AudioSource>().PlayOneShot(Z_EndSE);
            GameObject End = Instantiate(MageZ_end, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
            if (transform.localScale.x == 1)
            {
                scale = End.transform.localScale;
                scale.x = -1;
                End.transform.localScale = scale;
            }
            Destroy(gameObject);
        }
        if (time <= 0.0)
        {
            time = 0.7f;
            StartCoroutine("Heal");
        }
    }

    public IEnumerator Heal()
    {
        SkillDetail Skill = GetComponent<SkillDetail>();
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<BoxCollider2D>().enabled = false;
        Skill.HitTarget.Clear();
        Skill.HitList.Clear();
    }
}
