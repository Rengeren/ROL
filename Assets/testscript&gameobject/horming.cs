using UnityEngine;
using System.Collections;

public class horming : MonoBehaviour {
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public float Correction=0;
    Vector2 scale;
    void Update () {
        if (player == null) Destroy(gameObject);
        else
        {
            if (player.transform.localScale.x == 1)
            {
                scale = player.transform.localScale;
                scale.x = 1;
                transform.localScale = scale;
            }
            else
            {
                scale = player.transform.localScale;
                scale.x = -1;
                transform.localScale = scale;
            }
            Vector3 newPosition = player.transform.position;
            newPosition.x = player.transform.position.x;
            newPosition.y = player.transform.position.y+Correction;
            newPosition.z = -20;
            transform.position = newPosition;
        }
    }
}