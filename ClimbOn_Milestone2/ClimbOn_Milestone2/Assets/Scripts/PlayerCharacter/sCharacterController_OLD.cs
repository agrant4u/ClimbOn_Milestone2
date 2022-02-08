using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

enum Limbs { handRight, handLeft, footRight, footLeft  }

public class sCharacterController_OLD : MonoBehaviour
{
    // BONES
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftFoot;
    public GameObject rightFoot;
    public GameObject leftLeg;
    public GameObject rightLeg;
    public GameObject leftShoulder;
    public GameObject rightShoulder;
    public GameObject hips;

    //public GameObject animationShoulderLeft;
    //public GameObject animationShoulderRight;
    //public GameObject animationLegLeft;
    //public GameObject animationLegRight;

    // TARGETS IN ANIMATION RIGGING
    public GameObject targetArmRight;
    public GameObject targetArmLeft;
    public GameObject targetLegRight;
    public GameObject targetLegLeft;

    // IK MOVER GAME OBJECTS - PULLS TwoBoneIKContraint from this
    public GameObject pLeftHandMover;
    public GameObject pRightHandMover;
    public GameObject pLeftFootMover;
    public GameObject pRightFootMover;

    GameObject reachingLimb;  // TO BE USED TO REACH TOWARDS TARGET

    // ANIMATION RIGGING MOVERS
    TwoBoneIKConstraint leftHandIK;
    TwoBoneIKConstraint rightHandIK;
    TwoBoneIKConstraint leftLegIK;
    TwoBoneIKConstraint rightLegIK;

    TwoBoneIKConstraint limbToConstrain;  // CURRENT CONTSTRAINED LIMB

    // CONTROLS REFS
    InputAction.CallbackContext rightHandContext;
    InputAction.CallbackContext leftHandContext;
    InputAction.CallbackContext rightFootContext;
    InputAction.CallbackContext leftFootContext;

    // CURRENT LIMB BEING CONTROLED REF
    InputAction.CallbackContext currentLimbContext;


    // SET UP TOP LEFT GOING CLOCKWISE
    //public Transform[] armRightBarriers;
    //public Transform[] armLeftBarriers;
    //public Transform[] legRightBarriers;
    //public Transform[] legLeftBarriers;

    // MOVEMENT OF HIPS!
    public float hipSpeed;
    // JUMP POWER!
    public float jumpForce;

    //public float armRotationSpeed;
    //public float legRotationSpeed;

    public float limbTargetMovementSpeed;

    //public float rotationSpeed;

    //public Transform root;
    //public ConfigurableJoint jointToMove;

    GameObject limbToControl;

    //float mouseX, mouseY;

    //float rotationLimit;

    //public Quaternion maxRotation;

    //public Rigidbody rb_hips;
    //public Rigidbody rb_shoulderLeft;
    //public Rigidbody rb_shoulderRight;
    //public Rigidbody rb_legLeft;
    //public Rigidbody rb_legRight;
    //public Rigidbody rb_handRight;
    //public Rigidbody rb_handLeft;

    public static bool isGrounded;
    public static bool isLimbMoving;
    //public static bool isReaching;

    //JointDrive rightDrive;
    //JointDrive leftDrive;

    Vector2 limbMovement;

    //Vector2 cameraMovement;
    
    Vector2 hipMovement;

    //public static List<FixedJoint> fixedJoints;

    PlayerControls controller;

    PlayerInput playerInput;
    
    InputAction movement;

    public float cameraRotationSensitivity;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //public Transform cam;

    //public Animator playerAnimator;

    //public CharacterController characterControler;

    //public Transform rightHandTarget;
    //public Transform leftHandTarget;
    //public Transform rightFootTarget;
    //public Transform leftFootTarget;

    Vector3 rightHandStartingPos;
    Vector3 leftHandStartingPos;
    Vector3 rightFootStartingPos;
    Vector3 leftFootStartingPos;



    Vector3 tempStartingPos;

    private void Start()
    {
        //hips = GetComponent<Rigidbody>();

        //Cursor.lockState = CursorLockMode.Locked;

        //ConfigurableJoint rightJoint = shoulderRight.GetComponent<ConfigurableJoint>();
        //ConfigurableJoint leftJoint = shoulderLeft.GetComponent<ConfigurableJoint>();

        //rightDrive = new JointDrive();
        //leftDrive = new JointDrive();

        //rightDrive.mode = JointDriveMode.Position;
        //leftDrive.mode = JointDriveMode.Position;

        //rightJoint.zDrive = rightDrive;
        //leftJoint.zDrive = leftDrive;

        // rightDrive = rightJoint.zDrive;
        // leftDrive = leftJoint.zDrive;
       
        

    }


