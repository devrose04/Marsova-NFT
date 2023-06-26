using System;
using UnityEngine;

namespace ______Scripts______.UIScripts
{
    public class MeteorEndCollision : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Meteors"))
            {
                other.GetComponent<MeteorMove>().RandomCounts();
                other.GetComponent<MeteorMove>().AwakeMeteor();
                other.GetComponent<MeteorMove>().StartSpawn_x_position();
            }
        }
    }
}