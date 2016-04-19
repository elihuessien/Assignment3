using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    public bool isTower;
    private health hscr;
    private Money mscr;
    private EnemyStats escr;
    // Use this for initialization
	void Start ()
    {
        hscr = gameObject.GetComponent<health>();
        mscr = GameObject.Find("Working").GetComponent<Money>();
        if(!isTower)
        {
            escr = gameObject.GetComponent<EnemyStats>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(hscr.life <=0)
        {
            if(isTower)
            {
                Destroy(gameObject);
            }
            else
            {
                mscr.money += escr.Worth;
                Destroy(gameObject);
            }
        }
	}
}
