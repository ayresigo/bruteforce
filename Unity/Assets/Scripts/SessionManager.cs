﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SessionManager : MonoBehaviour
{
    [ReadOnly] public int accountId;
    [ReadOnly] public string login;
    [ReadOnly] public string creationDate;
    [ReadOnly] public List<Character> characters;

    private void Start()
    {
        characters = new List<Character>();
    }

    [Serializable]
    public class InventoryList
    {
        public CharacterJSON[] characters;
    }

    [Serializable]
    public class CharacterJSON
    {
        public int id, fk_owner_id;
        public string creation_date, name, surname, shape, job, basic_attack, active_skill;
        public int health, attack, magic, defense, resistance, critchance, critdmg, evade;
    }

    public IEnumerator getInventory(int account_id, GameObject session)
    {
        string inventory_info_json;
        WWWForm form = new WWWForm();
        form.AddField("account_id", account_id);
        UnityWebRequest www = UnityWebRequest.Post("https://bruteforcegame.000webhostapp.com/get_inventory.php", form);
        yield return www.SendWebRequest();
        if (www.isDone)
        {
            inventory_info_json = www.downloadHandler.text;
            InventoryList inventory = JsonUtility.FromJson<InventoryList>("{\"characters\": " + inventory_info_json + "}");
            for (int i = 0; i < inventory.characters.Length; i++)
            {
                Debug.Log("{\"characters\": " + inventory_info_json + "}");
                session.GetComponent<SessionManager>().InstantiateCharacter(inventory.characters[i], session);
            }
        }
        else
            Debug.LogError(www.error);
    }

   
    Skill.SkillName stringToSkillName(string skillNameString)
    {
        Skill.SkillName parsed_skillName = (Skill.SkillName)System.Enum.Parse(typeof(Skill.SkillName), skillNameString);
        return parsed_skillName;
    }

    public void InstantiateCharacter(CharacterJSON character, GameObject session)
    {
        GameObject newCharacter = new GameObject();
        newCharacter.AddComponent<Character>();
        newCharacter.name = "["+character.id+"]"+character.name+character.surname;
        Character newCharacterObject = newCharacter.GetComponent<Character>();
        newCharacterObject.id = character.id;
        newCharacterObject.ownerId = character.fk_owner_id;
        newCharacterObject.creationDate = character.creation_date;
        newCharacterObject.name = character.name;
        newCharacterObject.surname = character.surname;
        //newCharacterObject.shape = character.shape;
        //newCharacterObject.job = character.job;
        newCharacterObject.basicAttack = stringToSkillName(character.basic_attack);
        //newCharacterObject.activeSkill = character.active_skill;
        newCharacterObject.health = character.health;
        newCharacterObject.attack = character.attack;
        newCharacterObject.magic = character.magic;
        newCharacterObject.defense = character.defense;
        newCharacterObject.resistance = character.resistance;
        newCharacterObject.critchance = character.critchance;
        newCharacterObject.critdmg = character.critdmg;
        newCharacterObject.evade = character.evade;
        session.GetComponent<SessionManager>().characters.Add(newCharacterObject);
    }
}
