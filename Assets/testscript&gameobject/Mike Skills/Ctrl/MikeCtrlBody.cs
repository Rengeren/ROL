using UnityEngine;
using System.Collections;

public class MikeCtrlBody : MonoBehaviour {
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GameObject CtrlBreak;
    [HideInInspector]
    public int Durability = 3000;  //マイクのCtrl用
    [HideInInspector]
    public DamageCal damage;
    float time=0;

    void Start()
    {
        GetComponent<Animator>().SetTrigger("Start");
    }
    void Update()
    {
        time += Time.deltaTime;
        if (Durability <= 0)
        {
            CreateBreak();
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl)&&time>=0.5f || Input.GetKeyDown(KeyCode.RightControl) && time >= 0.5f)
        {
            CreateBreak();
        }
        else if (time > 6)
        {
            CreateBreak();
        }
    }
    void CreateBreak()
    {
        GameObject Attack = Instantiate(CtrlBreak, new Vector3(transform.position.x, transform.position.y, -20), Quaternion.identity) as GameObject;
        Attack.GetComponent<MikeCtrlBreak>().player = player;
        Attack.GetComponent<horming>().player = player;
        Durability = 0;
        damage.Ctrl = false;
        Destroy(gameObject);
    }
}
