using UnityEngine;
using System.Collections;

public class MikeCtrlBreak : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GameObject MikeCtrl_hit;
    [HideInInspector]
    public AudioClip Ctrl_BreakSE;
    [HideInInspector]
    public AudioClip Ctrl_HitSE;
    [HideInInspector]
    public GameObject SwoonedMark;
    private CharacterStatus Status;

    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(Ctrl_BreakSE);
        CharacterStatus Status = player.GetComponent<CharacterStatus>();
        //値獲得
        SkillDetail Skill = GetComponent<SkillDetail>();
        Skill.WhoseSkill = player;
        Skill.level = Status.Level;
        Skill.PlayerNumber = Status.PlayerNumber;
        Skill.MinATK = Status.minATK;
        Skill.MaxATK = Status.maxATK;
        Skill.criticalpercentage = Status.criticalpercentage;
        //スキルの固有値
        Skill.HitEffect = MikeCtrl_hit;
        Skill.HitSE = Ctrl_HitSE;
        Skill.skillpercentage = 2.5f;
        Skill.Hitlimit = 100;
        Skill.debuffType = 6;
        Skill.debuff = SwoonedMark;

        StartCoroutine("Judge");
    }
    public IEnumerator Judge()
    {
        yield return new WaitForSeconds(1.2f);
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
