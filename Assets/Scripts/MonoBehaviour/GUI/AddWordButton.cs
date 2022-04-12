using UnityEngine;
using UnityEngine.UI;

public class AddWordButton : MonoBehaviour
{
	private Button button;

	private void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(ClickHandler);
	}

	private void ClickHandler()
	{
		new RuntimeAppData.Command_AddWord();
	}
}
