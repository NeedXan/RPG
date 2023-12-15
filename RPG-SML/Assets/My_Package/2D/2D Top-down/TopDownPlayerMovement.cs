using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Animator))]
public class TopDownPlayerMovement : MonoBehaviour, IDataPersistence
{
    [HideInInspector] public static TopDownPlayerMovement instance { get; private set; }
    

    [Header("If you would like preset values, use the context menu!")]
    [SerializeField] private bool _debugMode;

    private Animator _animator;

    private Rigidbody2D _rigidbody;
    private Vector2 _movement;

    private float _curSpeed;

    private bool _isMoving;
    private bool _canMove;

    private Vector2 _facingLeft;
    private Vector2 _facingRight;

    [Space(3), Header("Customization")]
    [Range(2f, 20f), Tooltip("Movement Speed should be between the range of 2 and 20 to keep things under control")]
    [SerializeField] private float _movementSpeed;

    private void Awake() {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }

        instance = this;
        Initialization();
    }

    private void Initialization()
    {
        if (_debugMode) Debug.Log("Debug mode: ON"); else Debug.Log("Debug mode: OFF");

        _animator = GetComponent<Animator>();
        _facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        _facingRight = new Vector2(transform.localScale.x, transform.localScale.y);
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_movementSpeed > 2)
            _canMove = true;
        else _canMove = false;

        _animator.SetFloat("Speed", _curSpeed);

        if (_canMove)
        {
            // Calculate where we should move and by how much
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            _isMoving = (horizontalInput != 0 || verticalInput != 0);

            if (_isMoving)
            {
                _movement = new Vector2(horizontalInput, verticalInput).normalized * _movementSpeed;
                _curSpeed = 5;
            }
            else
            {
                _movement = Vector2.zero;
                _curSpeed = 0;
            }

            // Flip facing
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.localScale = _facingRight;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.localScale = _facingLeft;
            }
        }
        else _movement = Vector2.zero;
    }

    private void FixedUpdate() { _rigidbody.velocity = _movement; }

    #region Context Menu Functions

    [ContextMenu("Debug Mode")] public void DebugMode() { if (_debugMode) _debugMode = false; else _debugMode = true; }

    #endregion Context Menu Functions

    #region Save-Load

    public void LoadData(GameData data)
    {
        if (_debugMode) return;

        transform.position = data.playerPosition;
        if (data.isFacingLeft)
            transform.localScale = _facingLeft;
    }

    public void SaveData(ref GameData data)
    {
        if (_debugMode) return;

        data.playerPosition = transform.position;
        if (transform.localScale.Equals(_facingLeft))
            data.isFacingLeft = true;
    }

    #endregion Save-Load

    public void EnterDialogue()
    {
        _canMove = false;
    }

    public void EndDialogue()
    {
        _canMove = true;
    }
}
