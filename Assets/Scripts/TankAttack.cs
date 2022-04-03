using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    //����λ��
    private Transform FirePosition;
    //�ӵ�Ԥ����
    public GameObject shellPrefab;
    //�����
    public KeyCode fireKey;
    public float shellSpeed = 10;

    public AudioClip shotAudio;

    //��ʱ��
    private float lastTime;   
    private float curTime;


    void Start()
    {
        FirePosition = transform.Find("FirePosition");
    }

    
    void Update()
    {
        //������°���
        if(Input.GetKeyDown(fireKey))
        {
            AudioSource.PlayClipAtPoint(shotAudio, transform.position);
            GameObject go = GameObject.Instantiate(shellPrefab, FirePosition.position, FirePosition.rotation) as GameObject;
            go.GetComponent<Rigidbody>().velocity = go.transform.forward * shellSpeed;
        }
    }

    //�������
    public void OnTriggerEnter(Collider collider)
    {
        curTime = Time.time;
        if(curTime - lastTime < 1)
             return ;
        lastTime = Time.time; 

        float[] message = new float[2];  
        message[0] = 2;  
        message[1] = 5;  

        if(collider.tag == "tank" )
           collider.SendMessage("TakeDamage", message, SendMessageOptions.DontRequireReceiver);
        else if(collider.tag == "building")
            this.GetComponent<TankHealth>().TakeDamage(message);
    }
}
