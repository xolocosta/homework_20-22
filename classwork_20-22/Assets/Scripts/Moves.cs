using System.Collections.Generic;

public class Moves
{
    public Stack<float> Times { get => _times; }
    public UnityEngine.Vector2 OriginPos { get => _originPos; }
    public Stack<UnityEngine.Vector2> MoveDirections { get => _directions; }
    public Stack<UnityEngine.Vector2> RewindDirections { get => _rewindDir; }


    private Stack<float> _times = new Stack<float>();
    private UnityEngine.Vector2 _originPos;
    private Stack<UnityEngine.Vector2> _directions = new Stack<UnityEngine.Vector2>();
    private Stack<UnityEngine.Vector2> _rewindDir = new Stack<UnityEngine.Vector2>();
    public Moves(UnityEngine.Vector2 originPos)
    {
        _originPos = originPos;
    }
    ~Moves() { }

    public void AddDirection(UnityEngine.Vector2 direction)
    {
        _times.Push(UnityEngine.Time.time);
        _directions.Push(direction);
        _rewindDir.Push(_originPos - direction);
    }
    public float ReturnTime()
    {
        float moveTime = _times.Pop();
        moveTime -= _times.Peek();
        return moveTime;
    }
    public UnityEngine.Vector2 ReturnOriginPos()
        => _originPos;
    public UnityEngine.Vector2 ReturnMoveDirection()
        => _directions.Pop();
    public UnityEngine.Vector2 ReturnRewindDir()
        => _rewindDir.Pop();
    public bool CanRewind()
        => _times.Count > 1;
}
