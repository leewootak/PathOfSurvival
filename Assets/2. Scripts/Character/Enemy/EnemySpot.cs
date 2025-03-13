using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpot : MonoBehaviour
{
    NavMeshSurface Surface;
    public int EnemyCount;
    public List<GameObject> EnemyObjects = new List<GameObject>();

    private bool IsSummoned = false;
    void Start()
    {
        Surface = GetComponent<NavMeshSurface>();


        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    EnemyObjects.Add(transform.GetChild(i).GetComponent<EnemyObject>());
        //}

    }

    private void OnTriggerEnter(Collider other)
    {

    

        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < EnemyCount; i++)
            {
                EnemyObjects.Add(EnemyPool.Instance.GetQueue());
                EnemyObjects[i].transform.parent = transform;
            }
            //Surface.size.Scale(transform.localScale * 2);
            Surface.BuildNavMesh();
            for (int i = 0; i < EnemyObjects.Count; i++)
            {
                EnemyObjects[i].transform.localScale = new Vector3(1f / transform.localScale.x,
                    1f / transform.localScale.y, 1f / transform.localScale.z);
                EnemyObjects[i].transform.position = new Vector3(Random.Range(transform.position.x - transform.localScale.x / 2,
                    transform.position.x + transform.localScale.x / 2), transform.position.y, Random.Range(transform.position.z - transform.localScale.z / 2,
                    transform.position.z + transform.localScale.z / 2));
                EnemyObjects[i].gameObject.SetActive(true);

            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < EnemyCount; i++)
            {
                EnemyPool.Instance.ReQueue(EnemyObjects[i]);
                EnemyObjects[i].transform.parent = EnemyPool.Instance.transform;
                EnemyObjects[i].gameObject.SetActive(false);

            }
            EnemyObjects.Clear();
            Surface.navMeshData = null;
        }



    }
}
