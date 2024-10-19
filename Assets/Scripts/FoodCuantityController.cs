using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodCuantityController : MonoBehaviour
{
    public GameControllerAng gameController;
    public Button[] mybuttons ;
    public int currentButtonId=0;
    public GameObject startButton;
    void Start()
    {
        onChangeRepeatTimes(currentButtonId);
        startButton.SetActive(false);
        StartCoroutine(UIAppear());
    }
    public void onChangeRepeatTimes(int buttonId)
    {
        //UnityEngine.Debug.Log(buttonId);
        mybuttons[currentButtonId].interactable = true;

        mybuttons[buttonId].interactable = false;
        currentButtonId=buttonId;
        gameController.changeRepeatTimes(buttonId+1);
        startButton.SetActive(false);
        StartCoroutine(UIAppear());
    }
    IEnumerator UIAppear()
    {
        yield return new WaitForSeconds(2f);
        startButton.SetActive(true);
    }

    }
