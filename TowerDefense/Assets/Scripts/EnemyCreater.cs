using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater : MonoBehaviour {

	public static int countEnemyAlive = 0;
	public Wave[] waves;
	public Transform START;
	public float waveRate;
	private Coroutine coroutine;

	// Use this for initialization
	void Start () {
		coroutine = StartCoroutine(CreateEnemy());
	}

	//游戏停止
	public void Stop()
	{
		StopCoroutine(coroutine);
	}
	
	IEnumerator CreateEnemy()
	{
		foreach(Wave wave in waves)
		{
			for(int i=0;i<wave.count;i++)
			{
				GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
				countEnemyAlive++;
				if (i != wave.count - 1)
					yield return new WaitForSeconds(wave.rate);
			}
			while(countEnemyAlive>0)
			{
				yield return 0;
			}
			yield return new WaitForSeconds(waveRate);
		}
		while(countEnemyAlive>0)
		{
			yield return 0;
		}
		GameManagers.Instance.Win();
	}
}
