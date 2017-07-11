using UnityEngine;
using System.Collections;

public class Objetos : MonoBehaviour {

	// Use this for initialization
    int rand;
    public string Name;
    public int ID;
    public bool RandonOnStart = false;
	void Start () 
    {
       /* if(RandonOnStart)
        {
            Randomiza();
        }*/

	}
	
	// Update is called once per frame
	void Update () {
	
	}
   /* void Randomiza()
    {
        if (Name != "Empty")
        {
            rand = Random.Range(0, 100);
            if (rand < 50)
            {
                GetComponent<Rigidbody2D>().isKinematic = false;
            }
            else
            {
                GetComponent<Rigidbody2D>().isKinematic = true;
            }

            rand = Random.Range(0, 100);
            if (rand < 50)
            {
                GetComponent<Rigidbody2D>().gravityScale = -1;
            }
            else
            {
                GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }
    }*/
}
