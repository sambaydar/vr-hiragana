using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerAng : MonoBehaviour
{
    public PressurePlateAngela presureplate;
    public int repeatTimes = 1;
    public int TotalFoods = 6;

    public AudioClip[] requestAudios;
    public AudioClip[] FoodAudios;
    public bool repeat= true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameStarts()
    {
        // array of selected elements for the "game" (has to be <= totalfoods)
        // some track of the game started

    }

    public void RoundStarts()
    {
        // if round <= totalrounds
        ////Generate random number between 0 and the totalfoods-1 (it is not in the array) and add it to the array
        //// sends the id to pressureplate
        // else we finished here, show results and reset everything
    }

    //Called by preassurePlate to let the game know that the answer is right and it should go to the next round
    public void isCorrect()
    {

    }

    //Called by preassurePlate to let the game know that the answer is wrong and it should repeat the sound or skip to the next one
    public void isIncorrect()
    {
        if (repeat)
        {
            //repeat Request Audio
            //repeat= false;
        }
        else
        {
            //sound that failed.
            // next one and mark this as lost
        }
    }
}
