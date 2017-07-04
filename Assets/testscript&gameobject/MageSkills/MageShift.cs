using UnityEngine;
using System.Collections;

public class MageShift : MonoBehaviour {
    void Start()
    {
        StartCoroutine("Delay");
    }
    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
