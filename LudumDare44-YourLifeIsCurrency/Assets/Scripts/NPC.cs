using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Vector3 Target;

    Animator animator;
    NavMeshAgent agent;

    public Transform PitTransform;

    public bool AtDestination = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    public void StartWalkingTowardsPoint(Vector3 _targetPoint, Transform _pitTransform = null)
    {
        agent.destination = Target = _targetPoint;
        PitTransform = _pitTransform;

        AtDestination = false;
    }

    private void Update()
    {
        if (agent.velocity.x != 0 || agent.velocity.z != 0)
            animator.SetBool("IsWalking", true);

        else
            animator.SetBool("IsWalking", false);

        if (transform.position == agent.destination && !AtDestination)
        {
            //Debug.Log("AtDestination");
            if (PitTransform != null)
                transform.LookAt(PitTransform);
            AtDestination = true;
            AIManager.Instance.CheckIfAllAtDestination();
        }
    }

    public void JumpInPit()
    {
        animator.SetBool("IsBowing", false);
        animator.SetBool("IsJumpingInPit", true);
        //Destroy(gameObject);
    }

    public void BowToPit()
    {
        animator.SetBool("IsBowing", true);
    }

    public void StopBowing()
    {
        animator.SetBool("IsBowing", false);
    }
}
