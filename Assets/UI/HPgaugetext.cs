using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPgaugetext : MonoBehaviour {
    public GameObject Player;
    private int MaxHP;
    private int nowHP;
    private int MaxEXP;
    private int nowEXP;
    public GameObject uiText; //HP表示
    public GameObject uiText2; //EXP表示
    public GameObject ActorText; //職業表示
    private CharacterStatus Status;
    void Start()
    {
        this.Status = Player.GetComponent<CharacterStatus>();
    }

    void Update()
    {
        MaxHP = (int)Status.MaxHP;
        nowHP = Status.NowHP;
        MaxEXP = (int)CharacterStatus.Exptable[Status.Level - 1];
        nowEXP = Status.Exp;

        //Debug.Log(uiText.GetComponent<Text>().text);  // 現テキストをコンソールに表示
        uiText.GetComponent<Text>().text = "["+nowHP+"/"+MaxHP+"]";  // テキストを変更
        uiText2.GetComponent<Text>().text = "[" + nowEXP + "/" + MaxEXP + "]";
        switch (Status.Avator)
        {
            case 1:
                ActorText.GetComponent<Text>().text = "マンジ";
                break;
            case 2:
                ActorText.GetComponent<Text>().text = "マイク";
                break;
            case 3:
                ActorText.GetComponent<Text>().text = "ダークロード";
                break;
            case 4:
                ActorText.GetComponent<Text>().text = "ハインズ";
                break;
            case 5:
                ActorText.GetComponent<Text>().text = "武公";
                break;
            case 6:
                ActorText.GetComponent<Text>().text = "ヘレナ";
                break;
        }

    }
}
