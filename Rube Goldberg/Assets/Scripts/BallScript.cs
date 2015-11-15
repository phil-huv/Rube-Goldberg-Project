using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

    private bool beforeBasketCol = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    { 
        if(col.gameObject.tag == "Basket" && beforeBasketCol)
        {
            Debug.Log("Basket Move");
            Messenger.Instance.Broadcast("Basket Move");
            beforeBasketCol = false;
        }

        
    
        if(col.gameObject.tag == "Domino")
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BallTrigger")
            GameManager.Instance.UpdateCameraPos();
    }
}
