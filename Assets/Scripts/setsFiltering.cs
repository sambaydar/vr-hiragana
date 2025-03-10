using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class setsFiltering : MonoBehaviour
{
    public GameObject[] set1;
    public GameObject[] set2;

    public Button[] mybuttons;

    public int currentButtonId = 0;

    public GameObject foodItemPrefab; // Assign the FoodItem prefab in Inspector
    public Transform initialPos; // Assign the InitialPos Transform in Inspector

    public string[] namesSet1;
    public string[] namesSet2;

    private GameObject currentFoodItem;

    void Start()
    {
        onChangeRepeatTimes(currentButtonId);
    }

    public void onChangeRepeatTimes(int buttonId)
    {
        // Enable previous button and disable selected button
        mybuttons[currentButtonId].interactable = true;
        mybuttons[buttonId].interactable = false;
        currentButtonId = buttonId;

        // Instantiate the FoodItem with the selected set
        InstantiateFoodItem(buttonId);
    }

    public void InstantiateFoodItem(int buttonId)
    {
        // Destroy previous FoodItem if it exists
        if (currentFoodItem != null)
        {
            Destroy(currentFoodItem);
        }

        // Instantiate new FoodItem at InitialPos
        currentFoodItem = Instantiate(foodItemPrefab, initialPos.position, Quaternion.identity);

        // Find AnimationController inside the FoodItem
        Transform animationController = currentFoodItem.transform.Find("AnimationController");
        if (animationController == null)
        {
            Debug.LogError("AnimationController not found inside FoodItem!");
            return;
        }

        // Select the appropriate set and name based on buttonId
        GameObject[] selectedSet = buttonId == 0 ? set1 : set2;
        string[] selectedNames = buttonId == 0 ? namesSet1 : namesSet2;

        if (selectedSet.Length > 0)
        {
            // Instantiate a random object from the selected set
            GameObject selectedObject = Instantiate(selectedSet[Random.Range(0, selectedSet.Length)], animationController);
            selectedObject.transform.localPosition = Vector3.zero; // Reset position relative to parent

            // Find the UI/Name object inside FoodItem
            Transform uiTransform = currentFoodItem.transform.Find("UI");
            if (uiTransform != null)
            {
                Transform nameTransform = uiTransform.Find("Name");
                if (nameTransform != null)
                {
                    TMP_Text textComponent = nameTransform.GetComponent<TMP_Text>();
                    if (textComponent != null)
                    {
                        textComponent.text = selectedNames[Random.Range(0, selectedNames.Length)];
                    }
                    else
                    {
                        Debug.LogError("TextMeshProUGUI component not found in FoodItem/UI/Name!");
                    }
                }
                else
                {
                    Debug.LogError("Name transform not found inside FoodItem/UI!");
                }
            }
            else
            {
                Debug.LogError("UI transform not found inside FoodItem!");
            }
        }
        else
        {
            Debug.LogError("Selected set is empty!");
        }
    }
}
