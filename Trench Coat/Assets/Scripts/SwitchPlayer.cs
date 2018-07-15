using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour {
    
    GameObject PlayerGroup;
    GameObject R, B, G;
    public List<PlayerController> children;
    PlayerController rScript, gScript, bScript;
    public int character = 0;
    public Camera _mainCamera;

    private void Start()
    {
        PlayerGroup = this.gameObject;

        R = PlayerGroup.transform.GetChild(0).gameObject;
        rScript = R.GetComponent<PlayerController>();

        G = PlayerGroup.transform.GetChild(1).gameObject;
        gScript = G.GetComponent<PlayerController>();

        B = PlayerGroup.transform.GetChild(2).gameObject;GetComponent<PlayerController>();
        bScript = B.GetComponent<PlayerController>();

        children.Add(rScript);
        children.Add(bScript);
        children.Add(gScript);
        SetActive();
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Q)){
            if(character > 0){
                character--;
                SetActive();
            } else {
                character = children.Count - 1;
                SetActive();
            }

        }
        else if(Input.GetKeyDown(KeyCode.E)){
            if (character < children.Count - 1){
                character++;
                SetActive();
            } else {
                character = 0;
                SetActive();
            }
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
