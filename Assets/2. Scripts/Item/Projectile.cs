using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    bullet,
    arrow,
    enemy,
}


public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType type;
    public float damage;
    public float distance;
    public float speed;
    private float time;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    void Start()
    {
        time = distance / speed;
        Invoke("DisActive", time);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (rb != null)
        {
            if(type == ProjectileType.enemy)
            {
                if(collision.gameObject.CompareTag("Player"))
                {
                    CharacterManager.Instance.Player.condition.
                        TakePhysicalDamage((int)transform.parent.GetComponent<EnemyObject>().GetEnemyInfo().Attack);
                }
            }
            rb.constraints = RigidbodyConstraints.None;
            EnemyObject enemy = collision.gameObject.GetComponent<EnemyObject>();
            if (enemy != null)
            {
                enemy.SubHealth(damage);
            }
            DisActive();
        }
    }

    public void GetSpeed(float sp)
    {
        speed = sp;
    }

    private void DisActive()
    {
        switch (type)
        {
            case ProjectileType.bullet:
                Destroy(gameObject);
                break;
            case ProjectileType.arrow:
                damage = 0;
                break;
            case ProjectileType.enemy:
                Destroy(gameObject);
                break;
        }
    }
}
