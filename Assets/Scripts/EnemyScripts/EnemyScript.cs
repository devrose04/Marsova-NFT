using System;
using System.Collections;
using EnemyScripts.OwnScript;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace EnemyScripts
{
        public class EnemyScript : MonoBehaviour  // Bu Scripte dışardan public ile kullanılacak kodlar olucak. 
        {                                         // Enemy GameObjelerin hepsini GameManagerdan Update ile çagırıyorum.
                public float speed;
                public float health;
                public float damage;
                public float hitTimeRange;      // vuruş yapma sıklıgının süresi.
                (float, float, float, float) OwnInformations;
                
                [SerializeField] public ParticleSystem OwnEffect;  // bu kendi Effecti, boş olsada olur
                [SerializeField] public ParticleSystem HitEffect;  // bu vuruş effecti
                [SerializeField] public float effectTime;  // effecti gerçekleştirme sürem
                public float sabitEffectTime;

                private string Tag;
                private GameObject Enemy;
                private Rigidbody2D RB2;
                
                private ICustomScript __ICustomScript;
                public float dmgTime = 10f;  // 10 verme nedenim bir hatayı önlediğinden.   // en son ne zaman vuruş yaptıgının verisini tutuyor.
                [FormerlySerializedAs("dashTime")] public float dashHitTime = 10f;  // bu, burayla alakalı degil ama buraya yazdım. 

                private void Awake()
                {
                        Enemy = this.gameObject;
                        Tag = Enemy.tag;
                        RB2 = Enemy.GetComponent<Rigidbody2D>();
                        sabitEffectTime = effectTime;
                        
                        
                        __ICustomScript = Enemy.GetComponent<ICustomScript>();  // burda kendi özel oluşturdugumuz Scripti buluyoruz ve onu çagırıyoruz. 
                        if (__ICustomScript != null)
                                OwnInformations = __ICustomScript.OwnInformations();  // Bu Obejini kendi bilgilerini buraya geçiriyoruz. Her Enemyinin farklı aralıkta bilgileri var.
                        
                        speed = OwnInformations.Item1;
                        health = OwnInformations.Item2;
                        damage = OwnInformations.Item3;
                        hitTimeRange = OwnInformations.Item4;
                }
                

                // ReSharper disable Unity.PerformanceAnalysis
                public void TakeDamages(float dmg,Vector2 directionToEnemy)
                {
                        StartCoroutine(KnockBack(directionToEnemy));
                        health -= dmg;
                        // print($"<color=yellow>Enemy Health:</color>" + health);
                        if (health <= 0)
                        {
                                if (Tag == "Skeletons")
                                        Enemy.GetComponent<SkeletonsScript>().ReBorn();
                                Destroy(this.gameObject);
                        }
                }

                public void Effect()
                {
                        effectTime = 0;
                        ParticleSystem Effect = Instantiate(OwnEffect, Enemy.transform);
                        Destroy(Effect.gameObject,10f);
                }
                
                public void DeltaTimeUp()
                {
                        dmgTime += Time.deltaTime;
                        effectTime += Time.deltaTime;
                        dashHitTime += Time.deltaTime;
                }

                private IEnumerator KnockBack(Vector2 directionToEnemy)
                {
                        float duration = 0.5f; // Döngünün çalışma süresi
                        float elapsedTime = 0f; // Geçen süre

                        
                        while (elapsedTime < duration)  // buraya enemynin ne kadar uzaklıga savrulacagınıda yazabilirsin.
                        {
                                RB2.AddForce(new Vector2(directionToEnemy.x * 400, directionToEnemy.y));
                                yield return new WaitForSeconds(0.005f);
                                elapsedTime += 0.005f; // Geçen süreyi güncelle
                        }
                }
                
        }

}
