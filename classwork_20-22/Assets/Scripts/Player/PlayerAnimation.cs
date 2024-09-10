public class PlayerAnimation
{
    private const string _WALK_ANIMATION_NAME = "isWalk";
    private UnityEngine.Animator _animator;
    public PlayerAnimation(UnityEngine.Animator animator)
    {
        _animator = animator;
    }
    ~PlayerAnimation()
    {

    }
    public void SetAnimByDirection(UnityEngine.Vector2 velocity)
    {
        if (velocity == UnityEngine.Vector2.zero)
        {
            _animator.SetBool(_WALK_ANIMATION_NAME, false);
        }
        else if (velocity.x >= 0)
        {
            _animator.transform.localScale = new UnityEngine.Vector3(1.0f, 1.0f, 1.0f);
            _animator.SetBool(_WALK_ANIMATION_NAME, true);
        }
        else if (velocity.x <= 0)
        {
            _animator.transform.localScale = new UnityEngine.Vector3(-1.0f, 1.0f, 1.0f);
            _animator.SetBool(_WALK_ANIMATION_NAME, true);
        }
    }
}
