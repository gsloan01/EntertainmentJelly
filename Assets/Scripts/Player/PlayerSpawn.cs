using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{


    void Start()
    {
        FPSPlayer player = FindObjectOfType<FPSPlayer>();
        if (player != null)
        {
            player.gameObject.transform.position = transform.position;
            player.gameObject.transform.rotation = transform.rotation;

            player.gameObject.transform.localScale = transform.localScale;
        }
    }
}
