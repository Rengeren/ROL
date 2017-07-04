using UnityEngine;
using System.Collections;

public class BukoShift : MonoBehaviour {


    public IEnumerator SetActive()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<BoxCollider2D>().enabled = true;
    }
    void Start () {
        StartCoroutine("SetActive");
	}
	
	void Update () {
	
	}
}
