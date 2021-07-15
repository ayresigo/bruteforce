using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBattle : MonoBehaviour
{
    public GameObject session;
    public GameObject[] team1, team2, characters;
    public BattleStatus battleStatus;
    public Skill.Triggers currentTrigger = Skill.Triggers.None;
    public int[] turnOrders;
    public int currentTurn;

    public enum BattleStatus
    {
        Updating,
        PickingTeams,
        BattleStart,
        BattleEnd
    }

    void RollFirstTeam ()
    {
        Debug.Log("Random picking team to start...");
        if (GetPercent(50))
        {
            // team 1 starts
            Debug.Log("Team 1 starts...");
            for (int i = 0; i < 6; i++)
            {
                if (i < 3)
                    characters[i] = team1[i];
                else
                    characters[i] = team2[i-3];
            }
        } else
        {
            // team 2 starts
            Debug.Log("Team 2 starts...");
            for (int i = 0; i < 6; i++)
            {
                if (i < 3)
                    characters[i] = team2[i];
                else
                    characters[i] = team1[i - 3];
            }
        }
        // checa se alguém pode alterar a ordem dos turnos
        battleStatus = UpdateBattleStatus(BattleStatus.PickingTeams);
    }

    bool validTarget(GameObject t)
    {
        CharacterController _t = t.GetComponent<CharacterController>();
        if (_t.isaValidTarget && !_t.isDead)
            return true;
        else
            return false;
    }

    void getTarget (GameObject caster, Skill skill)
    {
        int casterTeam = caster.GetComponent<CharacterController>().team;



        /*
        if ((int)skill.targetType >= 0 && (int)skill.targetType < 3) // none, self and caster target
        {

        } else if ((int)skill.targetType >= 3 && (int)skill.targetType < 18) // allies target
        {

        } else if ((int)skill.targetType >= 18 && (int)skill.targetType < 34) // enemies target
        {

        }
        */
    }

    void StartBattle ()
    {
        // spawn characters, chose first turn and add characterController component for each character
    }

    BattleStatus UpdateBattleStatus(BattleStatus status = BattleStatus.Updating)
    {
        // checks for triggering
        for (int i = 0; i < 6; i++) // characters
        {
            for (int j = 0; j < 6; j++) // characters.skill
            {
                    // if skill trigger = triggers[] or battleStatus
                    if (characters[i].GetComponent<Character>().skillSet[j].triggeredBy == currentTrigger || characters[i].GetComponent<Character>().skillSet[j].triggeredBy.ToString() == battleStatus.ToString())
                    {
                        characters[i].GetComponent<Character>().skillSet[j].Cast();
                        return status;
                    
                }
            }
        }

        return status;
    }

    bool GetPercent(int pct)
    {
        bool p = false;
        int roll = Random.Range(0, 100);
        if (roll <= pct)
        {
            p = true;
        }
        return p;
    }
 

}
