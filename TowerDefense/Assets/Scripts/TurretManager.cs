using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretManager : MonoBehaviour {
	public TurretData laserData;
	public TurretData missileData;
	public TurretData standardData;

	//当前选择的炮台
	private TurretData selectedTurret;

	public Text moneyText;

	public Animator moneyAnimator;
	//玩家金币
	private int totalMoney = 10000;

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
							mapCube.BuildTurret(selectedTurret.turretPrefab);
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
				}
			}
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

	void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
	{
		upgradeCanvas.SetActive(true);
		pos.y += 4;
		upgradeCanvas.transform.position = pos;
		buttonUpgrade.interactable = !isDisableUpgrade;
	}

	void HideUpgradeUI()
	{
		upgradeCanvas.SetActive(false);
	}

	public void OnUpgradeButtonDown()
	{

	}

	public void OnDestroyButtonDown()
	{

	}
}
