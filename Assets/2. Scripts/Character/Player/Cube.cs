using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public int damage;
    public float damageRate;

    private List<IDamagable> things = new List<IDamagable>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other) // 트리거 영역에 들어갔을때
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            things.Add(damagable);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            things.Remove(damagable); // 트리거에서 나가면 리스트에서 damagable 를 지운다
        }
    }
}
