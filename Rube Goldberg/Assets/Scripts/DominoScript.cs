using UnityEngine;
using System.Collections;

public class DominoScript : MonoBehaviour {

    private Renderer rend;

    void Awake()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ball" || other.gameObject.tag == "Domino")
            rend.material.color = Color.black;
    }
}
