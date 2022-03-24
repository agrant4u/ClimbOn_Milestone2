using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sWaypoint1 : MonoBehaviour
{
    public RectTransform prefab;

    private RectTransform waypoint;

    private Transform player;
    private Text distanceText;

    // Start is called before the first frame update
    void Start()
    {
        var canvas = GameObject.Find("uInGameUI").transform;

       waypoint = Instantiate(prefab, canvas);

        player = GameObject.Find("PLAYER_MASTER_current").transform;
        distanceText = waypoint.GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {

        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        waypoint.position = screenPos;

        waypoint.gameObject.SetActive(screenPos.z > 0);

        distanceText.text = Vector3.Distance(player.position, transform.position).ToString("0") + " m";
    }
}
