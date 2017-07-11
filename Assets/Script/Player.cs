using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

	// Use this for initialization
    float speed = 3;
    Animator animator;
    Rigidbody2D rig;
    public Transform Isgrounded;
    public Transform SpawnShoot;
    public SpriteRenderer WeaponRot;
    public GameObject[] objetos;
    public AudioSource Som;
    public AudioClip[] Clip;

    public ParticleSystem HitParticle;
    Text texto;
    int bala = 100;
    bool change = false;
    int auxbal;
    

	void Start () 
    {
        HitParticle.Pause();
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //bala = 7;        
        texto = GameObject.FindGameObjectWithTag("textu").GetComponent<Text>();
        auxbal = 101;
	}
	
	// Update is called once per frame
	void Update () 
    {
        float horizontal = Input.GetAxis("Horizontal");
		bool Jump = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);
        bool fire = Input.GetButtonDown("Fire1");
        Moviment(horizontal,Jump);
        Shoot(fire);
        RotateWeapon();

        texto.text = "x " + bala;

        if (bala >= auxbal)
        {
        #if UNITY_EDITOR
            //Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }

	}

    void Moviment(float x,bool jmp)
    {
        if (x > 0)
        {
            Quaternion rot = Quaternion.Euler(0, 0, 0);
            transform.rotation = rot;
            transform.position += Vector3.right * speed * Time.deltaTime;
            //WeaponRot.enabled = false;
            Vector2 starpoint = new Vector2(transform.position.x, transform.position.y - 0.7f);
            RaycastHit2D hit = Physics2D.Linecast(starpoint, Isgrounded.position);
            if (hit && !Som.isPlaying)
            {
                Som.clip = Clip[0];
                Som.Play();
            }
        }
        if (x < 0)
        {
            Quaternion rot = Quaternion.Euler(0, 180, 0);
            transform.rotation = rot;
            transform.position += Vector3.left * speed * Time.deltaTime;
            //WeaponRot.enabled = false;
            Vector2 starpoint = new Vector2(transform.position.x, transform.position.y - 0.7f);
            RaycastHit2D hit = Physics2D.Linecast(starpoint, Isgrounded.position);
            if (hit && !Som.isPlaying)
            {
                Som.clip = Clip[0];
                Som.Play();
            }
        }        
        
        if(jmp)
        {
            Vector2 starpoint = new Vector2(transform.position.x, transform.position.y - 0.7f);
            RaycastHit2D hit = Physics2D.Linecast(starpoint, Isgrounded.position);                   
            
            if(hit)
            {
                rig.AddForce(Vector2.up * 300);
                animator.SetTrigger("Jump");
                //WeaponRot.enabled = false;
                Som.clip = Clip[1];
                Som.Play();
            }
            
        }
         
        animator.SetFloat("Runing", Mathf.Abs(x));
        
    }
    void Shoot(bool fire)
    {
        if (Time.timeScale == 0)
        {
            fire = false;
        }

        if (fire && bala >= 1)
        {
            Vector2 starpoint = new Vector2(transform.position.x, transform.position.y - 0.7f);
            RaycastHit2D hit = Physics2D.Linecast(starpoint, Isgrounded.position);

            if (hit)
            {
                Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                HitParticle.Play();
                RaycastHit2D Hitshoot = Physics2D.Linecast(SpawnShoot.position, v);
                Debug.DrawLine(SpawnShoot.position, v, Color.red);
                animator.SetTrigger("Fire");
                WeaponRot.enabled = true;
                
                Som.clip = Clip[2];
                Som.Play();

                if (!Hitshoot || !Hitshoot.transform.GetComponent<Objetos>())
                {
                    return;
                }

                print(Hitshoot.transform.gameObject.name);

                int randpossibility = Random.Range(0, 100);

                if (!change)
                {
                    int randobj = Random.Range(0, objetos.Length);

                    while (randobj == Hitshoot.transform.GetComponent<Objetos>().ID)
                    {
                        randobj = Random.Range(0, objetos.Length);
                    }

                    Instantiate(objetos[randobj], Hitshoot.transform.position, Hitshoot.transform.rotation);
                    Destroy(Hitshoot.transform.gameObject);
                    diminuiBala();
                    change = !change;
                }
                else
                {
                    int randDirection = Random.Range(0, 360);
                    if (SceneManager.GetActiveScene().buildIndex == 1)
                        randDirection = 270;
                    Hitshoot.rigidbody.isKinematic = false;
                    Vector2 vec = calcvector(randDirection);
                    vec.Normalize();
                    Hitshoot.rigidbody.AddForce(vec * 300);

                    diminuiBala();
                    change = !change;
                }
            }   
            
        }
    }
    void RotateWeapon()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(WeaponRot.transform.position);
        float angle = 0;
        if (WeaponRot.transform.eulerAngles.y < 10)
        {
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Mathf.Atan2(dir.y, -dir.x) * Mathf.Rad2Deg;
        }

        angle = Mathf.Clamp(angle, -90, 90);
        Quaternion quad = Quaternion.Euler(WeaponRot.transform.eulerAngles.x, WeaponRot.transform.eulerAngles.y, angle);
        WeaponRot.transform.rotation = quad;
    }
    Vector2 calcvector(float angulo)
    {
        float magnetude = 1;
        angulo = angulo * Mathf.PI / 180;
        float vx = magnetude*Mathf.Cos(angulo);
        float vy = magnetude*Mathf.Sin(angulo);
        Vector2 vec = new Vector2(vx,vy);
        return vec;
    }

    void diminuiBala()
    {
        auxbal = bala;       
        bala--;
        print(auxbal);
        
        
    }

   
    
}
