using System.Collections.Generic;

public partial class RuntimeAppData
{
	private Queue<Command> appDataCommandQueue = new Queue<Command>();

	public bool isDataDirty = false;
	public bool isDirty = false;
	public string SelectedExerciseName { get; private set; } = null;
	public string SelectedWordName { get; private set; } = null;
	private AppData appData;

	public RuntimeAppData(AppData appData)
	{
		this.appData = appData;
		isDirty = true;
	}

	public string[] ExerciseNameArray
	{
		get
		{
			string[] arr = new string[appData.exercises.Length];
			for (int i = 0; i < appData.exercises.Length; ++i)
			{
				arr[i] = appData.exercises[i].name;
			}
			return arr;
		}
	}

	bool TryFindSelectedExerciseIndex(out int index)
	{
		if (string.IsNullOrEmpty(SelectedExerciseName))
		{
			index = -1;
			return false;
		}
		return TryFindExerciseIndex(SelectedExerciseName, out index);
	}

	bool TryFindExerciseIndex(string exerciseName, out int index)
	{
		for (int i = 0; i < appData.exercises.Length; ++i)
		{
			if (appData.exercises[i].name == exerciseName)
			{
				index = i;
				return true;
			}
		}
		index = -1;
		return false;
	}

	bool TryFindSelectedWordIndex(out int wordIndex, out int exerciseIndex)
	{
		if (string.IsNullOrEmpty(SelectedWordName))
		{
			wordIndex = -1;
			exerciseIndex = -1;
			return false;
		}
		return TryFindWordIndex(SelectedWordName, out wordIndex, out exerciseIndex);
	}

	bool TryFindWordIndex(string wordName, out int wordIndex, out int exerciseIndex)
	{
		if (!TryFindSelectedExerciseIndex(out exerciseIndex))
		{
			wordIndex = -1;
			return false;
		}
		for (int i = 0; i < appData.exercises[exerciseIndex].words.Length; ++i)
		{
			if (appData.exercises[exerciseIndex].words[i] == wordName)
			{
				wordIndex = i;
				return true;
			}
		}
		wordIndex = -1;
		return false;
	}

	public string[] WordArray(string exerciseName)
	{
		for (int i = 0; i < appData.exercises.Length; ++i)
		{
			if (appData.exercises[i].name == exerciseName)
			{
				var len = appData.exercises[i].words.Length;
				string[] arr = new string[len];
				for (int k = 0; k < len; ++k)
				{
					arr[k] = appData.exercises[i].words[k];
				}
				return arr;
			}
		}
		return new string[0];
	}

	public void HandleCommandQueue()
	{
		isDirty = false;
		while (appDataCommandQueue.Count > 0)
		{
			var command = appDataCommandQueue.Dequeue();
			command.Exec();
		}
	}
}