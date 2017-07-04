using UnityEngine;
using System.Collections;

public class DarkLoadZ : MonoBehaviour {
    float time=0;
    void Start () {
        GetComponent<Animator>().SetTrigger("Start");
    }
	
	void Update () {
        time += Time.deltaTime;
        if (time >= 4) Destroy(gameObject);
    }
}
