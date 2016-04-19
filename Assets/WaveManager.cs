using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

    public int NumOut;
    public GameObject[] Enemies;
    public float Cooldown;
    private float cd;
    public int[] positions;
	// Use this for initialization
	void Start ()
    {
        cd = Cooldown * 2;
	}
	
	// Update is called once per frame
	void Update ()
    {
	if(cd > 0)
        {
            cd -= Time.deltaTime;
        }
    else
        {
            cd = Cooldown;
            Vector3 pos = new Vector3(positions[(int) (Random.Range(0, 5))], -5, 475);
            int index = Random.Range(0, Enemies.Length);
            Instantiate(Enemies[index], pos, Quaternion.Euler(0, 180, 0));
        }
	}
}
