using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrenchCoatController : MonoBehaviour {

    public List<GameObject> children;
    public Camera _camera;
    SwitchCamera _sC;
    NavMeshAgent agent;
    BoxCollider collider;

	// Use this for initialization
	void Start () {
        _sC = GameObject.Find("Camera Group").GetComponent<SwitchCamera>();
        agent = GetComponent<NavMeshAgent>();
        collider = GetComponent<BoxCollider>();
        collider.enabled = !collider.enabled;
	}
	
	// Update is called once per frame
	void Update () {

        if (children.Count == 3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            collider.enabled = !collider.enabled;
            foreach (GameObject child in children)
            {
                child.SetActive(true);
                child.GetComponent<PlayerController>().Start();
            }
            children.Clear();
        }


	}

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            children.Add(other.gameObject);
            other.gameObject.SetActive(false);
            _camera = _sC.mainCamera.GetComponent<Camera>();
        }
    }

    public void SetCamera(Camera passedCamera)
    {
        //Debug.Log("HERE");
        _camera = passedCamera;
    }

}
