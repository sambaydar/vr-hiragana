using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowButton : MonoBehaviour
{
    public GameObject[] buttons;
    
    private void OnEnable()
    {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].SetActive(false);
        }
        
        StartCoroutine(ShowButtonAfterDelay(2f));
    }

    private IEnumerator ShowButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }
    }
}