    void Awake()
    {
        leftHandIK = pLeftHandMover.GetComponent<TwoBoneIKConstraint>();
        rightHandIK = pRightHandMover.GetComponent<TwoBoneIKConstraint>();
        leftLegIK = pLeftFootMover.GetComponent<TwoBoneIKConstraint>();
        rightLegIK = pRightFootMover.GetComponent<TwoBoneIKConstraint>();

        leftHandIK.weight = 0;
        rightHandIK.weight = 0;
        leftLegIK.weight = 0;
        rightLegIK.weight = 0;

        rightHandStartingPos = targetArmRight.transform.localPosition;
        leftHandStartingPos = targetArmLeft.transform.localPosition;
        rightFootStartingPos = targetLegRight.transform.localPosition;
        leftFootStartingPos = targetLegLeft.transform.localPosition;

        playerInput = GetComponent<PlayerInput>();

        //controller.Player.MoveSelectedLimb.performed += context => MoveArm();
        controller = new PlayerControls();

        //controller.Gameplay.Enable();

        //rightShoulder.GetComponent<ConfigurableJoint>().targetRotation = animationShoulderRight.transform.rotation;
        //leftShoulder.GetComponent<ConfigurableJoint>().targetRotation = animationShoulderLeft.transform.rotation;
        //rightLeg.GetComponent<ConfigurableJoint>().targetRotation = animationLegRight.transform.rotation;
        //leftLeg.GetComponent<ConfigurableJoint>().targetRotation = animationLegLeft.transform.rotation;

        //controller.Gameplay.Jump.performed += Jump;

        //controller.Gameplay.Drop.performed += Drop;

        //controller.Gameplay.ArmRight.started += MoveRightArm;
        //controller.Gameplay.ArmRight.canceled += context => isLimbMoving = false;

        //controller.Gameplay.ArmLeft.started += MoveLeftArm;
        //controller.Gameplay.ArmLeft.canceled += context => isLimbMoving = false;

        //controller.Gameplay.LegLeft.started += MoveLeftLeg;
        //controller.Gameplay.LegLeft.canceled += context => isLimbMoving = false;

        //controller.Gameplay.LegRight.started += MoveRightLeg;
        //controller.Gameplay.LegRight.canceled += context => rightLegIK.weight = 0;

        //controller.Gameplay.Reach.performed += Reach;

        //controller.Gameplay.TargetMove.performed += context => cameraMovement = context.ReadValue<Vector2>();
        //controller.Gameplay.CameraMove.canceled += context => cameraMovement = Vector2.zero;

        isLimbMoving = false;
        limbMovement = new Vector2();
    }

    private void OnEnable()
    {

        controller.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controller.Gameplay.Disable();
    }

    private void FixedUpdate()  // NEW INPUT VERSION!!!
    {

        MoveHips();

        if (isLimbMoving)
        {
            MoveLimb();
        }
        

        //CheckIfLimbIsMoving();

    }

    void CheckIfLimbIsMoving()
    {
        if (limbToControl == null)  // IF TRIGGER ISNT HELD YOU CAN SELECT LIMB AGAIN
        {
            Debug.Log("Limb Isn't Moving");
            isLimbMoving = false;
        }
    }

    /*
    void CameraController()
    {
        //Transform target;
        //Vector3 pivot;
        //Vector3 offset;

        //Vector3 velo;

        Vector2 cameraVector = controller.Gameplay.CameraMove.ReadValue<Vector2>();

        Vector3 direction = new Vector3(cameraVector.x, 0, cameraVector.y).normalized;

        if (direction.magnitude >=0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //characterControler.Move(moveDir.normalized * cameraRotationSensitivity * Time.deltaTime);



        }

        //Vector3 localRight = Vector3.Cross(Vector3.up, offset);
        //offset = Quaternion.AngleAxis(cameraVector.x), Vector3.up) * Quaternion.AngleAxis(cameraVector.y), localRight) *offset;


        //transform.rotation = Vector3.SmoothDamp(transform.position, transform.position + cameraVector , ref velo, 0.5f, 20f, Time.fixedDeltaTime);
        
        //transform.rotation = Vector3.SmoothDamp();
        //transform.forward = target.position - transform.position;

        //transform.rotation = (Quaternion.Euler(new Vector3(0, cameraVector.x * cameraRotationSensitivity, 0)));


    }
    */// OLD CAM CONTROLLER



