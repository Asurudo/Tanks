using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
        
    public GameObject shellExplositionPrefab;

    public AudioClip shellExplositionAudio;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    //�������
    public void OnTriggerEnter(Collider collider)
    {
        AudioSource.PlayClipAtPoint(shellExplositionAudio, transform.position);
        GameObject.Instantiate(shellExplositionPrefab, transform.position, transform.rotation);
        GameObject.Destroy(this.gameObject);

        if(collider.tag == "tank")
        {
            float[] message = new float[2];  
            message[0] = 10;  
            message[1] = 20;  
            collider.SendMessage("TakeDamage", message, SendMessageOptions.DontRequireReceiver);
        }
    }
}
