using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {

	// Use this for initialization
	void Start ()
	{
		this.Projectile = Resources.Load("BulletPistolPrefab") as GameObject;
		this.Ammo = 12;
		this.FireRate = 0.5f;
		Debug.Log(this.Projectile);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
