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
			for (int i = 0; i < appData.exercises.Length; ++i)
			{
				if (appData.exercises[i].name == runtimeAppData.SelectedExerciseName)
				{
					appData.exercises[i].name = newName;
					break;
				}
			}
			runtimeAppData.isDirty = runtimeAppData.isDataDirty = true;
		}
	}
}