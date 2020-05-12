using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour
{
    public GameObject _child;
    public GameObject _goal1;
    public GameObject _goal2;

    public Player_1 _pl1;
    public Player_2 _pl2;

    public bool _gJ1;
    public bool _gJ2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal_P1" && _child.tag == "P1Control")
        {
            Debug.Log("BUT J1 !");
            _gJ1 = true;
        }

        if (other.tag == "Goal_P1" && _child.tag == "P1Launch")
        {
            Debug.Log("SHOOT J1 !");
            _gJ1 = true;
        }

        if (other.tag == "Goal_P2" && _child.tag == "P2Control")
        {
            Debug.Log("BUT J2 !");
            _gJ2 = true;
        }

        if (other.tag == "Goal_P2" && _child.tag == "P2Launch")
        {
            Debug.Log("SHOOT J2 !");
            _gJ2 = true;
        }
    
    }
}
