using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameControllerAng : MonoBehaviour
{
    public PressurePlateAngela presureplate;
    public int repeatTimes = 1;
    public int TotalFoods = 6;
    private int currentRound = 0;
    public AudioClip[] requestAudios;
    public AudioClip[] FoodAudios;
    private bool repeat= true;
    private int[] randomFoodsList;
    private bool[] foodResults;

    private AudioSource CurrSound;

    public GameObject StartUI;
    public GameObject ResultsUI;
    public GameObject IncorrectUI;
    public GameObject CorrectUI;
    public GameObject ResultsText;
    public GameObject RepeatUI;

    // Start is called before the first frame update
    void Start()
    {
        foodResults = new bool[repeatTimes];
        CurrSound= gameObject.GetComponent<AudioSource>();
    }

    public void GameStarts()
    {
        currentRound = 0;
        // Create the list of random elements that are going to go in this setting
        List<int> elements = new List<int>();
        int foodId;
        for (int i = 0; i < repeatTimes; i++)
        {
            int count = 0;
            while (true)
            {
                foodId = UnityEngine.Random.Range(0, TotalFoods);
                if (!elements.Contains(foodId))
                {
                    count++;
                    elements.Add(foodId);
                    UnityEngine.Debug.Log(foodId);

                    break;
                }
              
            }
        }
        randomFoodsList = elements.ToArray();

        roundStarts();


    }

    public void roundStarts()
    {
        repeat= true;


        if (currentRound < repeatTimes)
        {
            
            presureplate.SetCorrectId(randomFoodsList[currentRound]);
            CurrSound.clip = requestAudios[randomFoodsList[currentRound]];
            UnityEngine.Debug.Log(randomFoodsList[currentRound]);
            CurrSound.Play();
        }
        else
        {
            GetResults();

            // finished
        }

    }

 

    //Called by preassurePlate to let the game know that the answer is right and it should go to the next round
    public void isCorrect()
    {
        UnityEngine.Debug.Log("isCorrect "+ currentRound);
        // mark it as correct
        foodResults[currentRound] = true;
        currentRound++;

        roundStarts();

    }

    //Called by preassurePlate to let the game know that the answer is wrong and it should repeat the sound or skip to the next one
    public void isIncorrect()
    {
        UnityEngine.Debug.Log("isIncorrect " + currentRound);

        if (repeat)
        {
            repeatAudio();
            repeat = false;
        }
        else
        {
            //sound that failed.
            foodResults[currentRound]=false;
            currentRound++;

            // next one and mark this as lost
            roundStarts();
        }
    }
    void GetResults()
    {
        string resultsText = "できあがり\n";
        for (int i = 0; i < foodResults.Length; i++)
        {
            resultsText += (i + 1).ToString() + /*". " + hiraganaArray[chosenChars[i]] + */ "\t";
            if (foodResults[i] == true)
                resultsText += "\u2713"; // check
            else
                resultsText += "\u2717"; // x
            resultsText += "\n";
        }

        ResultsText.GetComponent<TMPro.TextMeshProUGUI>().text = resultsText;
        ResultsUI.SetActive(true);
        RepeatUI.SetActive(false);
    }
    public void repeatAudio()
    {
        CurrSound.Play();
    }
}
