using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyList;
    public GameObject[] spawnPointList;

    float spawnEverySeconds = 3.0f;
    float spawnTimer = 0f;

    [SerializeField] int minJumpForce = 3000;
    [SerializeField] int maxJumpForce = 5500;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnEverySeconds)
        {
            SpawnRandomEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnRandomEnemy()
    {
        int enemyIndex = Random.Range(0, enemyList.Length);
        int spawnPointIndex = Random.Range(0, spawnPointList.Length);

        GameObject enemyPref = Instantiate(enemyList[enemyIndex]);
        enemyPref.transform.position = spawnPointList[spawnPointIndex].transform.position;

        Rigidbody2D enemyRB = enemyPref.GetComponent<Rigidbody2D>();
        float randomJumpForce = Random.Range(minJumpForce, maxJumpForce);
        enemyRB.AddForce(new Vector2(0, randomJumpForce));
    }
}
