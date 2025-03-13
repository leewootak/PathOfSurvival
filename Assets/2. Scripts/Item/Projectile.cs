using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    bullet,
    arrow
}


public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType type;
    public float damage;
    public float distance;
    public float speed;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = distance / speed;
        Invoke("DisActive", time);
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
        }
    }
}
