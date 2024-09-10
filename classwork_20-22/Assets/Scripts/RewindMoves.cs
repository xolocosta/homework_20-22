using System.Collections.Generic;
using System.Diagnostics;

public class RewindMoves
{
    public Stack<Moves> Moves { get => _moves; } 
    private Stack<Moves> _moves;
    public RewindMoves()
    {
        _moves = new Stack<Moves>();
    }
    ~RewindMoves() { }


    public void CreateMove(UnityEngine.Vector2 originPosition)
    {
        _moves.Push(new Moves(originPosition));
    }
    public void AddDirectionToMove(UnityEngine.Vector2 direction)
    {
        _moves.Peek().AddDirection(direction);
        UnityEngine.Debug.Log($"Moves Count: {_moves.Count}, " +
            $"Last Move Times Count: {_moves.Peek().Times.Count}, " +
            $"\nLast Move Directions Count: {_moves.Peek().MoveDirections.Count}," +
            $"Last Move RewindDirections Count: {_moves.Peek().RewindDirections.Count}");
    }

    public bool CanRewindPlay(out float time, out UnityEngine.Vector2 direction)
    {
        time = default;
        direction = default;

        if ( _moves.Count > 0 )
            return RecursionMethod(out time, out direction);
        else return false;
    }
    private bool RecursionMethod(out float time, out UnityEngine.Vector2 direction)
    {
        if (_moves.Peek().CanRewind() == true)
        {
            time = _moves.Peek().ReturnTime();
            direction = _moves.Peek().ReturnRewindDir();
            return true;
        }
        else
        {
            if (_moves.Count >= 1)
            {
                time = default;
                direction = default;
                return false;
            }
            else
            {
                _moves.Pop();
            }

            return RecursionMethod(out time, out direction);
        }
    }

    public void Reset()
    {
        _moves.Clear();
    }
}
