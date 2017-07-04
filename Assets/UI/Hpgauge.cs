using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hpgauge : MonoBehaviour
{
    public GameObject Player;
    private int MaxHP;
    private int nowHP;
    private CharacterStatus Status;
    void Start()
    {
        this.Status = Player.GetComponent<CharacterStatus>();
    }

    void Update()
    {
        MaxHP = (int)Status.MaxHP;
        nowHP = Status.NowHP;
        if (nowHP / MaxHP > 1)
        {
            transform.localScale = new Vector3(310, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(310 * nowHP / MaxHP, 1, 1);
        }
    }


}