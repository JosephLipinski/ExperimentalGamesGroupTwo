using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    public Camera _camera;
    NavMeshAgent agent;

    public enum State {
        Idle,
        Move
    }

    State _state;

	// Use this for initialization
	void Start () {
        StartCoroutine(FSM());
        agent = GetComponent<NavMeshAgent>();
	}

    IEnumerator FSM(){
        while(true){
            switch(_state){
                case State.Idle:
                    Idle();
                    break;
                case State.Move:
                    Move();
                    break;
            }
            yield return null;
        }
    }

    void Idle(){
        
    }

    void Move(){
        if(Input.GetMouseButtonDown(0)){
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)){
                agent.SetDestination(hit.point);
            }
        }
    }

    public void SetState(State _passedState){
        _state = _passedState;
    }
}
