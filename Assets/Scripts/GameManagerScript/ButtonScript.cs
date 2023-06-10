using System;
using System.Collections;
using UnityEngine;

namespace GameManagerScript
{
    public class ButtonScript : MonoBehaviour   // 2 kere basımlık combo skillerde ManyPressButton kullanma en az 3 basımlık olacak ise kullan.
    {
        private bool canDoubleTap = false;
        private int tapCount = 0;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public (float,float) ManyPressButton(Action comboSkil, Action function, Func<IEnumerator> coroutineFunction,  float pressingTimeRange, int pressCount, float lastPressTime, float coolDownTime, float lastPressAvailableTime)    
        {    // Bu Fonksiyon bir tuşa arka arkaya 2 3 kere basınca çalışacaktır.  1. ve 2. argümanlarda ya Normal bir Fonksiyon yada Coroutine Fonksiyon girmelidir.
            if (canDoubleTap && Time.time - lastPressTime < pressingTimeRange)  // Buttona pressingTimeRange aralıgında çift tiklayınca çalışacak
            {
                tapCount++;
                if (tapCount == pressCount-1 && comboSkil != null)  // combo skili burda çalışır    Comboları 3 basımlık olacak ise kullan. yoksa hata verir
                {
                    comboSkil.Invoke();
                }
                if (tapCount >= pressCount)
                {
                    // İki kez tuşa basıldığında yapılacak işlem
                    if (function != null)
                    {
                        lastPressAvailableTime = ButtonAvailable_A(function, coolDownTime, lastPressAvailableTime);
                        tapCount = 0;     
                        canDoubleTap = false;
                        // Debug.Log("Double Action tap action!");
                    }
                    else if (coroutineFunction != null)
                    {
                        lastPressAvailableTime = ButtonAvailable_C(coroutineFunction, coolDownTime, lastPressAvailableTime);
                        tapCount = 0;     
                        canDoubleTap = false;
                        // Debug.Log("Double IEnumerator tap action!");
                    }
                }
            }
            else
                tapCount = 1;
                
            
            
            if (!canDoubleTap && Time.time - lastPressTime > pressingTimeRange)   // Buton dolum süresi kodu
                canDoubleTap = true;
                
            lastPressTime = Time.time;
            return (lastPressTime, lastPressAvailableTime);
        }
        
        public float ButtonAvailable_A(Action function,float coolDownTime, float lastPressAvailableTime)  // Action function: Normal Fonksiyonlar için
        {
            if (Time.time - lastPressAvailableTime > coolDownTime)        // buton dolum süresi kodu
            {
                function.Invoke();
                // Debug.Log("Skil action!");
                lastPressAvailableTime = Time.time;
            }
            return lastPressAvailableTime;
        }     
        public float ButtonAvailable_C(Func<IEnumerator> function,float coolDownTime, float lastPressAvailableTime)  // IEnumerator function: Coroutine Fonksiyonlar için
        {
            if (Time.time - lastPressAvailableTime > coolDownTime)        // buton dolum süresi kodu
            {
                StartCoroutine(function.Invoke());
                // Debug.Log("Skil action!");
                lastPressAvailableTime = Time.time;
            }
            return lastPressAvailableTime;
        }
    }
}
