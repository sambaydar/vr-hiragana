using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLogic : MonoBehaviour
{
    public AudioSource LeeAudio;
    public AudioClip IntroAudio;
    public GameObject[] nextStep;
    public GameObject[] currentStep;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(endAudio());
    }
    public void skipAudio()
    {
        LeeAudio.Stop();
    }
    IEnumerator endAudio()
    {
        yield return new WaitForSeconds(10f);

        LeeAudio.clip = IntroAudio;
        LeeAudio.Play();
        yield return new WaitForSeconds(1f+ LeeAudio.clip.length);

        foreach (GameObject item in nextStep)
        {
            item.SetActive(true);
        }
        foreach (GameObject item2 in currentStep)
        {
            item2.SetActive(false);

        }
        this.gameObject.SetActive(false);
    }

}
