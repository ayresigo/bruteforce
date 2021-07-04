using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    [Header("Possibles choices")]
    public Shape[] shapes;
    public Job[] jobs;

    [Header("Inheritment")]
    public Character dad;
    public Character mom;

    [Header("Identity")]
    public string id;
    public string name;
    public string surname;
    public Shape shape;
    public Job job;


    [Header("Screen elements")]
    public Button breedButton;

    #region Create New Character
    public void CreateCharacter()
    {
        GameObject createdCharacter = new GameObject();
        createdCharacter.AddComponent<Character>();
        createdCharacter.AddComponent<MeshRenderer>();
        createdCharacter.AddComponent<MeshFilter>();
        createdCharacter.name = "Created Character";
        Character characterComponent = createdCharacter.GetComponent<Character>();
        MeshRenderer meshRendererComponent = createdCharacter.GetComponent<MeshRenderer>();
        MeshFilter meshFilterComponent = createdCharacter.GetComponent<MeshFilter>();

        //characterComponent.id = System.Guid.NewGuid().ToString();
        characterComponent.name = name;
        characterComponent.surname = surname;
        characterComponent.shape = SelectShape();
        characterComponent.job = SelectJob();
        //characterComponent.skillset = characterComponent.shape.skillset[0];
        //characterComponent.activeSkill = characterComponent.shape.activeSkills[Random.Range(0, characterComponent.shape.activeSkills.Length)];
        //characterComponent.passiveSkill = characterComponent.job.passiveSkills[Random.Range(0, characterComponent.job.passiveSkills.Length)];
        characterComponent.health = characterComponent.shape.health + characterComponent.job.health;
        //characterComponent.mana = characterComponent.shape.mana + characterComponent.job.mana;
        characterComponent.manaspd = characterComponent.shape.manaspd + characterComponent.job.manaspd;
        characterComponent.attack = characterComponent.shape.attack + characterComponent.job.attack;
        characterComponent.magic = characterComponent.shape.magic + characterComponent.job.magic;
        characterComponent.defense = characterComponent.shape.defense + characterComponent.job.defense;
        characterComponent.attackspd = characterComponent.shape.attackspd + characterComponent.job.attackspd;
        characterComponent.critchance = characterComponent.shape.critchance + characterComponent.job.critchance;
        characterComponent.critdmg = characterComponent.shape.critdmg + characterComponent.job.critdmg;
        characterComponent.shapeMesh = characterComponent.shape.shapeMesh;
        characterComponent.shapeMaterial = characterComponent.job.material;
        characterComponent.shapeColor = characterComponent.job.color;

        meshFilterComponent.sharedMesh = characterComponent.shapeMesh;
        meshRendererComponent.material = characterComponent.shapeMaterial;
        meshRendererComponent.material.color = characterComponent.shapeColor;

        Job SelectJob()
        {
            int randomJob = (Random.Range(0, jobs.Length));
            Debug.Log("Picked Job: " + jobs[randomJob]);
            return jobs[randomJob];
        }

        Shape SelectShape()
        {
            int randomShape = (Random.Range(0, shapes.Length));
            Debug.Log("Picked Shape: " + shapes[randomShape]);
            return shapes[randomShape];
        }

}
    #endregion

    #region Breeding
    public void Breed(Character dad, Character mom)
    {
        surname = chooseSurname(dad.surname, mom.surname);
        shape = chooseShape(dad.shape, mom.shape);
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
    private Shape chooseShape(Shape dadShape, Shape momShape)
    {
        int _randomShape = Random.Range(0, 2);
        Debug.Log("Shape pick: " + _randomShape);
        switch (_randomShape)
        {
            case 1:
                return dadShape;
            case 2:
                return momShape;
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
