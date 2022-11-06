using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoseGame : MonoBehaviour
{
    public Player player;
    public GameObject uILoseGame;

    private void Awake()
    {
        player.OnPlayerDead += CallLoseGame;
    }

    public void CallLoseGame()
    {
        uILoseGame.SetActive(true);
    }
}
