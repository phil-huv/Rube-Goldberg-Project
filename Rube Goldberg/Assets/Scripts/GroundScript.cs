using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour 
{


    public void IAmHit()
    {
        Debug.Log("I AM HIT");
    }

	// Use this for initialization
	void Start () 
    {
	    Messenger.Instance.AddListener("Hit the ground", IAmHit, this.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    


}
