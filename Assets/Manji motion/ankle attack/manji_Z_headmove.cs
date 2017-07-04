using UnityEngine;
using System.Collections;

public class manji_Z_headmove : MonoBehaviour {

    private GameObject parent;
    public float mx = -10;
    public float my = -10;

	void Start () {
	    parent = gameObject.transform.parent.gameObject;
    }
	
	void Update () {
        if (parent.GetComponent<HelenaSkills>().useZ == true){
            transform.Translate(mx, my, transform.position.z+1);
            parent.GetComponent<HelenaSkills>().useZ = false;
        }
        if (parent.GetComponent<HelenaSkills>().endZ == true)
        {
            transform.Translate(-mx, -my, transform.position.z + 1);
            parent.GetComponent<HelenaSkills>().endZ = false;
        }
    }
}
