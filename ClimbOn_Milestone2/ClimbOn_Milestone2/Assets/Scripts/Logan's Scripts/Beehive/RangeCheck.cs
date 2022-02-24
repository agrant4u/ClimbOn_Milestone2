using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCheck : MonoBehaviour
{
    public Gun gun;
    public MountPoint[] mountPoints;
    public Transform target;

    private Turret turret;
    private MountPoint mountPoint;
    private sCharacterController characterController;

    //reference to range check on tower
    public SpriteRenderer rangedSpriteRenderer;
    private List<GameObject> ObjectsInRange;



    public float m_MaxDistance;
    public LayerMask m_Mask = 9;
    public GameObject m_MyGameObject;
    public GameObject player;
    Collider m_OtherGameObjectCollider;







    


}
