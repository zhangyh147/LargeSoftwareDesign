using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

	public void OnGameStart()
	{
		SceneManager.LoadScene(1);
	}

	public void OnGameExit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
#endif
		Application.Quit();
	}
}
