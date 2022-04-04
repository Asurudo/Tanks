using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankMovementTeki1 : MonoBehaviour
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

    void Start()
    {
        rigidbd = this.GetComponent<Rigidbody>();
        ad = this.GetComponent<AudioSource>();
        playerObject = GameObject.Find("Tank1");
        agent = GetComponent<NavMeshAgent>();
    }

    private Vector3 GivemeTheFinalDest(Vector3 oriposition)
    {
        Vector3 rnt = new Vector3(0, 0, 0);
        float r = (float)System.Math.Sqrt(Random.Range(4.0f, 36.0f));
        rnt.x = oriposition.x + r * (float)System.Math.Cos(Random.Range(0, 2 * 3.1415f));
        rnt.z = oriposition.z + r * (float)System.Math.Sin(Random.Range(0, 2 * 3.1415f));
        return rnt;
    }

    void FixedUpdate()
    {
        if (playerObject == null)
            return;
        agent.destination = GivemeTheFinalDest(playerObject.transform.position);

        ad.clip = drivingAudio;

        if (ad.isPlaying == false)
            ad.Play();
    }

    void Backnow()
    {
        Vector3 rnt = new Vector3(0, 0, 0);
        agent.destination = rnt;
    }
}
