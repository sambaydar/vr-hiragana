using System;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameControllerAng : MonoBehaviour
{
    public PressurePlateAngela presureplate;
    public int repeatTimes = 1;
    public int TotalFoods = 6;
    private int currentRound = 0;
    public AudioClip[] requestAudios;
    public AudioClip[] thisIsAudios;
    public AudioClip[] thisIsWrongAudios;

    public AudioClip CorrectAudio;
    public AudioClip IncorrectAudio;
    public AudioClip onegaiaudio;

    private bool repeat= true;
    private int[] randomFoodsList;
    private bool[] foodResults;
    private AudioSource CurrSound;
    private int[] foodAnswers;
    public GameObject StartUI;
    public GameObject ResultsUI;
    public GameObject IncorrectUI;
    public GameObject CorrectUI;
    public GameObject ResultsText;
    public GameObject RepeatUI;
    public GameObject ProcessUI;
    public TMPro.TextMeshProUGUI ProcessText;
    public AudioClip thisis_audio;
    public AudioClip not_audio;
    public AudioClip Silence;

    // Start is called before the first frame update
    void Start()
    {
        foodResults = new bool[repeatTimes];
        CurrSound= gameObject.GetComponent<AudioSource>();
        foodAnswers = new int[repeatTimes];    
    }
    public void changeRepeatTimes(int repeatTimes)
    {
        this.repeatTimes = repeatTimes;
        
        foodResults = new bool[repeatTimes];
        foodAnswers = new int[repeatTimes];
    }
    public void GameStarts()
    {
        CurrSound.clip = null;
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
                    break;
                }
              
            }
        }
        randomFoodsList = elements.ToArray();
       
        roundStarts();


    }

    public void roundStarts()
    {
        if(!ProcessUI.activeSelf)
        {
            ProcessUI.SetActive(true);

        }
        ProcessText.text = currentRound + "/" + repeatTimes;


        if (currentRound < repeatTimes)
        {

            UnityEngine.Debug.Log(randomFoodsList[currentRound]);

            presureplate.SetCorrectId(randomFoodsList[currentRound]);
            StartCoroutine(playEngineSound(currentRound));
        }
        else
        {
            GetResults();
            StartCoroutine(endAudio());
            //UnityEngine.Debug.Log(-1);

            presureplate.SetCorrectId(-1);
            // finished
        }

    }
    IEnumerator endAudio()
    {
        //UnityEngine.Debug.Log(thiscurrentRound);
        
            if (foodResults[foodResults.Length-1])
            {
                CurrSound.clip = CorrectAudio;
            }
            else
            {
                
                // I could add here that this was not the asked food.
                CurrSound.clip = IncorrectAudio;
                

            }
        CurrSound.Play();
        yield return new WaitForSeconds(CurrSound.clip.length);
        CurrSound.clip = Silence;
    }
    // We can add an audio saying that it's the end
    //CurrSound.clip = endAudio;
    //CurrSound.Play();

    IEnumerator playEngineSound(int thiscurrentRound)
    {
        //UnityEngine.Debug.Log(thiscurrentRound);

        // If there is previous answer.
        if (thiscurrentRound > 0)
        {
           // correct answer previously
            if (foodResults[thiscurrentRound - 1])
            {
                CurrSound.clip = CorrectAudio;
                CurrSound.Play();
                yield return new WaitForSeconds(CurrSound.clip.length);
            }
            // incorrect answer previously
            else
            {
                CurrSound.clip = thisIsWrongAudios[randomFoodsList[thiscurrentRound]];
                CurrSound.Play();
                yield return new WaitForSeconds(CurrSound.clip.length+1f);

                CurrSound.clip = thisIsAudios[foodAnswers[thiscurrentRound - 1]];
                CurrSound.Play();
                yield return new WaitForSeconds(CurrSound.clip.length);
            }
        }

        // give me the plate please
        CurrSound.clip = requestAudios[randomFoodsList[thiscurrentRound]];
        CurrSound.Play();
        yield return new WaitForSeconds(CurrSound.clip.length);



    }
    IEnumerator repeatAudio(int thiscurrentRound)
    {

        // This is incorrect food

        CurrSound.clip = thisIsAudios[foodAnswers[thiscurrentRound]];
        CurrSound.Play();
        yield return new WaitForSeconds(CurrSound.clip.length +1f);
        
        // correct food please

        CurrSound.clip = requestAudios[randomFoodsList[thiscurrentRound]];
        CurrSound.Play();
    }
    IEnumerator repeatAudioWrong(int thiscurrentRound)
    {
        Debug.Log("id prev: "+ foodAnswers[thiscurrentRound]+" correct id: " +randomFoodsList[thiscurrentRound]);
        // This is NOT correct food
        CurrSound.clip = thisIsWrongAudios[randomFoodsList[thiscurrentRound]];
        CurrSound.Play();
        yield return new WaitForSeconds(CurrSound.clip.length + 1f);

        // This is incorrect food

        CurrSound.clip = thisIsAudios[foodAnswers[thiscurrentRound]];
        CurrSound.Play();
        yield return new WaitForSeconds(CurrSound.clip.length + 1f);

        // correct food please

        CurrSound.clip = requestAudios[randomFoodsList[thiscurrentRound]];
        CurrSound.Play();
    }
    IEnumerator repeatAudioUI(int thiscurrentRound)
    {


        yield return new WaitForSeconds(0);
        CurrSound.clip = requestAudios[randomFoodsList[thiscurrentRound]];
        CurrSound.Play();
        // correct food



    }

    public void repeatAudio()
    {

        StartCoroutine(repeatAudioUI(currentRound));
    }

    //Called by preassurePlate to let the game know that the answer is right and it should go to the next round
    public void isCorrect(int entered)
    {
        UnityEngine.Debug.Log("isCorrect "+ currentRound);
        // mark it as correct
        foodResults[currentRound] = true;
        foodAnswers[currentRound] = entered;
        currentRound++;
        repeat = true;
        roundStarts();

    }

    //Called by preassurePlate to let the game know that the answer is wrong and it should repeat the sound or skip to the next one
    public void isIncorrect(int entered)
    {
        //UnityEngine.Debug.Log("isIncorrect " + " round: "+ currentRound + "repeat" + repeat);
        if (repeat)
        {
            foodAnswers[currentRound] = entered;
            StartCoroutine(repeatAudioWrong(currentRound));
            repeat = false;
        }
        else
        {
            repeat = true;
            //sound that failed.
            foodResults[currentRound]= false;
            foodAnswers[currentRound] = entered;
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
        ProcessUI.SetActive(false);
    }
    //public void repeatAudio()
    //{
    //    CurrSound.Play();
    //}
}
