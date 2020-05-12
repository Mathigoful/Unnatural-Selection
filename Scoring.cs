using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public int p1P;
    public int p2P;

    public Text _s1P;
    public Text _s2P;

    public Goals _goal;
    public Player_1 _p1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoringP1();
        ScoringP2();
        UpdateTable();
    }

    void ScoringP1()
    {
        if (_goal._gJ1 == true)
        {
            Debug.Log("+1 Score P1");
            p1P++;
        }
    }

    void ScoringP2()
    {
        if (_goal._gJ2 == true)
        {
            p2P++;
        }
        else if (_p1._ggJ1 == true)
        {
            p2P++;
            _p1._ggJ1 = false;
        }
    }

    void UpdateTable()
    {
        _s1P.text = "Score P1 = " + p1P.ToString();
        _s2P.text = "Score P2 = " + p2P.ToString();
    }
}
