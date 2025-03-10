using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badgetLogic : MonoBehaviour
{
    public GameObject fistefect;
    public GameObject secefect;
    public GameObject thirdefect;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateDeactivateAfterDelay(0f, fistefect, secefect));
        StartCoroutine(ActivateDeactivateAfterDelay(4f, secefect, fistefect));
        StartCoroutine(ActivateDeactivateAfterDelay(30f, null, secefect ));

    }
    IEnumerator ActivateDeactivateAfterDelay(float waitTime, List<GameObject> activateList, List<GameObject> deactivateList)
    {
        yield return new WaitForSeconds(waitTime);

        // Activate all objects in the activateList
        foreach (GameObject obj in activateList)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        // Deactivate all objects in the deactivateList
        foreach (GameObject obj in deactivateList)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    IEnumerator ActivateDeactivateAfterDelay(float waitTime, GameObject activateObj, GameObject deactivateObj)
    {
        yield return new WaitForSeconds(waitTime);

        // Activate the specified object
        if (activateObj != null)
        {
            activateObj.SetActive(true);
        }

        // Deactivate the specified object
        if (deactivateObj != null)
        {
            deactivateObj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
