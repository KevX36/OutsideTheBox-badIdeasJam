using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SurviceHub hub;
    [SerializeField] private GameStateManager stateManager;
    //player parts
    private Rigidbody rb;
    private Collider col;
    [SerializeField] private Camera cam;

    //movement

    [SerializeField] private Vector2 MoveDirection;
    public int speed = 5;
    private int baseSpeed;
    [SerializeField] private Vector3 move;
    //jump
    [SerializeField] Vector3 PlayerFall = new Vector3 (0,0,0);
    public int airJumps = 1;
    public int maxAirJumps = 1;
    private int baseMaxJumps;
    public int JumpHighet = 7;
    private bool Jumping = false;
    [SerializeField]private bool isGrounded = true;
    // box
    public GameObject GrabSpot;

    [SerializeField] private Rigidbody box;
    public float GrabRange = 5;




    private bool doNothingOnStart = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        stateManager = hub.stateManager;
        airJumps = maxAirJumps;
        baseMaxJumps = maxAirJumps;
        baseSpeed = speed;
        col = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        MoveDirection = rb.position;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(stateManager.currentState == GameStateManager.GameState.GamePlay)
        {
            InputAction input = context.action;
            Debug.Log("started jump");
            if (input.IsPressed())
            {
                if (isGrounded)
                {
                    Jumping = true;
                }
                else if (airJumps > 0)
                {
                    Jumping = true;
                    airJumps--;
                }


            }
        }
        

        
    }
    public void OnPause()
    {
        stateManager.OnPause();
    }
    public void GrabAndThrow()
    {
        RaycastHit hit;
        if (box == null && Physics.Raycast(cam.transform.position, (cam.transform.forward * GrabRange) + cam.transform.position, out hit))
        {
            box = hit.rigidbody;
            if (box.gameObject.CompareTag("Box"))
            {

            }
            else
            {
                box = null;
            }

            
        }
        else if (box != null)
        {
            Debug.Log("toss the box");
            box.AddForce(cam.transform.forward);
            box = null;
            box = null;
        }



    }
    public void cutBox()
    {
        Debug.Log("tried to cut box");
        if (box != null)
        {

        }
        
    }
    public void OnMove(InputAction.CallbackContext Context)
    {
        if (stateManager.currentState == GameStateManager.GameState.GamePlay)
        {
            Debug.Log("moving");
            MoveDirection = Context.ReadValue<Vector2>();
        }
        
    }
    public bool cheakIfGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up,col.bounds.extents.y + 0.1f);

        
    }
    public void ResetEffects()
    {
        speed = baseSpeed;
        maxAirJumps = baseMaxJumps;
    }
    void Update()
    {
        Debug.DrawLine(cam.transform.position, (cam.transform.forward * GrabRange) + cam.transform.position, Color.red);
        if (stateManager.currentState == GameStateManager.GameState.GamePlay)
        {
            isGrounded = cheakIfGrounded();
            if (isGrounded)
            {
                airJumps = maxAirJumps;


            }
            if (PlayerFall.y > 0)
            {
                PlayerFall += Physics.gravity * Time.deltaTime;
            }
            if (Jumping)
            {
                PlayerFall.y = JumpHighet;


                Debug.Log("Jumped");
                Jumping = false;
            }


            if (box != null)
            {

                box.position = GrabSpot.transform.position;
            }


            move = new Vector3(MoveDirection.x, PlayerFall.y, MoveDirection.y);
            move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * move;
            if (doNothingOnStart)
            {
                MoveDirection.y = 0;
                move.z = 0;
                doNothingOnStart = false;
            }
            rb.MovePosition(rb.position + move * speed * Time.deltaTime);
        }
        
        
        
        
    }
}
