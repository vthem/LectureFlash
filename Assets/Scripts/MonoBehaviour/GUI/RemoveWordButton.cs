using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveWordButton : MonoBehaviour
{
	private Button button;

	private void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(ClickHandler);
	}

	private void ClickHandler()
	{
		new RuntimeAppData.Command_RemoveSelectedWord();
	}
}
