using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	public int lives = 20;
	public int money = 100;


	public Text moneyText;
	public Text livesText;
	public void loselife(int l = 1)
	{
		lives -= 1;
		if (lives <= 0) 
		{
			GameOver();
		}
	}

	public void GameOver()
	{
		Debug.Log ("Game Over");
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = ("Money: €" + money.ToString());
		livesText.text = ("Lives: " + lives.ToString());
	}
}
