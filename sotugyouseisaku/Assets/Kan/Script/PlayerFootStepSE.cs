using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStepSE : MonoBehaviour
{
    [SerializeField]
    private AudioClip step;
    private new AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StopStepSE()
    {
        audio.Stop();
    }

    void RunStepSE()
    {
        audio.PlayOneShot(step);
    }
}
