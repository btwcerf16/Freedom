using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Controls _controls;

    private Rigidbody2D _rigidbody2D;
    private Vector2 moveInput;
    private EffectHandler _effectHandler;
    [SerializeField] private EffectData _testEffect;

    private void Awake()
    {
        _controls = new Controls();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _effectHandler = GetComponent<EffectHandler>();
    }
    private void Update()
    {
        ReadMovement();
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
        
        _rigidbody2D.linearVelocity = inputDirection * _moveSpeed;
    }
    private void AddTestEffect(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

}
