using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Anim
{
	public AnimationClip idle;
	public AnimationClip runForward;
	public AnimationClip runBackward;
	public AnimationClip runRight;
	public AnimationClip runLeft;
}

public class PlayerCtrl : MonoBehaviour {
	private float h = 0.0f;
	private float v = 0.0f;

	private Transform tr;
	public float moveSpeed = 10.0f;
	public float rotSpeed = 100.0f;

	public Anim anim;
	public Animation _animation;

	public int hp = 100;

	public delegate void PlayerDieHandler();
	public static event PlayerDieHandler OnPlayerDie;

	// Use this for initialization
	void Start () {
		tr = GetComponent <Transform>();

		_animation = GetComponentInChildren<Animation>();

		_animation.clip = anim.idle;
		_animation.Play();
	}
	
	// Update is called once per frame
	void Update () {
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");

		Debug.Log ("H=" + h.ToString());
		Debug.Log ("V=" + v.ToString());

		Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

		tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed, Space.Self);

		tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

		if (v >= 0.1f)
		{
			_animation.CrossFade(anim.runForward.name, 0.3f);
		}
		else if (v <= -0.1f)
		{
			_animation.CrossFade(anim.runBackward.name, 0.3f);
		}
		else if (h >= 0.1f)
		{
			_animation.CrossFade(anim.runRight.name, 0.3f);
		}
		else if (h <= -0.1f)
		{
			_animation.CrossFade(anim.runLeft.name, 0.3f);
		}
		else
		{
			_animation.CrossFade(anim.idle.name, 0.3f);
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "PUNCH")
		{
			hp -= 10;
			Debug.Log("Player HP = " + hp.ToString());

			if (hp <= 0)
			{
				PlayerDie();
			}
		}
	}

	void PlayerDie()
	{
		Debug.Log("Player Die !!");

/*
		GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

		foreach (GameObject monster in monsters)
		{
			monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
		}
*/

		OnPlayerDie();
	}
}