using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private float x = 0.0f;
    [SerializeField]
    private float y = 0.6f;
    [SerializeField]
    private float z = 0.0f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            CharacterController cc = other.GetComponent<CharacterController>();

            if (cc != null)
            {
                cc.enabled = false;
            }

            other.transform.position = new Vector3(x, y, z);

            if (cc != null)
            {
                cc.enabled = true;
            }
        }
    }
}
