using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour {

    public List<GameObject> cameras;
    public GameObject mainCamera;

    SwitchPlayer _sP;

    int activeCam = 0;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < transform.childCount; i++){
            cameras.Add(transform.GetChild(i).gameObject);
            if(i != 0){
                cameras[i].SetActive(false);
            }
        }
        mainCamera = cameras[0];
        _sP = GameObject.Find("Player Group").GetComponent<SwitchPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space)){
            ChangeCamera();
        }
	}

    void ChangeCamera(){
        if(activeCam < cameras.Count - 1){
            activeCam++;
        }
        else{
            activeCam = 0;
        }
        int i = 0;
        foreach(GameObject cam in cameras){
            if(i == activeCam){
                cam.SetActive(true);
            }
            else{
                cam.SetActive(false);
            }
            i++;
        }
        mainCamera = cameras[activeCam];
        _sP.SetCamera(mainCamera.GetComponent<Camera>());
    }
}
