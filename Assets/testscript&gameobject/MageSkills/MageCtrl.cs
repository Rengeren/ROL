using UnityEngine;
using System.Collections;

public class MageCtrl : MonoBehaviour {
    [HideInInspector]
    public GameObject SEmanager;
    [HideInInspector]
    public AudioClip EndSE;
    [HideInInspector]
    public float distance;
    float length;
    float Firstposition;
    private float mLength=0;
    private float mCur;
    private SkillDetail Skill;
    float time;

    public IEnumerator HitVanish()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<BoxCollider2D>().enabled = false;
        Skill.HitTarget.Clear();
        Skill.HitList.Clear();
    }
    void Start()
    {
        Skill=GetComponent<SkillDetail>();
        Firstposition = transform.position.x;
        if (distance > 0) GetComponent<Rigidbody2D>().velocity = new Vector2(50, 0);
        else GetComponent<Rigidbody2D>().velocity = new Vector2(-50, 0);
        length = Mathf.Abs(distance);
        mCur = 0;
        GetComponent<Animator>().SetTrigger("Start");
        StartCoroutine("HitVanish");
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
        if (mLength != 0)
        {
            mCur += Time.deltaTime;
            if (mCur > mLength)
            {
                GameObject SE = Instantiate(SEmanager, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
                SE.GetComponent<SoundManager>().SE = EndSE;
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - Firstposition) >= length)
        {
            GetComponent<Animator>().SetTrigger("End");
            mLength=GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
    }
}
