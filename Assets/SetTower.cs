using UnityEngine;
using System.Collections;

public class SetTower : MonoBehaviour {

    public int Selected = 1;
    public GameObject[] towers;
    public float[] prices;
    public GameObject Tile;
    private Money mscr;
	// Use this for initialization
	void Start ()
    {
        mscr = GameObject.Find("Working").GetComponent<Money>();
	}
  
    // Update is called once per frame
    void Update ()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            
            if (hit.transform.tag == "tile")
            {
                Debug.Log(hit.collider.name);
                Tile = hit.transform.gameObject;
            }
            else
            {
                Tile = null;
            }
        }


        if(Input.GetMouseButtonDown(0) && Tile !=null)
        {
            
            TileTaken tscr = Tile.GetComponent<TileTaken>();
            
            if(!tscr.isTaken && mscr.money>prices[Selected])
            {
                mscr.money -= prices[Selected];
                Vector3 pos = new Vector3(Tile.transform.position.x, (Tile.transform.position.y) + 3F, Tile.transform.position.z);
                tscr.Tower = (GameObject)Instantiate(towers[Selected], pos, Quaternion.identity);
                tscr.isTaken = true;
            }
            
        }
	}
   
    public void chaneselected(int sel)
    {
        Selected = sel;
    }
}
