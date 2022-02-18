using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public enum ePlayerControlState { CLIMBING, WALKING, FALLING, OVERHANGING, LEDGE, JUMPING, QTE }

public class sCharacterController : MonoBehaviour
{

    PlayerControls controller;

    [Space]
    [Header("Movement")]
    [SerializeField]public float climbSpeed = 5f;
    float startingClimbSpeed;
    //float totalClimbSpeed;

    float climbSlowDownAmount=0;

    [SerializeField] public float walkSpeed = 5f;
    float startingWalkSpeed;

    public float dashSpeed = 500f;
    public int dashCooldownTime = 5;
    bool isDashing;

    public float sprintMultiplier = 2f;

    public float speedBoostTime = 10f;

    [SerializeField] float jumpForce = 5f;
    bool isJumping;

    bool isOverHanging;

    public static bool isGettingBucked = false;

    [Space]
    [Header("Camera")]
    // CAMERA STUFF
    [SerializeField] Transform cameraTarget;
    Vector3 cameraPivot;
    Vector3 cameraOffset;
    //[SerializeField] float cameraSensitivity = 5f;
    // CINEMACHINE
    //public GameObject camController;
    //CinemachineFreeLook freeLookCam;
    //public Camera cam;

    Rigidbody rb;

    [Space]
    [Header("Player State")]
    [SerializeField] public ePlayerControlState currentState = ePlayerControlState.WALKING;

    bool jumpDown = false;

    [Space]
    [Header("Falling")]
    public float maxFallVelocity = -20f;

    Vector3 velo;

    [Space]
    [Header("Health")]
    public float maxHitPoints = 100; // make static?
    public static float currentHitPoints;
    public float fallKillDistance = 10f;
    


    public static bool isDead = false;

    [Space]
    [Header("Walk Stamina")]
    public static float maxStamina = 100;
    public static float currentStamina;
    [Space]
    public float staminaDrainPerSec = 5;
    public float staminaRecoveryPerSec = 2;
    bool isSprinting = false;
    bool isNotSprinting;

    [Space]
    [Header("Climb Stamina")]
    [Space]
    public float staminaClimbDrainPerSec = 5;
    public float staminaClimbRecoveryPerSec = 2;

    [Space]
    [Header("Mantling")]
    bool canMantle = false;
    bool attempingMantle;
    public float mantleSpeed = 1f;
    public GameObject shoulderL;
    public GameObject shoulderR;


    //public static sCharacterController globalPlayerReference;

    //public GameObject shoulderRight;
    //public GameObject shoulderLeft;

    //Animaton shoulderRightAnimator;
    // Animaton shoulderLeftAnimator;

    //public Transform[] checkPoints;
    //int checkPointPos;

    public Transform startingPosition;

    Vector3 currentCheckPointPosition;

    public GameObject masterPlayer;

    [Space]
    [Header("Umbrella")]
    public GameObject umbrella;
    Vector3 umbrellaGravityReductionForce;
    public float umbrellaFloatForce = 10;
    bool isHoldingUmbrella;
    Vector3 normalGravity;

    [Space]
    [Header("Animator")]
    public Animator animController;
    public float animatorSpeed = 1; // DEFAULT 1

    [Space]
    [Header("GrappleGun")]
    public GameObject grappleGun;
    sGrapplingGun grappleGunBehavior;
    bool isAimingGrapple;
    //bool isRetractingGrapple = false;
    public bool isGrappling;

    public GameObject reticle;
    Vector3 reticleStartingPos;
    float reticleSensitivity = 5f;


    public static int collectiblesHeld = 0;
    public static int totalCollectibles = 3;

    bool canPickupRock;
    bool holdingRock;
    GameObject rockToPickup;
    FixedJoint rockPickupJoint;

