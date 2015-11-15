using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameManager : StateMachine {

    private static GameManager instance;

    [SerializeField]
    private GameObject ball1Ref, ball2Ref, blockerRef, blocker2Ref, basket1Ref, basket2Ref, lightRef, top1Ref, top2Ref, bottom1Ref, musicRef, pacmanRef, cameraRef;

    [SerializeField]
    private GameObject[] cameraPositions;
    
    public int cameraPosIndex = 0;


    public static GameManager Instance
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


    

    

    
    private startState start = new startState();
    private stateTwo blockerMove = new stateTwo();
    private stateThree rollBall = new stateThree();
    private stateFour basketMove = new stateFour();
    private stateFive blocker2Move = new stateFive();
    private stateSix rollBall2 = new stateSix();
    private stateSeven lightUpTheNight = new stateSeven();


    void Awake()
    {

        Instance = this;
        Messenger.Instance.AddListener("GO!", Transition, blockerRef);
        Messenger.Instance.AddListener("Roll Ball", Transition, ball1Ref);
        Messenger.Instance.AddListener("Roll Ball", addRigidBodyToBall, ball1Ref);
        Messenger.Instance.AddListener("Basket Move", Transition, basket1Ref);
        Messenger.Instance.AddListener("Blocker2 Move", Transition, blocker2Ref);
        Messenger.Instance.AddListener("Blocker2 Move", TeleportBall, ball1Ref);
        Messenger.Instance.AddListener("Roll Ball Again", Transition, ball2Ref);
        Messenger.Instance.AddListener("Light Up", Transition, lightRef);

        

        AddTransition(start, blockerMove);
        AddTransition(blockerMove, rollBall);
        AddTransition(rollBall, basketMove);
        AddTransition(basketMove, blocker2Move);
        AddTransition(blocker2Move, rollBall2);
        AddTransition(rollBall2, lightUpTheNight);


        startMachine(start);

        
        
        
        
    }




    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        currentState.OnUpdate();
    }

    void LateUpdate()
    {
        //currentState = currentState.checkTransition();
    }




    public class startState : State
    {
        public startState() : base()
        {
            
        }

        
        // If the user presses "SPACE BAR" start the machine!
        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Messenger.Instance.Broadcast("GO!");
                Debug.Log("GO!");
                GameManager.Instance.pacmanRef.GetComponent<AudioSource>().enabled = true;
            }
        }

        public override State checkTransition()
        {
            return this;
        }
   

    
    }

    public class stateTwo : State
    {
        public stateTwo() : base()
        {

        }
        // Move the blocker up
        public override void OnUpdate()
        {
            GameManager.Instance.blockerRef.transform.position += new Vector3(0.0f, .75f, 0.0f) * Time.deltaTime;
        }


        public override State checkTransition()
        {
            return this;
        }

        

        
    }

    public class stateThree : State
    {
        bool playing = false;
        public stateThree() : base()
        {

        }

        
        // TURN ON THE MUSIC
        public override void OnUpdate()
        {
            if (!playing)
            {
                GameManager.Instance.musicRef.GetComponent<AudioSource>().enabled = true;
                playing = true;
            }
        }

        public override State checkTransition()
        {
            return this;
        }


    }

    public class stateFour : State
    {
        private bool closed = false;
        public stateFour() : base()
        { 
        
        }

        // Close the baskets and then move them.
        public override void OnUpdate()
        {
            if (!closed)
            {
                GameManager.Instance.top1Ref.transform.position += new Vector3(2, 0, 0) * Time.deltaTime;
                GameManager.Instance.top2Ref.transform.position += new Vector3(2, 0, 0) * Time.deltaTime;
                if (GameManager.Instance.top1Ref.transform.position.x > 0)
                    closed = true;
            }
            else
            {
                GameManager.Instance.basket1Ref.transform.position += new Vector3(0, -1, 0) * Time.deltaTime;
                GameManager.Instance.basket2Ref.transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
            }
        }

        public override State checkTransition()
        {
            return this;
        }
    }

    public class stateFive : State
    {
        private Vector3 initPos;
        private bool moving = false;
        public stateFive() : base()
        {

        }

        // Open the boxes and then roll the ball when they are open.
        public override void OnUpdate()
        {
            if(!moving)
            {
                initPos = GameManager.Instance.blocker2Ref.transform.position;
                moving = true;
            }
            
            GameManager.Instance.blocker2Ref.transform.position += new Vector3(0.0f, -.5f, 0.0f) * Time.deltaTime;
            GameManager.Instance.top1Ref.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
            if ((GameManager.Instance.blocker2Ref.transform.position - initPos).magnitude > 1)
                Messenger.Instance.Broadcast("Roll Ball Again");

        }

        public override State checkTransition()
        {
            return this;
        }
    }

    public class stateSix : State
    {
        private bool moving = false;
        public stateSix() : base()
        {

        }

        // Add a force to the rigid body once, and let physics do the rest!
        public override void OnUpdate()
        {
            if(!moving)
            {
                GameManager.Instance.ball1Ref.GetComponent<Rigidbody>().AddForce(new Vector3(50, 0, 0));
                moving = true;
            }

        }

        public override State checkTransition()
        {
            return this;
        }
    }

    public class stateSeven: State
    {
        public stateSeven() : base()
        {

        }

        //Switch the light bulbs color between yellow and white using Color.Lerp().
        public override void OnUpdate()
        {

            GameManager.Instance.lightRef.GetComponent<Renderer>().material.color = Color.Lerp(Color.yellow, Color.white, Mathf.Round(Time.time % 1));
            
            

            

        }

        public override State checkTransition()
        {
            return this;
        }
    }
    // Adds a rigidbody to the ball
    void addRigidBodyToBall()
    {
        ball1Ref.AddComponent<Rigidbody>();
    }
    // Moves the ball from basket1 to basket2. MAGIC
    void TeleportBall()
    {
        ball1Ref.transform.position = basket2Ref.transform.position + new Vector3(0f, .25f, 0f);
    }
    // Update the camera position/rotation.
    public void UpdateCameraPos()
    {
        cameraPosIndex += 1;
        cameraRef.transform.position = cameraPositions[cameraPosIndex].transform.position;
        cameraRef.transform.rotation = cameraPositions[cameraPosIndex].transform.rotation;
    }
}


