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
    public AnimationStateController asc;
    private int correctFoodId=-1;
    //private AudioSource CurrSound;
    public GameControllerAng gameController;
    private int lastId;
    void OnTriggerEnter(Collider other) {
        //UnityEngine.Debug.Log(other.gameObject.name);
        
        if (other.gameObject.tag == "food") {
        FoodClass script= other.gameObject.GetComponent<FoodClass>();
            

            if (correctFoodId > -1)
            {
                if(lastId != script.FoodId)
                {
                    lastId = script.FoodId;
                    if (script.FoodId== correctFoodId)
                    {
                        
                        UnityEngine.Debug.Log("entered" );
                        //currsound.clip = correctsound;
                        //CurrSound.Play();
                        asc.ApprovalAnimation();
                        gameController.isCorrect(script.FoodId);
                    }
                    else {
                        //CurrSound.clip = IncorrectSound;
                            //CurrSound.Play();
                            asc.RejectionAnimation();
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

   public void SetCorrectId(int id)
    {
        lastId = -1;
        correctFoodId = id;
       
    }

    IEnumerator returnProduct(FoodClass script)
    {
        yield return new WaitForSeconds(0f);
        script.resetTransform();

    }

    void Start()
    {
        //CurrSound = GetComponent<AudioSource>();
        
    }

}
