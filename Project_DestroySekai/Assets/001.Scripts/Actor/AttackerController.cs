using UnityEngine;

public class AttackerController : Actor
{
    public int nowPathIndex;
    public bool isEndMove = false;
    public Vector3 nowPathPos;
    public Vector3 moveDirection;

    public override void Init(ActorData _data)
    {
        data = _data;
        nowPathIndex = 0;
        isEndMove = false;
        FindPath();
    }

    public void Awake()
    {

    }

    public void FindPath()
    {
        nowPathPos = GameManager.Instance.GetPathPos(nowPathIndex).position;
        SetMoveDirection();
    }

    public void SetMoveDirection()
    {
        moveDirection = nowPathPos - gameObject.transform.position;
    }

    public void FixedUpdate()
    {
        Move();
    }

    public void CheckEndMove()
    {
        if(Vector3.Distance(transform.position, nowPathPos) <= 0.1f)
            EndMove();
    }

    public void EndMove()
    {
        isEndMove = true;
        if (nowPathIndex == GameManager.Instance.GetEndPathIndex())
        { 
            //TODO :: 종료 기능 넣기
            Managers.Resource.Destroy(gameObject);
            return;
        }
        nowPathIndex++;
        FindPath();
        isEndMove = false;
    }

    public void Move()
    {
        if (isEndMove) return;
        transform.Translate(moveDirection.normalized * 0.1f);
        CheckEndMove();
    }
}
