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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Check if the player's script exists
            if (_player != null)
            {
                _player.Damage();
            }

            _moving = false;
            _speed = 0;

            Destroy(this.gameObject);
        }
        else if (other.tag == "Firebolt")
        {
            _moving = false;
            _speed = 0;

            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.tag == "LightningBolt")
        {
            _moving = false;
            _speed = 0;

            Destroy(this.gameObject);
        }
        else if (other.tag == "Wave")
        {
            _moving = false;
            _speed = 0;

            Destroy(this.gameObject);
        }
    }
}
