using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the main game loop
public class GameLoop : MonoBehaviour
{
    // randomized game elements
    public AudioClip[] KanaSounds;
    public GameObject[] foods;
    public Material[] chars;

    public GameObject CorrectUI;
    public GameObject IncorrectUI;
    public GameObject ResultsUI;
    public GameObject ResultsText;
    public AnimationStateController asc;

    private AudioSource CurrSound;
    private PressurePlate pressurePlateController;
    private bool limboState;
    private bool firstEnable;

    // timer variables
    private float currentTime = 0f;
    private float startingTime = 10f;

    // a list of characters for each round (no repeats)
    private List<int> chosenChars;
    private List<bool> results;
    private string currentChar;

    // variables for setting the food
    private GameObject[] gameObjects = new GameObject[5];
    private Vector3[] positions = new Vector3[] {
        new Vector3(-2.35f, 1.7f, -0.3f),
        new Vector3(-2.35f, 1.7f, -0.65f),
        new Vector3(-2.35f, 1.7f, -1.05f),
        new Vector3(-2.47f, 1.7f, -1.45f),
        new Vector3(-3.02f, 1.7f, -1.586f)
    };
    private Quaternion[] rotations = new Quaternion[] {
        Quaternion.identity,
        Quaternion.identity,
        Quaternion.identity,
        Quaternion.Euler(new Vector3(0, 45, 0)),
        Quaternion.Euler(new Vector3(0, 90, 0)),
    };

    string[] hiraganaArray = new string[]
        {
            "あ", "い", "う", "え", "お",
            "か", "き", "く", "け", "こ",
            "さ", "し", "す", "せ", "そ",
            "た", "ち", "つ", "て", "と",
            "な", "に", "ぬ", "ね", "の",
            "は", "ひ", "ふ", "へ", "ほ",
            "ま", "み", "む", "め", "も",
            "や", "ゆ", "よ",
            "ら", "り", "る", "れ", "ろ",
            "わ", "ん"
        };

    void Awake() {
        firstEnable = true;
    }

    void Start()
    {
        // get components
        pressurePlateController = GameObject.Find("PressurePlate").GetComponent<PressurePlate>();
        CurrSound = GetComponent<AudioSource>();
        // generate the first round of food
        int newChar = GenerateNewChar();
        GetRandomFood(newChar);
    }

    void OnEnable() {
        // reset all variables
        currentTime = startingTime;
        limboState = false;
        chosenChars = new List<int>();
        results  = new List<bool>();

        // if this is a replay, call DisableUI()
        if (!firstEnable) {
            DisableUI();
        } else {
            firstEnable = false;
        }
    }

    void Update()
    {
        // update time
        currentTime -= Time.deltaTime;
        if (pressurePlateController.IsAnswerSubmitted() && !limboState) {
            // once the food is submitted
            results.Add(pressurePlateController.IsCorrect());
            Invoke("DestroyFood", 2);
            if (results.Count == 5) {
                GetResults();
                gameObject.SetActive(false);
            } else {
                currentTime = 10000;
                limboState = true;
                Invoke("DisableUI", 2);
            }
        }
        else if (currentTime <= 0) {
            // if the user doesn't submit within the time limit
            results.Add(false);
            DestroyFood();
            if (results.Count == 5) {
                GetResults();
                gameObject.SetActive(false);
            } else {
                int newChar = GenerateNewChar();
                GetRandomFood(newChar);
            }
        }
    }

    // populates and shows the results ui
    void GetResults() {
        CorrectUI.SetActive(false);
        IncorrectUI.SetActive(false);

        string resultsText = "できあがり\n";
        for (int i = 0; i < results.Count; i++) {
            resultsText += (i+1).ToString() + ". " + hiraganaArray[chosenChars[i]] + "\t";
            if (results[i] == true)
                resultsText += "\u2713"; // check
            else
                resultsText += "\u2717"; // x
            resultsText += "\n";
        }

        ResultsText.GetComponent<TMPro.TextMeshProUGUI>().text = resultsText;
        ResultsUI.SetActive(true);
    }

    // destroy all current food
    void DestroyFood() {
        for (int i = 0; i < gameObjects.Length; i++) {
            Destroy(gameObjects[i]);
        }
        currentTime = startingTime;
    }

    // called when an order is submitted to disable correct/incorrect ui, 
    // reset variables and generate new food
    void DisableUI() {
        int newChar = GenerateNewChar();
        GetRandomFood(newChar);
        CorrectUI.SetActive(false);
        IncorrectUI.SetActive(false);
        pressurePlateController.ResetAnswerSubmitted();
        limboState = false;
    }

    void GetRandomFood(int newChar) {
        List<int> foodChoices = new List<int>();
        foodChoices.Add(newChar);
        // generate 4 other options that are all unique
        for (int i = 0; i < 4; i++) {
            while (true) {
                int dummyChar = Random.Range(0, chars.Length);
                if (!foodChoices.Contains(dummyChar)) {
                    foodChoices.Add(dummyChar);
                    break;
                }
            }
        }
        // randomize order
        IListExtensions.Shuffle(foodChoices);
        // tag the correct answer
        for (int i = 0; i < gameObjects.Length; i++) {
            gameObjects[i] = Instantiate(GenerateCharFood(foodChoices[i]), positions[i], rotations[i]);
            if (foodChoices[i] == newChar)
                gameObjects[i].tag = "Player";
        }
        // play call out sound and animation
        CurrSound.clip = KanaSounds[newChar];
        CurrSound.Play();
        asc.CallOutAnimation();
    }
    
    // gets a random food prefab from array and puts the hiragana character on it,
    // returns the gameobject
    GameObject GenerateCharFood(int charIndex) {
        GameObject food = foods[Random.Range(0, foods.Length)];
        Transform charPaper = food.transform.Find("Kana_A");
        charPaper.GetComponent<Renderer>().material = chars[charIndex];
        return food;
    }

    // generates char (correct answer) for a given round
    int GenerateNewChar() {
        while (true) {
            int newChar = Random.Range(0, chars.Length);
            if (!chosenChars.Contains(newChar)) {
                chosenChars.Add(newChar);
                currentChar = chars[newChar].name;
                return newChar;
            }
        }
    }

}