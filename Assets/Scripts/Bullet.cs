using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 15f;
	public Transform target;
	public float damage = 1;
	public float blastRaduis = 1;
	
	// Update is called once per frame
	void Update () 
	{
		if (target == null) {
			//enemy went away
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - this.transform.localPosition;
		float distThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distThisFrame) 
		{
			//we reached the node
			DoBulletHit();
		}
		else 
		{
			transform.Translate (dir.normalized * distThisFrame, Space.World);
			this.transform.rotation = Quaternion.LookRotation(dir);
		}
	}

	void DoBulletHit()
	{
		if (blastRaduis == 0) 
		{
			target.GetComponent<Enemy> ().TakeDamage (damage);
		} 
		else 
		{
			Collider[] cols = Physics.OverlapSphere (transform.position, blastRaduis);

			foreach (Collider c in cols) 
			{
				Enemy e = c.GetComponent<Enemy> ();
				if (e != null) 
				{
					target.GetComponent<Enemy> ().TakeDamage (damage);
				}
			}
		}
		Destroy(gameObject);
	}
}
