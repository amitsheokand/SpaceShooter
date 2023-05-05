using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    private IObjectPool<Enemy> enemyPool;
    
    private float _spawnRate = 1f;
    
    public enum PoolType
    {
        Stack,
        LinkedList
    }

    public PoolType poolType;

    // Collection checks will throw errors if we try to release an item that is already in the pool.
    public bool collectionChecks = true;
    public int maxPoolSize = 10;


    public IObjectPool<Enemy> Pool
    {
        get
        {
            if (enemyPool == null)
            {
                if (poolType == PoolType.Stack)
                    enemyPool = new ObjectPool<Enemy>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 16, maxPoolSize);
                else
                    enemyPool = new LinkedPool<Enemy>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
            }
            return enemyPool;
        }
    }

    // Called when an item is returned to the pool using Release
    void OnReturnedToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    Enemy CreatePooledItem()
    {
        var go = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        var ps = go.GetComponent<Enemy>();

        return ps;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // initate the pool
        enemyPool = Pool;
        
        InvokeRepeating("SpawnEnemy", 0f, _spawnRate);
    }
    
    private string SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-2.75f, 2.75f), 6f, 0f);
        
        Enemy enemy = enemyPool.Get();
        enemy.transform.position = spawnPosition;
        enemy.gameObject.SetActive(true);
        return "Spawned enemy";
    }
    
    public void CancelEnemyInvoke()
    {
        CancelInvoke("SpawnEnemy");
    }
    
    public void ReleaseEnemy(Enemy enemy)
    {
        enemyPool.Release(enemy);
    }
    
    
}

