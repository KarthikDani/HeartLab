using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class HeartDataWrapper
{
    public List<HeartData> heartData;
}

[System.Serializable]
public class HeartData
{
    public string label;
    public string description;
}

public class JSONReader : MonoBehaviour
{
    private string jsonFileName = "HeartLabelDescriptionDataCopy"; // The name of your JSON file (e.g., "heartdata")

    private List<HeartData> heartDataList; // List to store the loaded HeartData objects

    private void Start()
    {
        // Load the JSON file from the Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

        // Parse the JSON and populate the heartDataList
        HeartDataWrapper dataWrapper = JsonUtility.FromJson<HeartDataWrapper>(jsonFile.text);
        heartDataList = dataWrapper.heartData;
    }


    public string GetDescriptionByLabel(string label)
    {
       
        HeartData heartData = heartDataList.Find(data => data.label == label);
        if (heartData != null)
        {
            return heartData.description;
        }
        return string.Empty;
    }


}

