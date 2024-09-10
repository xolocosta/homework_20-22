public class PlayerMovement
{
    private UnityEngine.Rigidbody2D _playerRB;
    public PlayerMovement(UnityEngine.Rigidbody2D playerRB)
    {
        _playerRB = playerRB;
    }
    ~PlayerMovement()
    {

    }
    public void MoveByDirection(UnityEngine.Vector2 direction)
    {
        _playerRB.velocity = direction * 3.0f;
    }
}
