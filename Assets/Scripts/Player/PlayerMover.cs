using UnityEngine;

[System.Serializable]
public enum Line { Left, Middle, Right }

[RequireComponent(typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _targetPosition;
    private Line _moveLine = Line.Middle;
    private Animator _animator;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    private void SetNextPosition(float offsetX)
    {
        _targetPosition = new Vector2(offsetX, transform.position.y);
    }

    private void SetStartPosition()
    {
        _targetPosition.x = _startPosition.x;
        _moveLine = Line.Middle;
    }

    public void ChangeDirection(int directionX)
    {
        if (directionX < 0)
        {
            if (_moveLine == Line.Middle)
            {
                SetNextPosition(directionX);
                _moveLine = Line.Left;
                _animator.Play(AnimatorPlayerController.States.SwipeRunLeft);
            }
            else if (_moveLine == Line.Right)
            {
                SetStartPosition();
                _animator.Play(AnimatorPlayerController.States.SwipeRunLeft);
            }
        }
        else if (directionX > 0)
        {
            if (_moveLine == Line.Middle)
            {
                SetNextPosition(directionX);
                _moveLine = Line.Right;
                _animator.Play(AnimatorPlayerController.States.SwipeRunRight);
            }
            else if (_moveLine == Line.Left)
            {
                SetStartPosition();
                _animator.Play(AnimatorPlayerController.States.SwipeRunRight);
            }
        }
    }

    public void ResetMover()
    {
        transform.position = Vector3.zero;
    }
}
