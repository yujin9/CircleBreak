using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyToggles : MonoBehaviour
{
    [SerializeField] private EnemyList _enemyList;
    [SerializeField] private Toggle[] toggles;

    private void Awake() {
        for(var i = 0 ; i < toggles.Length; i++){
            toggles[i].onValueChanged.AddListener(_enemyList.array[i].SetActive);
        }
    }
}
