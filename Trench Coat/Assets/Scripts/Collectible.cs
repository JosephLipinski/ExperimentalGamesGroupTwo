using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    public GameObject parent;

    //Deactivates the parent gameObject
    public void Collect(){
        parent.SetActive(false);
    }
}
