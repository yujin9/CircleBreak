using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using System;

public class BallObjPool : MonoBehaviour
{
    [HideInInspector] public List<BallController> _spawnBallList = new List<BallController>();

    public static BallObjPool instance;
    public int _capacity;

    [SerializeField] Material[] _ballColor;
    [SerializeField] private GameObject _ballPref;
    [SerializeField] private bool _autoQueueGrow;
    private Queue<BallController> _ballQue = new Queue<BallController>();
    private Vector3 _defaultSpawnPos;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        _defaultSpawnPos = transform.position;
    }

    private void Start()
    {
        for (var i = 0; i < _capacity; i++)
        {
            AddBallToQueue();
        }
        GameManager.instance.isSpdUp.TakeUntilDestroy(this).Subscribe(SetActiveTrail);
    }

    private void AddBallToQueue()
    {
        var ball = Instantiate(_ballPref, _defaultSpawnPos, Quaternion.identity, transform).GetComponent<BallController>();

        ball.gameObject.SetActive(false);
        ball.transform.eulerAngles = new Vector2(90f, 0);
        _ballQue.Enqueue(ball);
    }

    public BallController Spawn(int grade)
    {
        //풀에 남은 큐브가 없으면 큐브를 생성한다
        if (_ballQue.Count == 0)
        {
            if (_autoQueueGrow)
            {
                _capacity++;
                AddBallToQueue();
            }
            else
            {
                return null;
            }
        }

        var ball = _ballQue.Dequeue();
        SetBall(ball, grade);
        //화면에 돌아다니고 있는 공 리스
        _spawnBallList.Add(ball);
        GameManager.instance._curBallCount.Value++;
        var length = _spawnBallList.Count;
        for (var i = 0; i < length; i++)
        {
            _spawnBallList[i]._index = i;
        }

        //공이 생성된 후에 머지할 갯수가 되는지 체크
        GameManager.instance._canMerge.Value = CanMergeCheck();
        return ball;
    }

    public void DestroyBall(BallController ball)
    {
        //큐브가 파괴되는 것이 아닌 풀로 돌려보냄
        ball.transform.position = Vector2.zero;
        ball._grade = 0;
        ball._index = 0;
        ball.gameObject.SetActive(false);
        _ballQue.Enqueue(ball);

    }

    public bool MergeBalls()
    {
        var minGradeList = _spawnBallList.GroupBy(x => x._grade).OrderBy(x => x.Key).ToList();

        for (var i = 0; i < minGradeList.Count; i++)
        {
            if (minGradeList[i].Count() >= 3)
            {
                var mergeList = minGradeList[i].OrderBy(x => x._index).ToList();
                var grade = mergeList[0]._grade + 1;

                for (var j = 0; j < 3; j++)
                {
                    DestroyBall(mergeList[j]);
                    _spawnBallList.Remove(mergeList[j]);
                    GameManager.instance._curBallCount.Value--;
                }

                var ball = Spawn(grade);
                return true;
            }
            else
            {
                Debug.Log("cannot merge");
                continue;
            }
        }
        return false;
    }

    private bool CanMergeCheck()
    {
        var minGradeList = _spawnBallList.GroupBy(x => x._grade).OrderBy(x => x.Key).ToList();

        for (var i = 0; i < minGradeList.Count; i++)
        {
            if (minGradeList[i].Count() >= 3)
            {
                return true;
            }
            else
            {
                Debug.Log("cannot merge");
                continue;
            }
        }
        return false;
    }

    private void SetActiveTrail(bool isOn)
    {
        for (var i = 0; i < _spawnBallList.Count; i++)
        {
            var ball = _spawnBallList[i];
            ball._trail.enabled = isOn;
        }
    }

    private void SetBall(BallController ball, int grade)
    {
        ball.transform.position = transform.position;
        ball.gameObject.SetActive(true);
        ball.StartBall();
        ball._grade = grade;
        ball._ballMesh.material = _ballColor[grade - 1];
        var trailColor = _ballColor[grade - 1].color;
        
        ball._trail.startColor = new Color(trailColor.r, trailColor.g, trailColor.b, 0.5f);
        ball._trail.endColor = new Color(trailColor.r, trailColor.g, trailColor.b, 0f);
        ball._trail.enabled = GameManager.instance.isSpdUp.Value;
    }
}



