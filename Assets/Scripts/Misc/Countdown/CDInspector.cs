using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CDInspector : MonoBehaviour, ICountdown {
    [SerializeField]
    private UnityEvent OnCount;
    [SerializeField]
    private UnityEvent OnFinal;
    [SerializeField]
    private bool countOnFinal;

	public void Countdown(int count, int initialCount) {
        if(count == 0) {
            OnFinal.Invoke();
        }

        if(count > 0 || countOnFinal){
            OnCount.Invoke();
        }
    }
}
