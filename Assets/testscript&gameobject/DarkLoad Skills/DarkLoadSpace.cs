using UnityEngine;
using System.Collections;

public class DarkLoadSpace : MonoBehaviour {
    [HideInInspector]
    public GameObject Soundmanager;
    [HideInInspector]
    public GameObject SpaceEnd;
    [HideInInspector]
    public AudioClip End_SE;
    float time=0;
    float Endtime=0;
    SkillDetail Skill;
    Vector2 scale;

    public IEnumerator HitVanish()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<BoxCollider2D>().enabled = false;
        //Skill.HitNum = 0;
        Skill.HitTarget.Clear();
        Skill.HitList.Clear();
    }

    void Start()
    {
        Skill = GetComponent<SkillDetail>();
        StartCoroutine("HitVanish");
    }

    void Update()
    {
        time += Time.deltaTime;
        Endtime += Time.deltaTime;
        if (time >= 0.8f)
        {
            time = 0;
            GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine("HitVanish");
        }
        if (Endtime >= 10)
        {
            Instantiate(SpaceEnd, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity);
            if (transform.localScale.x == 1)
            {
                scale = SpaceEnd.transform.localScale;
                scale.x = -1;
                SpaceEnd.transform.localScale = scale;
            }
            GameObject SE=Instantiate(Soundmanager, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity)as GameObject;
            SE.GetComponent<SoundManager>().SE = End_SE;
            Destroy(gameObject);
        }
    }
}
