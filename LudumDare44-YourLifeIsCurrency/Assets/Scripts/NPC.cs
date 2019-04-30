using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public enum Location { Outside, SmallHouse, BigHouse }
    public Location CurrentLocation = Location.Outside;

    public Vector3 Target;

    public Animator Animator;
    NavMeshAgent agent;

    public Transform PitTransform;

    public bool AtPit = true;
    public bool AtHome = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
    }

    public void StartWalkingTowardsPit(Vector3 _targetPoint, Transform _pitTransform = null)
    {
        agent.destination = Target = _targetPoint;
        PitTransform = _pitTransform;

        AtPit = false;
    }

    public void StartWalkingTowardsHome(Vector3 _targetPoint, Location _location = Location.Outside)
    {
        agent.destination = Target = _targetPoint;
        PitTransform = null;

        CurrentLocation = _location;

        AtPit = true;
        AtHome = false;
    }

    private void Update()
    {
        if (agent.velocity.x != 0 || agent.velocity.z != 0)
            Animator.SetBool("IsWalking", true);

        else
            Animator.SetBool("IsWalking", false);

        if (transform.position == agent.destination && !AtPit)
        {
            //Debug.Log("AtDestination");
            if (PitTransform != null)
                transform.LookAt(PitTransform);
            AtPit = true;
            AIManager.Instance.CheckIfAllAtPit();
        }

        if (transform.position == agent.destination && !AtHome)
        {
            AtHome = true;
            if (CurrentLocation == Location.Outside)
                AIManager.Instance.CheckIfAllAtHome();
            else
                AIManager.Instance.CheckIfAllInside();

        }
    }

    public void JumpInPit()
    {
        Animator.SetBool("IsBowing", false);
        Animator.SetBool("IsJumpingInPit", true);

        //Destroy(gameObject);
    }

    public void BowToPit()
    {
        Animator.SetBool("IsBowing", true);
    }

    public void StopBowing()
    {
        Animator.SetBool("IsBowing", false);
    }
}
