using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagers : MonoBehaviour {

	public GameObject endUI;
	public Text endMessage;

	public static GameManagers Instance;
	private EnemyCreater enemyCreater;
	void Awake()
	{
		Instance = this;
		enemyCreater = GetComponent<EnemyCreater>();
	}

	//游戏胜利
	public void Win()
	{
		endUI.SetActive(true);
		endMessage.text = "Victory!";
	}

	//游戏失败
	public void Fail()
	{
		enemyCreater.Stop();
		endUI.SetActive(true);
		endMessage.text = "Defeat!";
	}

	//重玩按钮
	public void OnButtonRestart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	//返回按钮
	public void OnButtonBackMenu()
	{
		SceneManager.LoadScene(0);
	}
}
