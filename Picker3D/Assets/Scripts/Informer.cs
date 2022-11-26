using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Informer : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;



    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PickerBorderObject"))
        {
            _GameManager.BorderReach();
        }
    }




}
