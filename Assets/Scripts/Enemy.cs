using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;

    [SerializeField] private string title; 

    [SerializeField] private int attack;

    [SerializeField] private float speed;

    [SerializeField] private Transform target;

    [SerializeField] private float chaseRadius;

    [SerializeField] private float attackRadius;

    [SerializeField] private Transform defaultPosition;

    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void CheckTargetDistance() {
      // Calculate how far the enemy is from the target
      float distanceToTarget = Vector3.Distance(target.position, transform.position);
      
      // If the target is within the chase radius, chase the target
      // Chasing stops if the target is within the attack radius
      if (distanceToTarget <= chaseRadius && distanceToTarget > attackRadius) {
          transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
      }

      if (transform.position.x < target.position.x) {
        animator.SetFloat("moveX", 1f);
      }
      else {
        animator.SetFloat("moveX", -1f);
      }
    }
}
