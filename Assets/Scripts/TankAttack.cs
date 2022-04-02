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
}
