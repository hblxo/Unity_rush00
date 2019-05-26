using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Weapon {

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
			_source.PlayOneShot(ShotSound);
			var direction = -transform.up;
			direction.z = 0f;
			direction.x += Random.Range(-Spread, Spread);
			direction.y += Random.Range(-Spread, Spread);
			direction = direction.normalized;
			var clone = Instantiate(Projectile, transform.position + direction * 0.6f, transform.rotation * Quaternion.Euler(0, 0, -90f));
			Bullet blt = clone.GetComponent<Bullet>();
			blt.transform.parent = transform;
			blt.Direction = direction;
			NextShot = Time.time + FireRate;
			clone.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		}
	}

	public override void Equip(GameObject parent)
	{
		base.Equip(parent);
	}
	
	public void Drop()
	{
		base.Drop();
	}
}
