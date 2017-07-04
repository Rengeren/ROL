using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public AudioClip HitSE;
    public AudioClip SE;

    void Start()
    {
        StartCoroutine("Destroy");
        if(HitSE!=null) GetComponent<AudioSource>().PlayOneShot(HitSE);
        if(SE!=null)    GetComponent<AudioSource>().PlayOneShot(SE);
    }
    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
