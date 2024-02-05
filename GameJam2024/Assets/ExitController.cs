using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    private void OnTriggerEnter(Collider a)
    {
        if(a.gameObject.CompareTag("Goblin"))
        {
            a.gameObject.SetActive(false);
        }
    }
}
