using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    public float FOV = 1;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public float AttackRadius = 0f;
    public float AttackRate = 1f;
    public int AttackDamage = 10;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    public bool MoveTo(Vector3 targetPosition)
    {
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        _navMeshAgent.CalculatePath(targetPosition, path);
        if (path.status == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return false;
        }
        _navMeshAgent.destination = targetPosition;
        return true;
    }

    public void Attack(IDamagable damagable)
    {
        damagable.TakeDamage(AttackDamage);
        Debug.Log("Attack");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, FOV);
    }
}
