using UnityEngine;
using System.Collections;

public class ManjiSpaceDestroy : MonoBehaviour {
    [HideInInspector]
    public GameObject player;
    public IEnumerator HyperTime()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }
    void Start () {
        StartCoroutine("HyperTime");
	}

}