    void Awake()
    {

        // SETS STARTING PLAYER POSITION TO 1st CheckPoint or StartingPoint
        currentCheckPointPosition = startingPosition.position;
        grappleGunBehavior = grappleGun.GetComponent<sGrapplingGun>();

        reticleStartingPos = reticle.transform.localPosition;

        normalGravity = Physics.gravity;
        umbrella.SetActive(false);
        isHoldingUmbrella = false;
        umbrellaGravityReductionForce = new Vector3(0, umbrellaFloatForce, 0);

        controller = new PlayerControls();

        //freeLookCam = camController.GetComponent<CinemachineFreeLook>();
        // SET CAM ORBITS?
        animController = masterPlayer.GetComponent<Animator>();
        SetAnimatorSpeed(animatorSpeed);

        controller.Gameplay.Jump.performed += Jump;

        controller.Gameplay.GrappleAim.performed += context => isAimingGrapple = !isAimingGrapple;
        controller.Gameplay.GrappleShoot.performed += context => GrappleShoot();
        controller.Gameplay.GrapplePull.performed += context => GrapplePull();

        controller.Gameplay.Umbrella.performed += context => ToggleUmbrella();

        controller.Gameplay.Sprint.performed += context => Sprint();

        controller.Gameplay.Mantle.performed += context => attempingMantle = !attempingMantle;

        controller.Gameplay.PickupRock.performed += context => RockPickup();

        controller.Gameplay.DashRoll.performed += conext => DashRoll();

    }

    private void OnEnable()
    {
        controller.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controller.Gameplay.Disable();
    }

    void Start()
    {

        //globalPlayerReference = this;
        isOverHanging = false;
        isJumping = false;
        isGrappling = false;
        isAimingGrapple = false;
        attempingMantle = false;
        canPickupRock = false;
        isDashing = false;
        rb = GetComponent<Rigidbody>();
        startingWalkSpeed = walkSpeed;
        currentHitPoints = maxHitPoints;
        currentStamina = maxStamina;


    }

    private void Update()
    {

        //CameraUpdate();

        // INPUT PER FRAME HERE
        //Vector2 movement = controller.Gameplay.Movement.ReadValue<Vector2>();
        //h = movement.x;
        //v = movement.y;

        if (!jumpDown)
        {
            //jumpDown = controller.Gameplay.Jump.triggered;
        }

        FallCheck();

        HealthCheck();

        ReticleUpdate();

        
    }

    void FixedUpdate()
    {
        //CameraFollow();

        MovementHandler();



    }

    void SetAnimatorSpeed(float _speed)
    {
        animController.speed = _speed;
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Hold"))
        {

            currentState = ePlayerControlState.CLIMBING;
                       
           
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            if (rb.velocity.y <= maxFallVelocity)
            {
                //PlayerDeath();
                //currentState = ePlayerControlState.WALKING;
                Debug.Log("Ground death triggered from " + rb.velocity.y + " velocity");
            }
        }

        sMoveableRock movingRock;
        movingRock = collision.gameObject.GetComponent<sMoveableRock>();

        if (movingRock)
        {
            Debug.Log("Moving rock collison!");
            if (!holdingRock)
            {
                rockToPickup = collision.gameObject;
                canPickupRock = true;
            }
            

        }
        

    }

    private void OnCollisionExit(Collision collision)
    {

        sMoveableRock movingRock;
        movingRock = collision.gameObject.GetComponent<sMoveableRock>();

        if (movingRock)
        {

            Debug.Log("Can't pickup rock now");
            
            canPickupRock = false;

        }

    }

    // USE THIS TO INCREMENT AND SET NEXT CHECKPOINT POS
    public void SetNewCheckPoint(Vector3 _newCheckPointPosition)
    {
        //checkPointPos++;
        currentCheckPointPosition = _newCheckPointPosition;
        
    }

    void HealthCheck()
    {

        if (sCharacterController.isDead)
        {

            PlayerDeath();

        }

    }

    // NEEDS WORK!
    void FallCheck()
    {

      
        // CHECKS FOR REGULAR FALLING
        if (rb.velocity.y < -0.3 && currentState != ePlayerControlState.CLIMBING)
        {

            if (currentState != ePlayerControlState.FALLING)
            {
                Debug.Log("Falling Happening from " + rb.velocity.y + " velocity");
                currentState = ePlayerControlState.FALLING;
            }
            

        }

    }

