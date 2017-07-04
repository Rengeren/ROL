using UnityEngine;
using System.Collections;

public class BarAltCT : MonoBehaviour
{
    private CharacterStatus Status;
    public GameObject player;
    private float UseTime;
    private float BarY = 50f;
    private bool StartSkill;
    float Cooltime = 60f;
    // Use this for initialization
    void Start()
    {
        Status = player.GetComponent<CharacterStatus>();
    }

    public IEnumerator GrayBar()
    {
        yield return new WaitForSeconds(Cooltime);
        Status.AltCT = false;
        StartSkill = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Status.AltCT == true && StartSkill == false)
        {
            UseTime = Time.time;
            StartSkill = true;
            StartCoroutine("GrayBar");

        }
        if (StartSkill == true)
        {
            BarY = 50f * (Cooltime - (Time.time - UseTime)) / Cooltime;
            transform.localScale = new Vector3(this.transform.localScale.x, BarY, this.transform.localScale.z);
        }
    }
}
