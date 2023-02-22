using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            gameManager.Win();
        }
    }
}
