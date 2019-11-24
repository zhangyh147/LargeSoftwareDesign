using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour {
	public TurretData laserData;
	public TurretData missileData;
	public TurretData standardData;

	private TurretData selectedTurret;

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
}
