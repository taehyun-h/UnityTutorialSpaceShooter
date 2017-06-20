using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
	public Transform[] points;
	public GameObject monsterPrefab;
	public List<GameObject> monsterPool = new List<GameObject>();

	public float creatTime = 2.0f;
	public int maxMonster = 10;
	public bool isGameOver = false;

	public static GameMgr instance = null;
	public float sfxVolumn = 1.0f;
	public bool isSfxMute = false;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

		for (int i = 0; i < maxMonster; i ++)
		{
			GameObject monster = (GameObject)Instantiate(monsterPrefab);
			monster.name = "Monster_" + i.ToString();
			monster.SetActive(false);
			monsterPool.Add(monster);
		}

		if (points.Length > 0)
		{
			StartCoroutine(this.CreateMonster());
		}
	}

	IEnumerator CreateMonster()
	{
		while (!isGameOver)
		{
			yield return new WaitForSeconds(creatTime);

			if (isGameOver) yield break;

			foreach (GameObject monster in monsterPool)
			{
				if (!monster.activeSelf)
				{
					int idx = Random.Range(1, points.Length);
					monster.transform.position = points[idx].position;
					monster.SetActive(true);
					break;
				}
			}

			/*
			int monsterCount = (int) GameObject.FindGameObjectsWithTag("MONSTER").Length;
			if (monsterCount < maxMonster)
			{
				yield return new WaitForSeconds(creatTime);

				int idx = Random.Range(1, points.Length);
				Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);
			}
			else
			{
				yield return null;
			}
			*/
		}
	}

	public void PlaySfx(Vector3 pos, AudioClip sfx)
	{
		if (isSfxMute) return;

		GameObject soundObj = new GameObject("Sfx");
		soundObj.transform.position = pos;
		AudioSource audioSource = soundObj.AddComponent<AudioSource>();
		audioSource.clip = sfx;
		audioSource.minDistance = 10.0f;
		audioSource.maxDistance = 30.0f;
		audioSource.volume = sfxVolumn;
		audioSource.Play();

		Destroy(soundObj, sfx.length);
	}
}
