using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Children;
    public Transform[] SpawnPos;
    //public destroy Destroy_child;
    public bool TestCoroutine = false;

    public Goals _goals;
    public Player_1 _p1;
    public Player_2 _p2;

    AudioSource audioSrc;
    public AudioClip _eject;
    public AudioClip _crowd;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (_goals._gJ1 == true)
        {
            Debug.Log("Ball in Goal1");
            _goals._gJ1 = false;
            _p1._rb2.velocity = new Vector3(0, 0, 0);
            StartCoroutine(WaitToSpawn());
        }
        if (_goals._gJ2 == true)
        {
            _goals._gJ2 = false;
            _p2._rb2.velocity = new Vector3(0, 0, 0);
            StartCoroutine(WaitToSpawn());
        }
    }

    void SpawnBaby()
    {
        int spawnIndex = Random.Range(0, SpawnPos.Length);
        Children.transform.position = (SpawnPos[spawnIndex].position);
        Children.SetActive(true);
    }
   IEnumerator WaitToSpawn()
    {
        audioSrc.PlayOneShot(_eject, 1F);
        audioSrc.PlayOneShot(_crowd, 1F);
        Children.SetActive(false);
        yield return new WaitForSeconds(3);
        SpawnBaby();
        StopCoroutine("WaitToSpawn");
    }
}
