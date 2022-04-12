using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static System.Net.Mime.MediaTypeNames;

public class WordInputField : MonoBehaviour
{
    public InputField inputField;

    private string selectedWordName = null;

    void Start()
    {
        inputField.onEndEdit.AddListener(EndEditHandler);
    }
    
    private void EndEditHandler(string arg0)
    {
        new RuntimeAppData.Command_UpdateWord(arg0);
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(GlobalData.runtimeAppData.SelectedWordName) && selectedWordName != GlobalData.runtimeAppData.SelectedWordName)
		{
            selectedWordName = GlobalData.runtimeAppData.SelectedWordName;
            inputField.text = selectedWordName;
            Debug.Log($"set input field {selectedWordName}");
        }
        else if (string.IsNullOrEmpty(GlobalData.runtimeAppData.SelectedWordName))
		{
            selectedWordName = null;
            inputField.text = "";
        }
    }
}