    void MoveHips()
    {

        // THIS HANDLES MOVEMENT OF THE HIPS
        //Vector2 hipsVector = GetLeftStickValue();

        //float h = hipsVector.x;
        //float v = hipsVector.y;

        //Vector2 input = SquareToCircle(new Vector2(h, v));
        
        Rigidbody rb;
        rb = hips.GetComponent<Rigidbody>();
        //rb.useGravity = true;

        //hipsVector.Normalize();

        //RaycastHit hit;

        //if (hipsVector.magnitude >= 0.1f)
        //{
           // if (Physics.Raycast(transform.position, // POSITION
                           // transform.forward,  // DIRECTION 
                            //out hit)) // HIT DATA
            //{

                //transform.forward = -hit.normal;

                // SMOOTHES PLAYER POSITION TO STICK TO WALL
                //rb.position = Vector3.Lerp(rb.position, hit.point + hit.normal * 0.51f, 10f * Time.fixedDeltaTime);


           // }

            //rb.velocity = transform.forward * -hipsVector.x * hipSpeed + transform.right * hipsVector.y * hipSpeed;

            
            //rb.velocity = transform.TransformDirection(hipsVector.normalized) * hipSpeed;

          //  if (isGrounded)
           // {


                //Vector3 m = new Vector3(hipsVector.x, 0, 0);
                //hips.velocity = m;

                // ORIGINAL MOVE 
                //rb.AddForce(new Vector3(hipsVector.x, 0, 0) * hipSpeed, ForceMode.Acceleration);
           // }

           // else
            //{
                //Vector3 m = new Vector3(hipsVector.x, hipsVector.y, 0);
                //hips.velocity = m;

                //rb.velocity = transform.TransformDirection(hipsVector.normalized) * hipSpeed;

               // rb.AddForce(new Vector3(hipsVector.x, hipsVector.y, 0) * hipSpeed, ForceMode.Acceleration);
            //}
        //}

        
    }

    Vector2 SquareToCircle (Vector2 input)
    {
        return (input.sqrMagnitude >= 1f) ? input.normalized : input;
    }

    void MoveLeftArm(InputAction.CallbackContext context)  // LEFT ARM MOVEMENT
    {
        Debug.Log("Starting " + context);
        leftHandContext = context;

        //playerAnimator.SetBool("isLeftArmMoving", true);

        if (leftHand.GetComponent<sHoldGrab>().handHold != null)  // This checks to see if hand is connected to hold
        {
            Destroy(leftHand.GetComponent<FixedJoint>()); // This destroys the hand and hold connection
        }

        if (context.canceled)
        {
            limbToControl = null;
            isLimbMoving = false;
            limbToConstrain.weight = 0;
        }


        //MoveLimb(leftHand);

        //leftHand.GetComponent<Rigidbody>().AddForce(leftHandTarget.position * armRotationSpeed, ForceMode.Force);

        if (!isLimbMoving)
        {
            Debug.Log("Left Hand Movement");
            //leftHandIK.weight = 1;
            limbToControl = targetArmLeft;
            limbToConstrain = leftHandIK;
            isLimbMoving = true;
            reachingLimb = leftHand;
            leftHandStartingPos = targetArmLeft.transform.position;
            tempStartingPos = leftHandStartingPos;
            //rotationLimit = -90f;
        }


    }

    void MoveRightArm(InputAction.CallbackContext context) // RIGHT ARM MOVEMENT
    {
        Debug.Log("Starting " + context);
        //playerAnimator.SetBool("isRightArmMoving", true);
        rightHandContext = context;

        if (rightHand.GetComponent<sHoldGrab>().handHold != null)  // This checks to see if hand is connected to hold
        {
            Destroy(rightHand.GetComponent<FixedJoint>()); // This destroys the hand and hold connection
        }


        //rightHand.GetComponent<Rigidbody>().velocity = transform.TransformDirection(rightFootTarget.position) * armRotationSpeed;
        else
        {
            Debug.Log("Not gripping held");
        }
       

        if (!isLimbMoving)
        {
            currentLimbContext = rightHandContext;
            Debug.Log("Right Hand Movement");
            //rightHandIK.weight = 1;
            limbToControl = targetArmRight;
            limbToConstrain = rightHandIK;
            isLimbMoving = true;
            reachingLimb = rightHand;
            rightHandStartingPos = targetArmRight.transform.position;
            tempStartingPos = rightHandStartingPos;
            //rotationLimit = 90f;
        }


        if (context.canceled)
        {
            limbToControl = null;
            isLimbMoving = false;
            limbToConstrain.weight = 0;
        }
    }

