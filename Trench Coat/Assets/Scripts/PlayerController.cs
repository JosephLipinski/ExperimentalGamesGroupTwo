using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    [Header("An array of collected items")]
    public List<GameObject> collectedObjects;

    [HideInInspector]
    public Camera _camera;

    NavMeshAgent agent;
    GameObject collidedWith;

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
        ReadInput();
        if(Input.GetMouseButtonDown(0)){
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)){
                agent.SetDestination(hit.point);
            }
        }
    }

    void ReadInput(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(collidedWith != null){
                collectedObjects.Add(collidedWith);
                collidedWith.GetComponent<Collectible>().Collect();
                collidedWith = null;
            }
        }
    }

    public void Detained(){
        collectedObjects.Clear();
        agent.SetDestination(GameObject.Find("Back Room").transform.position);
        SwitchPlayer _sp = GameObject.Find("Player Group").GetComponent<SwitchPlayer>();
        _sp.RemoveChild(this.gameObject.GetComponent<PlayerController>());
    }

    public void SetState(State _passedState){
        _state = _passedState;
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == 11){
            collidedWith = other.gameObject;    
        }
        else if(other.gameObject.layer == 9){
            SwitchPlayer _sp = GameObject.Find("Player Group").GetComponent<SwitchPlayer>();
            _sp.AddChild(this.gameObject.GetComponent<PlayerController>());
        }


    }

    private void OnTriggerExit(Collider other){
        collidedWith = null;
    }
}
