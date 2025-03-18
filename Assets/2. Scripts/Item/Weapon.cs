using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponType
{
    Melee,
    Range
}

public class Weapon : Equip
{
    public float attackRate;
    private bool attacked;
    public Camera camera;
    public float useStamina;
    [SerializeField] private WeaponType type;


    [Header("Meleee")]
    public float damage;
    public float attackDistance;

    [Header("Range")]
    public float shootSpeed;
    public GameObject projectiles;
    public ItemID useProjectile;
   // private Animator animator;

    void Start()
    {
        //animator = GetComponent<Animator>();    
        //InvokeRepeating("OnAttackInput", 0, attackRate);
        camera = Camera.main;
    }

    public override void OnAttackInput()
    {
        if(!attacked)
        {
 //           if (CharacterManager.Instance.Player.condition.UseStamina(useStamina))
            {
                attacked = true;
                //animator.SetTrigger("Attack");
                if (type == WeaponType.Range)
                {
                    ItemSlot proc = ItemManager.Instance.inventory.FindItem(useProjectile);
                    if (proc.quantity > 0)
                    {
                        Shoot();
                        ItemManager.Instance.inventory.RemoveItem(useProjectile);
                    }
                }
                else OnHit();
                    Invoke("OnCanAttack", attackRate);
            }
        }
    }

    void Shoot()
    {
        Vector3 playerPos = CharacterManager.Instance.Player.transform.position;
        Vector3 spawnPosition = playerPos + CharacterManager.Instance.Player.transform.forward * 1.3f;
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
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            if (hit.collider.TryGetComponent(out EnemyObject enemy))
            {
                Destroy(enemy.gameObject);
            }

        }
    }
    
}
