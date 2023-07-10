using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteEnemyCollider : MonoBehaviour
{
    private Rigidbody2D RB2;

    private void Awake()
    {
        RB2 = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            RB2.velocity = new Vector2(0, 0);
        }
    }
}