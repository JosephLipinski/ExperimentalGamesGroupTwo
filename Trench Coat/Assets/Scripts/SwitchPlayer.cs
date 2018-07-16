using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour {
    
    //The list of all of the available player scripts
    public List<PlayerController> children;
    //The main camera
    public Camera _mainCamera;

    //The scripts attached to each of the players
    PlayerController rScript, gScript, bScript;
    //A reference to which character should be active first as int
    int character = 0;

    private void Start()
    {
        children.Add(transform.GetChild(0).gameObject.GetComponent<PlayerController>());
        children.Add(transform.GetChild(1).gameObject.GetComponent<PlayerController>());
        children.Add(transform.GetChild(2).gameObject.GetComponent<PlayerController>());
        SetActive();
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Q)){
            if(character > 0){
                character--;
            } else {
                character = children.Count - 1;
            }
            SetActive();
        }
        else if(Input.GetKeyDown(KeyCode.E)){
            if (character < children.Count - 1){
                character++;
            } else {
                character = 0;
            }
            SetActive();
        }

	}

    void SetActive(){
        int i = 0;
        foreach (PlayerController child in children){
            if (i == character)
            {
                child.SetState(PlayerController.State.Move);
            }
            else{
                child.SetState(PlayerController.State.Idle);
            }
            i++;
        }
    }

    public void RemoveChild(PlayerController _pc){
        children.Remove(_pc);
    }

    public void AddChild(PlayerController _pc){
        if(children.Contains(_pc) == false){
            children.Add(_pc);
        }
    }
}
