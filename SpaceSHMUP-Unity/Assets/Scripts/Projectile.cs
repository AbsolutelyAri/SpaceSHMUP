/**** 
 * Created by: Krieger
 * Date Created: Mar 30, 2022
 * 
 * Last Edited by: Krieger
 * Last Edited: Mar 30, 2022
 * 
 * Description: Projectile (it go pew)
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*** VARIABLES ***/
    private BoundsCheck bndsCheck; //reference to the boundscheck component on the prefab
    
    
    // Start is called before the first frame update
    void Awake()
    {
        bndsCheck = GetComponent<BoundsCheck>();
    }//end awake

    // Update is called once per frame
    void Update()
    {
        //destroy self once off screen
        if (bndsCheck.offUp || bndsCheck.offDown)
        {
            Destroy(gameObject);
        }
    }//end update
}
