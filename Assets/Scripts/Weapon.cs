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
	public float Force;
	public int Ammo;
	public float NextShot;
	public Sprite ViewModel;
	public Sprite WorldModel;
	public float Spread;
	public bool IsEquipped = false;
	public int NumberOfShots = 1;
	private Rigidbody2D _body;
	[FormerlySerializedAs("_target")] public Vector3 Target;
	[FormerlySerializedAs("_direction")] public Vector3 Direction;
	
	// Use this for initialization
	public void Start ()
	{
		_body = GetComponent<Rigidbody2D>();

		NextShot = 0f;
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
//		if (!this.IsEquipped && this.Target != transform.position && Target.x != 0f && Target.y != 0f)
//		{
//			transform.Rotate (Vector3.forward * -25);
//		}
		//while (Mathf.Abs(_body.velocity.x) > 0.01f || Mathf.Abs(_body.velocity.y) > 0.01f)
		//{
//			transform.Rotate (Vector3.forward * -25);
		//}
	}

	public virtual void FixedUpdate () 
	{
//		if (!this.IsEquipped && this.Target != transform.position && Target.x != 0f && Target.y != 0f)
//		{
//			transform.Rotate (Vector3.forward * -25);
//		}
		//while (Mathf.Abs(_body.velocity.x) > 0.01f || Mathf.Abs(_body.velocity.y) > 0.01f)
		//{
//			transform.Rotate (Vector3.forward * -25);
		//_body.velocity *= 0.90f;
		//}
	}
	
	public virtual void Shoot()
	{
		if (Time.time > NextShot && Ammo > 0)
		{
			for (int i = 0; i < NumberOfShots; i++)
			{
				var direction = -transform.up;
				direction.z = 0f;
				direction.x += Random.Range(-Spread, Spread);
				direction.y += Random.Range(-Spread, Spread);
				direction = direction.normalized;
				var clone = Instantiate(Projectile, transform.position + direction * 0.4f, transform.rotation);
				Bullet blt = clone.GetComponent<Bullet>();
				blt.Direction = direction;
			}
			NextShot = Time.time + FireRate;
			Ammo--;
		}
	}

	public virtual void Equip()
	{
		Quaternion save = transform.parent.rotation;
		gameObject.GetComponent<SpriteRenderer>().sprite = ViewModel;
		transform.parent.rotation = new Quaternion(0,0,0,0); // very degueulasse but i'm nul en maths so foutez moi la paix
		transform.position = transform.parent.transform.position + new Vector3(-0.2f, -0.2f, 0f);
		transform.parent.rotation = save;
		transform.rotation = transform.parent.rotation;
		IsEquipped = true;
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
		_body.velocity = new Vector2(0, 0);
		_body.bodyType = RigidbodyType2D.Kinematic;
	}
	
	public void Drop(Vector3 target)
	{
		gameObject.transform.parent = null;
		gameObject.GetComponent<SpriteRenderer>().sprite = WorldModel;
		target.z = 0;
		Direction = (target - transform.position).normalized;
		Direction.z = 0f;
		Target = transform.position + Direction * 2;
		IsEquipped = false;
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
		_body.bodyType = RigidbodyType2D.Dynamic;
		_body.velocity = -gameObject.transform.up * Force;
		_body.AddTorque(Random.Range(-180f, 180f));
	}
}
