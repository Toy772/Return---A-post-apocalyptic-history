using UnityEngine;
using System.Collections;

public class placa : MonoBehaviour {

    // Use this for initialization
    bool trigger;
    public GameObject tuto;
    
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        tuto.SetActive(trigger);	
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trigger = false;
        }
    }
}
