using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportJ1 : MonoBehaviour
{
    public GameObject J1;
    public GameObject TpLoc;

    AudioSource audioSrc;
    public AudioClip _door;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "Player1") && Input.GetButtonDown("P1_Y"))
        {
            audioSrc.PlayOneShot(_door, 1F);
            J1.transform.position = TpLoc.transform.position;
        }
    }


}
