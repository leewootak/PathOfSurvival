using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && transform.parent.GetComponent<MeleeEnemyAI>().IsAttack)
        {
            StartCoroutine(TakeDamage());
        }
    }

    private IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(1);
        CharacterManager.Instance.Player.condition.TakePhysicalDamage((int)transform.parent.
            GetComponent<MeleeEnemyAI>().enemyObject.GetEnemyInfo().Attack);
    }

}
