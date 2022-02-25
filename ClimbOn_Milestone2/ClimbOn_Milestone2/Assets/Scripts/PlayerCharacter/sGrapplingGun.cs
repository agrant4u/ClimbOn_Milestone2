using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sGrapplingGun : MonoBehaviour
{

    public GameObject pPlayer;
    sCharacterController player;

    public GameObject reticle;

    private LineRenderer lr;
    private Vector3 grapplingPoint;
    public LayerMask whatIsGrappleable;

    public Transform gunTip, playerPos;
    private float maxDistance = 500f;
    private SpringJoint joint;

    public float pullTime = 3f;

    public float grappleLengthMultiplier = 2f;

    public float grappleSpring = 4.5f;
    public float grappleDamper = 7f;
    public float grappleMassScale = 4.5f;



    private void Awake()
    {

        player = pPlayer.GetComponent<sCharacterController>();
        lr = GetComponent<LineRenderer>();
        playerPos = pPlayer.transform;
        

    }


    public void StartGrapple()
    {

        Debug.Log("Grapple Control triggered");

        RaycastHit hit;

        // OLD if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, maxDistance, whatIsGrappleable))
            if (Physics.Raycast(gameObject.transform.position, reticle.transform.forward * grappleLengthMultiplier, out hit, maxDistance, whatIsGrappleable))
            {

            Debug.Log("Starting Grapple Hook");
            Debug.DrawRay(gameObject.transform.position, hit.transform.position, Color.blue);

            grapplingPoint = hit.point;
            player.isGrappling = true;

            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplingPoint;

            float distanceFromPoint = Vector3.Distance(pPlayer.transform.position, grapplingPoint);

            // DISTANCE GRAPPLE WILL TRY TO KEEP FROM GRAPPLE POINT
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            // CHANGE THESE UP TO FEEL RIGHT
            joint.spring = grappleSpring;  //pull /push
            joint.damper = grappleDamper;
            joint.massScale = grappleMassScale;

            lr.positionCount = 2;

            }

    }

    void DrawRope()
    {

        if (!joint) return;

        
            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, grapplingPoint);

        


    }

    public void GrappleRetract()
    {


        if (joint)
        {

            StartCoroutine(GrappleMove());

        }

    }

    public void StopGrapple()
    {

        Debug.Log("Stoping Grapple Hook");

        lr.positionCount = 0;

        player.isGrappling = false;
        
        Destroy(joint);

    }

    IEnumerator GrappleMove()
    {

        

        float grappleCounter = 0;
        float grappleTime;

        grappleTime = (grapplingPoint.z - transform.position.z);

        //float grappleSpeed = 30f;

        Debug.Log("Grapple Move!");

            while (grappleCounter < grappleTime && joint)
            {
 
                player.transform.position = Vector3.Lerp(player.transform.position,
                                                   grapplingPoint,
                                                   (grappleCounter/grappleTime));

                grappleCounter += Time.deltaTime;
             
                //player.transform.position = Vector3.MoveTowards(player.transform.position, grapplingPoint, Time.deltaTime * grappleSpeed); 
                
                yield return null;

            }

        //yield return new WaitForSeconds(0.5f);

    }

    public bool isGrappling()
    {

        return joint != null;

    }

    public Vector3 GetGrapplePoint()
    {

        return grapplingPoint;

    }

    private void Update()
    {

       

    }

    private void LateUpdate()
    {

        DrawRope();

    }

}