    void MoveLeftLeg(InputAction.CallbackContext context) // LEFT LEG MOVEMENT
    {
        Debug.Log("Starting " + context);
        leftFootContext = context;
        //playerAnimator.SetBool("isLeftLegMoving", true);

        if (leftFoot.GetComponent<sHoldGrab>().handHold != null)  // This checks to see if hand is connected to hold
        {
            Destroy(leftFoot.GetComponent<FixedJoint>()); // This destroys the hand and hold connection
        }

        if (!isLimbMoving)
        {
            currentLimbContext = leftFootContext;
            Debug.Log("Left Leg Movement");
            //leftLegIK.weight = 1;
            limbToControl = targetLegLeft;
            limbToConstrain = leftLegIK;
            isLimbMoving = true;
            reachingLimb = leftFoot;
            leftFootStartingPos = targetLegLeft.transform.localPosition;
            tempStartingPos = leftFootStartingPos;
            //rotationLimit = 90f;
        }


        if (context.canceled)
        {
            limbToConstrain.weight = 0;
            limbToControl = null;
            isLimbMoving = false;
        }
    }

    void MoveRightLeg(InputAction.CallbackContext context)  // RIGHT LEG MOVEMENT
    {
        Debug.Log("Starting " + context);
        rightFootContext = context;
     
        //playerAnimator.SetBool("isRightLegMoving", true);


        //DESTROYS CURRENT GRIP ON HOLD
        if (rightFoot.GetComponent<sHoldGrab>().handHold != null)  // This checks to see if hand is connected to hold
        {
            Destroy(rightFoot.GetComponent<FixedJoint>()); // This destroys the hand and hold connection
        }


        if (!isLimbMoving) // CHECKS IF THERE IS A LIMB ALREADY MOVING
        {
            currentLimbContext = rightFootContext;
            Debug.Log("Right Leg Movement");
            //rightLegIK.weight = 1;
            limbToControl = targetLegRight;
            limbToConstrain = rightLegIK;
            isLimbMoving = true;
            reachingLimb = rightFoot;
            rightFootStartingPos = targetLegRight.transform.localPosition;
            tempStartingPos = rightFootStartingPos;
            //rotationLimit = 90f;
        }


        if (context.canceled)
        {
            limbToConstrain.weight = 0;
            limbToControl = null;
            isLimbMoving = false;
        }


    }

    void MoveLimb()
    {  
      //  limbMovement = controller.Gameplay.TargetMove.ReadValue<Vector2>();

        //controller.Gameplay.ArmLeft.canceled;

        Vector3 movement = new Vector3(limbMovement.x, limbMovement.y, 0);

        Debug.Log("Limb Vector is : " + limbMovement.ToString());

        Debug.Log("Limb to Control = " + limbToControl);

        tempStartingPos = limbToControl.transform.position;

        CheckIfLimbIsMoving();

        Vector3 offset = new Vector3();

        if(currentLimbContext.canceled)
        {
           // StopMovingLimb();
        }

        if (limbToControl != null && isLimbMoving)// && limbToControl.transform.rotation.z != rotationLimit)

        {
            Debug.Log("Moving " + limbToControl);

            // CONTROLS FOR LIMB TARGETS


            //limbToControl.transform.position += movement * limbTargetMovementSpeed * Time.deltaTime;
            
            limbToConstrain.weight = 1;

           

            if (limbToControl == targetArmLeft || limbToControl == targetLegLeft)
            {
                offset = new Vector3(-0.5f,0,0);
            }

            else if (limbToControl == targetArmRight || limbToControl == targetLegRight)
            {
                offset = new Vector3(0.5f,0,0);
            }

            else
            {

            }

            
            limbToControl.GetComponent<Rigidbody>().MovePosition(movement + gameObject.transform.position + offset);// * Time.fixedDeltaTime;


                //limbToControl.transform.localPosition = (movement + tempStartingPos);

                




            if (limbToControl == rightShoulder || limbToControl == leftShoulder)
            {
                
            }

            else if (limbToControl == leftLeg || limbToControl == rightLeg)
            {


            }


        }

        else

        {

            //limbToControl.transform.SetPositionAndRotation(startingPos, startingRot);

            //limbToControl.transform.position = startingPos;
            //limbToControl.transform.rotation = startingRot;

            //SendTargetBackToStart(tempStartingPos, limbToControl);

            

            //StopMovingLimb();

        }

    }

