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
    private bool roundStarted = true;
    public GameControllerAng gameController;
    private bool repeat = false;

    void OnTriggerEnter(Collider other) {
        //UnityEngine.Debug.Log(other.gameObject.name);
        if (correctFoodId>-1)
        { 
            if (other.gameObject.tag == "food") {
                FoodClass script= other.gameObject.GetComponent<FoodClass>();
                if (script.FoodId== correctFoodId && !repeat)
                {
                    CurrSound.clip = CorrectSound;
                    CurrSound.Play();
                    asc.ApprovalAnimation();
                    gameController.isCorrect();
                    roundStarted = false;
                    repeat = true;
                }
                else if(!(script.FoodId == correctFoodId))
                {
                    if (!repeat)
                    {
                    
                        CurrSound.clip = IncorrectSound;
                        CurrSound.Play();
                        asc.RejectionAnimation();
                        gameController.isIncorrect();
                        repeat = true;
                    }
                    else
                    {
                        if(roundStarted)
                        {
                            roundStarted = false;
                            CurrSound.clip = IncorrectSound;
                            CurrSound.Play();
                            asc.RejectionAnimation();
                            gameController.isIncorrect();

                        }
                    }
                }
                script.resetTransform();
            }
        }
        else
        {
            // Please start the game
        }
    }

   public void SetCorrectId(int id)
    {
        roundStarted = true;
        repeat = false;
        correctFoodId = id; }
   

    void Start()
    {
        CurrSound = GetComponent<AudioSource>();
    }

}
