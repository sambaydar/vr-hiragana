using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerAct2 : MonoBehaviour
{

    public AudioClip[] requestAudios;
    public AudioClip silence;
    private AudioSource CurrSound;
    // Start is called before the first frame update
    void Start()
    {
        CurrSound = gameObject.GetComponent<AudioSource>();
    }

    public void setAudioToSilence()
    {
        CurrSound.clip = silence;
    }

    public void buttonPressed(int id )
    {
        StartCoroutine(playAudio(id));
    }
    IEnumerator playAudio(int id)
    {
        CurrSound.clip = requestAudios[id];
        CurrSound.Play();
        yield return new WaitForSeconds(CurrSound.clip.length);
    }

}
