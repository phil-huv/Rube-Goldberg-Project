using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Button")
        {
            Messenger.Instance.Broadcast("Light Up");
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }
}
