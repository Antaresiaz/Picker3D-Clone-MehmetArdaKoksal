using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;


[Serializable]

public class BallPoolMechanics
{

    public Animator BallPoolElevator;
    public TextMeshProUGUI NumText;
    public int RequiredBallCount;
    public GameObject[] Balls;

}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pickerObject;
    [SerializeField] private GameObject BallControlObject;
    public bool pickerMovementStatus;
    


    int collectedBallCount;
    int totalCheckpointNumber;
    int currentCheckpointNumber;


    [SerializeField] private  List<BallPoolMechanics> _BallPoolMechanics = new List<BallPoolMechanics>();



    void Start()
    {
        pickerMovementStatus = true;

        for(int i = 0; i < _BallPoolMechanics.Count; i++)
        {
            _BallPoolMechanics[i].NumText.text = collectedBallCount + "/" + _BallPoolMechanics[i].RequiredBallCount;
        }

        totalCheckpointNumber = _BallPoolMechanics.Count-1;

        Debug.Log(_BallPoolMechanics.Count);
    }

    
    void Update()
    {

        if (pickerMovementStatus)
        {
            pickerObject.transform.position += 5f * Time.deltaTime * pickerObject.transform.forward;


            if(Time.timeScale != 0)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    pickerObject.transform.position = Vector3.Lerp(pickerObject.transform.position, new Vector3(pickerObject.transform.position.x - .1f, pickerObject.transform.position.y
                        , pickerObject.transform.position.z),.05f);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    pickerObject.transform.position = Vector3.Lerp(pickerObject.transform.position, new Vector3(pickerObject.transform.position.x +.1f, pickerObject.transform.position.y
                        , pickerObject.transform.position.z),.05f);
                }
            }
        }
        
    }



    public void BorderReach()
    {
        pickerMovementStatus = false;

        Invoke("PhaseCheck", 2f);


        Collider[] HitColl = Physics.OverlapBox(BallControlObject.transform.position,BallControlObject.transform.localScale / 2,Quaternion.identity);


        int i = 0;
        while(i < HitColl.Length)
        {
            HitColl[i+1].GetComponent<Rigidbody>().AddForce(new Vector3(0,0,.8f),ForceMode.Impulse);
            
                i++;
            

            
        }

    }

    
    public void CountBalls()
    {
        collectedBallCount++;
        _BallPoolMechanics[currentCheckpointNumber].NumText.text = collectedBallCount + "/" + _BallPoolMechanics[currentCheckpointNumber].RequiredBallCount;
    }


    void PhaseCheck() {

        if (collectedBallCount >= _BallPoolMechanics[currentCheckpointNumber].RequiredBallCount)
        {
            Debug.Log("YOU WIN");


            _BallPoolMechanics[currentCheckpointNumber].BallPoolElevator.Play("ElevatorAnim");


            foreach(var item in _BallPoolMechanics[currentCheckpointNumber].Balls)
            {
                item.SetActive(false);
            }

            if(currentCheckpointNumber == totalCheckpointNumber)
            {
                Debug.Log("GAME OVER!");
                Time.timeScale = 0;
            }
            else
            {
                currentCheckpointNumber++;
                collectedBallCount = 0;
            }
            
            
            
            


        }
        else
        {
            Debug.Log("YOU LOST");
        }
    
    }


}
