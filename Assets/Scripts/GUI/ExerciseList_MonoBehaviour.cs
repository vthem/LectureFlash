using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseList_MonoBehaviour : MonoBehaviour
{
    public GameObject template;

    private List<GameObject> exerciseObjectList = new List<GameObject>();

    void Update()
    {
        if (GlobalData.runtimeAppData.isDirty)
		{
            foreach (var exerciseObject in exerciseObjectList)
			{
                GameObject.Destroy(exerciseObject);
			}
            exerciseObjectList.Clear();
            var exerciseNameArray = GlobalData.runtimeAppData.ExerciseNameArray;
            for ( int i = 0; i < exerciseNameArray.Length; ++i)
			{
                var obj = Instantiate(template);
                obj.transform.SetParent(transform);
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<ExerciseEntry_MonoBehaviour>().SetExerciseName(exerciseNameArray[i]);
                exerciseObjectList.Add(obj);

            }
        }
    }
}
