using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ChangeSets : MonoBehaviour
{

    public GameObject set1;
    public GameObject set2;

    public Button[] mybuttons;
    // Start is called before the first frame update
    void Start()
    {
        set1.SetActive(true);
        set2.SetActive(false);
        mybuttons[0].interactable = false;
        mybuttons[1].interactable = true;
    }

    void changeSet(int set)
    {
        if (set== 0)
        {
            set1.SetActive(true);
            set2.SetActive(false);
            mybuttons[0].interactable = false;
            mybuttons[1].interactable = true;

        }
        else if (set == 0)
        {
            set1.SetActive(false);
            set2.SetActive(true);
            mybuttons[0].interactable = true;
            mybuttons[1].interactable = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
