using UnityEngine;
using System.Collections;

public class HelenaShift : MonoBehaviour {
    [HideInInspector]
    public GameObject player;
	void Start () {
        GetComponent<Animator>().SetTrigger("Start");
        StartCoroutine("Destroy");
	}

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(6);
        player.GetComponent<CharacterStatus>().WalkSpeedCorrection -= 0.2f;
        player.GetComponent<CharacterStatus>().SpeedCal();
        player.GetComponent<HelenaSkills>().HelenaBuff = false;
        Destroy(gameObject);
    }
}
