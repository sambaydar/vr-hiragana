using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// script connected to the pressure plate to detect which food
// collided with the plate
public class PressurePlateAngela : MonoBehaviour
{
    public AudioClip CorrectSound;
    public AudioClip IncorrectSound;
    public AnimationStateController asc;
    private int correctFoodId=-1;
    private AudioSource CurrSound;
    public GameControllerAng gameController;
    private bool round= true;
    void OnTriggerEnter(Collider other) {
        //UnityEngine.Debug.Log(other.gameObject.name);
        if (correctFoodId>-1)
        { 
            if (other.gameObject.tag == "food") {
                FoodClass script= other.gameObject.GetComponent<FoodClass>();
                if (script.FoodId== correctFoodId && round)
                {
                    UnityEngine.Debug.Log("entered" );
                    CurrSound.clip = CorrectSound;
                    CurrSound.Play();
                    asc.ApprovalAnimation();
                    gameController.isCorrect();
                }
                else {
                    
                        CurrSound.clip = IncorrectSound;
                        CurrSound.Play();
                        asc.RejectionAnimation();
                        gameController.isIncorrect();
                }
                script.resetTransform();
                round = false;

            }
        }
        else
        {
            // Please start the game
        }
    }

   public void SetCorrectId(int id)
    {
        correctFoodId = id;
        round = true;
    }


    void Start()
    {
        CurrSound = GetComponent<AudioSource>();
    }

}
