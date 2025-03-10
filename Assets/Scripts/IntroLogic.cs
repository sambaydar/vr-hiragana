using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLogic : MonoBehaviour
{
    public AudioSource LeeAudio;
    public AudioClip IntroAudio;
    public GameObject[] nextStep;
    public GameObject[] currentStep;

    private Coroutine runningCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        runningCoroutine = StartCoroutine(endAudio());
    }

    public void skipAudio()
    {
        LeeAudio.Stop();
        StopCoroutineIfRunning();
    }

    IEnumerator endAudio()
    {
        yield return new WaitForSeconds(10f);
        if (!this.gameObject.activeSelf) yield break;

        LeeAudio.clip = IntroAudio;
        LeeAudio.Play();
        yield return new WaitForSeconds(1f + LeeAudio.clip.length);
        if (!this.gameObject.activeSelf) yield break;

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

    private void OnDisable()
    {
        StopCoroutineIfRunning();
    }

    private void StopCoroutineIfRunning()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
    }
}
