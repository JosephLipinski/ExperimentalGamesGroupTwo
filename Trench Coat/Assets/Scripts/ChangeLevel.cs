using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    int index;

	// Use this for initialization
	void Start () {
        index = SceneManager.GetActiveScene().buildIndex + 1;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(index - 1);
        }
        if(Input.GetKeyDown(KeyCode.N)){
            SceneManager.LoadScene(index);
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit");
        if(other.gameObject.name == "Trench Coat"){
            ItemManager _iM = GameObject.Find("ItemManager").GetComponent<ItemManager>();

            for (int i = 0; i < other.gameObject.transform.childCount; i++){
                GameObject child = other.gameObject.transform.GetChild(i).gameObject;
                PlayerController _pc = child.GetComponent<PlayerController>();
                if(_pc != null){
                    foreach(GameObject item in _pc.collectedObjects){
                        _iM.collectedItems.Add(item);
                        //Debug.Log("HERE");
                    }
                }

            }
            SceneManager.LoadScene(index);
        }
    }
}
