using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExerciseEntry_MonoBehaviour : MonoBehaviour, IPointerClickHandler
{
    public Text text;
	public Color selectedColor;
	private bool selected = false;
	private Color defaultColor;

	public void OnPointerClick(PointerEventData eventData)
	{
		new AppDataCommand_SelectExercise(text.text);
	}

	public void SetExerciseName(string name)
	{
		if (!text)
			return;
		text.text = name;
	}

	public void Update()
	{
		if (GlobalData.runtimeAppData.SelectedExerciseName == text.text && !selected)
		{
			defaultColor = text.color;
			text.color = selectedColor;
			selected = true;
		} 
		if (GlobalData.runtimeAppData.SelectedExerciseName != text.text && selected)
		{
			text.color = defaultColor;
			selected = false;
		}
	}
}
