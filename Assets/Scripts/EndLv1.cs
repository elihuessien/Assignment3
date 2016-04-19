using UnityEngine;
using System.Collections;

public class EndLv1 : MonoBehaviour {
	GameObject pathGO;

	Transform targetPathNode;
	int i = 0;

	public float speed = 5f;

	// Use this for initialization
	void Start () 
	{
		pathGO = GameObject.Find("Path");
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
		float distThisFrame = speed * Time.deltaTime;

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

	}

	void ReachedGoal ()
	{
		Destroy (gameObject);
		GameObject.FindObjectOfType<FindMapPosition> ().nextLevel ();
	}
}