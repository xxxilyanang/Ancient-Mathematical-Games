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
    bool _gameover;//设置游戏状态默认为true



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _originCard = Resources.Load<GameObject>("Card");//加载出卡片
        _cardList = new List<Card>();//创建一个装卡片的列表
        _compareCardList = new List<Card>();//比较卡片列表
        _rotateCardList = new List<Card>();//旋转卡片列表

        string config = Resources.Load<TextAsset>("config").text;//读取配置文件中的卡片名字
        var fruits = JsonUtility.FromJson<FruitConfig>(config);//将卡片名字转换为FruitConfig这个类型的fruits这个对象，就是说txt中的名字都在fruitslist列表中



        List<string> randomList = new List<string>();//随机列表
        List<string> originList = new List<string>();//原列表
        originList.AddRange(fruits.FruitList);//将fruits.FruitList列表中的元素添加到originList列表中
        int count = originList.Count;//此时该列表中已经存入几个卡片的名字
        for (int i = 0; i < count; i++)
        {
            int random = Random.Range(0, originList.Count);//生成随机数字
            randomList.Add(originList[random]);//随即列表中添加已经随机过的名称
            originList.RemoveAt(random);//同时将原列表中数据进行移除
        }

        originList.AddRange(fruits.FruitList);//再次将fruits.FruitList列表中的元素添加到originList列表中。
        for (int i = 0; i < count; i++)
        {
            int random = Random.Range(0, originList.Count);//生成随机数字
            randomList.Add(originList[random]);//随即列表中添加已经随机过的名称
            originList.RemoveAt(random);//同时将原列表中数据进行移除
        }


        Vector3 offset = Vector3.down * (row - 1) / 2 * (_cardH + _spaceY) + Vector3.left * (col - 1) / 2 * (_cardH + _spaceX);
        for (int i = 0; i < row; i++)//创建四行四列的卡片
        {
            for (int j = 0; j < col; j++)
            {
                GameObject cloneCard = Instantiate(_originCard);//创建卡片模型
                var card = cloneCard.GetComponent<Card>();//获取Card的组件后面将使用card来对卡片属性进行设置和调整
                card.Initial(randomList[i * col + j]);//进行卡片的初始化
                _cardList.Add(card);//该卡片列表中加入当前卡片
                cloneCard.transform.position = transform.position + offset + Vector3.up * i * (_cardH + _spaceY) + Vector3.right * j * (_cardH + _spaceX);//设置卡片的位置和方向
                cloneCard.transform.SetParent(transform);

            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (_gameover)//如果游戏结束则return
        {
            Debug.Log("游戏结束+触发游戏结束ui");
            //return;
        }
        MouseDetect();//图片高亮处理
        MouseInput();//鼠标点击
    }

    private void MouseInput()//鼠标点击
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentTarget != null && _rotateCardList.Count < 2 && !_rotateCardList.Contains(_currentTarget))// 
            //确保选中的有目标同时旋转卡片数目不超过两个并且当前目标不在旋转卡片列表中
            //rotateCardList代表已经旋转的卡片列表
            {
                Debug.Log("进行旋转");
                step++;//步数加1
                //UIManager.Instance.ShowStep(step);//用该方法进行数据更新
                _currentTarget.Rotate();//选中的目标进行旋转

            }
        }
    }
    public void AddCard(Card card)//添加已经旋转卡片列表--------
    {
        _rotateCardList.Add(card);
    }

    public void CompareCards(Card card)//比较卡片---------------
    {
        _compareCardList.Add(card);//先将要比较的卡片放入比较列表
        if (_compareCardList.Count == 2)
        {
            if (_compareCardList[0].FruitName == _compareCardList[1].FruitName)
            {

                _compareCardList[0].SwitchState(StateEnum.已配对);//改变当前卡牌的状态
                _compareCardList[1].SwitchState(StateEnum.已配对);
                Destroy(_compareCardList[0].gameObject);
                Destroy(_compareCardList[1].gameObject);
                Clear();//及时清除比较和旋转列表
                if (IsVictory())
                {
                    Victory();
                }
            }
            else
            {
                _compareCardList[0].Rotate(false);//最终是否为反转状态
                _compareCardList[1].Rotate(false);
            }
        }

    }

    private void Victory()
    {
        _gameover = true;//游戏结束即游戏成功
    }

    public void Clear()//清洁列表
    {

        _compareCardList.Clear();//及时清除比较列表
        _rotateCardList.Clear();//及时清除旋转列表
    }

    private void MouseDetect()//卡片高亮处理
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);//从摄像机发出一条射线 (Ray) 到屏幕上的鼠标位置
        RaycastHit hitInfo;
        if (Physics.Raycast(mouseRay, out hitInfo))//mouseRay 是否与场景中的物体相交，如果相交则将相关信息存储到 hitInfo 变量中
        {
            Card card = hitInfo.transform.GetComponent<Card>();
            if (card != null)//在鼠标悬停在卡片上时，高亮显示当前鼠标指向的卡片，并将之前的目标卡片状态恢复成正常状态
            {//当鼠标没有悬停在任何卡片上时，将之前的目标卡片状态设为正常状态并将目标卡片置为空。
                if (_currentTarget != null && _currentTarget != card)
                {
                    _currentTarget.Normal();//白色
                }
                _currentTarget = card;
                _currentTarget.Highlight();//绿色
            }


        }
        else if (_currentTarget != null)
        {
            _currentTarget.Normal();
            _currentTarget = null;
        }
    }

    bool IsVictory()//判断是否胜利
    {

        bool isVictory = true;
        for (int i = 0; i < _cardList.Count; i++)
        {
            if (_cardList[i].CurrentState == StateEnum.未配对)//但凡有一个没有配对则未胜利
            {
                isVictory = false;
            }
        }
        return isVictory;
    }
}
