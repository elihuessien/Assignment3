using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public float Cooldown;
    private float cd;
    public GameObject projectile;
	// Use this for initialization
	void Start () {

        cd = Cooldown;
	}
	
	// Update is called once per frame
	void Update () {
	
        if(cd>0)
        {
            cd -= Time.deltaTime;

        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, 300))
            {
                if (hit.transform.tag == "enemy")
                {

                    cd = Cooldown;
                    Instantiate(projectile, transform.position, Quaternion.identity);
                }
            }
                
         }
            
        }
	}

