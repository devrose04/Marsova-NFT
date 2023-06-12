using System.Collections.Generic;
using System.Linq;
using EnemyScripts;
using EnemyScripts.AIScripts;
using PlayerScripts;
using UnityEngine;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation
// ReSharper disable Unity.PerformanceCriticalCodeNullComparison
// ReSharper disable SimplifyLinqExpressionUseAll
// ReSharper disable Unity.PreferNonAllocApi

public class GameManager : MonoBehaviour
{
    [SerializeField] float GameRadiues;   // oyunun çalıştırılma alanı
    
    private float timer = 0f;
    
    private PlayerController __PlayerController;
    
    private GameObject Player;
    private LayerMask enemyLayerMask;
    private List<GameObject> enemyList ;
    
    private void Awake()
    {
        Player = GameObject.Find("Player");
        enemyList = new List<GameObject>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
        __PlayerController = Player.GetComponent<PlayerController>();
    }

    private void FixedUpdate()    // tüm kodlar tek bir FixedUpdate ile çalıştırılacak. Oda burası.
    {
        __PlayerController.MYFixedUpdate();
        
        foreach (var enemy in enemyList)
        {
            if (enemy.gameObject != null)
            {
                var __EnemyScript = enemy.GetComponent<EnemyScript>();
                var __AIScript = enemy.GetComponent<AIScript>();
                
                __EnemyScript.DeltaTimeUp();
                __EnemyScript.MYFixedUpdate();
                __AIScript.MYFixedUpdate();
            }
        }
    }
    
    private void Update()    // tüm kodlar tek bir Update ile çalıştırılacak. Oda burası.
    {
        __PlayerController.MYUpdate();
        
        foreach (var enemy in enemyList)    // bu foreach'a bişi yazma aşagıdaki foreach'e yaz
        {
            if (enemy.gameObject==null) // eger eneym object ölmüş ise alttaki CalculationsWhichEnemiesAround çalışır. Bunu yapmazsak hata verir.
                timer = 1;
        }
        timer += Time.deltaTime;
        if (timer >= 1)    // bu koşul saniyede bir kere çalışmasını sağlıyor, buda Performanstan kazandırıyor.
        {
            CalculationsWhichEnemiesAround();
            timer = 0f;
        }

        foreach (var enemy in enemyList)
        {
            var __EnemyScript = enemy.GetComponent<EnemyScript>();
            
            __EnemyScript.CreateOwnEffect();
        }
    }

    void CalculationsWhichEnemiesAround()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(Player.transform.position, GameRadiues, enemyLayerMask);
        isNotHaveRemoveList(enemyColliders);
        addToList(enemyColliders);

        print("Çevredeki düşmanların sayısı: " + enemyList.Count);      // bunu canvasa geçir
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
