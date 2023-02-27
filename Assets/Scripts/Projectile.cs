using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // "Player" is the script for the Player
    private Player _player;
    private SpriteRenderer _renderer;


    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _travelTime = 3.0f;
    private bool _direction;


    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _renderer = GetComponent<SpriteRenderer>();

        _direction = _player.GetDirection();

        if (_direction)
        {
            _renderer.flipX = true;
        }

        StartCoroutine(ProjectileDestoryRoutine());
    }

    // Update is called once per frame
    void Update()
    {

        if (!_direction)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * -_speed * Time.deltaTime);
        }

    }

    IEnumerator ProjectileDestoryRoutine()
    {
        yield return new WaitForSeconds(_travelTime);
        Destroy(this.gameObject);
    }
}
