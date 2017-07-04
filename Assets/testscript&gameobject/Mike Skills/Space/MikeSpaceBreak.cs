using UnityEngine;
using System.Collections;

public class MikeSpaceBreak : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GameObject MikeSpace_hit;
    [HideInInspector]
    public AudioClip Space_BreakSE;
    [HideInInspector]
    public AudioClip Space_HitSE;
    private CharacterStatus Status;

    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(Space_BreakSE);
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
        Skill.HitEffect = MikeSpace_hit;
        Skill.HitSE = Space_HitSE;
        if (Status.Level <= 10) Skill.skillpercentage = 6f;
        else if (Status.Level > 10) Skill.skillpercentage = 10f;
        Skill.Hitlimit = 100;

        StartCoroutine("Judge");
    }
    public IEnumerator Judge()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