    void GroundCheck()
    {



        // CHECKS FOR HIT DIRECTLY BELOW CHARACTER
        RaycastHit hit;
        if (Physics.Raycast(transform.position,
                            Vector3.down,
                            out hit,
                            1.02f))
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.red);
            Debug.Log("Raycast Hit below");

            if (isJumping && rb.velocity.y == 0)
            {

                FallCheck();

            }

            else if (isJumping)
            {
                if (rb.velocity.y < maxFallVelocity)
                {
                    maxFallVelocity = rb.velocity.y;
                }
            }

            currentState = ePlayerControlState.WALKING;
            isJumping = false;
            isOverHanging = false;
        }


        else if (currentState != ePlayerControlState.CLIMBING && !isJumping)
        {
            // SETS FALL State
            //currentState = ePlayerControlState.FALLING;
        }

        else
        {

            //currentState = PlayerControlState.FALLING;

        }

    }

    void CeilingCheck()
    {

        RaycastHit hit;
        // CHECKS FOR HIT ABOVE CHARACTER
        if (Physics.Raycast(transform.position,
                         Vector3.up,
                         out hit, 1.02f))
        {
            Debug.DrawRay(transform.position, Vector3.up, Color.green);
            Debug.Log("Raycast Hit above");

            currentState = ePlayerControlState.OVERHANGING;
            isOverHanging = true;

            float rotSpeed = 10f;

            //shoulderL.transform.SetPositionAndRotation(shoulderL.transform.position, new Quaternion(0, 180, -180, 0));
            //shoulderR.transform.SetPositionAndRotation(shoulderR.transform.position, new Quaternion(0, 180, -180, 0));
            
            shoulderL.transform.rotation = Quaternion.Lerp(shoulderL.transform.rotation, new Quaternion(0, 180, 0, 0), Time.deltaTime * rotSpeed);
            shoulderR.transform.rotation = Quaternion.Lerp(shoulderR.transform.rotation, new Quaternion(0, 180, 0, 0), Time.deltaTime * rotSpeed);

        }

        else
        {

            //shoulderL.transform.SetPositionAndRotation(shoulderL.transform.position, new Quaternion(0, 180, 0, 0));
            //shoulderR.transform.SetPositionAndRotation(shoulderR.transform.position, new Quaternion(0, 180, 0, 0));

            //shoulderL.transform.rotation = Quaternion.Lerp(shoulderL.transform.rotation, new Quaternion(0, 180, 0, 0), Time.deltaTime);
            //shoulderR.transform.rotation = Quaternion.Lerp(shoulderR.transform.rotation, new Quaternion(0, 180, 0, 0), Time.deltaTime);



            //currentState = PlayerControlState.FALLING;
            isOverHanging = false;
            rb.useGravity = true;
        }


    }

    void MantleCheck()
    {
        

        Quaternion shoulderRot;

        float rotSpeed = 10f;

        Vector3 mantlePos;
        //FIRST CHECKS FOR A HIT AT WAIST LEVEL
        RaycastHit frontHit;

        if (Physics.Raycast(transform.position, Vector3.forward, out frontHit, 1.02f))

        {
            Debug.Log("Raycast Hit at chest level");
            Debug.DrawRay(transform.position, Vector3.forward, Color.red);

            RaycastHit hit1;

            // CHECKS FOR HIT ABOVE AND IN FRONT OF CHARACTER
            if (Physics.Raycast(transform.position,
                             Vector3.up + Vector3.forward,
                             out hit1, 1.02f))
            {
                Debug.DrawRay(transform.position, Vector3.up + Vector3.forward, Color.cyan);
                Debug.Log("Raycast Hit Ledge");

                Vector3 mantleOffset = new Vector3(0, 1.5f, 0);
                RaycastHit hit2;

                // CHECKS FOR HIT ABOVE AND IN FRONT OF CHARACTER FOR LEDGE TO GRAB
                if (Physics.Raycast(transform.position + mantleOffset,
                                 Vector3.forward,
                                 out hit2, 1.02f))
                {
                    Debug.DrawRay(transform.position + mantleOffset, Vector3.forward, Color.red);
                    Debug.Log("Raycast Hit spot to mantle to");

                    // HIT AT SPOT which means character can't mantle cause theres a wall
                    canMantle = false;


                }

                else
                {

                    mantlePos = new Vector3(0, transform.localPosition.y, transform.localPosition.z) + mantleOffset;
                    canMantle = true;

                    //shoulderRot = new Quaternion(150, 0, 0, 0);

                    //shoulderL.transform.localRotation = Quaternion.Slerp(shoulderL.transform.localRotation,shoulderRot, Time.deltaTime * rotSpeed);
                    //shoulderR.transform.localRotation = Quaternion.Slerp(shoulderR.transform.localRotation, shoulderRot, Time.deltaTime * rotSpeed);

                    // CHECKS FOR MANTLE INPUT HERE
                    if (attempingMantle && !holdingRock && !canPickupRock)
                    {
                        Debug.Log("Mantle attempt!");
                        MantleMove(mantlePos);
                    }
                }

            }


            else
            {
                //mantlePos = Vector3.forward + Vector3.up;

                //shoulderRot = new Quaternion(190, 0, 0, 0);

                //shoulderL.transform.localRotation = Quaternion.Slerp(shoulderL.transform.localRotation, shoulderRot, Time.deltaTime * rotSpeed);
                //shoulderR.transform.localRotation = Quaternion.Slerp(shoulderR.transform.localRotation, shoulderRot, Time.deltaTime * rotSpeed);


                mantlePos = new Vector3(0, transform.localPosition.y, transform.localPosition.z);

                if (attempingMantle && !holdingRock && !canPickupRock)
                {
                    Debug.Log("Mantle attempt!");
                    MantleMove(mantlePos);
                }

            }

        }     

    }

    IEnumerator MantleMovement()
    {
        int countDown = 2;   

            for (int i = 0; i < countDown; i++)
            {
                Debug.Log("Mantle Start");
            
                

                yield return new WaitForSeconds(1);
            }

            Debug.Log("Player has mantled!");
            
        
    }

    void MantleMove(Vector3 _spotToMove)
    {


            Vector3 offset = new Vector3(0, 1.5f, 0);
            Debug.Log("Mantling from " + transform.localPosition + " to " + (_spotToMove + offset));
            transform.localPosition = Vector3.Lerp(transform.localPosition, _spotToMove + offset, Time.fixedDeltaTime * mantleSpeed);
 

    }

    void PlayerDeath()
    {

        Vector3 offset = new Vector3(0, 1f, 0);
        gameObject.transform.position = currentCheckPointPosition + offset;
        sCharacterController.isDead = false;

    }

    // MAIN STATE MACHINE CONTROLLER
    void MovementHandler() 
    {
        
        // THIS HANDLES MOVEMENT WITH LEFT STICK
        Vector2 input = controller.Gameplay.Movement.ReadValue<Vector2>();
        
        //Vector2 camInput = controller.Gameplay.Camera.ReadValue<Vector2>();
        Transform cam = Camera.main.transform;

        // SWITCHES INPUT TO X/Z Plane for Walking
        Vector3 walkDirection = Quaternion.FromToRotation(cam.up, Vector3.up)
                                * cam.TransformDirection(new Vector3(input.x, 0f, input.y));


        // MOVEMENT STATE TRANSITIONS
        switch (currentState)
        {
            case ePlayerControlState.WALKING:
                {
                    WalkingMovement(walkDirection);
                    break;
                }
            case ePlayerControlState.FALLING:
                {
                    FallingMovement(walkDirection);
                    break;
                }
            case ePlayerControlState.CLIMBING:
                {                   
                    ClimbingMovement(input);
                    break;
                }
            case ePlayerControlState.OVERHANGING:
                {
                    if (isOverHanging)
                    OverHangMovement(walkDirection);
                    break;
                }
            case ePlayerControlState.JUMPING:
                {

                    JumpMovement(walkDirection);
                    break;
                }
           
        }

        GroundCheck();
        CeilingCheck();
        
        if (isHoldingUmbrella)
        {
            UmbrellaFly();
        }

        //rb.useGravity = currentState != PlayerControlState.CLIMBING;

        // RESET INPUT
        jumpDown = false;

    }

    void ClimbingMovement(Vector2 _input)
    {

        isJumping = false;

        float totalClimbSpeed = climbSpeed;

        Debug.Log("Climbing Happening");

        animController.SetBool("isClimbing", true);
        animController.SetBool("isFalling", false);

        // CHECK WALLS IN A CROSS PATTERN
        Vector3 offset = transform.TransformDirection(Vector2.one * 0.5f);
        Vector3 checkDirection = Vector3.zero;
        int k = 0;

        // RAYCASTS 4 times to check for direction AVG
        for (int i = 0; i < 4; i++)
        {

            RaycastHit checkHit;
            if (Physics.Raycast(transform.position + offset,
                                transform.forward,
                                out checkHit))
            {
                Debug.DrawRay(gameObject.transform.position + offset, transform.forward, Color.yellow);

                checkDirection += checkHit.normal;
                k++;
            }

            //ROTATE OFFSET BY 90 DEGREES
            offset = Quaternion.AngleAxis(90f, transform.forward) * offset;
        }

        checkDirection /= k;  // AVG OF THE RAYCASTS

        // CHECKS WALL DIRECTLY IN FRONT
        RaycastHit hit;
        if (Physics.Raycast(transform.position, // POSITION
                            -checkDirection,  // DIRECTION
                            out hit)) // HIT DATA
        {

            Debug.DrawRay(transform.position, -hit.transform.position, Color.blue);

            float dot = Vector3.Dot(transform.forward, -hit.normal);


            // SMOOTHES MOVEMENT ALONG THE WALL TO CURVE AROUND
            //transform.forward = -hit.normal;
            rb.position = Vector3.Lerp(rb.position,
                                       hit.point + hit.normal * 0.55f,
                                       5f * Time.fixedDeltaTime);

            transform.forward = Vector3.Lerp(transform.forward,
                                             -hit.normal,
                                             10f * Time.fixedDeltaTime);


            // WALL BEHAVIOR
            sWallBehavior wallBehavior;
            wallBehavior = hit.transform.gameObject.GetComponent<sWallBehavior>();
            
            if (wallBehavior)
            {

                // SETS SLOWDOWN AMOUNT BASED ON WALL BEHAVIOR
                climbSlowDownAmount = wallBehavior.CheckSlowDownState();


            }

            // MOVEMENT
            totalClimbSpeed = climbSpeed/climbSlowDownAmount;

            if (isSprinting)
            {

                SetAnimatorSpeed(animatorSpeed * sprintMultiplier);

                if (currentStamina > 0)
                {
                    Debug.Log("Sprinting happening");
                    totalClimbSpeed *= sprintMultiplier;
                    currentStamina -= staminaDrainPerSec * Time.fixedDeltaTime;
                }
            }

            else
            {
                SetAnimatorSpeed(animatorSpeed);
                Debug.Log("Not Sprinting");
                
                currentStamina += staminaRecoveryPerSec * Time.deltaTime;
                if (currentStamina > maxStamina)
                    currentStamina = maxStamina;
            }

            rb.useGravity = false;
            rb.velocity = transform.up * _input.y * totalClimbSpeed + transform.right * _input.x * totalClimbSpeed;

            if (jumpDown)
            {
                rb.velocity = Vector3.up * 5f + hit.normal * 2f;
                currentState = ePlayerControlState.FALLING;
            }
        }

        else
        {
            currentState = ePlayerControlState.FALLING;
            rb.useGravity = true;
        }
    }

    void OverHangMovement(Vector3 _moveDirection)
    {

        Debug.Log("OverHanging Movement Happening");

        animController.SetBool("isClimbing", false);
        animController.SetBool("isFalling", false);

        float totalWalkSpeed = walkSpeed;

        rb.useGravity = false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, // POSITION
                            Vector3.up,  // DIRECTION
                            out hit)) // HIT DATA
        {

            Debug.DrawRay(transform.position, Vector3.up, Color.blue);

            //float dot = Vector3.Dot(transform.forward, -hit.normal);


            // SMOOTHES MOVEMENT ALONG THE WALL TO CURVE AROUND
            //transform.forward = -hit.normal;

            //rb.position = Vector3.Lerp(rb.position,
            //                        hit.normal,
            //                      Time.fixedDeltaTime);


            transform.up = Vector3.Lerp(transform.up,
                                             -hit.normal,
                                             Time.fixedDeltaTime);

            /* SPRINTING?
            if (isSprinting)
            {


                if (currentStamina > 0)
                {
                    Debug.Log("Sprinting happening");
                    totalWalkSpeed *= sprintMultiplier;
                    currentStamina -= staminaDrainPerSec * Time.fixedDeltaTime;
                }
            }

            else
            {
                Debug.Log("Not Sprinting");
                currentStamina += staminaRecoveryPerSec * Time.deltaTime;
                if (currentStamina > maxStamina)
                    currentStamina = maxStamina;
            }
            */

        }

        else
        {
            //isOverHanging = false;
        }

        Vector3 oldVelo = rb.velocity;

        Vector3 newVelo = _moveDirection * totalWalkSpeed;
        newVelo.y = oldVelo.y;

        if (jumpDown)
        {

            newVelo.y = 5f;
            //currentState = PlayerControlState.FALLING;
        }

        rb.velocity = newVelo;

        if (_moveDirection.sqrMagnitude > 0.01f)
        {

            transform.forward = _moveDirection;

        }

    }

    void WalkingMovement(Vector3 _moveDirection)
    {
        Debug.Log("Walking happening");

        animController.SetBool("isClimbing", false);
        animController.SetBool("isFalling", false);

        MantleCheck();

        float totalWalkSpeed = walkSpeed;

        if (isSprinting)
        {
            SetAnimatorSpeed(animatorSpeed * sprintMultiplier);

            if (currentStamina > 0)
            {
                Debug.Log("Sprinting happening");
                totalWalkSpeed *= sprintMultiplier;
                currentStamina -= staminaDrainPerSec * Time.fixedDeltaTime;
            }
        }

        else
        {
            SetAnimatorSpeed(animatorSpeed);

            Debug.Log("Not Sprinting");
            currentStamina += staminaRecoveryPerSec * Time.deltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
        }
            

        Vector3 oldVelo = rb.velocity;

        Vector3 newVelo = _moveDirection * totalWalkSpeed;
        newVelo.y = oldVelo.y;

        if (jumpDown)
        {

            newVelo.y = 5f;
            currentState = ePlayerControlState.FALLING;
        }

        rb.velocity = newVelo;

        if (_moveDirection.sqrMagnitude > 0.01f)
        {

            transform.forward = _moveDirection;

        }
    }

    void FallingMovement(Vector3 _moveDirection)
    {
        Debug.Log("Falling happening");

        animController.SetBool("isClimbing", false);
        animController.SetBool("isFalling", true);
        
       

        if (_moveDirection.sqrMagnitude > 0.01f)
        {

            transform.forward = _moveDirection * walkSpeed;

        }

        if (jumpDown && Physics.Raycast(transform.position,
                                        transform.forward*0.8f))
        {

            Debug.DrawRay(transform.position, transform.forward * 0.8f, Color.white);

            currentState = ePlayerControlState.CLIMBING;
            animController.SetBool("isClimbing", true);
        }

    }

    void Sprint()  // USED FOR CONTROLS
    {

        isSprinting = !isSprinting;  // DONT CHANGE THIS
              
    }

    void Jump(InputAction.CallbackContext _context)  // JUMP ACTION.  FEET HAVE OTHER SCRIPT TO CHECK FOR GROUND COLLISION
    {

        if (!jumpDown)
        {
            Debug.Log("Jump");

            if(!isJumping)
            {
                // WALL JUMP
                if (currentState == ePlayerControlState.CLIMBING)
                {
                    Debug.Log("Climbing Jump");
                    isJumping = true;
                    rb.useGravity = true;
                    currentState = ePlayerControlState.JUMPING;
                    rb.AddForce(new Vector3(0, jumpForce, -jumpForce*2), ForceMode.Impulse);
                }

                //REGULAR WALK JUMP
                else if (currentState == ePlayerControlState.WALKING)
                {
                    Debug.Log("Walking Jump");
                    isJumping = true;
                    rb.useGravity = true;
                    currentState = ePlayerControlState.JUMPING;
                    rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                }

                maxFallVelocity = 0;


            }        

        }

    }

    void JumpMovement(Vector3 _moveDirection)
    {

        if (_moveDirection.sqrMagnitude > 0.01f)
        {

            transform.forward = _moveDirection * walkSpeed;

        }

    } // LETS PLAYER MOVE WHILE JUMPING IN AIR


    void DashRoll()
    {

        if (!isDashing)
        {

            Debug.Log("Dashing!");

            isDashing = true;
            Vector2 input = controller.Gameplay.Movement.ReadValue<Vector2>();

            if (currentState == ePlayerControlState.CLIMBING)
            {

                rb.useGravity = true;

                //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(input.x * dashSpeed, input.y * dashSpeed, 0), Time.deltaTime * 3);

                rb.AddForce(new Vector3(input.x * dashSpeed, input.y * dashSpeed, 0), ForceMode.Impulse);
                
                StartCoroutine(DashCooldown());
            }

            else
            {

                //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(input.x * dashSpeed, 0, input.y * dashSpeed), Time.deltaTime * 3);
                rb.AddForce(new Vector3(input.x * dashSpeed, 0, input.y * dashSpeed), ForceMode.Impulse);
                StartCoroutine(DashCooldown());
            }
        }

        

    }

    IEnumerator DashCooldown()
    {
        Debug.Log("Dash cooling down");

        for (int i = 0; i < dashCooldownTime; i++)
        {

            yield return new WaitForSeconds(1);
        }

        Debug.Log("You can now dash again!");
        isDashing = false;
    }

    // NEESD IMPLEMENTATION
    private bool isFacingWall()
    {
        //TODO: set angle threshold on wall facing

        RaycastHit hitInfo1, hitInfo2;
        LayerMask mask = LayerMask.GetMask("Default");

        //hard coding radius and player half height
        //cast at eye level
        Physics.SphereCast(transform.position + new Vector3(0, 0.25f, 0), 0.35f, transform.forward, out hitInfo1,
            0.5f, mask.value, QueryTriggerInteraction.Ignore);

        //cast at waist level
        Physics.SphereCast(transform.position, 0.35f, Vector3.down, out hitInfo2,
            0.5f, mask.value, QueryTriggerInteraction.Ignore);

        return (hitInfo1.collider != null || hitInfo2.collider != null);

    }

    public void SpeedBurst(float _boostAmount)
    {

        startingClimbSpeed = climbSpeed;
        startingWalkSpeed = walkSpeed;

        climbSpeed += _boostAmount;
        walkSpeed += _boostAmount;

        StartCoroutine("SpeedUp");

    }

    IEnumerator SpeedUp()
    {

        Debug.Log("Speed Boost Happening!");

        for (int i = 0; i < speedBoostTime; i++)
        {

            yield return new WaitForSeconds(1);

        }

        SpeedUpEnd();

    }

    void SpeedUpEnd()
    {


        Debug.Log("Speed Boost Off!");


        climbSpeed = startingClimbSpeed;
        walkSpeed = startingWalkSpeed;


    }

    public void StaminaChange(float _amount)
    {

        currentStamina += _amount;

    }

    public void StaminaBuff(float _time)
    {



    }

    void GrappleShoot()
    {
        // FLIPS THE BOOL FOR PRESS AND RELEASE.  STARTS AS FALSE SO FIRST PRESS WITLL MAKE IT TRUE.
        //isGrappling = !isGrappling;

        if (!holdingRock)
        {

            if (isGrappling == false)
            {

                animController.SetBool("isFalling", false);

                grappleGunBehavior.StartGrapple();


            }

            else
            {


                grappleGunBehavior.StopGrapple();


            }
        }
        
        else
        {
            isGrappling = false;
        }

    }



    public void GrapplePull()
    {

        if (isGrappling)
        {
            isJumping = false;
            grappleGun.GetComponent<sGrapplingGun>().GrappleRetract();

        }

    }

    void ReticleUpdate()
    {
        
        if (isAimingGrapple)
        {
            reticle.SetActive(true);

            Vector2 input = controller.Gameplay.Camera.ReadValue<Vector2>();
            Vector3 movement = new Vector3(input.x, input.y, 0);
            //Quaternion newRot = new Quaternion(input.x, input.y, 0);
            Vector2 reticleMovement = new Vector2(input.x * reticleSensitivity, input.y * reticleSensitivity);

            movement.Normalize();
            reticleMovement.Normalize();

            //reticle.GetComponent<Rigidbody2D>().MovePosition(reticleMovement);


            //reticle.transform.rotation = cam.gameObject.transform.rotation;

            //reticle.transform.rotation = Quaternion.AngleAxis(cam.transform.rotation.y, gameObject.transform.position);

            //reticle.transform.RotateAround(cam.transform.position, )

            reticle.transform.localPosition = reticleStartingPos + movement;

            //reticle.transform.rotation = Quaternion.identity;
        }

        else
        {
            reticle.SetActive(false);
        }

    }

    void ToggleUmbrella()
    {

        Debug.Log("Umbrella Toggled");

        isHoldingUmbrella = !isHoldingUmbrella;
        
        if (isHoldingUmbrella)
        {
            if (currentState != ePlayerControlState.CLIMBING)
            {

                umbrella.SetActive(true);

                animController.SetBool("isFalling", false);


            }
           
        }

        else
        {

            umbrella.SetActive(false);


        }

    }

    void UmbrellaFly()
    {

        rb.velocity = -normalGravity + umbrellaGravityReductionForce * Time.fixedDeltaTime;

    }

    void RockPickup()
    {

        if (holdingRock && rockPickupJoint)
        {

            Destroy(rockPickupJoint);
            rockToPickup.GetComponent<Rigidbody>().AddForce(gameObject.transform.localPosition + Vector3.up * 500);
            rockToPickup = null;
            holdingRock = false;

        }

        if(canPickupRock)
        {
            rockPickupJoint = gameObject.AddComponent<FixedJoint>();
            rockPickupJoint.connectedBody = rockToPickup.GetComponent<Rigidbody>();
            
            holdingRock = true;
            
        }

    }


    void CameraUpdate()
    {

        Vector2 cameraControls = controller.Gameplay.Camera.ReadValue<Vector2>();

        Vector3 localRight = Vector3.Cross(Vector3.up, cameraOffset);
        cameraOffset = Quaternion.AngleAxis(cameraControls.x, Vector3.up)
                        * Quaternion.AngleAxis(cameraControls.y, localRight)
                        * cameraOffset;
        
    }

    void CameraFollow()
    {

        transform.position = Vector3.SmoothDamp(transform.position,
                                                cameraTarget.position + cameraPivot + cameraOffset,
                                                ref velo,
                                                0.5f,
                                                20f,
                                                Time.fixedDeltaTime);
        transform.forward = cameraTarget.position - transform.position;

    }

}
