using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_1 : MonoBehaviour
{
    public GameObject _player;
    public GameObject _childtaken;
    public Transform _taken;
    public float range;
    public float _speed;
    public float _launch;

    public bool _istaken1;
    public bool _islaunch1;
    public bool _ggJ1;
    public bool _inT2;

    public Child_Spawners _cS;
    public Player_2 _p2;

    public Rigidbody rb;
    public Rigidbody _rb2;

    AudioSource audioSrc;
    public AudioClip firstAudioClip;
    public AudioClip secondAudioClip;
    public AudioClip thirdAudioClip;
    public AudioClip fourthAudioClip;
    public AudioClip fifthAudioClip;

    public Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Moves();
        Actions();
    }

    void Moves()
    {
        if (_player.tag == "Player1")
        {
            float h = Input.GetAxis("P1Left_Stick_Horizontal");
            float v = Input.GetAxis("P1Left_Stick_Vertical");

            if (v >= 0.1)
            {
                rb.velocity = new Vector3(rb.velocity.x, 5, 0);
                _taken.position = _player.transform.position + new Vector3(0, 1, 0);
                _anim.SetBool("_runB", true);
                if (!audioSrc.isPlaying) audioSrc.PlayOneShot(firstAudioClip, 1F);
            }
            else rb.velocity = new Vector3(0, 0, 0);  

            if (v <= -0.1)
            {
                rb.velocity = new Vector3(rb.velocity.x, -5, 0);
                _taken.position = _player.transform.position + new Vector3(0, -1, 0);
                _anim.SetBool("_runF", true);
                if (!audioSrc.isPlaying) audioSrc.PlayOneShot(firstAudioClip, 1F);
            }
            if (h >= 0.1)
            {
                rb.velocity = new Vector3(5, rb.velocity.y, 0);
                _taken.position = _player.transform.position + new Vector3(1, 0, 0);
                if (!audioSrc.isPlaying) audioSrc.PlayOneShot(firstAudioClip, 1F);
            }
            if (h <= -0.1)
            {
                rb.velocity = new Vector3(-5, rb.velocity.y, 0);
                _taken.position = _player.transform.position + new Vector3(-1, 0, 0);
                _anim.SetBool("_runL", true);
                if (!audioSrc.isPlaying) audioSrc.PlayOneShot(firstAudioClip, 1F);
            }

            if (rb.velocity == new Vector3(0,0,0))
            {
                audioSrc.Stop();
                _anim.SetBool("_runB", false);
                _anim.SetBool("_runF", false);
                _anim.SetBool("_runL", false);
            };
        }
    }

    void Actions()
    {
        if (_istaken1 == true)
        {
            _childtaken.transform.position = _taken.transform.position;
            _rb2 = _childtaken.GetComponent<Rigidbody>();
            _islaunch1 = false;
        }

        if (_istaken1 == true && Input.GetButtonDown("P1_X"))
        {
            float h = Input.GetAxis("P1Left_Stick_Horizontal");
            float v = Input.GetAxis("P1Left_Stick_Vertical");

            Debug.Log("Launch Children");
            _istaken1 = false;
            _islaunch1 = true;
            _childtaken.tag = "P1Launch";

            StartCoroutine("Launched");

            if (v >= 0.1)
            {
                _rb2.velocity = new Vector3(_rb2.velocity.x, 8, 0);
            }
            else _rb2.velocity = new Vector3(0, 0, 0);
            if (v <= -0.1) _rb2.velocity = new Vector3(_rb2.velocity.x, -8, 0);
            if (h >= 0.1) _rb2.velocity = new Vector3(8, _rb2.velocity.y, 0);
            if (h <= -0.1) _rb2.velocity = new Vector3(-8, _rb2.velocity.y, 0);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Child")
        {
            Debug.Log("Child Detected");
            _childtaken = other.gameObject;

            if (Input.GetButtonDown("P1_A"))
            {
                Debug.Log("Children Taken");
                _istaken1 = true;
                _childtaken.tag = "P1Control";
                audioSrc.PlayOneShot(secondAudioClip, 1F);
            }
        }

        else if (other.tag == "Player2")
        {
            Debug.Log("In Player2 Trigger");
            _inT2 = true;
        }
        else _inT2 = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal_P2" && (_player.tag == "P2Launch" || _player.tag == "P2Control"))
        {
            audioSrc.PlayOneShot(thirdAudioClip, 1F);
            Debug.Log("P1 IN GOAL !");
            _ggJ1 = true;
            _player.transform.position = _cS._spawners[3].transform.position;
            rb.velocity = new Vector3(0, 0, 0);

            StartCoroutine("Respawn");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player2" && _inT2 == false)
        {
            Debug.Log("Out Player2 Trigger");
            _inT2 = false;
            _player.GetComponent<Player_1>().enabled = true;
            _player.tag = "Player1";
        }
    }

    IEnumerator Launched()
    {
        audioSrc.PlayOneShot(fourthAudioClip, 1F);
        yield return new WaitForSeconds(3);
        _childtaken.tag = "Child";
        StopCoroutine("Launched");
    }

    IEnumerator Respawn()
    {
        _player.tag = "P2Control";
        yield return new WaitForSeconds(3);
        _player.tag = "Player1";
        _player.GetComponent<Player_1>().enabled = true;
        StopCoroutine("Respawn");
    }
}
