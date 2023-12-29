using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRifle : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip AudioC;

    private AudioSource AudioS;

    // Start is called before the first frame update
     
    private void Awake()
    {
        AudioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        PlaySound(AudioC);
    }

    private void PlaySound(AudioClip clip) 
    {
        AudioS.Stop();
        AudioS.clip = clip;
        AudioS.Play();
        
    }
}
