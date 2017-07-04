using UnityEngine;
using System.Collections;

public class ManjiHit : MonoBehaviour {
    float time;
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 0.1f) GetComponent<BoxCollider2D>().enabled = false;
    }
}
