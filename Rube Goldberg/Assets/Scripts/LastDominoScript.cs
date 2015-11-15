using UnityEngine;
using System.Collections;

public class LastDominoScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Button")
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            Messenger.Instance.Broadcast("Light Up");
            GameManager.Instance.UpdateCameraPos();
        }
    }

}
