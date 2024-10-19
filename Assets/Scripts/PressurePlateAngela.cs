using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// script connected to the pressure plate to detect which food
// collided with the plate
public class PressurePlateAngela : MonoBehaviour
{
//    public AudioClip CorrectSound;
//    public AudioClip IncorrectSound;
    private int correctFoodId=-1;
    //private AudioSource CurrSound;
    public GameControllerAng gameController;
    private int lastId= -1;
    public GameObject UIPlateIndication;
    public bool isHard = false;
    void OnTriggerEnter(Collider other) {
        //UnityEngine.Debug.Log(other.gameObject.name);
        
        if (other.gameObject.tag == "food") {
        FoodClass script= other.gameObject.GetComponent<FoodClass>();
            UIPlateIndication.SetActive(false);

            if (correctFoodId > -1)
            {
                
                if (lastId != script.FoodId)
                {
                    lastId = script.FoodId;
                    if (script.FoodId== correctFoodId)
                    {
                        
                        //UnityEngine.Debug.Log("entered" );
                        //currsound.clip = correctsound;
                        //CurrSound.Play();
                        gameController.isCorrect(script.FoodId);
                    }
                    else {
                        //CurrSound.clip = IncorrectSound;
                            //CurrSound.Play();
                            gameController.isIncorrect(script.FoodId);
                     }
                 }
            }
            
            StartCoroutine(returnProduct(script));
        }
        else
        {
            // Please start the game
        }
    }
    public void ShowObject()
    {

    }
   public void SetCorrectId(int id)
    {
        correctFoodId = id;
        if(correctFoodId == -1)
        {
            UIPlateIndication.SetActive(true);
        }       
    }

    IEnumerator returnProduct(FoodClass script)
    {
        yield return new WaitForSeconds(0f);
        script.resetTransform();
        lastId = -1;
    }

    void Start()
    {
        //CurrSound = GetComponent<AudioSource>();
        
    }

}
