using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
    stage1: 1 - teki1; 1 - teki2
    stage2: 2 - teki1; 1 - teki2
    stage3: 2 - teki1; 2 - teki2: speed up
    stage4: 3 - teki1; 2 - teki2: speed up
    stage5: 3 - teki1: speed up; 3 - teki2: speed up up
 */


public class StageControl : MonoBehaviour
{
    //�궨��
    const int ON = 1;
    const int OFF = 0;
    const int INF = 1000000;
    
    //���ڵĹؿ���
    private int curStage;
    //���ŵĵ�������
    public int liveTeki;
    //�ؿ���ʱ��
    private float stageLastTime;
    private float stageCurTime;
    public float stageGapTime;

    //����
    public GameObject tankTeki1;
    public GameObject tankTeki2;

    //ʵ������ĵ����б�
    private List<GameObject> tankTeki_1_List = new List<GameObject>();
    private List<GameObject> tankTeki_2_List = new List<GameObject>();

    //���
    private GameObject playerObject;

    //����
    private int freezeState;
    private float freezeStartTime;
    public float freezeLastingTime;
    public float freezeDistance;
    //������ס�ĵ����±��б�
    List<int> tankTeki_1_FreezeIndexList = new List<int>();
    List<int> tankTeki_2_FreezeIndexList = new List<int>();

    //�޵�
    private int mutekiState;
    private float mutekiStartTime;
    //�޵�ǰ��Ѫ��
    private float mutekiBeforeHP;
    public float mutekiLastingTime;

    public Material[] goldMaterial = new Material[2];
    public Material[] redMaterial = new Material[2];

    //����һ����ͼ�Ϻ����λ��
    private Vector3 randomPositionVec3()
    {
        return new Vector3(Random.Range(-25, 25), 0, Random.Range(-25, 25));
    }

    //�Ե���̹�˽��м�ǿ
    private GameObject tankPowerUp(GameObject tank, float upLevel)
    {
        tank.GetComponent<NavMeshAgent>().speed += 5 * upLevel;
        tank.GetComponent<NavMeshAgent>().acceleration += 5 * upLevel;
        tank.GetComponent<TankAttackTeki2>().tankAttackGapTime -= 0.2f * upLevel;
        return tank;
    }

    void Start()
    {
        curStage = liveTeki = 0;
        freezeState = mutekiState = OFF;
        stageCurTime = Time.time;
        stageLastTime = stageCurTime - stageGapTime + 1;
        playerObject = GameObject.Find("Tank1");
        mutekiBeforeHP = playerObject.GetComponent<TankHealth>().HP;
    }

    void Update()
    {
        //����Ѫ����Ϊ�޵�״̬��׼��
        mutekiBeforeHP = System.Math.Min(mutekiBeforeHP, playerObject.GetComponent<TankHealth>().HP);
        //�ж��Ƿ�Ӧ�ý������״̬
        if (freezeState == ON && Time.time - freezeStartTime > freezeLastingTime)
            unfreeze();
        //�ж��Ƿ�Ӧ�ý���޵�״̬
        if (mutekiState == ON && Time.time - mutekiStartTime > mutekiLastingTime)
            unmuteki();
        //�ж��Ƿ�Ӧ�ý�����һ�ؿ�
        stageCurTime = Time.time;
        if (stageCurTime - stageLastTime < stageGapTime && liveTeki != 0)
            return;
        stageLastTime = Time.time;
        curStage ++;

        //���ݲ�ͬ�׶���ӵ���
        if (curStage == 1)
        {
            liveTeki += 2;
            for(int i = 0; i < 1; i ++)
                tankTeki_1_List.Add(GameObject.Instantiate(tankTeki1, randomPositionVec3(), playerObject.transform.rotation) as GameObject);

            for (int i = 0; i < 1; i++)
                tankTeki_2_List.Add(GameObject.Instantiate(tankTeki2, randomPositionVec3(), playerObject.transform.rotation) as GameObject);
        }
        else if (curStage == 2)
        {
            liveTeki += 3;
            for (int i = 0; i < 2; i++)
                tankTeki_1_List.Add(GameObject.Instantiate(tankTeki1, randomPositionVec3(), playerObject.transform.rotation) as GameObject);

            for (int i = 0; i < 1; i++)
                tankTeki_2_List.Add(GameObject.Instantiate(tankTeki2, randomPositionVec3(), playerObject.transform.rotation) as GameObject);
        }
        else if (curStage == 3)
        {
            liveTeki += 4;
            for (int i = 0; i < 2; i++)
                tankTeki_1_List.Add(GameObject.Instantiate(tankTeki1, randomPositionVec3(), playerObject.transform.rotation) as GameObject);

            for (int i = 0; i < 2; i++)
                tankTeki_2_List.Add(GameObject.Instantiate(tankTeki2, randomPositionVec3(), playerObject.transform.rotation) as GameObject);
        }
        else if (curStage == 4)
        {
            liveTeki += 5;

            for (int i = 0; i < 3; i++)
                tankTeki_1_List.Add(GameObject.Instantiate(tankTeki1, randomPositionVec3(), playerObject.transform.rotation) as GameObject);

            for (int i = 0; i < 2; i++)
                tankTeki_2_List.Add(tankPowerUp(GameObject.Instantiate(tankTeki2, randomPositionVec3(), playerObject.transform.rotation), 1) as GameObject);

        }
        else if (curStage == 5)
        {
            liveTeki += 6;

            for (int i = 0; i < 3; i++)
                tankTeki_1_List.Add(GameObject.Instantiate(tankTeki1, randomPositionVec3(), playerObject.transform.rotation) as GameObject);

            for (int i = 0; i < 3; i++)
                tankTeki_2_List.Add(tankPowerUp(GameObject.Instantiate(tankTeki2, randomPositionVec3(), playerObject.transform.rotation), 2) as GameObject);
        }
        else if(curStage >= 6 && liveTeki!=0)
        {
            Debug.Log("Game Over!");
        }
    }

