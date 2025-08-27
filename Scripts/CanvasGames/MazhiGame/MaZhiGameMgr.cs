using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaZhiGameMgr : MonoBehaviour
{
    GameObject _originCard;
    int row = 4;
    int col = 4;
    float _cardH = 0.1875f;
    float _spaceX = 0.046f;
    float _spaceY = 0.046f;
    public static MaZhiGameMgr Instance;
    List<Card> _cardList;
    Card _currentTarget;
    List<Card> _compareCardList;
    List<Card> _rotateCardList;
    int step;
    bool _gameover;//������Ϸ״̬Ĭ��Ϊtrue



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _originCard = Resources.Load<GameObject>("Card");//���س���Ƭ
        _cardList = new List<Card>();//����һ��װ��Ƭ���б�
        _compareCardList = new List<Card>();//�ȽϿ�Ƭ�б�
        _rotateCardList = new List<Card>();//��ת��Ƭ�б�

        string config = Resources.Load<TextAsset>("config").text;//��ȡ�����ļ��еĿ�Ƭ����
        var fruits = JsonUtility.FromJson<FruitConfig>(config);//����Ƭ����ת��ΪFruitConfig������͵�fruits������󣬾���˵txt�е����ֶ���fruitslist�б���



        List<string> randomList = new List<string>();//����б�
        List<string> originList = new List<string>();//ԭ�б�
        originList.AddRange(fruits.FruitList);//��fruits.FruitList�б��е�Ԫ����ӵ�originList�б���
        int count = originList.Count;//��ʱ���б����Ѿ����뼸����Ƭ������
        for (int i = 0; i < count; i++)
        {
            int random = Random.Range(0, originList.Count);//�����������
            randomList.Add(originList[random]);//�漴�б�������Ѿ������������
            originList.RemoveAt(random);//ͬʱ��ԭ�б������ݽ����Ƴ�
        }

        originList.AddRange(fruits.FruitList);//�ٴν�fruits.FruitList�б��е�Ԫ����ӵ�originList�б��С�
        for (int i = 0; i < count; i++)
        {
            int random = Random.Range(0, originList.Count);//�����������
            randomList.Add(originList[random]);//�漴�б�������Ѿ������������
            originList.RemoveAt(random);//ͬʱ��ԭ�б������ݽ����Ƴ�
        }


        Vector3 offset = Vector3.down * (row - 1) / 2 * (_cardH + _spaceY) + Vector3.left * (col - 1) / 2 * (_cardH + _spaceX);
        for (int i = 0; i < row; i++)//�����������еĿ�Ƭ
        {
            for (int j = 0; j < col; j++)
            {
                GameObject cloneCard = Instantiate(_originCard);//������Ƭģ��
                var card = cloneCard.GetComponent<Card>();//��ȡCard��������潫ʹ��card���Կ�Ƭ���Խ������ú͵���
                card.Initial(randomList[i * col + j]);//���п�Ƭ�ĳ�ʼ��
                _cardList.Add(card);//�ÿ�Ƭ�б��м��뵱ǰ��Ƭ
                cloneCard.transform.position = transform.position + offset + Vector3.up * i * (_cardH + _spaceY) + Vector3.right * j * (_cardH + _spaceX);//���ÿ�Ƭ��λ�úͷ���
                cloneCard.transform.SetParent(transform);

            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (_gameover)//�����Ϸ������return
        {
            Debug.Log("��Ϸ����+������Ϸ����ui");
            //return;
        }
        MouseDetect();//ͼƬ��������
        MouseInput();//�����
    }

    private void MouseInput()//�����
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentTarget != null && _rotateCardList.Count < 2 && !_rotateCardList.Contains(_currentTarget))// 
            //ȷ��ѡ�е���Ŀ��ͬʱ��ת��Ƭ��Ŀ�������������ҵ�ǰĿ�겻����ת��Ƭ�б���
            //rotateCardList�����Ѿ���ת�Ŀ�Ƭ�б�
            {
                Debug.Log("������ת");
                step++;//������1
                //UIManager.Instance.ShowStep(step);//�ø÷����������ݸ���
                _currentTarget.Rotate();//ѡ�е�Ŀ�������ת

            }
        }
    }
    public void AddCard(Card card)//����Ѿ���ת��Ƭ�б�--------
    {
        _rotateCardList.Add(card);
    }

    public void CompareCards(Card card)//�ȽϿ�Ƭ---------------
    {
        _compareCardList.Add(card);//�Ƚ�Ҫ�ȽϵĿ�Ƭ����Ƚ��б�
        if (_compareCardList.Count == 2)
        {
            if (_compareCardList[0].FruitName == _compareCardList[1].FruitName)
            {

                _compareCardList[0].SwitchState(StateEnum.�����);//�ı䵱ǰ���Ƶ�״̬
                _compareCardList[1].SwitchState(StateEnum.�����);
                Destroy(_compareCardList[0].gameObject);
                Destroy(_compareCardList[1].gameObject);
                Clear();//��ʱ����ȽϺ���ת�б�
                if (IsVictory())
                {
                    Victory();
                }
            }
            else
            {
                _compareCardList[0].Rotate(false);//�����Ƿ�Ϊ��ת״̬
                _compareCardList[1].Rotate(false);
            }
        }

    }

    private void Victory()
    {
        _gameover = true;//��Ϸ��������Ϸ�ɹ�
    }

    public void Clear()//����б�
    {

        _compareCardList.Clear();//��ʱ����Ƚ��б�
        _rotateCardList.Clear();//��ʱ�����ת�б�
    }

    private void MouseDetect()//��Ƭ��������
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);//�����������һ������ (Ray) ����Ļ�ϵ����λ��
        RaycastHit hitInfo;
        if (Physics.Raycast(mouseRay, out hitInfo))//mouseRay �Ƿ��볡���е������ཻ������ཻ�������Ϣ�洢�� hitInfo ������
        {
            Card card = hitInfo.transform.GetComponent<Card>();
            if (card != null)//�������ͣ�ڿ�Ƭ��ʱ��������ʾ��ǰ���ָ��Ŀ�Ƭ������֮ǰ��Ŀ�꿨Ƭ״̬�ָ�������״̬
            {//�����û����ͣ���κο�Ƭ��ʱ����֮ǰ��Ŀ�꿨Ƭ״̬��Ϊ����״̬����Ŀ�꿨Ƭ��Ϊ�ա�
                if (_currentTarget != null && _currentTarget != card)
                {
                    _currentTarget.Normal();//��ɫ
                }
                _currentTarget = card;
                _currentTarget.Highlight();//��ɫ
            }


        }
        else if (_currentTarget != null)
        {
            _currentTarget.Normal();
            _currentTarget = null;
        }
    }

    bool IsVictory()//�ж��Ƿ�ʤ��
    {

        bool isVictory = true;
        for (int i = 0; i < _cardList.Count; i++)
        {
            if (_cardList[i].CurrentState == StateEnum.δ���)//������һ��û�������δʤ��
            {
                isVictory = false;
            }
        }
        return isVictory;
    }
}
