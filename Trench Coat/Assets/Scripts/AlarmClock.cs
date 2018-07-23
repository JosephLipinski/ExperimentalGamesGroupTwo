using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmClock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10){
            Debug.Log(other.gameObject.name);
            //Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 10)
        {
            collision.gameObject.GetComponent<EnemyAI>().SetState(EnemyAI.State.Move);
            Destroy(this.gameObject);
        }

    }
}