    void freezeFunc()
    {
        //����Ѿ��б����ĵ��ˣ�������ǵı���״̬��������һ�ֱ���
        if (freezeState == ON)
            unfreeze();
        freezeStartTime = Time.time;
        freezeState = ON;
        //��������һ�����
        for (int i = 0; i < tankTeki_1_List.Count; i++)
        {
            if (tankTeki_1_List[i] == null)
                continue;
            //�ҵ����Ͼ��뷶Χ��
            if (Vector3.Distance(tankTeki_1_List[i].transform.position, playerObject.transform.position) <= freezeDistance)
            {
                //���±�
                tankTeki_1_FreezeIndexList.Add(i);
                //���Զ�Ѱ·���ƶ�
                tankTeki_1_List[i].GetComponent<NavMeshAgent>().enabled = false;
                tankTeki_1_List[i].GetComponent<TankMovementTeki1>().enabled = false;
            }
        }
        
        //�������ж������
        for (int i = 0; i < tankTeki_2_List.Count; i++)
        {
            if (tankTeki_2_List[i] == null)
                continue;
            //�ҵ����Ͼ��뷶Χ��
            if (Vector3.Distance(tankTeki_2_List[i].transform.position, playerObject.transform.position) <= freezeDistance)
            {
                //���±�
                tankTeki_2_FreezeIndexList.Add(i);
                //�ر��Զ�����Լ��ƶ����Զ�Ѱ·
                tankTeki_2_List[i].GetComponent<NavMeshAgent>().enabled = false;
                tankTeki_2_List[i].GetComponent<TankAttackTeki2>().enabled = false;
                tankTeki_2_List[i].GetComponent<TankMovementTeki2>().enabled = false;
            }
        }
    }

    void mutekiFunc()
    {
        mutekiStartTime = Time.time;
        mutekiState = ON;

        //����һ��Ͻ���
        MeshRenderer[] allChildren = playerObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer child in allChildren)
            child.materials = goldMaterial;
        
        //Ѫ����ֵΪ�����
        playerObject.GetComponent<TankHealth>().HP = INF;
    }

    void unmuteki()
    {
        mutekiState = OFF;

        //�������
        MeshRenderer[] allChildren = playerObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer child in allChildren)
            child.materials = redMaterial;

        //�ָ�Ѫ��
        playerObject.GetComponent<TankHealth>().HP = mutekiBeforeHP;
    }

    void unfreeze()
    {
        freezeState = OFF;

        //����һ����ˣ��ָ��Զ�Ѱ·���ƶ�
        for (int i = 0; i < tankTeki_1_FreezeIndexList.Count; i++)
        {
            if (tankTeki_1_List[tankTeki_1_FreezeIndexList[i]] != null)
            {
                tankTeki_1_List[tankTeki_1_FreezeIndexList[i]].GetComponent<NavMeshAgent>().enabled = true;
                tankTeki_1_List[tankTeki_1_FreezeIndexList[i]].GetComponent<TankMovementTeki1>().enabled = true;
            }
        }
        //���ڶ�����ˣ��ָ��Զ�Ѱ·���ƶ��Լ����
        for (int i = 0; i < tankTeki_2_FreezeIndexList.Count; i++)
        {
            if (tankTeki_2_List[tankTeki_2_FreezeIndexList[i]] == null)
                continue;
            tankTeki_2_List[tankTeki_2_FreezeIndexList[i]].GetComponent<NavMeshAgent>().enabled = true;
            tankTeki_2_List[tankTeki_2_FreezeIndexList[i]].GetComponent<TankAttackTeki2>().enabled = true;
            tankTeki_2_List[tankTeki_2_FreezeIndexList[i]].GetComponent<TankMovementTeki2>().enabled = true;
        }
        //��ձ����±��б�
        tankTeki_1_FreezeIndexList.Clear();
        tankTeki_2_FreezeIndexList.Clear(); 
    }
}
