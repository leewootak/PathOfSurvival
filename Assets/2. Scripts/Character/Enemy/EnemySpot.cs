using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class EnemySpot : MonoBehaviour
{
    NavMeshSurface Surface;
    public int EnemyCount;
    public List<EnemyObject> EnemyObjects = new List<EnemyObject>();
    void Start()
    {
        Surface = GetComponent<NavMeshSurface>();
        

        for (int i = 0; i < transform.parent.childCount-1; i++) 
        {
            EnemyObjects.Add(transform.parent.transform.GetChild(i+1).GetComponent<EnemyObject>());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Surface.size.Scale(transform.parent.localScale * 2);
            Surface.BuildNavMesh();
            for (int i = 0; i < transform.parent.childCount - 1; i++)
            {
                EnemyObjects[i].transform.localScale = new Vector3(1f / transform.parent.transform.localScale.x,
                    1f / transform.parent.localScale.y, 1f / transform.parent.transform.localScale.z);
                EnemyObjects[i].transform.position = new Vector3(Random.Range(transform.parent.position.x - transform.parent.localScale.x/2,
                    transform.parent.position.x + transform.parent.localScale.x /2), transform.position.y, Random.Range(transform.parent.position.z - transform.parent.localScale.z / 2,
                    transform.parent.position.z + transform.parent.localScale.z / 2));
                EnemyObjects[i].gameObject.SetActive(true);
            }
        }
    }
   


}
