using UnityEngine;
using System.Collections;

public class SpawnTurrets : MonoBehaviour {

	void OnMouseUp()
	{
		Debug.Log("Tower Spot clicked");

		BuildingManager bm = GameObject.FindObjectOfType<BuildingManager> ();

		if (bm.selectedTower != null) {
			ScoreManager sm = GameObject.FindObjectOfType<ScoreManager> ();

			if (sm.money < bm.selectedTower.GetComponent<TowerAiming>().cost) 
			{
				Debug.Log ("No monies!");
				return;
			}

			sm.money -= bm.selectedTower.GetComponent<TowerAiming> ().cost;
			Instantiate(bm.selectedTower, this.transform.position, transform.parent.rotation);
			Destroy(gameObject);
		}
	}
}
