using UnityEngine;
using System.Collections;

public class MageSpace : MonoBehaviour {
    
    void Start () {
        StartCoroutine("Hit");
	}

    public IEnumerator Hit()
    {
        yield return new WaitForSeconds(1);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
