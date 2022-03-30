using System.IO;

using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
	public readonly string appDataFilePath = "AppData.json";
	public Text targetText = null;

	public Button nextButton = null;
	public Button currentButton = null;
	public Button nextExerciseButton = null;
	public Button previousExerciseButton = null;

	private AppData appData;
	private int currentWordIndex = 0;
	private float displayTime;
	private int exerciseIndex = 0;

	// Start is called before the first frame update
	void Start()
	{
		if (!File.Exists(appDataFilePath))
		{
			appData = new AppData();
			Exercise ex1 = new Exercise();
			ex1.name = "Exercice 1";
			ex1.words = new string[] { "mot1", "mot2", "mot3" };
			ex1.displayDuration = 1f;
			appData.exercises = new Exercise[2];
			appData.exercises[0] = new Exercise();
			appData.exercises[0].name = "Exercice 1";
			appData.exercises[0].words = new string[] { "mot1", "mot2", "mot3" };
			appData.exercises[0].displayDuration = 1f;
			appData.exercises[1] = new Exercise();
			appData.exercises[1].name = "Exercice 2";
			appData.exercises[1].words = new string[] { "Chat", "Chien", "Chouette" };
			appData.exercises[1].displayDuration = 1f;
			var json = JsonUtility.ToJson(appData, true);
			File.WriteAllText(appDataFilePath, json);
		}
		else
		{
			appData = JsonUtility.FromJson<AppData>(File.ReadAllText(appDataFilePath));
		}

		GlobalData.runtimeAppData = new RuntimeAppData(appData);

		nextButton.onClick.AddListener(NextHandler);
		currentButton.onClick.AddListener(CurrentHandler);
		previousExerciseButton.onClick.AddListener(PreviousExerciseHandler);
		nextExerciseButton.onClick.AddListener(NextExerciseHandler);

		Ready();
	}

	void Ready()
	{
		currentWordIndex = -1;
		displayTime = -1f;
		DisplayMainText($"'espace' pour démarrer l'exercice\n{appData.exercises[exerciseIndex].name}");
	}

	private void NextHandler()
	{
		DisplayNextWord();
	}

	private void CurrentHandler()
	{
		string[] words = appData.exercises[exerciseIndex].words;
		if (currentWordIndex < words.Length && currentWordIndex >= 0)
		{
			displayTime = -1f;
			DisplayMainText(words[currentWordIndex]);
		}
	}

	private void NextExerciseHandler()
	{
		exerciseIndex += 1;
		exerciseIndex = exerciseIndex % appData.exercises.Length;
		currentWordIndex = -1;
		Ready();
	}

	private void PreviousExerciseHandler()
	{
		exerciseIndex -= 1;
		if (exerciseIndex < 0)
		{
			exerciseIndex = appData.exercises.Length - 1;
		}
		currentWordIndex = -1;
		Ready();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			nextButton.OnSubmit(null);
		}
		if (Input.GetKeyDown(KeyCode.PageUp))
		{
			previousExerciseButton.OnSubmit(null);
		}
		if (Input.GetKeyDown(KeyCode.PageDown))
		{
			nextExerciseButton.OnSubmit(null);
		}

		if (displayTime > 0 && Time.realtimeSinceStartup - displayTime > appData.exercises[exerciseIndex].displayDuration)
		{
			Hide();
		}

		GlobalData.runtimeAppData.HandleCommandQueue();
	}

	private void DisplayNextWord()
	{
		string[] words = appData.exercises[exerciseIndex].words;

		currentWordIndex++;
		currentWordIndex = currentWordIndex % words.Length;

		displayTime = Time.realtimeSinceStartup;

		DisplayMainText(words[currentWordIndex]);
	}

	private void DisplayMainText(string text)
	{
		if (!targetText)
			return;

		targetText.text = text;
	}

	private void Hide()
	{
		if (!targetText)
			return;

		targetText.text = "";
	}
}
