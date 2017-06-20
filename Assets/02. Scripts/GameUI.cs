using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameUI : MonoBehaviour
{
	public Text txtScore;
	private int totScroe = 0;

	// Use this for initialization
	void Start ()
	{
		totScroe = PlayerPrefs.GetInt("TOT_SCORE", 0);
		DispScore(0);
	}
	
	public void DispScore(int score)
	{
		totScroe += score;
		txtScore.text = "score <color=#ff0000>" + totScroe.ToString() + "</color>";

		PlayerPrefs.SetInt("TOT_SCORE", totScroe);
	}
}
