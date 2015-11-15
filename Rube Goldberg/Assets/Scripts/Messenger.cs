using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


// Cannot inhert from Monobehaviour because new is being called in the Instance property.
public class Messenger
{
    void OnUpdate()
    {

    }
    private static Messenger instance;

    // Singleton implementation
    public static Messenger Instance
    {
        get
        {
            if (instance == null)
                instance = new Messenger();
            return instance;
        }

    }

    //Initializes the function dictionary and object dictionary.
    public Messenger()
    {

        functionDic = new Dictionary<string,Action>();
        

        objectDic = new Dictionary<string, List<GameObject>>();
    }
    //Maps a message to an Action
    private Dictionary<string, Action> functionDic;
    // Maps a message to a List of Game Objects listening to that message.
    private Dictionary<string, List<GameObject>> objectDic;

    // Adds the passed function to the Action Delegate and adds the gameobject to the list
    // of objects listening to the specified message.
    public void AddListener(string message, Action function, GameObject listener)
    {
        if(functionDic.ContainsKey(message))
        {
            functionDic[message] += function;
            
        }
        else
        {
            functionDic[message] = function;
        }
        
        if(!objectDic.ContainsKey(message))
        {
            objectDic[message] = new List<GameObject>();
            objectDic[message].Add(listener);
        }
        else
        {
            objectDic[message].Add(listener);
        }
        
        

    }


    // Broadcast a message, meaning invoke all functions that are listening to that message.
    public void Broadcast(string message)
    {
        functionDic[message].Invoke();
    }



}
