using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private SpriteRenderer _renderer;
    private UIManager _uiManager;


    [SerializeField]
    private float _speed = 3.0f; //8.0
    [SerializeField]
    private float _gravity = 0.0385f; //0.4  0.13
    [SerializeField]
    private float _jumpHeight = 5.0f; //15.0
    private bool _canJump = true;
    private bool _canDoubleJump = true;
    // Saves the proper y velocity so the code doesnt override it
    private float _yVelocity;


    [SerializeField]
    private GameObject _fireboltPrefab;
    [SerializeField]
    private GameObject _lightningBoltPrefab;
    [SerializeField]
    private GameObject _wavePrefab;
    private GameObject _lightningBolt;
    private bool _lightningFired = false;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _lightningRate = 1.0f;
    [SerializeField]
    private float _waterRate = 1.0f;
    private float _canFire = -1f;


    //[SerializeField]
    //private int _coinsCollected;
    private int _lives = 3;
    private int _form = 0;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _renderer = GetComponent<SpriteRenderer>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            UnityEngine.Debug.LogError("The UI Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        
        // Switch forms based on Input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _form = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _form = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _form = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _form = 3;
        }

        if (_lightningFired && Input.GetKeyUp(KeyCode.Mouse0))
        {
            Destroy(_lightningBolt.gameObject);
            _lightningFired = false;
            _speed = 3.0f;
            _canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _canFire)
        {
            UseAbility();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;

        if (_speed != 0.0f && horizontalInput < 0 && _renderer.flipX == false)
        {
            _renderer.flipX = true;
        }
        else if (_speed != 0.0f && horizontalInput > 0 && _renderer.flipX == true)
        {
            _renderer.flipX = false;
        }

        if (_canJump)
        {
            if (_controller.isGrounded)
            {
                // Allows the Player to jump
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _yVelocity = _jumpHeight;
                    _canDoubleJump = true;
                }
            }
            else
            {
                if (_canDoubleJump && Input.GetKeyDown(KeyCode.Space) && _form == 0)
                {
                    _yVelocity = _jumpHeight;
                    _canDoubleJump = false;
                }
                _yVelocity -= _gravity;
            }
        }

        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }

    void UseAbility()
    {

        if (_form == 1)
        {
            // _canFire is the time when the Player is able to fire again
            _canFire = Time.time + _fireRate;

            // The Player is in the Fire form
            Instantiate(_fireboltPrefab, transform.position + new Vector3(0, -0.03f, 0.01f), Quaternion.identity);
        }
        else if (_form == 2 && _controller.isGrounded)
        {
            // _canFire is the time when the Player is able to fire again
            _canFire = Time.time + _lightningRate;

            // The Player is in the Lightning form
            _speed = 0f;
            _canJump = false;
            _lightningFired = true;

            if (_renderer.flipX)
            {
                _lightningBolt = Instantiate(_lightningBoltPrefab, transform.position + new Vector3(-0.6f, 0, 0), Quaternion.Euler(0, 180, 0));
            }
            else
            {
                _lightningBolt = Instantiate(_lightningBoltPrefab, transform.position + new Vector3(0.6f, 0, 0), Quaternion.identity);
            }

        }
        else if (_form == 3)
        {
            // _canFire is the time when the Player is able to fire again
            _canFire = Time.time + _waterRate;

            // The Player is in the Water form
            if (_renderer.flipX)
            {
                Instantiate(_wavePrefab, transform.position + new Vector3(-0.14f, 0.04f, 0.01f), Quaternion.identity);
            }
            else
            {
                Instantiate(_wavePrefab, transform.position + new Vector3(0.14f, 0.04f, 0.01f), Quaternion.identity);
            }
        }
    }

    public bool GetDirection()
    {
        // The direction the player is facing
        return _renderer.flipX;
    }

    //public void CollectCoin()
    //{
    //    _coinsCollected++;
    //    _uiManager.UpdateCoinDisplay(_coinsCollected);
    //}

    public void Damage()
    {
        _lives--;
        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
