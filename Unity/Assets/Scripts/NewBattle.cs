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
        int alliesCount = 0, enemiesCount = 0;
        GameObject[,] targets = new GameObject[System.Enum.GetNames(typeof(Skill.TargetType)).Length,2];
        GameObject[] regurnGO;

        /*  0   None,
            
            3   RandomAlly,
            4   FirstAlly,
            5   SecondAlly,
            6   ThirdAlly,
            7   RandomAllies,
            8   FirstTwoAllies,
            9   LastTwoAllies,
            10  EdgeAllies,
            11  AllAllies,
            12  LowestHealthAlly,
            13  HighestHealthAlly,
            14  LowestAttackAlly,
            15  HighestAttackAlly,
            16  LowestDefenseAlly,
            17  HighestDefenseAlly,
            18  RandomEnemy,
            19  FirstEnemy,
            20  SecondEnemy,
            21  ThirdEnemy,
            22  RandomEnemies,
            23  FirstTwoEnemies,
            24  LastTwoEnemies,
            25  EdgeEnemies,
            26  AllEnemies,
            27  MirrorSideEnemy,
            28  LowestHealthEnemy,
            29  HighestHealthEnemy,       
            30  LowestAttackEnemy,
            31  HighestAttackEnemy,
            32  LowestDefenseEnemy,
            33  HighestDefenseEnemy */

        for (int i = 0; i < characters.Length; i++)
        {
            if (validTarget(characters[i])) // considerar apenas se for um target válido
            {
                if (characters[i] == caster) // começa a varredura checando se é o caster
                {
                    if (alliesCount == 0) // se o caster for o primeiro ou o único target válido, ele também será a unica opção de target ally
                    {
                        for (int j = (int)Skill.TargetType.Self; j <= (int)Skill.TargetType.HighestDefenseAlly; j++) 
                        {
                            targets[j, 0] = characters[i]; // é a unica opção de ally até agora
                        }
                    } else // se existir um outro ally válido e já encontrado...
                    {
                        targets[(int)Skill.TargetType.Self, 0] = characters[i];
                        targets[(int)Skill.TargetType.Caster, 0] = characters[i];
                    }                    
                } else if (characters[i].GetComponent<CharacterController>().team == casterTeam && characters[i] != caster)
                {
                    //  se o character atual não for o caster e for aliado...
                    switch (alliesCount)
                    {
                        case 0: // se for o primeiro aliado encontrado:
                            for (int j = (int)Skill.TargetType.RandomAlly; j <= (int)Skill.TargetType.HighestDefenseAlly; j++)
                            {
                                // IMPORTANTE:  RandomAlly          -> primeira opção de ally do enum
                                //              HighestDefenseAlly  -> ultima opção de ally do enum

                                // o primeiro aliado válido é a unica opção de aliado no target type
                                // OBS: se o caster ja foi encontrado, ele não será o primeiro aliado.
                                // Sendo assim, o targets sobrescreverá as opções de ally que foi escrita pelo caster

                                targets[j, 0] = characters[i];
                            }
                            alliesCount = 1;
                            break;
                        case 1: // se ja tiver sido encontrado um aliado...
                            for (int j = (int)Skill.TargetType.RandomAlly; j <= (int)Skill.TargetType.HighestDefenseAlly; j++)
                            {
                                switch(j)
                                {
                                    case (int)Skill.TargetType.RandomAlly: targets[j, 1] = characters[i]; break;
                                    case (int)Skill.TargetType.SecondAlly: targets[j, 0] = characters[i]; break;
                                    case (int)Skill.TargetType.ThirdAlly: targets[j, 0] = characters[i]; break; // recebe temporariamente (ou nao) como third ally, caso o próximo esteja morto
                                    case (int)Skill.TargetType.RandomAllies: targets[j, 1] = characters[i]; break;
                                    case (int)Skill.TargetType.FirstTwoAllies: targets[j, 1] = characters[i]; break;
                                    case (int)Skill.TargetType.LastTwoAllies: targets[j, 1] = characters[i]; break; // recebe temporariamente também
                                    case (int)Skill.TargetType.EdgeAllies: targets[j, 1] = characters[i]; break; // recebe temporariamente também
                                    case (int)Skill.TargetType.AllAllies: targets[j, 1] = characters[i]; break;
                                    case (int)Skill.TargetType.LowestHealthAlly: if (compareAttributes(characters[i], targets[j, 0], "health", "lower")) targets[j, 0] = characters[i]; break;
                                    case (int)Skill.TargetType.HighestHealthAlly: if (compareAttributes(characters[i], targets[j, 0], "health")) targets[j, 0] = characters[i]; break;
                                    default: break;
                                }
                            }
                            alliesCount = 2;
                            break;
                        case 2:
                            for (int j = (int)Skill.TargetType.RandomAlly; j <= (int)Skill.TargetType.HighestDefenseAlly; j++)
                            {
                                switch(j)
                                {
                                    case (int)Skill.TargetType.RandomAlly: targets[j, 2] = characters[i]; break;
                                    case (int)Skill.TargetType.ThirdAlly: targets[j, 0] = characters[i]; break;
                                    case (int)Skill.TargetType.RandomAllies: targets[j, 2] = characters[i]; break;
                                    case (int)Skill.TargetType.LastTwoAllies: targets[j, 1] = characters[i]; break;
                                    case (int)Skill.TargetType.EdgeAllies: targets[j, 1] = characters[i]; break;
                                    case (int)Skill.TargetType.AllAllies: targets[j, 2] = characters[i]; break;
                                    case (int)Skill.TargetType.LowestHealthAlly: if (compareAttributes(characters[i], targets[j, 0], "health", "lower")) targets[j, 0] = characters[i]; break;
                                    case (int)Skill.TargetType.HighestHealthAlly: if (compareAttributes(characters[i], targets[j, 0], "health")) targets[j, 0] = characters[i]; break;
                                    default: break;
                                }
                            }
                            alliesCount = 3;
                            break;
                    }          
                } else // se for do time inimigo...
                {

                }
            } 
        }
    }

    bool compareAttributes (GameObject char1, GameObject char2, string att, string op = "higher")
    {
        int _att1 = 0, _att2 = 0;
        switch (att)
        {
            case "health":
                _att1 = char1.GetComponent<CharacterController>().currentHealth;
                _att2 = char2.GetComponent<CharacterController>().currentHealth;
                break;
        }

        switch (op)
        {
            case "higher":
                if (_att1 > _att2)
                    return true;
                else
                    return false;
            case "lower":
                if (_att1 < _att2)
                    return true;
                else
                    return false;
        }
        return false;
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
