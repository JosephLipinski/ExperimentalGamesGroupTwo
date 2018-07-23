using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    [Header("An array of collected items")]
    public List<GameObject> collectedObjects;
    GameObject spawn;
    GameObject TrenchCoat;
    public Camera _camera;
    public GameObject alarmClock;
    GameObject PlayerGroup;
    SwitchPlayer _sP;
    NavMeshAgent agent;
    GameObject collidedWith;


    public enum State {
        Idle,
        Move
    }

    //[HideInInspector]
    public State _state;

	// Use this for initialization
	public void Start () {
        StartCoroutine(FSM());
        agent = GetComponent<NavMeshAgent>();
        TrenchCoat = GameObject.Find("Player Group/Trench Coat");
        PlayerGroup = GameObject.Find("Player Group");
        _sP = PlayerGroup.GetComponent<SwitchPlayer>();

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
        if (Input.GetKeyDown(KeyCode.T) && _sP.children.Contains(this.gameObject.GetComponent<PlayerController>()))
        {
            agent.SetDestination(TrenchCoat.transform.position);
            transform.SetParent(TrenchCoat.transform);
        }
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

        if(Input.GetKeyDown(KeyCode.F)){
            if (spawn != null)
                Destroy(spawn);
            spawn = Instantiate(alarmClock);
            spawn.transform.position = transform.position;

        }

        if (Input.GetKeyDown(KeyCode.T)){
            agent.SetDestination(TrenchCoat.transform.position);
            transform.SetParent(TrenchCoat.transform);
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


    }

    private void OnTriggerExit(Collider other){
        collidedWith = null;
    }

    public void SetCamera(Camera passedCamera){
        //Debug.Log("HERE");
        _camera = passedCamera;
    }
}
