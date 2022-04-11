/**** 
 * Created by: Akram Taghavi-Burris
 * Date Created: March 16, 2022
 * 
 * Last Edited by: Krieger
 * Last Edited: April 11, 2022
 * 
 * Description: Hero ship controller
****/

/** Using Namespaces **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] //forces selection of parent object
public class Hero : MonoBehaviour
{
    /*** VARIABLES ***/

    #region PlayerShip Singleton
    static public Hero SHIP; //refence GameManager
   
    //Check to make sure only one gm of the GameManager is in the scene
    void CheckSHIPIsInScene()
    {

        //Check if instnace is null
        if (SHIP == null)
        {
            SHIP = this; //set SHIP to this game object
        }
        else //else if SHIP is not null send an error
        {
            Debug.LogError("Hero.Awake() - Attempeeeeeeeted to assign second Hero.SHIP"); //why fix typos when you can make them worse for the sake of the funny?
        }
    }//end CheckGameManagerIsInScene()
    #endregion

    GameManager gm; //reference to game manager
    ObjectPool pool; //reference to object pool
    AudioSource audioSource; //reference to the audio source component

    [Header("Ship Movement")]
    public float speed = 10;
    public float rollMult = -45;
    public float pitchMult = 30;



    [Space(10)]
    [Header("Projectile Settings")]
    public float projectileSpeed = 40f; //speed of projectile
    public AudioClip projectileSound; // sound clip of the projectile firing

    [Space(10)]
    private GameObject lastTriggerGo; //reference to the last triggering game object
   
    [Header("ShieldSettings")]
    [SerializeField] //show in inspector
    private float _shieldLevel = 1; //level for shields
    public int maxShield = 4; //maximum shield level
    
    //method that acts as a field (property), if the property falls below zero the game object is desotryed
    public float shieldLevel
    {
        get { return (_shieldLevel); }
        set
        {
            _shieldLevel = Mathf.Min(value, maxShield); //Min returns the smallest of the values, therby making max i before e ishelds 4

            //if the shledi is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject);
                Debug.Log(gm.name);
                gm.LostLife();
                
            }

        }
    }

    /*** MEHTODS ***/

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        CheckSHIPIsInScene(); //check for Hero SHIP
    }//end Awake()

    //Start is called once before the update
    private void Start()
    {
        gm = GameManager.GM; //find the game manager
        pool = ObjectPool.POOL; //find teh object pool
        audioSource = GetComponent<AudioSource>(); //find the audio source component
    }//end Start()

    // Update is called once per frame (page 551)
    void Update()
    {

        //player input
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        //change the transform based on input
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;

        transform.position = pos;

        //rotate the ship for the sake of dynamic feel
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        //Allow ship to fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Firing gamer beam");

            FireProjectile();
        }

    }//end Update()


    //Taking Damage
    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root; //gets the topmost transform in the hierarchy attached to other (i.e. the parent)
        GameObject go = rootT.gameObject; //object with that topmost transform component
        
        //check if this colliding object is the same as the last one
        if(go == lastTriggerGo) { return; }

        lastTriggerGo = go; //set the last triggering object equal to this object
        
        if(go.tag == "Enemy")
        {
            Debug.Log("Just hit some idiot named " + other.gameObject.name);
            shieldLevel--;
            Destroy(go); //
        } 
        else
        {
            Debug.Log("Collided with a poor pedestrian named " + other.gameObject.name);
        }
    }//end OnTriggerEnter()

    

    public void AddScore(int value)
    {
        gm.UpdateScore(value);
    }



    private void FireProjectile()
    {
        GameObject projGO = pool.GetObject();
        if(projGO != null)
        {
            projGO.transform.position = this.transform.position;
            Rigidbody rb = projGO.GetComponent<Rigidbody>();
            rb.velocity = Vector3.up * projectileSpeed;
            if(audioSource != null && projectileSound != null)
            {
                audioSource.PlayOneShot(projectileSound); // plays the projectile sound
            }
        }
        
    }

}
