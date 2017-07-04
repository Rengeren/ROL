using UnityEngine;
using System.Collections;

public class BGmove : MonoBehaviour {
    public GameObject player;
    public float mapleftlimit = -35;
    public float maprightlimit = 4044;
    public float mapunderlimit = -1800;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 newPosition = transform.position;
        newPosition.x = player.transform.position.x-150f;
        newPosition.y = player.transform.position.y - 1800f ;
        newPosition.z = 50;


        if (newPosition.x <= mapleftlimit)
        {
            newPosition.x = mapleftlimit;

        }

        else if (newPosition.x >= maprightlimit)
        {
            newPosition.x = maprightlimit;

        }

        if (newPosition.y <= mapunderlimit)
        {
            newPosition.y = mapunderlimit;

        }
        transform.position = Vector3.Lerp(transform.position, newPosition, 10.0f * Time.deltaTime);

    
}
}
