using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour 
{
    private static Singleton instance;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static Singleton Instance()
    {
        if (instance == null)
            instance = new Singleton();
        return instance;
    }
    
}
