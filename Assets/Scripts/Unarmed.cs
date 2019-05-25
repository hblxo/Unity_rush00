using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unarmed : Weapon {

	// Use this for initialization
	private new void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	private new void Update () {
		base.Update();
	}

	public override void Shoot()
	{

		if (Time.time > NextShot)
		{
			var direction = -transform.up;
			direction.z = 0f;
			direction.x += Random.Range(-Spread, Spread);
			direction.y += Random.Range(-Spread, Spread);
			direction = direction.normalized;
			var clone = Instantiate(Projectile, transform.position + direction * 0.4f, transform.rotation * Quaternion.Euler(0, 0, -90f));
			Bullet blt = clone.GetComponent<Bullet>();
			blt.transform.parent = transform;
			blt.Direction = direction;
			NextShot = Time.time + FireRate;
		}
	}

	public override void Equip(GameObject parent)
	{
		Quaternion save = transform.parent.rotation;
		transform.parent.rotation = new Quaternion(0,0,0,0); // very degueulasse but i'm nul en maths so foutez moi la paix
		transform.position = transform.parent.transform.position + new Vector3(-0.2f, -0.2f, 0f);
		transform.parent.rotation = save;
		transform.rotation = transform.parent.rotation;
		IsEquipped = true;
	}
	
	public void Drop()
	{
		
	}
}
