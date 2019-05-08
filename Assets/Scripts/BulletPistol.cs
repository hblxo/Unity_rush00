using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPistol : Bullet
{

	// Use this for initialization
	private new void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	private new void Update()
	{
		base.Update();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);
	}
}
