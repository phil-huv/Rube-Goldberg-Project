using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FSM : StateMachine
{
    private static FSM instance;
    
    [SerializeField]
    private GameObject playerRef;

    public enum states
    {
        idle,
        walk,
        run
        
    }

    private List<State> stateList;


    public static FSM Instance
    {
        get
        {
            return instance;
        }

        set
        {
            if (instance != null)
                Destroy(value.gameObject);
            else
            {
                instance = value;
            }
            
        }
    }
    

    private Renderer rend;
    private Transform playerTrans;
    private Vector3 movement;
    private bool running;
    private float speed;

    private Dictionary<string, states> transition = new Dictionary<string, states>();

    delegate void stateUpdate();
    stateUpdate updateMethod;

    private idleState idle = new idleState();
    private runState run = new runState();
    private walkState walk = new walkState();

    void Awake()
    {
        
        Instance = this;
        
        AddTransition(idle, walk);
        AddTransition(idle, run);
        AddTransition(walk, idle);
        AddTransition(walk, run);
        AddTransition(run, walk);
        AddTransition(run, idle);


  /*      AddTransition(states.walk, states.idle);
        AddTransition(states.walk, states.run);
        AddTransition(states.run, states.idle);
        AddTransition(states.run, states.walk);
        AddTransition(states.idle, states.walk);
        AddTransition(states.idle, states.run); */
        rend = playerRef.GetComponent<Renderer>();
        playerTrans = playerRef.GetComponent<Transform>();

        startMachine(idle);
        running = false;
        speed = 1;
    }




	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 3;
            running = true;
        }
        else
        {
            speed = 1;
            running = false;
        }
        movement = new Vector3(moveHorizontal, 0, moveVertical);
        currentState.OnUpdate();
	} 

    void LateUpdate()
    {
        currentState = currentState.checkTransition();
    }


    void idleUpdate()
    {
        rend.material.color = Color.green;
    }

    void walkUpdate()
    {
        rend.material.color = Color.red;
        playerTrans.position += movement * Time.deltaTime * speed;
    }

    void runUpdate()
    {
        rend.material.color = Color.blue;
        playerTrans.position += movement * Time.deltaTime * speed;
    }

    public class idleState : State
    {
        public idleState() : base()
        {
            
        }
        public override State checkTransition()
        {
            if (FSM.Instance.movement.magnitude == 0)
            {
                return this;
            }
            else if(!FSM.Instance.running && transitions.Contains(Instance.walk))
            {
                return FSM.Instance.walk;
            }
            else if (FSM.Instance.running && transitions.Contains(Instance.run))
            {
                return FSM.Instance.run;
            }

            return this;
        }

        public override void OnUpdate()
        {
            Instance.rend.material.color = Color.green;
        }

    }

    public class walkState : State
    {
        public walkState() : base()
        {

        }

        public override State checkTransition()
        {
            if (Instance.movement.magnitude == 0 && transitions.Contains(Instance.idle))
            {   
                return Instance.idle;
            }
            else if(FSM.Instance.running && transitions.Contains(Instance.run))
            {
                return FSM.Instance.run;
            }
            return this;
        }

        public override void OnUpdate()
        {
            Instance.rend.material.color = Color.red;
            Instance.playerTrans.position += Instance.movement * Time.deltaTime * Instance.speed;
        }
    }

    public class runState : State
    {
        public runState() : base()
        {

        }


        public override State checkTransition()
        {
            if (FSM.Instance.movement.magnitude == 0 && transitions.Contains(Instance.idle))
            {
                return FSM.Instance.idle;
            }
            else if (!FSM.Instance.running && transitions.Contains(Instance.walk))
            {
                return Instance.walk;
            }
            else
            {
                return this;
            }
        }

        public override void OnUpdate()
        {
            Instance.rend.material.color = Color.blue;
            Instance.playerTrans.position += Instance.movement * Time.deltaTime * Instance.speed;
        }



    }





}


