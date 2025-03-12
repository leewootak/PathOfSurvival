using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{

    public Queue<GameObject> Pool = new Queue<GameObject>();
    public GameObject[] EnemyList;
    public GameObject[] Grounds;
    public GameObject GroundsPrefabs;
    public int PoolCount;

    private static EnemyPool instance;
    public static EnemyPool Instance {  get { return instance; } }

    private void Awake()
    {
        if(instance == null)
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
        for (int i = 0; i < Grounds.Length; i++)
        {
            GameObject Ground = Instantiate(GroundsPrefabs, Grounds[i].transform);
            Ground.GetComponent<EnemySpot>().EnemyCount = PoolCount / Grounds.Length;
            for (int j = 0; j < PoolCount/Grounds.Length; j++)
            {
                GameObject Enemy = Instantiate(EnemyList[0], Grounds[i].transform);
                Enemy.SetActive(false);
                Pool.Enqueue(Enemy);
            }
        }
    }

    public void GetQueue(int HowMany)
    {
        for (int i = 0; i < HowMany; i++)
        {
            if (Pool.Count <= 0)
            {
                GameObject NewObject = Instantiate(EnemyList[0], transform.parent);
            }
            else
            {
                GameObject NewObject = Pool.Dequeue();
                //NewObject.transform.localScale = new Vector3(1f / Grounds[0].transform.localScale.x, 
                //    1f / Grounds[0].transform.localScale.y, 1f / Grounds[0].transform.localScale.z);
                NewObject.SetActive(true);

            }
        }
        

    }
   
}
