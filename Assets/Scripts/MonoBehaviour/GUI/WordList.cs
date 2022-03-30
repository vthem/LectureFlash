using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class WordList : MonoBehaviour
{
	public GameObject template;

	private readonly List<GameObject> wordObjectList = new List<GameObject>();

	private void Update()
	{
		if (GlobalData.runtimeAppData.isDirty)
		{
			foreach (GameObject wordObject in wordObjectList)
			{
				GameObject.Destroy(wordObject);
			}
			wordObjectList.Clear();

			if (!string.IsNullOrEmpty(GlobalData.runtimeAppData.SelectedExerciseName))
			{
				PopulateList();
			}
		}
	}

	private void PopulateList()
	{
		string[] wordArray = GlobalData.runtimeAppData.WordArray(GlobalData.runtimeAppData.SelectedExerciseName);
		for (int i = 0; i < wordArray.Length; ++i)
		{
			GameObject obj = Instantiate(template);
			obj.transform.SetParent(transform);
			obj.transform.localScale = Vector3.one;
			obj.GetComponent<Text>().text = wordArray[i];
			wordObjectList.Add(obj);
		}
	}
}
