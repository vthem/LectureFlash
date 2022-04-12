using System.Collections.Generic;

public partial class RuntimeAppData
{
	public abstract class Command
	{
		protected RuntimeAppData runtimeAppData;
		protected AppData appData;

		public Command()
		{
			runtimeAppData = GlobalData.runtimeAppData;
			appData = runtimeAppData.appData;
			runtimeAppData.appDataCommandQueue.Enqueue(this);
		}

		public abstract void Exec();
	}

	public class Command_SelectExercise : Command
	{
		public Command_SelectExercise(string exerciseName) : base()
		{
			ExerciseName = exerciseName;
		}

		public string ExerciseName { get; private set; }

		public override void Exec()
		{
			runtimeAppData.SelectedExerciseName = ExerciseName;
			runtimeAppData.SelectedWordName = null;
			runtimeAppData.isDirty = true;
		}
	}

	public class Command_AddExercise : Command
	{
		public override void Exec()
		{
			string newExerciceName = "Nouvel Exercice";
			if (appData.exercises[appData.exercises.Length - 1].name == newExerciceName)
				return;
			var ex = new Exercise();
			ex.name = newExerciceName;
			ex.words = new string[0];
			ex.displayDuration = 1.0f;
			List<Exercise> l = new List<Exercise>(appData.exercises);
			l.Add(ex);
			appData.exercises = l.ToArray();
			runtimeAppData.isDirty = runtimeAppData.isDataDirty = true;
		}
	}

	public class Command_RemoveSelectedExercise : Command
	{
		public override void Exec()
		{
			List<Exercise> l = new List<Exercise>(appData.exercises);
			int idx = l.FindIndex(_ex => _ex.name == runtimeAppData.SelectedExerciseName);
			if (idx < 0)
				return;
			l.RemoveAt(idx);
			appData.exercises = l.ToArray();
			runtimeAppData.isDirty = runtimeAppData.isDataDirty = true;
		}
	}

	public class Command_RenameSelectedExercise : Command
	{
		private readonly string newName;
		public Command_RenameSelectedExercise(string newName)
		{
			this.newName = newName;
		}

		public override void Exec()
		{
			if (runtimeAppData.TryFindSelectedExerciseIndex(out int i))
			{
				appData.exercises[i].name = newName;
			}
			runtimeAppData.isDirty = runtimeAppData.isDataDirty = true;
		}
	}

	public class Command_AddWord : Command
	{
		public override void Exec()
		{
			if (!runtimeAppData.TryFindSelectedExerciseIndex(out int i))
			{
				return;
			}
			string newWordName = "Nouveau Mot";
			List<string> l = new List<string>(appData.exercises[i].words);
			l.Add(newWordName);
			appData.exercises[i].words = l.ToArray();
			runtimeAppData.isDirty = runtimeAppData.isDataDirty = true;
		}
	}

	public class Command_SelectWord: Command
	{
		public Command_SelectWord(string wordName) : base()
		{
			WordName = wordName;
		}

		public string WordName { get; private set; }

		public override void Exec()
		{
			runtimeAppData.SelectedWordName = WordName;
			runtimeAppData.isDirty = true;
		}
	}

	public class Command_RemoveSelectedWord: Command
	{
		public override void Exec()
		{
			if (!runtimeAppData.TryFindSelectedWordIndex(out int wordIndex, out int exerciseIndex))
			{
				return;
			}
			List<string> l = new List<string>(appData.exercises[exerciseIndex].words);
			l.RemoveAt(wordIndex);
			appData.exercises[exerciseIndex].words = l.ToArray();
			runtimeAppData.isDirty = runtimeAppData.isDataDirty = true;
		}
	}

	public class Command_UpdateWord : Command
	{
		private readonly string newWord;

		public Command_UpdateWord(string newWord) : base()
		{
			this.newWord = newWord;
		}

		public override void Exec()
		{
			if (!runtimeAppData.TryFindSelectedWordIndex(out int wordIndex, out int exerciseIndex))
			{
				return;
			}
			UnityEngine.Debug.Log($"Command_UpdateWord > {appData.exercises[exerciseIndex].words[wordIndex]} -> {newWord}");
			appData.exercises[exerciseIndex].words[wordIndex] = newWord;
			runtimeAppData.isDirty = runtimeAppData.isDataDirty = true;
		}
	}
}