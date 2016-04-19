using UnityEngine;
using System.Collections;

public class FindMapPosition : MonoBehaviour {

	GameObject levelView;
	GameObject levels;
	Transform targetlevel;

	public int level = 0;
	int changedetector;

	public float speed = 50f;

	// Use this for initialization
	void Start () {
		levelView = GameObject.Find("Camera Spots");
		levels = GameObject.Find("Levels");
		changedetector = level;
		ActivateLevel ();
		GetPosition ();
	}
	
	// Update is called once per frame
	void Update () {

		//level change detected
		if (changedetector != level) {
			GetPosition ();
			ActivateLevel ();
			changedetector = level;
		}


		//move camera to target location
		Vector3 dir = targetlevel.position - this.transform.localPosition;
		float distThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distThisFrame) 
		{
			//we reached the node
			reachedGoal ();
		}
		else 
		{
			transform.Translate (dir.normalized * distThisFrame, Space.World);
			Quaternion targetRotation = Quaternion.LookRotation(dir);
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, targetRotation, Time.deltaTime*4);
		}
			
	}

	public void setlevel ( int level)
	{
		this.level = level;
	}

	void ActivateLevel()
	{
		if (level < levels.transform.childCount) {

			//Turn off all levels
			for(int i = 0; i < levels.transform.childCount; i++) {
				levels.transform.GetChild(i).gameObject.SetActive (false);
			}

			//turn on the level we are playing
			levels.transform.GetChild(level).gameObject.SetActive (true);
		}
		else {
			level = 0;
			Debug.Log ("More levels!!!");
		}
	}

	void GetPosition(){
		targetlevel = levelView.transform.GetChild (level);
	}

	void reachedGoal ()
	{
		if (this.transform.rotation == targetlevel.transform.rotation) {
			//reached goal
		}
		else {
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, targetlevel.transform.rotation, Time.deltaTime*4);
		}
	}

	public void nextLevel ()
	{
		level++;
	}
}