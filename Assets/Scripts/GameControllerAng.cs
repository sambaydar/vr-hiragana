using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameControllerAng : MonoBehaviour
{
    public bool isExam = false;
    public PressurePlateAngela presureplate;
    public int repeatTimes = 1;
    public int TotalFoods = 6;
    private int currentRound = 0;
    public AudioClip[] requestAudios;
    public AudioClip[] thisIsAudios;
    public String[] names;
    public AudioClip[] thisIsWrongAudios;
    public AudioClip CorrectAudio;
    public AudioClip IncorrectAudio;
    public AudioClip onegaiaudio;
    private GameObject fatherofinstantiatedObjects;
    private bool repeat = true;
    private int[] randomFoodsList;
    private bool[] foodResults;
    private List<KeyValuePair<string, string>> duplaListWrongAttempts;
    public AudioSource CurrSound;
    private int[] foodAnswers;
    public GameObject[] prefabs;
    public Transform[] positions_prefabs;
    public GameObject StartUI;
    public GameObject ResultsUI;
    public GameObject IncorrectUI;
    public GameObject CorrectUI;
    public GameObject ResultsText;
    public GameObject RepeatUI;
    public GameObject ProcessUI;
    public TMPro.TextMeshProUGUI ProcessText;
    public AudioClip thisis_audio;
    public AudioClip goshisoosamadeshita;
    public FeedbackToFormOnline feedbackSubmit;
    public AudioClip not_audio;
    public AudioClip Silence;
    public GameObject[] badges;
    public ButtonTrigger[] buttonTrigger_exam;
    public GameObject[] ExamActivates;
    public GameObject[] ExamDeactivates;

    //public GameObject confetti;
    public AnimationStateController characterController;
    public string typeOfExam = "";
    public void changeTypeExam(GameObject[] list, string typeOfExam)
    {
        prefabs = list;
        this.typeOfExam = typeOfExam;
        Debug.Log("saved" + this.typeOfExam);
    }
    // Start is called before the first frame update
    void Start()
    {
        foodResults = new bool[repeatTimes];
        foodAnswers = new int[repeatTimes];
        duplaListWrongAttempts = new List<KeyValuePair<string, string>>();
        if (!feedbackSubmit)
        {
            feedbackSubmit = GameObject.Find("SendInfo").GetComponent<FeedbackToFormOnline>();
        }
    }
    public void changeRepeatTimes(int repeatTimes)
    {
        this.repeatTimes = repeatTimes;

        foodResults = new bool[repeatTimes];
        foodAnswers = new int[repeatTimes];
    }
    public void createFatherforObjects()
    {
        // Create a new empty GameObject
        fatherofinstantiatedObjects = new GameObject("EmptyFather");

        // Set the current GameObject (this.transform) as the parent of the new empty GameObject
        fatherofinstantiatedObjects.transform.SetParent(this.transform);

        // Optionally, reset the local position, rotation, and scale of the new child
        fatherofinstantiatedObjects.transform.localPosition = Vector3.zero;
        fatherofinstantiatedObjects.transform.localRotation = Quaternion.identity;
        fatherofinstantiatedObjects.transform.localScale = Vector3.one;


    }
    public void GameStarts()
    {
        duplaListWrongAttempts = new List<KeyValuePair<string, string>>();
        CurrSound.clip = null;
        currentRound = 0;
        // Create the list of random elements that are going to go in this setting
        List<int> elements = new List<int>();
        HashSet<int> usedElements = new HashSet<int>();
        int foodId;
        while (elements.Count < repeatTimes)
        {
            if (usedElements.Count >= TotalFoods) // All elements have been used, reset the set
            {
                usedElements.Clear();
            }

            // Generate a random foodId
            foodId = UnityEngine.Random.Range(0, TotalFoods);

            // Add to elements if not already used in this set
            if (!usedElements.Contains(foodId))
            {
                elements.Add(foodId);
                usedElements.Add(foodId);
            }
        }
        randomFoodsList = elements.ToArray();
        roundStarts();
    }

    public void handleObjects()
    {
        // If there are not enough positions, use only as many prefabs as there are positions
        int objectCount = Mathf.Min(prefabs.Length, positions_prefabs.Length);

        // Get the correct object's ID for this round
        int correctId = randomFoodsList[currentRound];

        // Create a list of positions and shuffle it to randomize where objects will be placed
        List<Transform> availablePositions = positions_prefabs.ToList();
        Shuffle(availablePositions);

        // Randomly choose one of the positions for the correct object
        int correctPositionIndex = UnityEngine.Random.Range(0, availablePositions.Count);

        // Place the correct object at the randomly chosen position
        GameObject correctObject = Instantiate(prefabs[correctId], availablePositions[correctPositionIndex].position, Quaternion.identity);
        correctObject.transform.SetParent(fatherofinstantiatedObjects.transform);


        // Remove the used position
        availablePositions.RemoveAt(correctPositionIndex);

        // Now place the remaining objects randomly, up to the available positions
        HashSet<int> usedIds = new HashSet<int> { correctId }; // Track used prefabs to avoid duplicates
        for (int i = 0; i < objectCount - 1; i++) // One position is already used by the correct object
        {
            int randomId;

            // Ensure that the same prefab is not placed twice (unless necessary)
            do
            {
                randomId = UnityEngine.Random.Range(0, prefabs.Length);
            }
            while (usedIds.Contains(randomId) && usedIds.Count < prefabs.Length);

            // Place the object
            GameObject randomObject = Instantiate(prefabs[randomId], availablePositions[i].position, Quaternion.identity);
            randomObject.transform.SetParent(fatherofinstantiatedObjects.transform);

            // Add to used IDs and remove the used position
            usedIds.Add(randomId);
        }
    }

    // Method to remove all objects when the round is over
    public void clearObjects()
    {
        if (fatherofinstantiatedObjects != null) {
            Destroy(fatherofinstantiatedObjects);
        }
    }

    // Helper method to shuffle positions
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    public void roundStarts()
    {
        clearObjects();
        createFatherforObjects();

        if (!ProcessUI.activeSelf)
        {
            ProcessUI.SetActive(true);

        }
        ProcessText.text = currentRound + "/" + repeatTimes;

        if (currentRound < repeatTimes)
        {
            handleObjects();

            UnityEngine.Debug.Log(randomFoodsList[currentRound]);

            presureplate.SetCorrectId(randomFoodsList[currentRound]);
            StartCoroutine(playEngineSound(currentRound));
        }
        else
        {
            clearObjects();

            GetResults();
            StartCoroutine(endAudio());
            //UnityEngine.Debug.Log(-1);

            presureplate.SetCorrectId(-1);
            // finished
        }

    }
    //IEnumerator playConfetti()
    //{
    //confetti.SetActive(true);
    //yield return new WaitForSeconds(6f);
    //confetti.SetActive(false);
    //}

    IEnumerator endAudio()
    {
        //UnityEngine.Debug.Log(thiscurrentRound);

        if (foodResults[foodResults.Length - 1])
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
        CurrSound.clip = goshisoosamadeshita;
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
            // incorrect answer previously and it's time for the next food.
            else
            {
                // thi is not prev. food
                CurrSound.clip = thisIsWrongAudios[randomFoodsList[thiscurrentRound - 1]];
                CurrSound.Play();
                yield return new WaitForSeconds(CurrSound.clip.length);
                // thi is prev. food answer

                CurrSound.clip = thisIsAudios[foodAnswers[thiscurrentRound - 1]];
                CurrSound.Play();
                yield return new WaitForSeconds(CurrSound.clip.length);
                // it's a shame 
                CurrSound.clip = IncorrectAudio;
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
        yield return new WaitForSeconds(CurrSound.clip.length + 1f);

        // correct food please

        CurrSound.clip = requestAudios[randomFoodsList[thiscurrentRound]];
        CurrSound.Play();
    }
    IEnumerator repeatAudioWrong(int thiscurrentRound)
    {
        Debug.Log("id prev: " + foodAnswers[thiscurrentRound] + " correct id: " + randomFoodsList[thiscurrentRound]);
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
        characterController.ApprovalAnimation();
        //StartCoroutine(playConfetti());
        UnityEngine.Debug.Log("isCorrect " + currentRound);
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
        characterController.RejectionAnimation();
        KeyValuePair<string, string> newDupla = new KeyValuePair<string, string>(names[randomFoodsList[currentRound]], names[entered]);
        duplaListWrongAttempts.Add(newDupla);
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
            foodResults[currentRound] = false;
            // THIS NEEDS TO CHANGE TO GET THE CURRENT ONE. 
            foodAnswers[currentRound] = entered;
            currentRound++;
            // next one and mark this as lost
            roundStarts();
        }
    }
    IEnumerator getbadge() 
        {
        // give badget acording to exam type
        if(typeOfExam== "visual")
        {
            Instantiate(badges[0]);
            if (buttonTrigger_exam[0]!=null)
            {
                buttonTrigger_exam[0].DisableButton();
            }


        }
        if (typeOfExam == "names")
        {
            if (buttonTrigger_exam[1] != null)
            {
                buttonTrigger_exam[1].DisableButton();
            }
            Instantiate(badges[1]);
        }

        yield return new WaitForSeconds(10f);
        if (ExamActivates !=null) { 
            for (int i = 0;i<ExamActivates.Length; i++)
            {
                ExamActivates[i].SetActive(true);
            }
        }
        if (ExamDeactivates != null)
        {
            for (int i = 0; i < ExamDeactivates.Length; i++)
            {
                ExamDeactivates[i].SetActive(false);

            }
        }
    }
    void GetResults()
    {
        clearObjects();
        string resultsText = "できあがり\n";
        if(!isExam)
        {
            for (int i = 0; i < foodResults.Length; i++)
            {
                resultsText += (i + 1).ToString() + /*". " + hiraganaArray[chosenChars[i]] + */ "\t";
                if (foodResults[i] == true)
                    resultsText += "\u2713"; // check
                else
                    resultsText += "\u2717"; // x
                resultsText += "\n";
            }
            ResultsUI.SetActive(true);
        }
        

        string textRight = "";
            string textWrong = "";

            for (int i = 0; i < foodResults.Length; i++)
            {
                if(foodResults[i] == true )
                {
                    textRight= textRight+ names[randomFoodsList[i]] + ",";
                }
                else
                {
                    textWrong = textWrong + names[randomFoodsList[i]] + ",";
                }

            }
        textWrong= textWrong.TrimEnd(' ', ',');
        textRight = textRight.TrimEnd(' ', ',');
        string textAttempts = DuplaListToString();
        if (textAttempts == "" && isExam )
        {
            // give badge
                // first element is going to be the badge
               
            StartCoroutine(getbadge());



        }
        else if(textAttempts != "" && isExam)
        {
            string newnames = "";
            string tempname = "";
            foreach (var pair in duplaListWrongAttempts)
            {
                if(tempname!= pair.Key)
                {
                    newnames += pair.Key+",";
                    tempname = pair.Key;
                }
            }
            textWrong = textWrong.TrimEnd(' ', ',');

            string[] names = newnames.Split(',');
            string currentLine = "";
            // give other feedback saying that it is wrong. Every 16 letters 
            resultsText += "\n You got wrong:\n";
            foreach (string name in names)
            {

                string trimmedName = name.Trim(); // Remove any extra spaces around the name

                // Check if adding this name would exceed the max line length
                if (currentLine.Length + trimmedName.Length + 2 > 16) // +2 for comma and space
                {
                    // If so, add the current line to resultsText and start a new line
                    resultsText += currentLine.TrimEnd() + "\n";
                    currentLine = ""; // Reset currentLine for the new line
                }
                currentLine += trimmedName + ", ";
            }
                if (currentLine.Length > 0)
                {
                    resultsText += currentLine.TrimEnd(',', ' ') + "\n";
                }

                ResultsUI.SetActive(true);
        }
        if (!feedbackSubmit)
            {
                feedbackSubmit = GameObject.Find("SendInfo").GetComponent<FeedbackToFormOnline>();
            }
        if (isExam)
        {
            feedbackSubmit.SubmitFeedbackAssignment1(textWrong, textRight, this.gameObject.name+"- "+ typeOfExam, textAttempts);

        }
        else
        {
            feedbackSubmit.SubmitFeedbackAssignment1(textWrong, textRight, this.gameObject.name, textAttempts);
        }
        ResultsText.GetComponent<TMPro.TextMeshProUGUI>().text = resultsText;
        
        RepeatUI.SetActive(false);
        ProcessUI.SetActive(false);
        duplaListWrongAttempts.Clear();
    }


    // Method to convert the list to a readable string format
    public string DuplaListToString()
    {
        string result = "";
        foreach (var pair in duplaListWrongAttempts)
        {
            result += $"{pair.Key}:{pair.Value}, ";
        }
        return result.TrimEnd(' ', ','); // Remove trailing comma and space
    }
    //public void repeatAudio()
    //{
    //    CurrSound.Play();
    //}
}
