using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioSphere : MonoBehaviour
{
    SphereCollider col;
    Audio audio;
    public float forceBattle;
    public float forceChill;
    public GameObject player;
    private void Start()
    {
        col = GetComponent<SphereCollider>();
        audio = FindAnyObjectByType<Audio>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Collisions(other);
    }
    private void OnTriggerStay(Collider other)
    {
        Collisions(other);
    }
    void Collisions(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            float r = col.radius;
            float d = Vector3.Distance(player.transform.position, other.gameObject.transform.position);
            
            float total = Mathf.Clamp01(1 - (d / r));
            float t = total;
            audio.songLayers[0].volume = t;
            audio.songLayers[1].volume = total;
            if(t >= forceBattle)
            {
                audio.songLayers[0].volume = 1;
                audio.songLayers[1].volume = 0;
            }
            if (t <= forceChill)
            {
                audio.songLayers[0].volume = 0;
                audio.songLayers[1].volume = 1;
            }
            Debug.Log("total"+ t);
        }
    }
}
