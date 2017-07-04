using UnityEngine;
using System.Collections;

public class DarkLoadCtrl : MonoBehaviour {
    [HideInInspector]
    public float distance;
    float length;
    float Firstposition;

    void Start()
    {
        Firstposition = transform.position.x;
        if (distance > 0) GetComponent<Rigidbody2D>().velocity = new Vector2(450, 0);
        else GetComponent<Rigidbody2D>().velocity = new Vector2(-450, 0);
        length = Mathf.Abs(distance);
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - Firstposition) >= length) Destroy(gameObject);
    }
}
