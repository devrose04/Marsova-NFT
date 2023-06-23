using System;
using System.Collections;
using ______Scripts______.EnemyScripts.Enemy.EnemyAnimations;
using ______Scripts______.EnemyScripts.Enemy.EnemyAnimationsScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyScripts.AIScripts
{
    public class AISkillsScript : MonoBehaviour // bunu Unityden bagla Enemy'e 
    {
        private GameObject Enemy;
        private GameObject GameManger;

        private AIScript __AIScript;
        private AnimationsController _animationsController;

        private void Awake()
        {
            Enemy = this.gameObject;
            GameManger = GameObject.Find("GameManager");
            __AIScript = Enemy.GetComponent<AIScript>();
            _animationsController = GameManger.GetComponent<AnimationsController>();
        }


        public void MoveOwnBase(float distance, float moveSpeed, Vector2 startingPosition, Vector2 basePosition, bool isRight, float baseRange)
        {
            if (Random.Range(1, 500) == 2 && distance < 13) // Player 15m Enemyye yakınsa bir ihtimal StopMoveAndLookAround fonksiyonunu çalıştırabilir.
            {
                __AIScript.isWaitingInTheBase = true;
            }

            var _position = transform.position;

            __AIScript.startingPosition.y = _position.y; // bunu ve altındakini koymaz isem, hep spawn oldugu -y position'a göre işlem yapıyor.
            __AIScript.basePosition.y = _position.y;
            // alttaki kod: Base'in içinde hareket ettirir.
            _position = Vector2.MoveTowards(_position, basePosition, moveSpeed / 2.5f * Time.deltaTime);

            transform.position = _position;

            if (Vector2.Distance(transform.position, basePosition) < 0.1f) // Base'in en sol ve en sağına ulaştığında diger yere yöneltir
            {
                switch (isRight)
                {
                    case true: // en sağda ise sola dönsün
                        __AIScript.isRight = false;
                        __AIScript.basePosition = startingPosition - new Vector2(baseRange, 0);
                        Enemy.transform.rotation = new Quaternion(0, 1, 0, 0);
                        break;

                    case false: // en solda ise sağa dönsün
                        __AIScript.isRight = true;
                        __AIScript.basePosition = startingPosition + new Vector2(baseRange, 0);
                        Enemy.transform.rotation = new Quaternion(0, 0, 0, 0);
                        break;
                }
            }
        }

        public IEnumerator StopMoveAndLookAround(float distance)
        {
            int oppositeRotation = 0;
            int directionRotation = 1;
            if (Enemy.transform.rotation.y == 0) // eger sağa bakıyor ise çalışır
            {
                oppositeRotation = 1; // eger sağa bakıyorsa ilk sola bakmasını istiyorum.
                directionRotation = 0;
            }

            __AIScript.isWaitingInTheBase = true;

            _animationsController.EnemyAnimations(Enemy);

            yield return new WaitForSeconds(1f);
            Enemy.transform.rotation = new Quaternion(0, oppositeRotation, 0, 0); // bu zıttına kafasını çeviriyor
            yield return isPlayerCloseBreakit(distance);

            yield return new WaitForSeconds(1f);
            Enemy.transform.rotation = new Quaternion(0, directionRotation, 0, 0); // bu normal asıl baktıgı yere baktırıyor.
            yield return isPlayerCloseBreakit(distance);

            yield return new WaitForSeconds(1f);
            Enemy.transform.rotation = new Quaternion(0, oppositeRotation, 0, 0); // bu gine zıttına 
            yield return isPlayerCloseBreakit(distance);

            yield return new WaitForSeconds(1f);
            Enemy.transform.rotation = new Quaternion(0, directionRotation, 0, 0); // bu asıl baktıgı yöne bakıyor ve yürümeye başlıyor.
            yield return isPlayerCloseBreakit(distance);

            yield return new WaitForSeconds(1f);
            yield return isPlayerCloseBreakit(distance);

            __AIScript.isWaitingInTheBase = false;

            _animationsController.EnemyAnimations(Enemy);
        }

        IEnumerator isPlayerCloseBreakit(float distance)
        {
            if (distance < 10) // Player Enemy'nin menziline girdiyse çalışır ve StopMoveAndLookAround fonksiyonu durur.
            {
                __AIScript.isWaitingInTheBase = false;
                _animationsController.EnemyAnimations(Enemy);
                yield break;
            }
        }
    }
}