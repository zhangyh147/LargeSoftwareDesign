using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretManager : MonoBehaviour {
	public TurretData laserData;
	public TurretData missileData;
	public TurretData standardData;

	//消灭敌人获得的金币
	public static float moneyFromEnemy = 0;
	//检测是否有敌人被消灭
	public static bool enemyDie = false;

	//当前选择的炮台
	private TurretData selectedTurret;

	private MapCube selectedMapCube;

	public Text moneyText;

	public Animator moneyAnimator;
	//玩家金币
	private int totalMoney = 5000;

	//控制升级按钮的显示
	public GameObject upgradeCanvas;
	public Button buttonUpgrade;

	void ChangeMoney(int change=0)
	{
		totalMoney += change;
		moneyText.text = "$" + totalMoney;
	}

	void Start()
	{
		ChangeMoney();
	}
	void Update()
	{
		GetMoneyFromEnemy();
		if(Input.GetMouseButtonDown(0))
		{
			if(EventSystem.current.IsPointerOverGameObject()==false)
			{
				//建造炮台
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				bool isCollider=Physics.Raycast(ray,out hit, 1000, LayerMask.GetMask("MapCube"));
				if(isCollider)
				{
					MapCube mapCube = hit.collider.GetComponent<MapCube>();
					if(mapCube.turretGo==null&&selectedTurret!=null)
					{
						//可以创建
						if(totalMoney>=selectedTurret.cost)
						{
							ChangeMoney(-selectedTurret.cost);
							mapCube.BuildTurret(selectedTurret);
						}
						else
						{
							//提示钱不够
							moneyAnimator.SetTrigger("Flicker");
						}
					}
					else if(mapCube.turretGo != null)
					{
						//升级处理
						ShowUpgradeUI(mapCube.transform.position,mapCube.isUpgrade);
					}
					selectedMapCube = mapCube;
				}
			}
		}
	}

	void GetMoneyFromEnemy()
	{
		if(enemyDie)
		{
			ChangeMoney((int)moneyFromEnemy);
			enemyDie = false;
		}
	}

	public void OnLaserSelected(bool isOn)
	{
		if(isOn)
		{
			selectedTurret = laserData;
		}
	}
	public void OnMissileSelected(bool isOn)
	{
		if(isOn)
		{
			selectedTurret = missileData;
		}
	}
	public void OnStandaredSelected(bool isOn)
	{
		if(isOn)
		{
			selectedTurret = standardData;
		}
	}

	//显示升级界面
	void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
	{
		upgradeCanvas.SetActive(true);
		pos.y += 4;
		upgradeCanvas.transform.position = pos;
		buttonUpgrade.interactable = !isDisableUpgrade;
	}

	//隐藏升级界面
	void HideUpgradeUI()
	{
		upgradeCanvas.SetActive(false);
	}

	//按下升级按钮
	public void OnUpgradeButtonDown()
	{
		if(totalMoney>=selectedMapCube.turretData.costUpgrade)
		{
			ChangeMoney(-selectedMapCube.turretData.costUpgrade);
			selectedMapCube.UpgradeTurret();
			HideUpgradeUI();
		}
		else
		{
			//提示钱不够
			moneyAnimator.SetTrigger("Flicker");
		}
	}

	//按下拆除按钮
	public void OnDestroyButtonDown()
	{
		selectedMapCube.DestroyTurret();
		ChangeMoney((int)(selectedTurret.cost * 0.8));//返还80%金币
		HideUpgradeUI();
	}
}
