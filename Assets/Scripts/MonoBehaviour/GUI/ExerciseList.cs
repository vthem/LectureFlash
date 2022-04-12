public class ExerciseList : AbstractList
{

	protected override string[] GetEntries()
	{
		return GlobalData.runtimeAppData.ExerciseNameArray;

	}

	protected override bool ShouldUpdate()
	{
		return GlobalData.runtimeAppData.isDirty;
	}
}
