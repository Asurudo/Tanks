using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankMovementTeki2 : MonoBehaviour
{
    //�н��ٶ�һ������    
    public float speed;
    //��ת�ٶ�һ����ʮ��
    public float angularSpeeed;

    public AudioClip idleAudio;
    public AudioClip drivingAudio;

    private AudioSource ad;

    //��ȡ�������
    private Rigidbody rigidbd;

    //��ȡ���
    private GameObject playerObject;

    private NavMeshAgent agent;

    //��ʱ��
    private float lastTime;
    private float curTime;

    void Start()
    {
        rigidbd = this.GetComponent<Rigidbody>();
        ad = this.GetComponent<AudioSource>();
        playerObject = GameObject.Find("Tank1");
        agent = GetComponent<NavMeshAgent>();
        curTime = Time.time;
        lastTime = curTime - 2;
    }

    private Vector3 GivemeTheFinalDest(Vector3 oriposition)
    {
        Vector3 rnt = new Vector3(0, 0, 0);
        float r = (float)System.Math.Sqrt(Random.Range(500.0f, 800.0f));
        rnt.x = oriposition.x + r * (float)System.Math.Cos(Random.Range(0, 2 * 3.1415f));
        rnt.z = oriposition.z + r * (float)System.Math.Sin(Random.Range(0, 2 * 3.1415f));
        return rnt;
    }

    void FixedUpdate()
    {
        if (playerObject == null || this == null)
            return;

        float distance = Vector3.Distance(this.transform.position, playerObject.transform.position);
        if (distance > 15.0f)
        {
            this.GetComponent<NavMeshAgent>().enabled = true;
            agent.destination = playerObject.transform.position;
        }
        else if (distance < 15.0f)
        {
            this.transform.forward = playerObject.transform.position - this.transform.position;
            this.GetComponent<NavMeshAgent>().enabled = false;
            this.SendMessage("Fire", SendMessageOptions.DontRequireReceiver);
        }

        ad.clip = drivingAudio;

        if (ad.isPlaying == false)
            ad.Play();
    }

    void Backnow()
    {

    }
}
