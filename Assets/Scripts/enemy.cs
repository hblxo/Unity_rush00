using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
	public float speed = 1f;
	private Vector3 _botPos;
	private Animator _animator;
	private Vector3 _target;
	private bool _isMoving;

	
	// Use this for initialization
	void Start () {
		_botPos = transform.position;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (_isMoving)
			transform.position = Vector3.MoveTowards(transform.position, _botPos, speed * Time.deltaTime);
		if (transform.position == _botPos)
		{
			_isMoving = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D bot)
	{
		Move(bot.gameObject);
	}
	
	
	public void Move(GameObject bot)
	{
		_botPos = bot.transform.position;
		_botPos.z = transform.position.z;
		_target = bot.transform.position;
		_target.z = 0;

		Vector3 objectPos = transform.position;
		_target.x = _target.x - objectPos.x;
		_target.y = _target.y - objectPos.y;
		float angle = Mathf.Atan2(_target.y, _target.x) * Mathf.Rad2Deg;
		angle += 90;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		_isMoving = true;
	}
}
