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

    private AudioSource audio;

    //��ȡ�������
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        audio = this.GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        //��ֱ WS�� v = 1|-1
        float v = Input.GetAxis("Verticalplayer" + number);
        rigidbody.velocity = transform.forward * v * speed;

        //ˮƽ AD�� h = 1|-1
        float h = Input.GetAxis("Horizontalplayer" + number);
        //ˮƽ��ת�ٶ�               ����y��
        rigidbody.angularVelocity = transform.up * h * angularSpeeed;

        if (Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1)
            audio.clip = drivingAudio;
        else
            audio.clip = idleAudio;
        if (audio.isPlaying == false)
            audio.Play();

    }
}
