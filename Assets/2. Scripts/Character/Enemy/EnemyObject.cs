using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    [SerializeField] private EnemyInfo EnemyInfo;
    [SerializeField] private List<ItemData> DropItem;
    private Animator animator;
    private float health;

    public void Set()
    {
        health = EnemyInfo.Health;
        DropItemSet(DropItem);
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }
    public EnemyInfo GetEnemyInfo()
    {
        return EnemyInfo;
    }

    public void SubHealth(float Damage)
    {
        animator.SetTrigger("Scream");
        health -= Damage;
    }

    private IEnumerator Die()
    {
        animator.SetTrigger("DIE");
        Drop();
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        EnemyPool.Instance.ReQueue(gameObject);
    }

    private void DropItemSet(List<ItemData> items)
    {
        int Persentage  = Random.Range(0, 100);

        if(Persentage <5)
        {
            items.Add(ItemManager.Instance.list[0]);
        }
        if(Persentage < 40)
        {
            items.Add(ItemManager.Instance.list[1]);
        }
        if (Persentage < 60)
        {
            items.Add(ItemManager.Instance.list[2]);
        }
        if (Persentage % 2 == 1)
        {
            items.Add(ItemManager.Instance.list[3]);
        }

    }

    private void Drop()
    {
        foreach(ItemData item in DropItem)
        {
           GameObject DropItem = Instantiate(item.dropPrefab, transform);
            Vector3 RandomDropPosition = new Vector3(transform.position.x+ Random.Range(-1f,1f), 
                transform.position.y, transform.position.z + Random.Range(-1f,1f));
            DropItem.transform.position = RandomDropPosition;
            DropItem.transform.rotation = Quaternion.identity;
        }
    }
    
}
