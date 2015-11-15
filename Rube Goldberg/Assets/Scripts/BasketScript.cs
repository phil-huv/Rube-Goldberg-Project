using UnityEngine;
using System.Collections;

public class BasketScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Blocker2 Move");
        Messenger.Instance.Broadcast("Blocker2 Move");
    }
}
