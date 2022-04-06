/**** 
 * Created by: Akram Taghavi-Burris
 * Date Created: March 16, 2022
 * 
 * Last Edited by: Krieger
 * Last Edited: April 6, 2022
 * 
 * Description: Pool of objects for reuse
****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //**Variables**//
    #region POOL Singleton
        static public ObjectPool POOL;

    void CheckPOOLIsInScene()
    {
        if(POOL == null)
        {
            POOL = this;
        }
        else
        {
            Debug.LogError("POOL.Awake() - Attempted to assign a second ObjectPool.POOL, remove duplicate ObjectPool");
        }
    }
    #endregion

    private Queue<GameObject> projectiles = new Queue<GameObject>(); //queue of projectile gameobjects

    [Header("Pool Settings")]
    public GameObject projectilePrefab;
    public int poolStartSize = 5;

    // Start is called before the first frame update
    void Awake()
    {
        CheckPOOLIsInScene();
    }

    private void Start()
    {
        for(int index = 0; index < poolStartSize; index++)
        {
            GameObject gObj = Instantiate(projectilePrefab);
            projectiles.Enqueue(gObj); //add the new projectile to the queue
            gObj.SetActive(false); //disable the projectile
        }
    }

    // Update is called once per frame
    public GameObject GetObject()
    {
        if(projectiles.Count > 0)
        {
            GameObject gObj = projectiles.Dequeue();
            gObj.SetActive(true);
            return gObj;
        }
        else
        {
            Debug.LogWarning("out of bullets");
            return null;
        }
    }

    public void ReturnObjects(GameObject gObj)
    {
        projectiles.Enqueue(gObj);
        gObj.SetActive(false);
    }
}
