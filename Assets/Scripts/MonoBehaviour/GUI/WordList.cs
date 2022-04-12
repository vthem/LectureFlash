
public class WordList : AbstractList
{
	protected override string[] GetEntries()
	{
		return GlobalData.runtimeAppData.WordArray(GlobalData.runtimeAppData.SelectedExerciseName);
	}

	protected override bool ShouldUpdate()
	{
		return GlobalData.runtimeAppData.isDirty;
	}
}
