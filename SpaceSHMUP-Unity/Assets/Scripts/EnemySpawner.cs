/**** 
 * Created by: Krieger
 * Date Created: March 28, 2022
 * 
 * Last Edited by: 
 * Last Edited: 
 * 
 * Description: spawns the enemies
 * 
 *              A small enemy spawner [Jerma noises here]
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /*** VARIABLES ***/
    [Header("Enemy Settings")]
    public GameObject[] prefabEnemies; //array containing all enemy prefabs
    public float enemiesSpawnedPerSecond; //enemy count to spawn every second
    public float enemyDefaultPadding; //padding position of each enemy, keeps them from spawning together

    private BoundsCheck bndCheck; //reference to the bounds check component

    // Start is called before the first frame update
    void Start()
    {
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", (1f/enemiesSpawnedPerSecond));
    }//end start

    // Update is called once per frame
    void SpawnEnemy()
    {
        //pick a random index of an enemy from the array and instantiate
        int index = Random.Range(0, prefabEnemies.Length);

        GameObject go = Instantiate<GameObject>(prefabEnemies[index]);

        //Position the enemy above the screen at random X position
        float enemyPadding = enemyDefaultPadding;

        if(go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        //Set initial position
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;

        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;

        go.transform.position = pos;

        //difficulty scaling
        enemiesSpawnedPerSecond += .01f;

        //invoke method again
        Invoke("SpawnEnemy", 1f / enemiesSpawnedPerSecond);
    }
}
