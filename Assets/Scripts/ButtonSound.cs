using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private AudioSource CurrSound;
    public AudioClip StartSound;
    private bool first = true;
    public AudioClip Silence;
    // plays a sound when button is clicked
    void Start()
    {
        CurrSound = GetComponent<AudioSource>();
        CurrSound.clip = StartSound;
        CurrSound.Play();
    }

    void OnDisable()
    {
        
            CurrSound.clip = Silence;
        
    }

    void OnEnable()
    {
            if(first)
        {
            first = false;
        }
        else
        {
            CurrSound.clip = StartSound;

        }

    }
    public void sound() {
        
        CurrSound.Play();
        //gameObject.SetActive(false);
    }
}