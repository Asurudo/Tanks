using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject shellExplositionPrefab;
    public AudioClip shellExplositionAudio;

    //�����������һ�����������ܵ�ֵ��player��teki���ֱ������Һ͵���
    private string fromWhere;

    public void setFromWhere(string fw)
    {
        fromWhere = fw;
    }

    //װ�䷵��һ����Χ����
    float[] damageRange(float minDamage, float maxDamage)
    {
        float[] damageRange = new float[2];
        damageRange[0] = minDamage;
        damageRange[1] = maxDamage;
        return damageRange;
    }

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

        //���˲��ᱻ�˴˵��ڵ��˺���
        if(collider.tag == "player" || fromWhere == "player" && collider.tag == "tank")
        {
            collider.SendMessage("TakeDamage", damageRange(10, 20), SendMessageOptions.DontRequireReceiver);
        }
    }
}
