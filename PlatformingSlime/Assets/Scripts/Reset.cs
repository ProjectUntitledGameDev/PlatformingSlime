using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    private GlobalData globalData;

    private void Start()
    {
        globalData = GameObject.FindGameObjectWithTag("GlobalData").GetComponent<GlobalData>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            globalData.ResetPlayer(other.gameObject);
        }
    }
}
