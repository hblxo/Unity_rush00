using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Weapon : MonoBehaviour
{

	public GameObject Projectile;
	public float FireRate;
	public float Range;
	public float BulletSpeed;
	public int Ammo;
	public float NextShot;
	public Sprite ViewModel;
	public Sprite WorldModel;
	public float Spread;
	public bool IsEquipped = false;
	public int NumberOfShots = 1;
	[FormerlySerializedAs("_target")] public Vector3 Target;
	[FormerlySerializedAs("_direction")] public Vector3 Direction;
	
	// Use this for initialization
	void Start ()
	{
		NextShot = 0f;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if (!this.IsEquipped && this.Target != transform.position)
		{
			transform.position += this.Direction * 0.05f;
			transform.Rotate (Vector3.forward * -25);
		}
	}

	public void Shoot(Transform source, Vector3 target)
	{
		if (Time.time > NextShot && Ammo > 0)
		{
			for (int i = 0; i < NumberOfShots; i++)
			{
				var clone = Instantiate(Projectile, transform.position, transform.rotation);
				Bullet blt = clone.GetComponent<Bullet>();
				target.z = 0f;
				blt.Direction = (target - transform.position).normalized;
				blt.Direction.x += Random.Range(-Spread, Spread);
				blt.Direction.y += Random.Range(-Spread, Spread);
				blt.Direction = blt.Direction.normalized;
			}
			NextShot = Time.time + FireRate;
			Ammo--;
		}
	}

	public void Drop(Vector3 target)
	{
		target.z = 0;
		Direction = (target - transform.position).normalized;
		Direction.z = 0f;
		Target = transform.position + Direction * 2;
		IsEquipped = false;
	}
}
