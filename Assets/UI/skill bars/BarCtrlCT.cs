using UnityEngine;
using System.Collections;

public class BarCtrlCT : MonoBehaviour
{
    private CharacterStatus Status;
    public GameObject player;
    private float UseTime;
    private float BarY = 50f;
    private bool StartSkill;
    float[] Cooltime;
    // Use this for initialization
    void Start()
    {
        Status = player.GetComponent<CharacterStatus>();
        Cooltime = new float[7] { 0, player.GetComponent<ManjiSkill>().Ctrlct, player.GetComponent<MikeSkills>().Ctrlct, player.GetComponent<DarkLoadSkills>().Ctrlct, player.GetComponent<MageSkills>().Ctrlct, player.GetComponent<BukoSkills>().Ctrlct, player.GetComponent<HelenaSkills>().Ctrlct };
    }

    public IEnumerator GrayBar()
    {
        yield return new WaitForSeconds(Cooltime[player.GetComponent<CharacterStatus>().Avator]);
        Status.CtrlCT = false;
        StartSkill = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Status.CtrlCT == true && StartSkill == false)
        {
            UseTime = Time.time;
            StartSkill = true;
            StartCoroutine("GrayBar");

        }
        if (StartSkill == true)
        {
            BarY = 50f * (Cooltime[player.GetComponent<CharacterStatus>().Avator] - (Time.time - UseTime)) / Cooltime[player.GetComponent<CharacterStatus>().Avator];
            transform.localScale = new Vector3(this.transform.localScale.x, BarY, this.transform.localScale.z);
        }
    }
}
