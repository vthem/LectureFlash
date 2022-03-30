using System;

[Serializable]
public class Exercise
{
	public float displayDuration = 1f;
	public string name;
	public string[] words;
}

[Serializable]
public class AppData
{
	public Exercise[] exercises;
}