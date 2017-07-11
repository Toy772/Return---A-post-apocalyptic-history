using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

	// Use this for initialization
    SpriteRenderer Sprite;
    public Sprite sprOpen;
    public Sprite sprClosed;
    public GameObject stageclereCanvas;
    public GameObject terminoooooo;
    float time = 2;

    bool stageclear = false;
    bool instiated = false;

	void Start () 
    {
        Sprite = GetComponent<SpriteRenderer>();
        Sprite.sprite = sprClosed;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(stageclear)
        {
            time -= Time.deltaTime;
            if(!instiated)
            {
                instiated = true;
                if (SceneManager.GetActiveScene().buildIndex == 3)
                    Instantiate(terminoooooo);
                else
                    Instantiate(stageclereCanvas);
            }
            
            if(time<0)
            {
                if (SceneManager.GetActiveScene().buildIndex == 3)
                    SceneManager.LoadScene(0);
                else
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Sprite.sprite = sprOpen;
            stageclear = true;            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Sprite.sprite = sprClosed;
        }
    }
}
