/**** 
 * Created by: Krieger
 * Date Created: April 6, 2022
 * 
 * Last Edited by: Krieger
 * Last Edited: April 6, 2022
 * 
 * Description: Returns the object back to the pool
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    private ObjectPool pool; //reference to pool

    // Start is called before the first frame update
    void Start()
    {
        pool = ObjectPool.POOL;
    }

    // Update is called once per frame
    private void OnDisable() //calls when the object is disabled
    {
        if (pool != null)
        {
            pool.ReturnObjects(this.gameObject); //return this object to the pool
        }
    }
}
