using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Scoring _score;

    public int _timer;

    public Text _time;

    public GameObject PanelWinP1;
    public GameObject PanelWinP2;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 60;
        StartCoroutine("Chrono");
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer <= 0)
        {
            _timer = 0;

            if (_score.p1P > _score.p2P)
            {
                PanelWinP1.SetActive(true);
            }
            else if (_score.p1P < _score.p2P)
            {
                PanelWinP2.SetActive(true);
            }
        }
    }

    IEnumerator Chrono()
    {
        yield return new WaitForSeconds(1);
        _timer--;
        _time.text = "Time Left : " + _timer.ToString();
        StartCoroutine("Chrono");
    }
}
