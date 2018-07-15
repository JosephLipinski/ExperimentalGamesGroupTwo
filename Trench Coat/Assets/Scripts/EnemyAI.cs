using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject backRoom;
    public GameObject patrolPath;
    public GameObject patrolPoint;
    public Vector3 patrolPointVector;

    BoxCollider boxCollider;

    float chaseTime = 4.0f;


    public enum State
    {
        Idle,
        Move,
        Pursue,
        Detain
    }

    NavMeshAgent agent;
    public State _state;
    GameObject chasing;

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

    void Idle() { }

    void Move() { 
      
    }

    void Pursue(){ 
        agent.SetDestination(chasing.transform.position);
    }

    void Detain(){
        StopCoroutine(ChaseCoolDown());
        agent.SetDestination(backRoom.transform.position);
    }

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

    IEnumerator ChaseCoolDown(){
        yield return new WaitForSeconds(chaseTime);
        agent.SetDestination(patrolPointVector);
        //_state = State.Idle;
    }

    IEnumerator TurnOffCollider(){
        boxCollider.enabled = !boxCollider.enabled;
        yield return new WaitForSeconds(1.5f);
        boxCollider.enabled = !boxCollider.enabled;
    }
}