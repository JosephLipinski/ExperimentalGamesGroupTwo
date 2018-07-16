using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //The accessible states of the Enemy AI
    public enum State
    {
        Idle,
        Move,
        Pursue,
        Detain
    }

    [Header("Required Variables")]
    //The back office where kids are led to
    public GameObject backRoom;
    //The patrol path an adult is supposed to take
    public GameObject patrolPath;

    [Header("Current State")]
    //The state of the AI
    public State _state;

    //The patrol point to which the AI should walk
    GameObject patrolPoint;
    //The position of the patrol point
    Vector3 patrolPointVector;
    //The box collider attached to the AI
    BoxCollider boxCollider;
    //The Navmesh agent attached to the gameObject
    NavMeshAgent agent;
    //The gameobject that the enemy is chasing
    GameObject chasing;
    //Cooldown between chasing a player
    float chaseTime = 4.0f;

    //Initializes all private variables
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolPoint = patrolPath.transform.GetChild(0).gameObject;
        patrolPointVector = patrolPoint.gameObject.transform.position;
        agent.SetDestination(patrolPointVector);
        _state = State.Idle;
        boxCollider = GetComponent<BoxCollider>();
        StartCoroutine(FSM());
    }

    //Replacement to an update loop
    //Controlls the finite state machine of the AI
    IEnumerator FSM()
    {
        while (true)
        {
            switch (_state)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Move:
                    Move();
                    break;
                case State.Pursue:
                    yield return new WaitForSeconds(0.1f);
                    Pursue();
                    break;
                case State.Detain:
                    Detain();
                    break;
            }
            yield return null;
        }
    }

    //Idle and Move are both empty behavior methods
    void Idle() { }

    void Move() { 
      
    }

    //Set the AI to move towards to the chasing gameobject
    void Pursue(){ 
        agent.SetDestination(chasing.transform.position);
    }

    //Causes the AI to go to the back room and "escort" the captured player there
    void Detain(){
        StopCoroutine(ChaseCoolDown());
        agent.SetDestination(backRoom.transform.position);
    }

    //All necessary trigger handling
    private void OnTriggerEnter(Collider other)
    {
        if(_state != State.Detain){
            if (other.gameObject.tag == "PatrolPoint")
            {
                if (other.gameObject == patrolPoint)
                {
                    patrolPoint = patrolPoint.GetComponent<PatrolPoint>().nextPatrolPoint;
                    patrolPointVector = patrolPoint.transform.position;
                }
                agent.SetDestination(patrolPointVector);
            }
            if (other.gameObject.tag == "Player")
            {
                _state = State.Pursue;
                chasing = other.gameObject;
                agent.SetDestination(chasing.transform.position);
            }
        }
        if(other.gameObject.layer == 12){
            StartCoroutine(TurnOffCollider());
            _state = State.Idle;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(ChaseCoolDown());
        StartCoroutine(ChaseCoolDown());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && _state != State.Detain){
            StopCoroutine(ChaseCoolDown());
            _state = State.Detain;
            PlayerController _pc = collision.gameObject.GetComponent<PlayerController>();
            _pc.SetState(PlayerController.State.Idle);
            _pc.Detained();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    //Currently unused
    //Meant to be a cooldown while the player is getting chased
    IEnumerator ChaseCoolDown(){
        yield return new WaitForSeconds(chaseTime);
        agent.SetDestination(patrolPointVector);
        //_state = State.Idle;
    }

    //Turns the collider off of the gameobject so that the AI will leave the back room
    IEnumerator TurnOffCollider(){
        boxCollider.enabled = !boxCollider.enabled;
        yield return new WaitForSeconds(1.5f);
        boxCollider.enabled = !boxCollider.enabled;
    }
}