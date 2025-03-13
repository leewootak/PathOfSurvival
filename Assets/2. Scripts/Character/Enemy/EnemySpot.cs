using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpot : MonoBehaviour
{
    NavMeshSurface Surface;
    public int EnemyCount;
    private List<GameObject> EnemyObjects = new List<GameObject>();

    private bool IsSummoned = false;
    void Start()
    {
        Surface = GetComponent<NavMeshSurface>();
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

              

                Vector3 Position = new Vector3(Random.Range(Surface.transform.position.x - Surface.size.x / 2,
                    Surface.transform.position.x + Surface.size.x / 2), Surface.transform.position.y, 
                    Random.Range(Surface.transform.position.z - Surface.size.z / 2,
                    Surface.transform.position.z + Surface.size.z / 2));
                Debug.Log($"{Position.x}, {Position.z}");
                NavMeshHit hit;
          
                NavMesh.SamplePosition(Position, out hit, 100f, NavMesh.AllAreas);
                Debug.Log("Debug"+hit.position);
                EnemyObjects[i].transform.position = hit.position;
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
