using UnityEngine;
using System.Collections;

public class HelenaSpace : MonoBehaviour {
    private SkillDetail Skill;
    [HideInInspector]
    public GameObject SEmanager;
    [HideInInspector]
    public GameObject Attack;
    [HideInInspector]
    public GameObject Eff;
    [HideInInspector]
    public GameObject Arrow;
    [HideInInspector]
    public AudioClip EndSE;
    private GameObject Eff2;
    float time = 0;
    float time2 = 1;
    private float mlength;
    bool Stop=false;

    void Start () {
        Skill = GetComponent<SkillDetail>();
        StartCoroutine("Appear");
	}
    public IEnumerator Appear()
    {
        mlength = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(mlength);
        Eff.GetComponent<Animator>().SetTrigger("Start");
        GetComponent<AudioSource>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        Eff2 = Instantiate(Arrow, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
        Eff2.GetComponent<horming>().player = gameObject;
    }
    public IEnumerator Destroy()
    {
        Destroy(Eff2);
        GetComponent<AudioSource>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("End");
        Eff.GetComponent<Animator>().SetTrigger("End");
        GameObject SE = Instantiate(SEmanager, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
        SE.GetComponent<SoundManager>().SE = EndSE;
        Skill.WhoseSkill.GetComponent<CharacterStatus>().canmove = true;
        Skill.WhoseSkill.GetComponent<HelenaSkills>().Delayend = true;
        yield return new WaitForSeconds(0.3f);
        Destroy(Eff);
        Destroy(Attack);
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

    void Update () {
        if (Stop != true)
        {
            time += Time.deltaTime;
            time2 += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && time >= 0.5f && Stop != true)
        {
            Stop = true;
            StartCoroutine("Destroy");
        }
        if (time >= 5 && Stop != true)
        {
            Stop = true;
            StartCoroutine("Destroy");
        }
        if (time2 >= 0.7f&&Stop!=true)
        {
            time2 = 0;
            GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine("HitVanish");
        }

        //動かす
        if (Input.GetKey(KeyCode.UpArrow) && Stop != true) transform.position += new Vector3(0, 300 * Time.deltaTime);
        else if (Input.GetKey(KeyCode.DownArrow) && Stop != true) transform.position += new Vector3(0, -300 * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow) && Stop != true) transform.position += new Vector3(300 * Time.deltaTime,0);
        else if (Input.GetKey(KeyCode.LeftArrow) && Stop != true) transform.position += new Vector3(-300 * Time.deltaTime,0);
    }
}
