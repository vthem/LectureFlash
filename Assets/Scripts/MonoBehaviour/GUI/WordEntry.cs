using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordEntry : MonoBehaviour, IPointerClickHandler
{
    public Text text;
	public Color selectedColor;
	private bool selected = false;
	private Color defaultColor;

	public void OnPointerClick(PointerEventData eventData)
	{
		new RuntimeAppData.Command_SelectWord(text.text);
	}

	public void Update()
	{
		if (GlobalData.runtimeAppData.SelectedWordName == text.text && !selected)
		{
			defaultColor = text.color;
			text.color = selectedColor;
			selected = true;
		} 
		if (GlobalData.runtimeAppData.SelectedWordName != text.text && selected)
		{
			text.color = defaultColor;
			selected = false;
		}
	}
}
