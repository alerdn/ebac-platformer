using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    public GameObject uiEndgame;
    public GameObject enemies;

    private void Update()
    {
        CallEndgame();
    }

    public void CallEndgame()
    {
        if (enemies.GetComponentsInChildren<EnemyBase>()?.Count() != 0) return;

        PauseManager.Instance.Pause();
        uiEndgame.SetActive(true);
    }
}
