using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPistol : Bullet
{

	// Use this for initialization
	void Start()
	{
		this.Speed = 0.5f;
		this.Enemy = false;
		this.Lifespan = 3.0f;

		this.StartTime = Time.time;
		this.Target.z = 0.0F;
	}

	// Update is called once per frame
	private new void Update()
	{
		base.Update();
	}
}
