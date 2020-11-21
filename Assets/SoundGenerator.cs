using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play() {
        transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<AudioSource>().Play();
    }
}
