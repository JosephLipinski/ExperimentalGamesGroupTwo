using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
    public Text scoreText;

	// Use this for initialization
	void Start () {
        GameObject ItemManager = GameObject.Find("ItemManager");
        if(ItemManager != null){
            int size = ItemManager.GetComponent<ItemManager>().collectedItems.Count;
            scoreText.text = (10 * size).ToString();
        }
        else{
            scoreText.text = "0";
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
