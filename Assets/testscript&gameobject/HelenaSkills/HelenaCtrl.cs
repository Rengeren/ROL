using UnityEngine;
using System.Collections;

public class HelenaCtrl : MonoBehaviour {
    bool start = false;
    [HideInInspector]
    public int angle;
    public IEnumerator Appear()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().material.color += new Color(0, 0, 0, 1);
        start = true;
    }
    void Start () {
        StartCoroutine("Appear");
	}
	
	void Update () {
	    if(start) GetComponent<Renderer>().material.color -= new Color(0, 0, 0, 1*Time.deltaTime);
        if(transform.localScale.x==1)   transform.position += new Vector3(-350 *Mathf.Cos(angle * Mathf.Deg2Rad) * Time.deltaTime, -350 * Mathf.Sin(angle * Mathf.Deg2Rad) * Time.deltaTime, 0);
        else transform.position += new Vector3(350 * Mathf.Cos(angle * Mathf.Deg2Rad) * Time.deltaTime, 350 * Mathf.Sin(angle * Mathf.Deg2Rad) * Time.deltaTime, 0);
        if (start&&GetComponent<Renderer>().material.color.a <= 0.1f) Destroy(gameObject);
        if (GetComponent<SkillDetail>().HitNum >= GetComponent<SkillDetail>().Hitlimit) Destroy(gameObject);
    }
}
