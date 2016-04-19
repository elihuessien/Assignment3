using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	int waveOn = 0;
	float spawnCD = 1f;
    int breakTime = 10;
    float time = 0;
	float spawnCDRemaining = 0;

	[System.Serializable]
	public class WaveComponent 
	{
		public GameObject enemyPrefab;
		public int num;
        public int Level;

        [System.NonSerialized]
		public int spawned = 0;
	}

	public WaveComponent[] waveComps;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		bool didSpawn = false;
		spawnCDRemaining -= Time.deltaTime;
		if (spawnCDRemaining <= 0) {
			spawnCDRemaining = spawnCD;

			//Spawn wave;
			foreach(WaveComponent wc in waveComps){
				if (wc.spawned < wc.num) {
                    int l = wc.Level;
					//spawn it
					wc.spawned++;
					GameObject Enemy = (GameObject)Instantiate (wc.enemyPrefab, this.transform.position, this.transform.rotation);

                    Enemy e = Enemy.GetComponent<Enemy>();
                    e.SetLevel(l);
                    didSpawn = true;
					break;
				}
			}


			if (didSpawn == false) {

                
                    //spaw next wave object
                    if (waveOn < transform.parent.childCount)
                    {
                        transform.parent.GetChild(waveOn + 1).gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("No more enemies");
                    }
                    transform.parent.GetChild(waveOn).gameObject.gameObject.SetActive(false);
                    waveOn++;

                    time = 0;
                
			}
		}
	}
}
