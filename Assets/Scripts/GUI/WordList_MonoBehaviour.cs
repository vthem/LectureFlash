using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordList_MonoBehaviour : MonoBehaviour
{
    public GameObject template;

    private List<GameObject> wordObjectList = new List<GameObject>();

    void Update()
    {
        if (GlobalData.runtimeAppData.isDirty)
        {
            foreach (var wordObject in wordObjectList)
            {
                GameObject.Destroy(wordObject);
            }
            wordObjectList.Clear();

            if (!string.IsNullOrEmpty(GlobalData.runtimeAppData.SelectedExerciseName))
                PopulateList();

        }
    }

    private void PopulateList()
	{
        var wordArray = GlobalData.runtimeAppData.WordArray(GlobalData.runtimeAppData.SelectedExerciseName);
        for (int i = 0; i < wordArray.Length; ++i)
        {
            var obj = Instantiate(template);
            obj.transform.SetParent(transform);
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<Text>().text = wordArray[i];
            wordObjectList.Add(obj);
        }
    }
}
