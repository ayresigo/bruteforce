using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public Character[] playerTeam, enemyTeam;
    // [ 0, 1, 2 ] vs [ 0, 1, 2 ]
    //   3, 2, 1        4, 5, 6
    
    public enum BattleState
    {
        Starting,
        PlayerTurn,
        EnemyTurn,
        Won,
        Lost,
        Ending
    }





    public void startBattle()
    {
        int turn = 1;
        while (turn != 0)
        {
            if (turn == 1)
            {
                playerTeam[0].basicAttack.Cast(playerTeam[0], enemyTeam);
                if (enemyTeam[0].health <= 0)
                {
                    Debug.LogWarning(enemyTeam[0].name + " died.");
                    turn = 0;
                }
                else
                {
                    turn = 2;
                }
            }
            else
            {
                enemyTeam[0].basicAttack.Cast(enemyTeam[0], playerTeam);
                if (playerTeam[0].health <= 0)
                {
                    Debug.LogWarning(playerTeam[0].name + " died.");
                    turn = 0;
                }
                else
                {
                    turn = 1;
                }
            }
        }
    }
}
