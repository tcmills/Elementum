using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // "Player" is the script for the Player
    private Player _player;


    [SerializeField]
    private float _speed = 0.5f;
    private bool _moving = true;
    [SerializeField]
    private float _travelTime = 4.0f;

    [SerializeField]
    private int _form;

    private int _health = 10;
    private bool _iframe = false;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        StartCoroutine(MoveRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        // Move the Enemy based on its speed
        transform.Translate(Vector3.right * _speed * Time.deltaTime);

        if (_health < 1)
        {
            Destroy(this.gameObject);
        }
    }

    // While the enemy is allowed to move, it will chenge directions every "_travelTime" seconds
    IEnumerator MoveRoutine()
    {
        while (_moving)
        {
            yield return new WaitForSeconds(_travelTime);
            _speed = -_speed;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            // Check if the player's script exists
            if (_player != null)
            {
                _player.Damage();
            }
        }
        
        if (!_iframe)
        {
            if (other.tag == "Firebolt")
            {
                Destroy(other.gameObject);

                if (_form == 0)
                {
                    _health -= 2;
                }
                else
                {
                    _health -= 4;
                }
            }
            else if (other.tag == "LightningBolt")
            {
                if (_form == 1)
                {
                    _health -= 2;
                }
                else
                {
                    _health -= 4;
                }
            }
            else if (other.tag == "Wave")
            {
                if (_form == 2)
                {
                    _health -= 2;
                }
                else
                {
                    _health -= 4;
                }
            }

            _iframe = true;
            StartCoroutine(IFrameRoutine());
        }

    }

    // After the enemy is hit, make it invulnerable for a short amount of time.
    IEnumerator IFrameRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _iframe = false;
    }
}
