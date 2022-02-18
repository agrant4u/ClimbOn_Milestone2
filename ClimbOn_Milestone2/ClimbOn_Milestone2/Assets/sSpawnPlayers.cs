using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class sSpawnPlayers : MonoBehaviour
{

    public GameObject pPlayer;

    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;

    private void Start()
    {

        //Vector3 randomPos = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));

        PhotonNetwork.Instantiate(pPlayer.name, this.gameObject.transform.position, Quaternion.identity);

    }

}
