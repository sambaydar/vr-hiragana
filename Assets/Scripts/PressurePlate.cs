using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script connected to the pressure plate to detect which food
// collided with the plate
public class PressurePlate : MonoBehaviour
{
    public AudioClip CorrectSound;
    public AudioClip IncorrectSound;
    public GameObject CorrectUI;
    public GameObject IncorrectUI;
    public AnimationStateController asc;

    private AudioSource CurrSound;
    private bool answerSubmitted;
    private bool correct;

    void OnTriggerEnter(Collider other) {
        if (!answerSubmitted) {
            if (other.gameObject.tag != "Untagged") {
                CurrSound.clip = CorrectSound;
                CurrSound.Play();
                CorrectUI.SetActive(true);
                correct = true;
                asc.ApprovalAnimation();
            } else {
                CurrSound.clip = IncorrectSound;
                CurrSound.Play();
                IncorrectUI.SetActive(true);
                correct = false;
                asc.RejectionAnimation();
            }
        }
        answerSubmitted = true;
    }

    public bool IsAnswerSubmitted() {
        return answerSubmitted;
    }

    public void ResetAnswerSubmitted() {
        answerSubmitted = false;
    }

    public bool IsCorrect() {
        return correct;
    }

    void Start()
    {
        CurrSound = GetComponent<AudioSource>();
        answerSubmitted = false;
        correct = false;
    }

}
