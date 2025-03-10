using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfExamController : MonoBehaviour
{
    public GameObject[] visualsOnly;
    public GameObject[] namesOnly;
    public GameControllerAng gameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void changeTypeOfExamtoVisual(bool controller)
    {
        if (controller) {
            gameController.changeTypeExam(visualsOnly, "visual");
        }
        else {
            gameController.changeTypeExam(namesOnly, "names");
        } 
    }
}
