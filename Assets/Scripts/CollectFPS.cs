using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CollectFPS : MonoBehaviour
{
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            StartCoroutine(CollectFrames(5)); //<-- Anzahl der Sekunden
        }
    }

    IEnumerator CollectFrames(float seconds)
    {
        float currentTime = 0;

        List<float> fpsList = new List<float>();

        Debug.Log("Start collecting");
        while (currentTime < seconds)
        {
            currentTime += Time.deltaTime;

            fpsList.Add(1.0f / Time.deltaTime);

            yield return null;
        }
        Debug.Log("Done collecting");

        Debug.Log("Result : " + fpsList.Average());
        logData("" + fpsList.Average());
    }

    private void logData(string input)
    {
        string fname = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + "_FPS.csv";
        string path = Path.Combine(Application.persistentDataPath, fname);
        StreamWriter file = new StreamWriter(path);
        file.WriteLine("ID;SceneName;Decayrow;StartTime;TotalTime;Errors;Interactions;Handedness");
        string log = input;
        file.WriteLine(log);
        file.Close();
    }
}

