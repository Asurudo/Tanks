using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    //�н��ٶ�һ������    
    public float speed;
    //��ת�ٶ�һ����ʮ��
    public float angularSpeeed;
    //̹�˱��
    public float number;

    public AudioClip idleAudio;
    public AudioClip drivingAudio;

    private AudioSource runningaudio;

    //��ȡ�������
    private Rigidbody objrigidbody;

    void Start()
    {
        objrigidbody = this.GetComponent<Rigidbody>();
        runningaudio = this.GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        //��ֱ WS�� v = 1|-1
        float v = Input.GetAxis("Verticalplayer" + number);
        objrigidbody.velocity = transform.forward * v * speed;

        //ˮƽ AD�� h = 1|-1
        float h = Input.GetAxis("Horizontalplayer" + number);
        //ˮƽ��ת�ٶ�               ����y��
        objrigidbody.angularVelocity = transform.up * h * angularSpeeed;

        if (Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1)
            runningaudio.clip = drivingAudio;
        else
            runningaudio.clip = idleAudio;
        if (runningaudio.isPlaying == false)
            runningaudio.Play();

    }
}
