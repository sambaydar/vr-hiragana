using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowButton : MonoBehaviour
{
    public GameObject button;
    
    private void OnEnable()
    {
        button.SetActive(false);
        StartCoroutine(ShowButtonAfterDelay(2f));
    }

    private IEnumerator ShowButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        button.SetActive(true);
    }
}
