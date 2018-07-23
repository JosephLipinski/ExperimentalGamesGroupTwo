using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public List<GameObject> collectedItems;




	// Use this for initialization
	void Start () {
        ItemManager[] itemManagers = GameObject.FindObjectsOfType<ItemManager>();
        if (itemManagers.Length > 1)
        {
            Destroy(gameObject);

        }
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
