using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	// Use this for initialization
    public Transform Player;
    public Transform biginColider;
    public Transform endCollider;
    public float clapbegin = 100;
    public float clapEnd = 100;

	void Start () 
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        // Calculations assume map is position at the origin
        float minX = horzExtent - clapbegin / 2;
        float maxX = clapEnd / 2 - horzExtent;

        //float minY = vertExtent - ClampYBegin / 2;
        //float maxY = clampYEnd / 2 - vertExtent;
       

        Vector3 newvec3 = new Vector3(Player.position.x,transform.position.y,transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newvec3, Time.time);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);

	}
}
