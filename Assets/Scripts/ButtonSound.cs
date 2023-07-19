using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private AudioSource CurrSound;
    public AudioClip StartSound;

    // plays a sound when button is clicked
    void Start()
    {
        CurrSound = GetComponent<AudioSource>();
        CurrSound.clip = StartSound;
        CurrSound.Play();
    }

    void OnEnable() {
        CurrSound.clip = StartSound;
        CurrSound.Play();
        gameObject.SetActive(false);
    }
}