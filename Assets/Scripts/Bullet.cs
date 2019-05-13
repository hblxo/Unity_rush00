using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public float Speed;
	public float StartTime;
	public bool Enemy;
	public Vector3 Target;
	public Vector3 Direction;
	public float Lifespan;
	private Rigidbody2D _body;
	
	// Use this for initialization
	public void Start ()
	{
		StartTime = Time.time;
		_body = GetComponent<Rigidbody2D>();
		_body.velocity = Direction * Speed;
	}
	
	// Update is called once per frame
	public void Update () {
		if (this.StartTime + Lifespan < Time.time)
		{
			Destroy(gameObject);
		}
		//transform.position += this.Direction * this.Speed;
	}

	public void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Block"))
		{
			Destroy(gameObject);
		}
		Debug.Log(other.gameObject);

	}
}
