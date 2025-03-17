using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{

    public Queue<GameObject> Pool = new Queue<GameObject>();
    public GameObject[] EnemyList;
    public int PoolCount;

    private static EnemyPool instance;
    public static EnemyPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SetQueue();
    }

    private void SetQueue()
    {

        for (int j = 0; j < PoolCount; j++)
        {
            GameObject Enemy = Instantiate(EnemyList[0], transform);
            Enemy.GetComponent<EnemyObject>().Set();
            Enemy.SetActive(false);
            Pool.Enqueue(Enemy);
        }
    }

    public void ReQueue(GameObject gameObject)
    {
        Pool.Enqueue(gameObject);
    }

    public GameObject GetQueue()
    {
        if (Pool.Count <= 0)
        {
            GameObject NewObject = Instantiate(EnemyList[Random.Range(0, EnemyList.Length)], transform);
            NewObject.SetActive(false);
            NewObject.GetComponent<EnemyObject>().Set();
            return NewObject;
        }
        else
        {
            GameObject NewObject = Pool.Dequeue();
            NewObject.GetComponent<EnemyObject>().Set();
            return NewObject;

        }


    }

}
