/**** 
 * Created by: Krieger
 * Date Created: April 11, 2022
 * 
 * Last Edited by: Krieger
 * Last Edited: April 11, 2022
 * 
 * Description: Make a material scroll by offset
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialScroller : MonoBehaviour
{
    private Material goMat; // the game object's material
    private Renderer goRend; // the mesh renderer

    public Vector2 scrollSpeed = new Vector2(0, 0); // x and y scroll speed per second

    private Vector2 offset; // offset of the scroll over time

    // Start is called before the first frame update
    void Start()
    {
        goRend = GetComponent<Renderer>();
        goMat = goRend.material;
    }

    // Update is called once per frame
    void Update()
    {
        offset = scrollSpeed * Time.deltaTime;
        goMat.mainTextureOffset += offset;
    }
}