    void StopMovingLimb()
    {

        Debug.Log("Stopping Limb Movment");

        isLimbMoving = false;

        limbToControl.transform.localPosition = tempStartingPos;

        limbToControl = null;

        limbToConstrain.weight = 0;
        limbToConstrain = null;

        
    }


    // IS USED AFTER A LIMB HAS BEEN TRIGGERED AND WILL REACH TOWARD THE TARGET OF TRIGGERED LIMB
    void Reach(InputAction.CallbackContext context)
    {

        if (isLimbMoving)
        {

            Debug.Log("Reaching!");
            //reachingLimb.GetComponent<Rigidbody>().AddForce(limbToControl.transform.position.x * jumpForce, limbToControl.transform.position.y * jumpForce, 0);

        }

    }

    // SETS THE HOLD TARGETS BACK TO THEIR DEFAULT LOCATION
    void SendTargetBackToStart(Transform _startingPos, GameObject _limbToControl)
    {

       // _limbToControl.transform.SetPositionAndRotation(_startingPos.position, _startingPos.rotation);
        

    }

    void Jump(InputAction.CallbackContext context)  // JUMP ACTION.  FEET HAVE OTHER SCRIPT TO CHECK FOR GROUND COLLISION
    {

        if (isGrounded)
        {
            Debug.Log("Jump");

            ReleaseAllHolds();

            Rigidbody rb_hips = hips.GetComponent<Rigidbody>();
            Rigidbody rb_handLeft = leftHand.GetComponent<Rigidbody>();
            Rigidbody rb_handRight = rightHand.GetComponent<Rigidbody>();
            Rigidbody rb_shoulderLeft = leftShoulder.GetComponent<Rigidbody>();
            Rigidbody rb_shoulderRight = rightShoulder.GetComponent<Rigidbody>();



            rb_hips.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

            // REPLACE THESE LOWER ONES WITH DYNO ANIMATION
            rb_handLeft.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            rb_handRight.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

            rb_shoulderLeft.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            rb_shoulderRight.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

            isGrounded = false;
        }
    }

    void Drop(InputAction.CallbackContext context)  // DESTROYS ALL FIXED JOINTS IN LIMBS TO DROP PLAYER
    {

        Debug.Log("Drop");

        ReleaseAllHolds();

    }

    public void ReleaseAllHolds()
    {
    
        if (leftFoot.GetComponent<sHoldGrab>().handHold != null)  // This checks to see if hand is connected to hold
        {
            Destroy(leftFoot.GetComponent<FixedJoint>()); // This destroys the hand and hold connection
        }
        if (rightFoot.GetComponent<sHoldGrab>().handHold != null)  // This checks to see if hand is connected to hold
        {
            Destroy(rightFoot.GetComponent<FixedJoint>()); // This destroys the hand and hold connection
        }
        if (rightHand.GetComponent<sHoldGrab>().handHold != null)  // This checks to see if hand is connected to hold
        {
            Destroy(rightHand.GetComponent<FixedJoint>()); // This destroys the hand and hold connection
        }
        if (leftHand.GetComponent<sHoldGrab>().handHold != null)  // This checks to see if hand is connected to hold
        {
            Destroy(leftHand.GetComponent<FixedJoint>()); // This destroys the hand and hold connection
        }

    }

