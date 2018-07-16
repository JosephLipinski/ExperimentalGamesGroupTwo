using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Much like BackRoom.cs
public class TrenchCoat : MonoBehaviour {

    //A list of the colliding player objects
    public List<GameObject> numberOfChildren;

    //Adds player objects to the list
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            if (numberOfChildren.Contains(other.gameObject) == false)
                numberOfChildren.Add(other.gameObject);
        }
    }

    //Removes exiting players from the lisst
    private void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Player")
            if (numberOfChildren.Contains(other.gameObject))
                numberOfChildren.Remove(other.gameObject);
    }

    //Exits the current level
    public void ExitLevel(){
        if (numberOfChildren.Count == 3)
            SceneManager.LoadScene("Victory", LoadSceneMode.Single);
    }
}
