using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2 : MonoBehaviour
{
    public GameObject _player2;
    public GameObject _child;
    public GameObject _childtaken;
    public Transform _taken2;

    public Player_1 _p1S;
    public Goals _goals;

    public float range;
    public float _speed;
    public float _launch;

    public bool _istaken2;
    public bool _islaunch2;
    public bool _isSmash;

    public Rigidbody rb;
    public Rigidbody _rb2;

    AudioSource audioSrc;
    public AudioClip firstAudioClip;
    public AudioClip secondAudioClip;
    public AudioClip thirdAudioClip;
    public AudioClip fourthAudioClip;

    public Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Moves();
        Actions();
    }

    void Moves()
    {
        float h2 = Input.GetAxis("P2Left_Stick_Horizontal");
        float v2 = Input.GetAxis("P2Left_Stick_Vertical");

        if (v2 >= 0.1)
        {
            rb.velocity = new Vector3(rb.velocity.x, 5, 0);
            _taken2.position = _player2.transform.position + new Vector3(0, 1, 0);
            _anim.SetBool("_walkF", false);
            _anim.SetBool("_walkB", true);
            if (!audioSrc.isPlaying) audioSrc.PlayOneShot(firstAudioClip, 1F);
        }
        else rb.velocity = new Vector3(0, 0, 0);

        if (v2 <= -0.1)
        {
            rb.velocity = new Vector3(rb.velocity.x, -5, 0);
            _taken2.position = _player2.transform.position + new Vector3(0, -1, 0);
            _anim.SetBool("_walkF", true);
            if (!audioSrc.isPlaying) audioSrc.PlayOneShot(firstAudioClip, 1F);
        }
        if (h2 >= 0.1)
        {
            rb.velocity = new Vector3(5, rb.velocity.y, 0);
            _taken2.position = _player2.transform.position + new Vector3(1, 0, 0);
            _anim.SetBool("_walkS", true);
            _anim.SetBool("_walkF", false);
            if (!audioSrc.isPlaying) audioSrc.PlayOneShot(firstAudioClip, 1F);
        }
        if (h2 <= -0.1)
        {
            rb.velocity = new Vector3(-5, rb.velocity.y, 0);
            _taken2.position = _player2.transform.position + new Vector3(-1, 0, 0);
            _anim.SetBool("_walkS2", true);
            _anim.SetBool("_walkF", false);
            if (!audioSrc.isPlaying) audioSrc.PlayOneShot(firstAudioClip, 1F);
        }

        if (rb.velocity == new Vector3(0, 0, 0))
        {
            audioSrc.Stop();
            _anim.SetBool("_walkB", false);
            _anim.SetBool("_walkS", false);
            _anim.SetBool("_walkS2", false);
            //_anim.SetBool("_walkF", false);
        }
    }

    void Actions()
    {
        if (_istaken2 == true)
        {
            _childtaken.transform.position = _taken2.transform.position;
            _rb2 = _childtaken.GetComponent<Rigidbody>();
            _islaunch2 = false;
        }

        if (_istaken2 == true && Input.GetButtonDown("P2_X"))
        {
            float h = Input.GetAxis("P1Left_Stick_Horizontal");
            float v = Input.GetAxis("P1Left_Stick_Vertical");

            Debug.Log("Launch Children");
            _istaken2 = false;
            _islaunch2 = true;
            _childtaken.tag = "P2Launch";

            if (_islaunch2 == true && _childtaken.name == "Player_1")
            {
                StartCoroutine("LaunchedP1");
            }
            else if (_islaunch2 == true && _childtaken.name == "Child")
            {
                StartCoroutine("Launched2");
            }

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
        _childtaken = other.gameObject;

        if (other.tag == "Child" && (Input.GetButtonDown("P2_A")))
        {
            Debug.Log("Child Detected");

                Debug.Log("Child or P1 Taken");
                _istaken2 = true;
                _childtaken.tag = "P2Control";
                audioSrc.PlayOneShot(secondAudioClip, 1F);
        }

        if (other.tag == "Player1" && (Input.GetButtonDown("P2_A")))
        {
            Debug.Log("P1 Detected");
            _istaken2 = true;
            _childtaken.tag = "P2Control";

            _p1S.enabled = false;
            audioSrc.PlayOneShot(secondAudioClip, 1F);
        }

        if (other.tag == "Player1" && (Input.GetButtonDown("P2_Y")))
        {
            Debug.Log("P1 Smashed");
            _p1S._istaken1 = false;
            _p1S._childtaken.tag = "Child";
            audioSrc.PlayOneShot(thirdAudioClip, 1F);
        }
    }

    IEnumerator Launched2()
    {
        audioSrc.PlayOneShot(fourthAudioClip, 1F);
        yield return new WaitForSeconds(3);
        _childtaken.tag = "Child";
        StopCoroutine("Launched");
    }

    IEnumerator LaunchedP1()
    {
        audioSrc.PlayOneShot(fourthAudioClip, 1F);
        _child.tag = "Child";
        yield return new WaitForSeconds(2);
        _p1S.enabled = true;
        _p1S._istaken1 = false;
        _childtaken.tag = "Player1";
        StopCoroutine("Launched");
    }
}
