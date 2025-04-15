using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class FeedbackToFormOnline : MonoBehaviour
{
    public string data1; // Words Wrong ID
    public string data2; // Words Right ID
    public string data3; // Type
    public string data4; // Wrong attempts record
    
    public string userID;    // User ID
    public string SetID="1";    // Set

    public string time;    // Time

    public bool send;

    private string formURLAssignment1 = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSepC_Q8EwJ4n8ziKsplGzipZVXhgkLUPB1y0S_copdU4jJcVw/formResponse";
    private string jsonFilePath;

    [System.Serializable]
    public class FeedbackData
    {
        public string entry_438216833;  // Words Wrong ID
        public string entry_1853988905; // Words Right ID
        public string entry_903667495;  // Type
        public string entry_1194675123;  //  Wrong attempts record

        public string entry_1431189021; // userID
        public string entry_894541062;  // SET
        public string entry_1858430160; // Time
        public bool isSent;
    }

    [System.Serializable]
    public class FeedbackList
    {
        public List<FeedbackData> feedbackAttempts = new List<FeedbackData>();
    }

    private FeedbackList feedbackList;

    void Start()
    {
        // Set the path where the JSON data will be saved
        jsonFilePath = Path.Combine(Application.persistentDataPath, "offline_feedback.json");
        Debug.Log("JSON will be saved at: " + jsonFilePath);

        // Load existing feedback attempts or initialize a new list
        LoadFeedbackData();

        // On game start, check for any unsent data and retry sending
        RetryUnsentFeedback();
    }

    void Update()
    {

            // Update the time variable to the Oculus Quest local time
        time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        if (send)
        {
        
            SubmitFeedbackAssignment1(data1, data2, data3, data4);
            send = false;
        }

    }

    public void SubmitFeedbackAssignment1(string data1, string data2, string data3,string data4)
    {
        StartCoroutine(PostAssignment1(data1, data2, data3,data4));
        //Debug.Log("JSON says: " + data1+", "+ data2 + ", " + data3 + ", " + data4);

    }

    private IEnumerator PostAssignment1(string data1, string data2, string data3, string data4, FeedbackData feedbackData = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry_438216833", data1);   // Corresponding to entry_438216833
        form.AddField("entry_1853988905", data2);  // Corresponding to entry_1853988905
        form.AddField("entry_903667495", data3);   // Corresponding to entry_903667495
        form.AddField("entry_1194675123", data4);   // Corresponding to entry_1194675123
        form.AddField("entry_1431189021", userID);   // Corresponding to entry_1431189021
        form.AddField("entry_894541062", SetID);   // Corresponding to entry_894541062 
        form.AddField("entry_1858430160", time);   // Corresponding to entry_1858430160

        using (UnityWebRequest www = UnityWebRequest.Post(formURLAssignment1, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Submit success");

                // If the feedbackData is not null, mark as sent
                if (feedbackData != null)
                {
                    feedbackData.isSent = true;
                    SaveFeedbackData(); // Save the updated JSON file with the updated isSent status
                }
                else
                {
                    // For newly submitted feedback, save it with `isSent = true`
                    SaveDataToJson(data1, data2, data3,data4, userID, SetID, time, true);
                }
            }
            else
            {
                Debug.Log("Submit failed. Saving data to JSON for retry.");
                if (feedbackData == null)
                {
                    // If this is a new submission and it fails, save it with `isSent = false`
                    SaveDataToJson(data1, data2, data3, data4, userID, SetID, time, false);
                }
            }
        }
    }

    private void SaveDataToJson(string data1, string data2, string data3,string data4, string data5, string data6, string data7, bool isSent)
    {
        // Create new feedback attempt
        FeedbackData feedbackData = new FeedbackData
        {
            entry_438216833 = data1,
            entry_1853988905 = data2,
            entry_903667495 = data3,
            entry_1194675123 = data4,
            entry_1431189021 = data5,
            entry_894541062 = data6,
            entry_1858430160 = data7,
            isSent = isSent
        };

        // Add the new feedback attempt to the list
        feedbackList.feedbackAttempts.Add(feedbackData);

        // Save the entire feedback list back to JSON
        SaveFeedbackData();
    }

    private void SaveFeedbackData()
    {
        // Convert the list to JSON and save it to the file
        string jsonData = JsonUtility.ToJson(feedbackList);
        try
        {
            File.WriteAllText(jsonFilePath, jsonData);
            Debug.Log("Feedback data saved locally as JSON: " + jsonFilePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save JSON: " + e.Message);
        }
    }

    private void LoadFeedbackData()
    {
        // Initialize a new feedback list if the file doesn't exist
        if (!File.Exists(jsonFilePath))
        {
            feedbackList = new FeedbackList();
            return;
        }

        // If the file exists, load the data from the JSON file
        try
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            feedbackList = JsonUtility.FromJson<FeedbackList>(jsonData);
            Debug.Log("Loaded feedback data from JSON.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load JSON data: " + e.Message);
            feedbackList = new FeedbackList(); // Initialize a new list on failure
        }
    }

    private void RetryUnsentFeedback()
    {
        // Check each feedback attempt to see if it was sent
        foreach (FeedbackData feedbackData in feedbackList.feedbackAttempts)
        {
            if (!feedbackData.isSent) // Retry if it wasn't sent
            {
                Debug.Log("Retrying unsent feedback.");
                StartCoroutine(PostAssignment1(feedbackData.entry_438216833, feedbackData.entry_1853988905, feedbackData.entry_903667495,feedbackData.entry_1194675123,feedbackData));
            }
        }
    }
}
