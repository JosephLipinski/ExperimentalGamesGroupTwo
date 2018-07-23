using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SentryAI : MonoBehaviour {

    public enum State{
        Idle,
        Detain
    }
    public State _state;
    public GameObject backRoom;

    Vector3 originalPosition;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        originalPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        _state = State.Idle;

	}

    IEnumerator FSM(){
        while(true){
            switch(_state){
                case State.Idle:
                    Idle();
                    break;
            }
            yield return null;
        }
    }

    void Idle(){}

    private void OnTriggerEnter(Collider other){
        if(_state != State.Detain){
            if (other.gameObject.tag == "Player"){
                agent.SetDestination(other.gameObject.transform.position);
            }
        }

    }
}
