using UnityEngine;
using System.Collections;

public class BlockerScript : MonoBehaviour 
{
    
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
        Messenger.Instance.Broadcast("Roll Ball");
        Debug.Log("Roll Ball");
    }
}
