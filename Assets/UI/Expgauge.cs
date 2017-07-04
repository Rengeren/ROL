using UnityEngine;
using System.Collections;

public class Expgauge : MonoBehaviour {
    public GameObject Player;
    private int MaxEXP;
    private int nowEXP;
    private CharacterStatus Status;
    // Use this for initialization
    void Start ()
    {
        this.Status = Player.GetComponent<CharacterStatus>();
    }
	
	// Update is called once per frame
	void Update () {
        MaxEXP = (int)CharacterStatus.Exptable[Status.Level - 1];
        nowEXP = Status.Exp;
        if (MaxEXP != 0)
        {
            if (nowEXP / MaxEXP > 1)
            {
                transform.localScale = new Vector3(310, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(310 * nowEXP / MaxEXP, 1, 1);
            }
        }
        else { transform.localScale = new Vector3(310, 1, 1); }
    }
}
