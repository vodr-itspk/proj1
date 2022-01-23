using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float maxDelay = 2.0f;
    [SerializeField] private float minDelay = 1.5f;
    private GameManager gameManager;
    private List<float> posList;

    void Awake()
    {
        StartCoroutine(WaitAndSpawn());
        posList = new List<float> { -1.0f, 0.0f, 1.0f };
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        gameManager.gameover += GameOver;
    }

    private void OnDisable()
    {
        gameManager.gameover -= GameOver;
    }

    private void GameOver()
    {
        gameObject.SetActive(false);
    }

    private void SpawnNewEnemy()
    {
        var numberOfEnemy = Random.Range(1, 4);
        var enemyTransformPosition = enemyPrefab.transform.position;
        var spawnPos1 = new Vector3(
            posList[0], 
            enemyTransformPosition.y,
            enemyTransformPosition.z
        );
        var spawnPos2 = new Vector3(
            posList[1], 
            enemyTransformPosition.y,
            enemyTransformPosition.z
        );
        var spawnPos3 = new Vector3(
            posList[2], 
            enemyTransformPosition.y,
            enemyTransformPosition.z
        );
        
        var quaternion = enemyPrefab.transform.rotation;
        switch (numberOfEnemy)
        {
            case 1:
                Instantiate(enemyPrefab, GetRandomVector3X(), quaternion, transform);
                break;
            case 2:
                var position = Random.Range(0, 3);
                if (position == 2)
                {
                    Instantiate(enemyPrefab, spawnPos1, quaternion, transform);
                    Instantiate(enemyPrefab, spawnPos3, quaternion, transform);
                }
                else
                {        
                    var spawnPosTwo1 = new Vector3(
                        posList[position], 
                        enemyTransformPosition.y,
                        enemyTransformPosition.z
                    );
                    var spawnPosTwo2 = new Vector3(
                        posList[position+1], 
                        enemyTransformPosition.y,
                        enemyTransformPosition.z
                    );
                    
                    Instantiate(enemyPrefab, spawnPosTwo1, quaternion, transform);
                    Instantiate(enemyPrefab, spawnPosTwo2, quaternion, transform);
                }
                break;
            case 3:
                Instantiate(enemyPrefab, spawnPos1, quaternion, transform);
                Instantiate(enemyPrefab, spawnPos2, quaternion, transform);
                Instantiate(enemyPrefab, spawnPos3, quaternion, transform);
                break;
            default:
                Instantiate(enemyPrefab, GetRandomVector3X(), quaternion, transform);
                break;
        }
    }

    private Vector3 GetRandomVector3X()
    {
        var enemyTransformPosition = enemyPrefab.transform.position;
        return new Vector3(
            posList[Random.Range(0, 3)],
            enemyTransformPosition.y,
            enemyTransformPosition.z
        );
    }


    private IEnumerator WaitAndSpawn()
    {
        while (true)
        {
            var waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);
            SpawnNewEnemy();
        }
    }
}