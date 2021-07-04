using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public Character[] playerTeam, enemyTeam, characters;
    public GameObject[] charInstance;
    public Skill[] skillList;

    // [ 0, 1, 2 ] vs [ 0, 1, 2 ]   codigo
    // [ 2, 1, 0 ]    [ 0, 1, 2 ]   render
    //   3, 2, 1        4, 5, 6     turno
    //   0, 1, 2        3, 4, 5     characters[]
    //   2, 1, 0        3, 4, 5     characters[] render


    //pega cada skill de cada personagem
    //pega as informações (como target e efeito) e aplica do dono para o target (com a informaçao anterior)

    public int turn = 0;
    public GameObject currentActor;
    public Skill action;
    public GameObject[] target;

    public void SimulateBattle()
    {
        castSkill(currentActor, action);
    }

    private void Start()
    {

        StartCoroutine(StartBattle());
    }

    void castSkill(GameObject caster, Skill skill)
    {
        switch (skill.type)
        {
            case Skill.Type.BasicAttack:
                //caster.GetComponent<Character>().basicAttack.Cast(caster, getTarget(charInstance, caster, caster.GetComponent<Character>().basicAttack));
                break;

            case Skill.Type.Active:
                //float reqMana = caster.GetComponent<Character>().basicAttack.reqMana;
                float currentMana = caster.GetComponent<CharacterController>().currentMana;
                //if(currentMana >= reqMana)
                {
                    //caster.GetComponent<Character>().activeSkill.Cast(caster, getTarget(charInstance, caster, caster.GetComponent<Character>().basicAttack));
                    caster.GetComponent<CharacterController>().currentMana = 0;
                }
                break;            

            case Skill.Type.Passive:
                break;
            default:
                break;
        }            
    }
    GameObject[] getTarget(GameObject[] characters, GameObject caster, Skill skill)
    {   
        GameObject[] returnObject;
        int team = 0, allyCount = 0, enemyCount = 0;
        int casterPosition = 0,
            firstValidAllyPosition = 0, secondValidAllyPosition = 0, ThirdValidAllyPosition = 0,
            firstValidEnemyPsotition = 0, secondValidEnemyPosition = 0, ThirdValidEnemyPosition = 0;


        for (int i = 0; i < characters.Length; i++)
        {
            if (caster == characters[i])
            {
                casterPosition = i;

                if (i < 3)
                    team = 1;
                else
                    team = 2;

                Debug.Log("The caster is on the " + i + " position of the battlefield and belongs to the Team "+team);
            }
            if(characters[i].GetComponent<CharacterController>().isaValidTarget)
            {
                if (caster.GetComponent<CharacterController>().team == characters[i].GetComponent<CharacterController>().team && caster != characters[i])
                {
                    switch(allyCount)
                    {
                        case 0:
                            firstValidAllyPosition = i;
                            allyCount++;
                            Debug.Log(characters[i].name + " is the first valid ally to be targeted");
                            break;
                        case 1:
                            secondValidAllyPosition = i;
                            allyCount++;
                            Debug.Log(characters[i].name + " is the second valid ally to be targeted");
                            break;
                        case 2:
                            ThirdValidAllyPosition = i;
                            Debug.Log(characters[i].name + " is the third valid ally to be targeted");
                            break;
                        default:
                            Debug.LogError("Thats not right.");
                            break;
                    }
                } else if (caster.GetComponent<CharacterController>().team != characters[i].GetComponent<CharacterController>().team && caster != characters[i])
                {
                    switch (enemyCount)
                    {
                        case 0:
                            firstValidEnemyPsotition = i;
                            enemyCount++;
                            Debug.Log(characters[i].name + " is the first valid enemy to be targeted");
                            break;
                        case 1:
                            secondValidEnemyPosition = i;
                            enemyCount++;
                            Debug.Log(characters[i].name + " is the second valid enemy to be targeted");
                            break;
                        case 2:
                            ThirdValidEnemyPosition = i;
                            Debug.Log(characters[i].name + " is the third valid enemy to be targeted");
                            break;
                        default:
                            Debug.LogError("Thats not right.");
                            break;
                    }
                }
            }
        }

        switch (skill.targetType)
        {
            case Skill.TargetType.Self:
                returnObject = new GameObject[1];
                returnObject[0] = characters[casterPosition];
                return returnObject;

            case Skill.TargetType.FirstAlly:
                returnObject = new GameObject[1];
                returnObject[0] = characters[firstValidAllyPosition];
                return returnObject;

            case Skill.TargetType.SecondAlly:
                returnObject = new GameObject[1];
                returnObject[0] = characters[secondValidAllyPosition];
                return returnObject;

            case Skill.TargetType.ThirdAlly:
                returnObject = new GameObject[1];
                returnObject[0] = characters[ThirdValidAllyPosition];
                return returnObject;

            case Skill.TargetType.FirstEnemy:
                returnObject = new GameObject[1];
                returnObject[0] = characters[firstValidEnemyPsotition];
                return returnObject;

            case Skill.TargetType.SecondEnemy:
                returnObject = new GameObject[1];
                returnObject[0] = characters[secondValidEnemyPosition];
                return returnObject;

            case Skill.TargetType.ThirdEnemy:
                returnObject = new GameObject[1];
                returnObject[0] = characters[ThirdValidEnemyPosition];
                return returnObject;
            default:
                break;
        }
        return characters;
    }


    IEnumerator StartBattle()
    {
        characters = new Character[6];
        charInstance = new GameObject[6];

        for (int i = 0; i < 6; i++)
        {
            if (i < 3)
                characters[i] = playerTeam[i];
            else
                characters[i] = enemyTeam[i-3];

            charInstance[i] = SpawnCharacter(characters[i]);
        }
        turn = 1;
        yield return new WaitForSeconds(1f);
    }


    GameObject SpawnCharacter(Character character)
    {
        GameObject newCharacter = new GameObject();
        newCharacter.AddComponent<Character>();
        newCharacter.AddComponent<CharacterController>();
        newCharacter.AddComponent<MeshRenderer>();
        newCharacter.AddComponent<MeshFilter>();
        newCharacter.AddComponent<BaseAttack>(); ///////////////////////////CRIAR MÉTODO PARA RECUPERAR SKILLS DE CADA CHARACTER//////////////////////////////

        Character characterComponent = newCharacter.GetComponent<Character>();
        CharacterController characterControllerComponent = newCharacter.GetComponent<CharacterController>();
        MeshRenderer meshRendererComponent = newCharacter.GetComponent<MeshRenderer>();
        MeshFilter meshFilterComponent = newCharacter.GetComponent<MeshFilter>();

        newCharacter.name = character.name + character.surname;
        characterControllerComponent.character = newCharacter;
        characterControllerComponent.currentHealth = character.GetComponent<Character>().health;
        return newCharacter;
    }

    #region comment
    /*void Attack(Character attacker, Character[] target)
    {
        Debug.Log(attacker.name + " used " + attacker.basicAttack.name + " to attack: ");
        for(int i = 0; i < target.Length; i++)
        {
            Debug.Log(target[i].name + "("+target[i].currentHealth+" health)");
            Debug.Log("Dealing xxx damage.");
        }
    }*/


    /*public void startBattle()
    {
        int turn = 1;
        while (turn != 0)
        {
            if (turn == 1)
            {
                //playerTeam[0].basicAttack.Cast(playerTeam[0], enemyTeam);
                if (enemyTeam[0].health <= 0)
                {
                    Debug.LogWarning(enemyTeam[0].name + " died.");
                    turn = 0;
                }
                else
                {
                    turn = 2;
                    turn = 0;
                }
            }
            else
            {
                //enemyTeam[0].basicAttack.Cast(enemyTeam[0], playerTeam);
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
    }*/
    #endregion
}
