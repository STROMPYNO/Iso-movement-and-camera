using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class follow : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;
    Animator animator;  
 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);   

        if(distance > agent.stoppingDistance)
        {
            agent.SetDestination(player.transform.position);
            animator.SetBool(("move"), true);
        }
        else
        {
            animator.SetBool(("move"), false);
            FaceTarget();
        }
       
    }

    void FaceTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }
}
