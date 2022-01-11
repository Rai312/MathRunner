using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMover _mover;
    private int positiveX = 1;
    private int negativeX = -1;

    private void Start()
    {
        _mover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _mover.ChangeDirection(negativeX);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _mover.ChangeDirection(positiveX);
        }
    }
}
