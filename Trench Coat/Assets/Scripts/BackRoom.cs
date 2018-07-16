using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackRoom : MonoBehaviour {

    //A list of idle players gameobject in the back room
    public List<GameObject> numberOfChildren;

    //If all three kids are caught by the manager the level is over
    private void Update(){
        if(numberOfChildren.Count == 3)
            SceneManager.LoadScene("Game Over", LoadSceneMode.Single);
    }

    //Adds player gameobjects to the List if they are in their idle state
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            if (numberOfChildren.Contains(other.gameObject) == false)
                if(other.gameObject.GetComponent<PlayerController>()._state == PlayerController.State.Idle)
                    numberOfChildren.Add(other.gameObject);
    }

    //Removes children as they leave the back room
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            if (numberOfChildren.Contains(other.gameObject))
                numberOfChildren.Remove(other.gameObject);
    }
}
