using System;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour, IForceReceiver
{
    [SerializeField] private float _moveSpeed = 5f;

    private Controls _controls;

    private Rigidbody2D _rigidbody2D;
    private Vector2 moveInput;
    private EffectHandler _effectHandler;
    [SerializeField] private EffectData _testEffect;
    [SerializeField] private float backwardMultiplier = 0.6f;
    [SerializeField] private Hand _hand;
    [SerializeField] private Transform _visual;

    private void Awake()
    {
        _controls = new Controls();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _effectHandler = GetComponent<EffectHandler>();
    }
    private void Update()
    {
        ReadMovement();
        UpdateFacing();
    }
    private void OnEnable()
    {
        _controls.Player.Enable();
        _controls.Player.TestButton.performed += AddTestEffect;
    }
    private void OnDisable()
    {
        _controls.Player.Disable();
        _controls.Player.TestButton.performed -= AddTestEffect;
    }
    private void ReadMovement()
    {
        Vector2 inputDirection = _controls.Player.Move.ReadValue<Vector2>();

        float speed = _moveSpeed;

        float handDir = _hand.IsRightSide ? 1f : -1f;

        if (Mathf.Sign(inputDirection.x) == -handDir && inputDirection.x != 0)
            speed *= backwardMultiplier;

        _rigidbody2D.linearVelocity = inputDirection * speed;
        _hand.Player.PlayerAnimator.SetFloat("velocity", _rigidbody2D.linearVelocity.magnitude);
    }
    private void AddTestEffect(InputAction.CallbackContext context)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _effectHandler.AddEffect(_testEffect);
    }
    private void UpdateFacing()
    {
        if (_hand.IsRightSide)
            _visual.localScale = new Vector3(1, 1, 1);
        else
            _visual.localScale = new Vector3(-1, 1, 1);
    }

    public void ApplyForce(Vector2 direction, float force, float duration)
    {
        StartCoroutine(ForceCoroutine(direction, force, duration));
    }
    IEnumerator ForceCoroutine(Vector2 direction, float force, float duration)
    {
        float timer = 0;

        while (timer < duration)
        {
            _rigidbody2D.linearVelocity = direction * force;
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
