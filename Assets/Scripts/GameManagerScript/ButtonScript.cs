using System;
using System.Collections;
using UnityEngine;

namespace GameManagerScript
{
    public class ButtonScript : MonoBehaviour   // 2 kere basımlık combo skillerde ManyPressButton kullanma en az 3 basımlık olacak ise kullan.
    {
        private bool canDoubleTap = false;
        
        private int tapCount = 0;
        
        // *** Bu alltaki şeyler Data Verileridir. En son ne zaman tuşa bastığının veya o sklinin Dolum süresinin verisini burda tutar.
        // ***  1-) lastPressTime    2-) lastPressCanUseTime     Aşagıdakiler Bu 2 sinin kısaltımı şekilde yazılmıştır.
        
        // ***  1-) lastPressTime = En son basıldıgı zamanın verisini tutar
        // ***  2-) lastPressCanUseTime = Skilin en son ne zaman kullanıldıgının zaman verisini tutar
        
        // ReSharper disable Unity.PerformanceAnalysis
        public (float,float) ManyPressButton(Action comboSkil, Action function, Func<IEnumerator> coroutineFunction,  float onTimePress, int pressCount, float lastPressTime, float coolDownTime, float lastPressCanUseTime)    
        {    // Bu Fonksiyon bir tuşa arka arkaya 2 3 kere basınca çalışacaktır.  1. ve 2. argümanlarda ya Normal bir Fonksiyon yada Coroutine Fonksiyon girmelidir.
           
            float _lastPressTime = Time.time - lastPressTime;  // en son Butona basıldığı süre.
            
            // Aşagıdaki kod: onTimePress zamanı içinde basılır ise bu if true olucaktır..
            if (canDoubleTap && _lastPressTime < onTimePress)  // Buttona pressingTimeRange aralıgında çift tiklayınca çalışacak
            {
                tapCount++;
                if (tapCount == pressCount-1 && comboSkil != null)  // combo skili burda çalışır    Comboları 3 basımlık olacak ise kullan. yoksa hata verir
                {
                    comboSkil.Invoke();
                }
                
                // Aşagıdaki kod: ilk if te içeri girdikten sonra, pressCount sayısı kadar basarsa bu if true olucaktır..
                if (tapCount >= pressCount)
                {
                    // İki kez tuşa basıldığında yapılacak işlem
                    if (function != null)
                    {
                        lastPressCanUseTime = ButtonCanUse_A(function, coolDownTime, lastPressCanUseTime);
                        tapCount = 0;     
                        canDoubleTap = false;
                        // Debug.Log("Double Action tap action!");
                    }
                    else if (coroutineFunction != null)
                    {
                        lastPressCanUseTime = ButtonCanUse_C(coroutineFunction, coolDownTime, lastPressCanUseTime);
                        tapCount = 0;     
                        canDoubleTap = false;
                        // Debug.Log("Double IEnumerator tap action!");
                    }
                }
            }
            else
                tapCount = 1;
                
            
            if (!canDoubleTap && _lastPressTime > onTimePress)   // Combo Skilin dolum süresi kodu
                canDoubleTap = true;    // ve combo Sklini kullanabilir.
                
            lastPressTime = Time.time;
            return (lastPressTime, lastPressCanUseTime);
        }
        
        public float ButtonCanUse_A(Action function,float coolDownTime, float lastPressCanUseTime)  // Action function: Normal Fonksiyonlar için
        {
            float _lastPressCanUseTime = Time.time - lastPressCanUseTime;   // en son Skilin kullanıldığı zaman.
            
            if (_lastPressCanUseTime > coolDownTime)        // buton dolum süresi kodu
            {
                function.Invoke();
                // Debug.Log("Skil action!");
                lastPressCanUseTime = Time.time;    // bu skilin en son ne zaman kullanıldıgının verisini lastPressCanUseTime'a atar.
            }
            return lastPressCanUseTime;
        }     
        public float ButtonCanUse_C(Func<IEnumerator> function,float coolDownTime, float lastPressCanUseTime)  // IEnumerator function: Coroutine Fonksiyonlar için
        {
            float _lastPressCanUseTime = Time.time - lastPressCanUseTime;   // en son Skilin kullanıldığı zaman.
            
            if (_lastPressCanUseTime > coolDownTime)        // buton dolum süresi kodu
            {
                StartCoroutine(function.Invoke());
                // Debug.Log("Skil action!");
                lastPressCanUseTime = Time.time;    // bu skilin en son ne zaman kullanıldıgının verisini lastPressCanUseTime'a atar.
            }
            return lastPressCanUseTime;
        }
    }
}
