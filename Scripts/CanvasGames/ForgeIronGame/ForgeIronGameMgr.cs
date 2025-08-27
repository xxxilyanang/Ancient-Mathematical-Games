using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public enum ScoreBarType
{
    RED,
    BLUE
}

public class ForgeIronGameMgr : SingletonMono<ForgeIronGameMgr>
{
    public GameObject verticalBar;
    public GameObject horizontalBar;
    private List<Range> ranges = new List<Range>();

    private float barLength;

    //判断是否结束游戏：
    [HideInInspector]
    public bool isEnd;
    //当前游戏的分数
    [HideInInspector]
    public int totalScore = 0;

    void Start()
    {
        barLength = 1200.0f;
        GenerateNewRanges();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckScore();
            print(totalScore);
            if (totalScore == 50)//如果分数达成
            {
                isEnd = true;
                return;
            }
            GenerateNewRanges();
        }
    }

    void GenerateNewRanges()
    {
        ranges.Clear();
        float start = 0;
        float length = 0;
        int score = 0;

        // 第一个范围
        start = Random.Range(barLength / 6, barLength / 3);
        length = Random.Range(barLength / 18, barLength / 6);
        score = 10;
        ranges.Add(new Range(start, length, score, ScoreBarType.BLUE));
        start += length;

        // 第二个范围
        if (start < barLength)
        {
            start = Random.Range(barLength / 2, barLength / 2 + barLength / 18);
            length = Random.Range(barLength / 180, barLength / 18);
            score = 20;
            ranges.Add(new Range(start, length, score, ScoreBarType.RED));
            start += length;
        }

        // 第三个范围
        if (start < barLength)
        {
            start = Random.Range(barLength / 2 + barLength / 9, barLength / 1);
            length = Random.Range(barLength / 18, barLength - start);
            score = 10;
            ranges.Add(new Range(start, length, score, ScoreBarType.BLUE));
        }

        UpdateBarDisplay();
        /*        ScoreBarType nowType;
                for (int i = 0; i < 3; i++)
                {
                    float start;
                    float length;
                    int score;
                    if (i == 0)
                        nowType = ScoreBarType.RED;
                    else
                        nowType = ScoreBarType.BLUE;
                    switch (nowType)
                    {
                        case ScoreBarType.RED:
                            start = Random.Range(0, barLength);
                            length = Random.Range(0.1f, barLength - start);
                            if (length > 1) length = 1;
                            score = 20;
                            ranges.Add(new Range(start, length, score, nowType));
                            break;
                        case ScoreBarType.BLUE:
                            start = Random.Range(0, barLength);
                            length = Random.Range(1.5f, barLength - start);
                            score = 10;
                            ranges.Add(new Range(start, length, score, nowType));
                            break;
                    }
                }*/

    }

    void CheckScore()
    {
        float barPosition = verticalBar.transform.position.x + barLength / 2;
        bool isGet = false;
        foreach (var range in ranges)
        {
            if (barPosition > range.start && barPosition < range.start + range.length)
            {
                Debug.Log("获得分数: " + range.score);
                totalScore += range.score;
                ForgeIronGameUI.GetInstance().UpdateScore("得分: " + totalScore + " / 50");
                isGet = true;
                break;
            }
        }
        if (!isGet) { Debug.Log("未获得分数"); }
    }

    void UpdateBarDisplay()
    {
        GameObject redBarPrefab = Resources.Load<GameObject>("Games/RedBar");
        GameObject blueBarPrefab = Resources.Load<GameObject>("Games/BlueBar");

        // 清除旧的范围显示
        foreach (Transform child in horizontalBar.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var range in ranges)
        {
            GameObject prefab = range.scoreBarType == ScoreBarType.RED ? redBarPrefab : blueBarPrefab;
            GameObject barInstance = Instantiate(prefab, horizontalBar.transform);
            // 设置条的位置和大小
            float start = (range.start - barLength / 2) / barLength;
            float length = (range.length) / barLength;

            barInstance.transform.localScale = new Vector2(length, 1);
            barInstance.transform.localPosition = new Vector2(start, 0);

        }
    }
}

public class Range
{
    public ScoreBarType scoreBarType;
    public float start;
    public float length;
    public int score;

    public Range(float start, float length, int score, ScoreBarType scoreBarType)
    {
        this.start = start;
        this.length = length;
        this.score = score;
        this.scoreBarType = scoreBarType;
    }
}
