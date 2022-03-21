using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class WordList
{
    public string[] words;
}

[Serializable]
public class AppData
{
    public WordList wordList;
    public float displayDuration = 1f;
}

public class Main_Monobehaviour : MonoBehaviour
{
    public readonly string appDataFilePath = "AppData.json";
    public Text targetText = null;

    private AppData appData;
    private int currentWordIndex = 0;
    private float displayTime;

    // Start is called before the first frame update
    void Start()
    {
        if (!File.Exists(appDataFilePath))
        {
            appData = new AppData();
            WordList wordList = new WordList();
            wordList.words = new string[] { "mot1", "mot2", "mot3" };
            appData.wordList = wordList;
            var json = JsonUtility.ToJson(appData, true);
            File.WriteAllText(appDataFilePath, json);
        }
        else
        {
            appData = JsonUtility.FromJson<AppData>(File.ReadAllText(appDataFilePath));
        }

        currentWordIndex = -1;
        DisplayNextWord();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextWord();
        }

        if (Time.realtimeSinceStartup - displayTime > appData.displayDuration)
        {
            Hide();
        }
    }

    private void DisplayNextWord()
    {
        if (!targetText)
            return;

        currentWordIndex++;
        currentWordIndex = currentWordIndex % appData.wordList.words.Length;

        targetText.text = appData.wordList.words[currentWordIndex];

        displayTime = Time.realtimeSinceStartup;
    }

    private void Hide()
    {
        if (!targetText)
            return;

        targetText.text = "";
    }
}
