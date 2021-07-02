using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    [Header("Possibles choices")]
    public Race[] races;
    public Job[] jobs;

    [Header("Inheritment")]
    public Character dad;
    public Character mom;

    [Header("Identity")]
    public int id;
    public string name;
    public string surname;
    public Race race;
    public Job job;

    public ActiveSkill activeSkill;
    public PassiveSkill passiveSkill;

    [Header("Screen elements")]
    public Button breedButton;

    #region Create New Character
    public void CreateCharacter()
    {
        GameObject createdCharacter = new GameObject();
        createdCharacter.AddComponent<Character>();
        createdCharacter.name = "Created Character";
        Character newChar = createdCharacter.GetComponent<Character>();
        newChar.id = id;
        newChar.name = name;
        newChar.surname = surname;
        newChar.race = SelectRace();
        newChar.job = SelectJob();
        //newChar.activeSkill = newChar.race.skills[Random.Range(0,race.skills.Length)];
        //newChar.passiveSkill = passiveSkill;
        
        newChar.health = newChar.race.health + newChar.job.health;
        newChar.mana = newChar.race.mana + newChar.job.mana;
        newChar.manaspd = newChar.race.manaspd + newChar.job.manaspd;
        newChar.attack = newChar.race.attack + newChar.job.attack;
        newChar.magic = newChar.race.magic + newChar.job.magic;
        newChar.defense = newChar.race.defense + newChar.job.defense;
        newChar.attackspd = newChar.race.attackspd + newChar.job.attackspd;
        newChar.critchance = newChar.race.critchance + newChar.job.critchance;
        newChar.critdmg = newChar.race.critdmg + newChar.job.critdmg;

        Job SelectJob()
        {
            int randomJob = (Random.Range(0, jobs.Length));
            Debug.Log("Picked Job: " + races[randomJob]);
            return jobs[randomJob];
        }

        Race SelectRace()
        {
            int randomRace = (Random.Range(0, races.Length));
            Debug.Log("Picked Race: " + jobs[randomRace]);
            return races[randomRace];
        }

}
    #endregion

    #region Breeding
    public void Breed(Character dad, Character mom)
    {
        surname = chooseSurname(dad.surname, mom.surname);
        race = chooseRace(dad.race, mom.race);
        job = chooseJob(dad.job, mom.job);
    }

    private string chooseSurname(string dadSurname, string momSurname)
    {
        int _randomSurname = Random.Range(0, 2);
        Debug.Log("Surname pick: " + _randomSurname);
        switch (_randomSurname)
        {
            case 1:
                return dadSurname;
            case 2:
                return momSurname;
            default:
                return null;
        }
    }
    private Race chooseRace(Race dadRace, Race momRace)
    {
        int _randomRace = Random.Range(0, 2);
        Debug.Log("Race pick: " + _randomRace);
        switch (_randomRace)
        {
            case 1:
                return dadRace;
            case 2:
                return momRace;
            default:
                return null;
        }
    }
    private Job chooseJob(Job dadJob, Job momJob)
    {
        int _randomJob = Random.Range(0, 100);
        Debug.Log("Job pick: " + _randomJob);
        // 90% de chances de herdar o job (45% do pai, 45% da mãe). 10% de breedar com uma classe diferente dos pais.

        if (_randomJob < 45) 
        {
            return dadJob;
        } else if (_randomJob < 90)
        {
            return momJob;
        } else
        {
            return jobs[Random.Range(0, jobs.Length)];
        }
    }
    #endregion
}
