using UnityEngine;
using System.Collections;

public class TowerAiming : MonoBehaviour {

	public int cost = 5;

	Transform turretTransform;
	Transform tipTransform;
	public float range = 10;
	public GameObject bulletPrefab;

	public float fireCooldown = 0.5f;
	public float damage = 2f;
	public float blastRaduis = 0;
	float coolDownLeft = 0f;
	// Use this for initialization
	void Start () {
		turretTransform = transform.Find ("Head");
		tipTransform = transform.Find ("Tip");
	}
	
	// Update is called once per frame
	void Update ()
	{
		Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

		Enemy nearestEnemy = null;
		float dist = Mathf.Infinity;

		foreach (Enemy e in enemies) {
			float d = Vector3.Distance (this.transform.position, e.transform.position);
			if (nearestEnemy == null || d < dist) {
				nearestEnemy = e;
				dist = d;
			}
		}

		if (nearestEnemy == null) {
			Debug.Log ("No Enemies");
			return;
		} 


		Vector3 dir = nearestEnemy.transform.position - this.transform.position; 
		Quaternion LookR = Quaternion.LookRotation(dir);

		turretTransform.rotation = Quaternion.Euler (LookR.eulerAngles.x, LookR.eulerAngles.y, 0);
		tipTransform.rotation = Quaternion.Euler (LookR.eulerAngles.x, LookR.eulerAngles.y, 0);

		coolDownLeft -= Time.deltaTime;

		if (coolDownLeft <= 0  && dir.magnitude <= range) 
		{
			shootAt(nearestEnemy);
			coolDownLeft = fireCooldown;
		}
	}

	void shootAt(Enemy e)
	{
		GameObject bulletGO = (GameObject)Instantiate (bulletPrefab, tipTransform.position, tipTransform.rotation);

		Bullet b = bulletGO.GetComponent<Bullet> ();
		b.damage = this.damage;
		b.target = e.transform;
		b.blastRaduis = this.blastRaduis;
	}
}