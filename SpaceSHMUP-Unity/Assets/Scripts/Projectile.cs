/**** 
 * Created by: Akram Taghavi-Burris
 * Date Created: March 16, 2022
 * 
 * Last Edited by: Krieger
 * Last Edited: April 6, 2022
 * 
 * Description: Determines Projectile behavior
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /**Variables**/
    private BoundsCheck bndCheck; //reference to the bounds check component

    // Start is called before the first frame update
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bndCheck.offUp)
        {
            gameObject.SetActive(false);
            bndCheck.offUp = false; //reset the boundary settings so that it doesn't believe the projectile is off the screen
        }
    }
}
