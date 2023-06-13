using System;
using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess

namespace ObjectsScripts
{
    public class ObjectColliderScript : MonoBehaviour
    {
        //1-) zemine degdiginde gravity kapanacak  2-) Trigger hep açık olucak   
        private GameObject GameObject;
        private Rigidbody2D RB2;

        private void Awake()
        {
            GameObject = this.gameObject;
            RB2 = GameObject.GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other) // Obje zemine ilk degdigi anda 1 kere çalışacak
        {
            if (other.CompareTag("Ground"))
                RB2.gravityScale = 0f;
            RB2.velocity = new Vector2(RB2.velocity.x * 0.5f, 0); // bu: Obje zıplayıp zemine degdiginde çarpmış hissi uyandırıyor.
        }

        private void OnTriggerExit2D(Collider2D other) // Obje zeminden çıktıgı zaman 1 kere çalışacak
        {
            if (other.CompareTag("Ground"))
                RB2.gravityScale = 1f;
        }

        private void OnTriggerStay2D(Collider2D other) // Obje zemine degdidi süre boyunca hep çalışacak
        {
            if (other.CompareTag("Ground"))
            {
                RB2.velocity = new Vector2(RB2.velocity.x * 0.9f, 0); // 0.9f hızını yavaş yavaş azaltmasına yarıyor
            }
        }
    }
}