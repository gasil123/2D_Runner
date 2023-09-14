using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle_Script : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player_Movement>();

        if (player != null)
        {
            collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.SetParent(null);
    }
}
