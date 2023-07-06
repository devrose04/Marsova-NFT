using System.Collections.Generic;
using System.Linq;
using ______Scripts______.EnemyScripts.Enemy.Enemy;
using ______Scripts______.PlayerScripts.Player;
using ______Scripts______.UIScripts.Canvas.Player;
using EnemyScripts;
using EnemyScripts.AIScripts;
using EnemyScripts.Enemy;
using PlayerScripts;
using PlayerScripts.Player;
using UnityEngine;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation
// ReSharper disable Unity.PerformanceCriticalCodeNullComparison
// ReSharper disable SimplifyLinqExpressionUseAll
// ReSharper disable Unity.PreferNonAllocApi

public class GameManager : MonoBehaviour
{
    [SerializeField] float GameRadiues; // oyunun çalıştırılma alanı

    private float timer = 0f;

    private PlayerController __PlayerController;
    private PlayerScript _playerScript;
    private JetPackBar _jetPackBar;

    [SerializeField] private GameObject _SettingButton;

    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject DifficultyMenu;

    // [SerializeField] private GameObject Options; // todo: bunları yapınca ayarla
    // [SerializeField] private GameObject HowToPlay;

    private GameObject Player;
    private GameObject ButtonManager;
    private LayerMask enemyLayerMask;
    private List<GameObject> enemyList;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        ButtonManager = GameObject.Find("ButtonManager");
        _jetPackBar = ButtonManager.GetComponent<JetPackBar>();
        enemyList = new List<GameObject>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
        __PlayerController = Player.GetComponent<PlayerController>();
    }

    private void FixedUpdate() // tüm kodlar tek bir FixedUpdate ile çalıştırılacak. Oda burası.
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

    private void Update() // tüm kodlar tek bir Update ile çalıştırılacak. Oda burası.
    {
        SettingButton();
        __PlayerController.MYUpdate();
        _jetPackBar.JetPackBarUpdate();
        // _playerScript.CreateTouchTheGroundEffeckt();

        foreach (var enemy in enemyList) // bu foreach'a bişi yazma aşagıdaki foreach'e yaz
        {
            if (enemy.gameObject == null) // eger eneym object ölmüş ise alttaki CalculationsWhichEnemiesAround çalışır. Bunu yapmazsak hata verir.
                timer = 1;
        }

        timer += Time.deltaTime;
        if (timer >= 1) // bu koşul saniyede bir kere çalışmasını sağlıyor, buda Performanstan kazandırıyor.
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

        // print("Çevredeki düşmanların sayısı: " + enemyList.Count); // bunu canvasa geçir
    }

    void isNotHaveRemoveList(Collider2D[] _enemyColliders) // GameRadiues alanındaki olmayan Enemyleri listeden çıkart
    {
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            GameObject enemyObject = enemyList[i];

            if (!_enemyColliders.Any(collider2d => collider2d.gameObject == enemyObject)) // enemyColliders.Any bu True veya False döndürür
            {
                // eger listede bulamadı ise o Objeyi Listeden kaldırır.
                // print("Listeden Çıkardık: " + enemyList[i]);
                enemyList.RemoveAt(i);
            }
        }
    }

    void addToList(Collider2D[] _enemyColliders) // GameRadiues alanındaki olan Enemyleri listeye ekler
    {
        foreach (Collider2D enemyCollider in _enemyColliders)
        {
            GameObject enemyObject = enemyCollider.gameObject;

            if (!enemyList.Contains(enemyObject)) // Contains içerip içermedigine bakar
            {
                // print("Listeye Ekledik: " + enemyObject);
                enemyList.Add(enemyObject);
            }
        }
    }

    void SettingButton()
    {
        if (MainMenu.activeSelf == false && GameOver.activeSelf == false && DifficultyMenu.activeSelf == false)
            _SettingButton.SetActive(true);
        else
            _SettingButton.SetActive(false);
    }
}