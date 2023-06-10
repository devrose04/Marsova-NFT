using System.Collections.Generic;
using System.Linq;
using EnemyScripts;
using UnityEngine;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation
// ReSharper disable Unity.PerformanceCriticalCodeNullComparison
// ReSharper disable SimplifyLinqExpressionUseAll
// ReSharper disable Unity.PreferNonAllocApi

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float GameRadiues;   // oyunun çalıştırılma alanı

    private LayerMask enemyLayerMask;
    
    private List<GameObject> enemyList ;
    private void Awake()
    {
        enemyList = new List<GameObject>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
    }

    private void FixedUpdate()    // tüm kodlar tek bir FixedUpdate ile çalıştırılacak. Oda burası.
    {
        foreach (var enemy in enemyList)
        {
            var __EnemyScript = enemy.GetComponent<EnemyScript>();
            var __AIScript = enemy.GetComponent<AIScript>();
            
            __EnemyScript.DeltaTimeUp();
            __AIScript.MYFixedUpdate();
            
        }
    }
    
    private void Update()    // tüm kodlar tek bir Update ile çalıştırılacak. Oda burası.
    { 
        
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(Player.transform.position, GameRadiues, enemyLayerMask);
        isNotHaveRemoveList(enemyColliders);
        addToList(enemyColliders);
        
        print("Çevredeki düşmanların sayısı: " + enemyList.Count);
        
        foreach (var enemy in enemyList)
        {
            var __EnemyScript = enemy.GetComponent<EnemyScript>();
            
            if (__EnemyScript.OwnEffect != null && __EnemyScript.effectTime >= __EnemyScript.sabitEffectTime)  // 5 sn de bir gerçekleştirmesine yarar
                __EnemyScript.Effect();
        }
    }
    
    void isNotHaveRemoveList(Collider2D[] _enemyColliders)  // GameRadiues alanındaki olmayan Enemyleri listeden çıkart
    {
        
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            GameObject enemyObject = enemyList[i];

            if (!_enemyColliders.Any(collider2d => collider2d.gameObject == enemyObject))  // enemyColliders.Any bu True veya False döndürür
            {                                                                             // eger listede bulamadı ise o Objeyi Listeden kaldırır.
                // print("Listeden Çıkardık: " + enemyList[i]);
                enemyList.RemoveAt(i);
            }
        }
    }

    void addToList(Collider2D[] _enemyColliders)  // GameRadiues alanındaki olan Enemyleri listeye ekler
    {
        
        foreach (Collider2D enemyCollider in _enemyColliders)
        {
            GameObject enemyObject = enemyCollider.gameObject;

            if (!enemyList.Contains(enemyObject))  // Contains içerip içermedigine bakar
            {
                // print("Listeye Ekledik: " + enemyObject);
                enemyList.Add(enemyObject);
            }
            
        }
    }
    
}
