using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player_Movement>();

        if (player)
        {
            Destroy(gameObject);
        }
    }
}
