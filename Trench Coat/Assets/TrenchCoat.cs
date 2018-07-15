using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrenchCoat : MonoBehaviour {


    public List<GameObject> numberOfChilren;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            if (numberOfChilren.Contains(other.gameObject) == false)
                numberOfChilren.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Player")
            if (numberOfChilren.Contains(other.gameObject))
                numberOfChilren.Remove(other.gameObject);
    }

    public void ExitLevel(){
        if (numberOfChilren.Count == 3)
            Debug.Log("Level Over");
    }
}
