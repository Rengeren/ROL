using UnityEngine;
using System.Collections;

public class HelenaX : MonoBehaviour {
    [HideInInspector]
    public float distance;
    float length;
    float Firstposition;
    SkillDetail Skill;
    bool destroy = false;
    void Start()
    {
            Firstposition = transform.position.x;
            if (distance > 0) GetComponent<Rigidbody2D>().velocity = new Vector2(800, 0);
            else GetComponent<Rigidbody2D>().velocity = new Vector2(-800, 0);
            length = Mathf.Abs(distance);
            Skill = GetComponent<SkillDetail>();
    }

	void Update () {
        if (destroy) Destroy(gameObject);
        if (Skill.HitNum== Skill.Hitlimit && Skill.HitTarget != null)
        {
            destroy = true;
        }
    }
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - Firstposition) >= length) Destroy(gameObject);
    }
}
