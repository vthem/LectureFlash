using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractList : MonoBehaviour
{
	public GameObject entryTemplate;
	public GameObject addTemplate;

	private readonly List<GameObject> entries = new List<GameObject>();

	protected abstract string[] GetEntries();

	protected abstract bool ShouldUpdate();

	private void Update()
	{
		if (ShouldUpdate())
		{
			foreach (GameObject wordObject in entries)
			{
				GameObject.Destroy(wordObject);
			}
			entries.Clear();

			PopulateList();
		}
	}

	private void PopulateList()
	{
		string[] wordArray = GetEntries();
		for (int i = 0; i < wordArray.Length; ++i)
		{
			GameObject obj = Instantiate(entryTemplate);
			obj.transform.SetParent(transform);
			obj.transform.localScale = Vector3.one;
			obj.GetComponent<Text>().text = wordArray[i];
			entries.Add(obj);
		}
		{
			GameObject obj = Instantiate(addTemplate);
			obj.transform.SetParent(transform);
			obj.transform.localScale = Vector3.one;
			entries.Add(obj);
		}
	}
}