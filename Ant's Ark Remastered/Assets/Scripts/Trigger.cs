using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent onTrigger;
    public GameManager gameManager;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            onTrigger.Invoke();
            gameManager.Win();
        }
    }
}
