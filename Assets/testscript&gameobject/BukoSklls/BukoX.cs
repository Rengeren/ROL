using UnityEngine;
using System.Collections;

public class BukoX : MonoBehaviour {
    float time;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            Destroy(gameObject);
        }
    }
}
