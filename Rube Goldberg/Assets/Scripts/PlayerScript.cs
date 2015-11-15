using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
//        Messenger.Instance.AddListener("Hit the ground", HitTheGround, this.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Ground")
        {
            Messenger.Instance.Broadcast("Hit the ground");
        }
    }

//    public void HitTheGround();
    

    
}