    /*

    public void FixedUpdate()  // OLD INPUT VERSION
    {
        
        if (Input.GetKey(KeyCode.Q)) // Left arm
        {

            //shoulderLeft.transform.Rotate(shoulderLeft.transform.position * armRotationSpeed * Time.deltaTime);
            
            if(leftHand.GetComponent<sHoldGrab>().handHold != null)  // This checks to see if hand is connected to hold
            {
                Destroy(leftHand.GetComponent<sHoldGrab>().handHold.GetComponent<FixedJoint>()); // This destroys the hand and hold connection
            }

            //shoulderLeft.GetComponent<Rigidbody>().AddForce(new Vector3(0, armRotationSpeed, 0));

            //leftHand.GetComponent<Rigidbody>().AddForce(new Vector3(10, armRotationSpeed, 0));

            //leftDrive.positionSpring += armRotationSpeed;

            //LimbMover(leftShoulder);
            Vector3 differece = Camera.main.ScreenToWorldPoint(Input.mousePosition) - leftShoulder.transform.position;

            differece.Normalize();

            float rotZ = Mathf.Atan2(differece.y, differece.x) * Mathf.Rad2Deg;

            leftShoulder.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        }

        if (Input.GetKey(KeyCode.E)) // Right Arm
        {

            if (rightHand.GetComponent<sHoldGrab>().handHold != null)  // This checks to see if hand is connected to hold
            {
                Destroy(rightHand.GetComponent<sHoldGrab>().handHold.GetComponent<FixedJoint>()); // This destroys the hand and hold connection
            }


            //shoulderRight.GetComponent<Rigidbody>().AddForce(new Vector3(0, armRotationSpeed, 0));

            //rightHand.GetComponent<Rigidbody>().AddForce(new Vector3(10, armRotationSpeed, 0));

            //rightDrive.positionSpring += armRotationSpeed;

            //LimbMover(rightShoulder);

            Vector3 differece = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rightShoulder.transform.position;

            differece.Normalize();

            float rotZ = Mathf.Atan2(differece.y, differece.x) * Mathf.Rad2Deg;

            rightShoulder.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            
            if (rotZ < -90 || rotZ > 90)
            {
                if (transform.eulerAngles.y == 0)
                {
                    rightShoulder.transform.localRotation = Quaternion.Euler(180, 0, -rotZ);
                }

                else if (transform.eulerAngles.y == 180)
                {
                    transform.localRotation = Quaternion.Euler(180, 180, -rotZ);
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.W))// Dyno
        {

            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Z))// Left Leg
        {

            if (leftFoot.GetComponent<sHoldGrab>().handHold != null)  // This checks to see if hand is connected to hold
            {
                Destroy(leftFoot.GetComponent<sHoldGrab>().handHold.GetComponent<FixedJoint>()); // This destroys the hand and hold connection
            }

            legLeft.GetComponent<Rigidbody>().AddForce(new Vector3(0, legRotationSpeed, 0));
            leftFoot.GetComponent<Rigidbody>().AddForce(new Vector3(-legRotationSpeed, 0, 0));

            //LimbMover(leftLeg);
        }

        if (Input.GetKeyDown(KeyCode.C))// Right Leg
        {

            if (rightFoot.GetComponent<sHoldGrab>().handHold != null)  // This checks to see if hand is connected to hold
            {
                Destroy(rightFoot.GetComponent<sHoldGrab>().handHold.GetComponent<FixedJoint>()); // This destroys the hand and hold connection
            }

            legRight.GetComponent<Rigidbody>().AddForce(new Vector3(0, legRotationSpeed, 0));
            rightFoot.GetComponent<Rigidbody>().AddForce(new Vector3(legRotationSpeed/2, 0, 0));

            //LimbMover(rightLeg);

        }


        if (Input.GetKey(KeyCode.A))// Left Hips
        {

            hips.AddForce(new Vector3(-hipSpeed, 0, 0));

        }

        if (Input.GetKey(KeyCode.D))// Right Hips
        {

            hips.AddForce(new Vector3(hipSpeed, 0, 0));

        }

    }

    public void LimbMover(GameObject limbToMove)
    {

        Vector3 differece = Camera.main.ScreenToWorldPoint(Input.mousePosition) - limbToMove.transform.position;

        differece.Normalize();

        float rotZ = Mathf.Atan2(differece.y, differece.x) * Mathf.Rad2Deg;

        limbToMove.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        //root = limbToMove.transform;

        //jointToMove = limbToMove.GetComponent<ConfigurableJoint>();

        //mouseX += Input.GetAxis("Horizontal") * rotationSpeed;
        //mouseY += Input.GetAxis("Vertical") * rotationSpeed;

        //Quaternion rootRotation = Quaternion.Euler(mouseY, mouseX, 0);

       // root.rotation = rootRotation;

        //jointToMove.targetRotation = Quaternion.Euler(mouseX, mouseY, 0);

        //limbToMove.transform.rotation = Quaternion.Slerp(limbToMove.transform.rotation, jointToMove.targetRotation, Time.deltaTime*5);

    }

    */ // OLD STUFF

}

