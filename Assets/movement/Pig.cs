using UnityEngine;
using System.Collections;
//Layer:Kinoko,KinokoHook

public class Pig : MonoBehaviour
{
    public GameObject mob;
    //移動用
    private float walkForce = 150f;
    private float maxwalkspeed = 120f;
    private float targetPosition;
    private float time = 0;
    private bool stopmove = true;
    //移動範囲の制限
    private float MaxX;
    private float MinX;
    private float MaxX1 = 3070;
    private float MinX1 = 851;
    private float MaxX2 = 2908;
    private float MinX2 = 1118;

    void start()
    {
        targetPosition = transform.position.x;
    }

    void FixedUpdate()
    {
        time = time + Time.deltaTime;
        if (time >= 4f)
        {
            time = 0;
            stopmove = false;
            targetPosition = transform.position.x + Random.Range(-600, 600);

            if (transform.position.y >= -31.29005)
            {
                MinX = MinX2;
                MaxX = MaxX2;
            }
            else
            {
                MinX = MinX1;
                MaxX = MaxX1;
            }
            if (targetPosition > MaxX) targetPosition = MaxX;
            else if (targetPosition < MinX) targetPosition = MinX;
        }

        //移動
        if (stopmove != true && mob.GetComponent<Transform>().position.x > targetPosition && mob.GetComponent<Rigidbody2D>().velocity.x > maxwalkspeed * -1)
        {
            Vector2 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
            mob.GetComponent<Rigidbody2D>().AddForce(Vector2.left * walkForce);
            if (mob.GetComponent<Transform>().position.x <= targetPosition + 10f) stopmove = true;
        }
        if (stopmove != true && mob.GetComponent<Transform>().position.x < targetPosition && mob.GetComponent<Rigidbody2D>().velocity.x < maxwalkspeed)
        {
            Vector2 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
            mob.GetComponent<Rigidbody2D>().AddForce(Vector2.right * walkForce);
            if (mob.GetComponent<Transform>().position.x >= targetPosition - 10f) stopmove = true;
        }
    }
}
