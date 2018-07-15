using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    public GameObject parent;

    public void Collect(){
        parent.SetActive(false);
    }
}
