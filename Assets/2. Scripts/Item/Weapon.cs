using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponType
{
    Melee,
    Range
}

public class Weapon : MonoBehaviour
{
    public float attackRate;
    private bool attacked;
    
    public float useStamina;
    public WeaponType type;


    [Header("Meleee")]
    public float damage;
    public float attackDistance;

    [Header("Range")]
    public float shootSpeed;
    public GameObject projectiles;

   // private Animator animator;

    void Start()
    {
        //animator = GetComponent<Animator>();    
        InvokeRepeating("OnAttackInput", 0, attackRate);
    }

    public void OnAttackInput()
    {
        if(!attacked)
        {
 //           if (CharacterManager.Instance.Player.condition.UseStamina(useStamina))
            {
                attacked = true;
               //animator.SetTrigger("Attack");
                if(type == WeaponType.Range)
                {
                    Shoot();
                }
                Invoke("OnCanAttack", attackRate);
            }
        }
    }

    void Shoot()
    {
        Vector3 spawnPosition = transform.position + transform.forward * 1.3f;
        Quaternion spawnRotation = transform.rotation;

        GameObject procjec = Instantiate(projectiles, spawnPosition, spawnRotation);
        Rigidbody rb = procjec.GetComponent<Rigidbody>();
        if(rb!= null)
        {
            rb.AddForce(transform.forward * shootSpeed, ForceMode.Impulse);
        }
        procjec.GetComponent<Projectile>().GetSpeed(shootSpeed);
    }

    void OnCanAttack()
    {
        attacked = false;
    }

    public void OnHit()
    {

    }
}
