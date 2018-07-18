using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    [Header("An array of collected items")]
    public List<GameObject> collectedObjects;

    [HideInInspector]
    public Camera _camera;

    GameObject PlayerGroup;
    NavMeshAgent agent;
    GameObject collidedWith;
    GameObject trenchCoat;

    public enum State {
        Idle,
        Move
    }

    //[HideInInspector]
    public State _state;

	// Use this for initialization
	void Start () {
        StartCoroutine(FSM());
        agent = GetComponent<NavMeshAgent>();
        //if (transform.GetChild(0) != null){
        //    trenchCoat = this.gameObject;
        //}	
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
        
        /*
        if(Input.GetKeyDown(KeyCode.Space)){
            
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (trenchCoat != null)
                trenchCoat.GetComponent<TrenchCoat>().ExitLevel();
        }
        */
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
            collectedObjects.Add(collidedWith);
            collidedWith.GetComponent<Collectible>().Collect();
            collidedWith = null;    
        }
        else if(other.gameObject.layer == 9){
            if(other.gameObject.GetComponent<PlayerController>()._state != PlayerController.State.Idle){
                SwitchPlayer _sp = GameObject.Find("Player Group").GetComponent<SwitchPlayer>();
                _sp.AddChild(this.gameObject.GetComponent<PlayerController>());    
            }

        }
        else if(other.gameObject.tag == "Trench Coat"){
            trenchCoat = other.gameObject;
        }


    }

    private void OnTriggerExit(Collider other){
        collidedWith = null;
    }
}
