using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject characterObject;
    private CharacterController characterController;
    private Character character;
    public float currentHealth;
    public float currentMana;
    public bool isDead, isaValidTarget;
    public int team;    

    // Tentar fazer com que os personagens recebam suas skills como no skill manager no inicio de cada batalha
    // Tendo assim um componente com o script especifico de cada skill salvo.

    private void Start()
    {
        characterController = characterObject.GetComponent<CharacterController>();
        character = characterObject.GetComponent<Character>();

        characterController.currentHealth = character.health;
        characterController.currentMana = 0;
        characterController.isDead = false;
        characterController.isaValidTarget = true;
    }

    

    public bool takeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            isDead = true;
            isaValidTarget = false;
        }
        else
        {
            isDead = false;
            isaValidTarget = true;
        }            
        return isDead;
    }
}
