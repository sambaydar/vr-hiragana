using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script connected to the pressure plate to detect which food
// collided with the plate
public class PressurePlateAngela : MonoBehaviour
{
    public AudioClip CorrectSound;
    public AudioClip IncorrectSound;
    public GameObject CorrectUI;
    public GameObject IncorrectUI;
    public AnimationStateController asc;
    public AudioClip[] CorrectSays;
    public AudioClip[] IncorrectSays;
    private int correctFoodId=-1;
    private AudioSource CurrSound;

    private bool correct;
    public GameControllerAng gameController;

    void OnTriggerEnter(Collider other) {
        if (correctFoodId>-1)
        { 
            if (other.gameObject.tag != "Untagged") {
                CurrSound.clip = CorrectSound;
                // Random correct of array is the new clip.
                CurrSound.Play();
                CorrectUI.SetActive(true);
                correct = true;
                asc.ApprovalAnimation();
                gameController.isCorrect();
            } else {
                CurrSound.clip = IncorrectSound;
                CurrSound.Play();
                IncorrectUI.SetActive(true);
                correct = false;
                asc.RejectionAnimation();
                gameController.isIncorrect();
            }
        }
        else
        {
            // Please start the game
        }
    }

   public void SetCorrectId(int id)
    { correctFoodId = id; }


    
    public bool IsCorrect() {
        return correct;
    }

    void Start()
    {
        CurrSound = GetComponent<AudioSource>();

        correct = false;
    }

}
