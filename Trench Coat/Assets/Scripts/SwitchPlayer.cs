using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour {
    
    GameObject PlayerGroup;
    GameObject R, B, G;
    public PlayerController rScript, gScript, bScript;
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

        SetActive();
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Q)){
            if(character > 0){
                character--;
                SetActive();
            } else {
                character = 2;
                SetActive();
            }

        }
        else if(Input.GetKeyDown(KeyCode.E)){
            if (character < 2){
                character++;
                SetActive();
            } else {
                character = 0;
                SetActive();
            }
        }
	}

    void SetActive(){
        switch(character){
            case 0:
                rScript.SetState(PlayerController.State.Move);
                bScript.SetState(PlayerController.State.Idle);
                gScript.SetState(PlayerController.State.Idle);
                break;
            case 1:
                rScript.SetState(PlayerController.State.Idle);
                bScript.SetState(PlayerController.State.Move);
                gScript.SetState(PlayerController.State.Idle);
                break;
            case 2:
                rScript.SetState(PlayerController.State.Idle);
                bScript.SetState(PlayerController.State.Idle);
                gScript.SetState(PlayerController.State.Move);
                break;
        }

    }


}
