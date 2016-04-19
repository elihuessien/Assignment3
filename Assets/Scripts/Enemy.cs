using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
	GameObject pathGO;

	Transform targetPathNode;
	int i = 0;

	public double speed = 5f;
    public int level;
	public float Maxhealth = 1;
	float Currenthealth = 0;
	public int moneyValue;
	public GameObject healthbar;
	public GameObject Mcamera;
	public GameObject canvas;
    public GameObject levelpick;
    public Text Leveltext;

	// Use this for initialization
	void Start () 
	{
		pathGO = GameObject.Find("Path");
		Mcamera = GameObject.Find("Main Camera");

    }

    public void SetLevel (int l)
    {
        level = l;

        Maxhealth += (5 * level);
        speed += (0.5 * level);
        moneyValue += (2 * level);
        Currenthealth = Maxhealth;
    }
	
	// Update is called once per frame
	void GetNextPathNode () 
	{
		if (i < pathGO.transform.childCount) {
			targetPathNode = pathGO.transform.GetChild (i);
			i++;
		}
		else {
			targetPathNode = null;
		}
	}

	void Update ()
	{
        Leveltext.text = (level.ToString());

        if (targetPathNode == null)
		{
			GetNextPathNode ();
			if (targetPathNode == null)
			{
				//no more path
				ReachedGoal();
				return;
			}
		}

		Vector3 dir = targetPathNode.position - this.transform.localPosition;
		float distThisFrame = (float)speed * Time.deltaTime;

		if (dir.magnitude <= distThisFrame)
		{
			//we reached the node
			targetPathNode = null;
		}
		else 
		{
			transform.Translate (dir.normalized * distThisFrame, Space.World);
			Quaternion targetRotation = Quaternion.LookRotation(dir);
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, targetRotation, Time.deltaTime*4);
		}

		canvas.transform.rotation = Mcamera.transform.rotation;
        levelpick.transform.rotation = Mcamera.transform.rotation;
    }

	void ReachedGoal ()
	{
		Destroy (gameObject);
		GameObject.FindObjectOfType<ScoreManager> ().loselife ();
	}

	public void TakeDamage(float damage)
	{

		Currenthealth -= damage;
		if (Currenthealth <= 0) 
		{
			Die();
		}

		float h = Currenthealth / Maxhealth;
		SetHealthBar (h);
	}

	public void Die()
	{
		GameObject.FindObjectOfType<ScoreManager> ().money += moneyValue;
		Destroy (gameObject);
	}

	public void SetHealthBar(float myHealth) 
	{
		healthbar.transform.localScale = new Vector3 (myHealth, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
	}
}