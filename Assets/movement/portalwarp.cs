using UnityEngine;
using System.Collections;

public class portalwarp : MonoBehaviour
{
    public Vector3 warppoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<move>().warppoint = warppoint;
        other.GetComponent<move>().onportal = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<move>().onportal = false;
    }
}
