using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorStatus : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private Animator Barriers;

   


    public void RemoveBarriers()
    {
        Barriers.Play("RemoveBarriers");
    }

    public void Finish()
    {
        _GameManager.pickerMovementStatus = true;
    }

}
