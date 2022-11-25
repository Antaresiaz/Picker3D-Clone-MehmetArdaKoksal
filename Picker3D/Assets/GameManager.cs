using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


[Serializable]

public class BallPoolMechanics
{

    public Animator BallPoolElevator;
    public TextMeshProUGUI NumText;
    public int RequiredBallCount;

}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pickerObject;
    [SerializeField] private GameObject BallControlObject;
    [SerializeField] private bool pickerMovementStatus;


    int collectedBallCount;
    [SerializeField] private  List<BallPoolMechanics> _BallPoolMechanics = new List<BallPoolMechanics>();



    void Start()
    {
        pickerMovementStatus = true;
        _BallPoolMechanics[0].NumText.text = collectedBallCount + "/" + _BallPoolMechanics[0].RequiredBallCount;
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


        Collider[] HitColl = Physics.OverlapBox(BallControlObject.transform.position,BallControlObject.transform.localScale / 2,Quaternion.identity);


        int i = 0;
        while(i < HitColl.Length)
        {
            HitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0,0,.8f),ForceMode.Impulse);
            
                i++;
            

            
        }

        Debug.Log(i);
            
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(BallControlObject.transform.position,BallControlObject.transform.localScale);
    }
    
}
