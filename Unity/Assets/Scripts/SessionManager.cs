using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SessionManager : MonoBehaviour
{ 
    public int accountId;
    public string login;
    public string creationDate;
    public int privilegeLevel;
    public int level;
    public int energy;
    public int gold;
    public int diamond;
    public Character char1, char2, char3;
    public List<Character> characters;
    public GameObject loginPopup, lobby, common, modularCharacter;

    private void Start()
    {
        Debug.Log("Starting Session Manager...");
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
        public string creation_date, name, surname, rarity, gender, race, job, basic_attack, active_skill;
        public int health, attack, magic, defense, resistance, critchance, critdmg, evade;
        public string head, eyebrow, facialHair, torso, arm_Upper_Right, arm_Upper_Left, arm_Lower_Right,
            arm_Lower_Left, hand_Right, hand_Left, hips, leg_Right, leg_Left, headCoverings, hair,
            head_Attachment, back_Attachment, shoulder_Attachment_Right, shoulder_Attachment_Left,
            elbow_Attachment_Right, elbow_Attachment_Left, hips_Attachment, knee_Attachement_Right,
            knee_Attachement_Left, elf_Ear;
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
            Debug.Log("{\"characters\": " + inventory_info_json + "}");
            InventoryList inventory = JsonUtility.FromJson<InventoryList>("{\"characters\": " + inventory_info_json + "}");

            for (int i = 0; i < inventory.characters.Length; i++)
            {
                session.GetComponent<SessionManager>().InstantiateCharacter(inventory.characters[i], session);
            }

            modularCharacter = lobby.GetComponent<LobbyController>().modularCharacter;
            modularCharacter.SetActive(true);
            modularCharacter.GetComponent<ModularCharacterRenderer>().session = session;
            modularCharacter.GetComponent<ModularCharacterRenderer>().startLists = true;
        }
        else
            Debug.LogError(www.error);

        if (loginPopup.active)
        {
            common.SetActive(true);
            lobby.SetActive(true);
            loginPopup.SetActive(false);            
        }

        www.Dispose();
    }

    Character.Rarity stringToRarity(string rarityString)
    {
        Character.Rarity parsed_rarity = (Character.Rarity)System.Enum.Parse(typeof(Character.Rarity), rarityString);
        return parsed_rarity;
    }
    Character.Gender stringToGender(string genderString)
    {
        Character.Gender parsed_gender = (Character.Gender)System.Enum.Parse(typeof(Character.Gender), genderString);
        return parsed_gender;
    }
    Race.RaceName stringToRaceName(string raceNameString)
    {
        Race.RaceName parsed_raceName = (Race.RaceName)System.Enum.Parse(typeof(Race.RaceName), raceNameString);
        return parsed_raceName;
    }
    Job.JobName stringToJobName(string jobNameString)
    {
        Job.JobName parsed_jobName = (Job.JobName)System.Enum.Parse(typeof(Job.JobName), jobNameString);
        return parsed_jobName;
    }
    Skill.SkillName stringToSkillName(string skillNameString)
    {
        Skill.SkillName parsed_skillName = (Skill.SkillName)System.Enum.Parse(typeof(Skill.SkillName), skillNameString);
        return parsed_skillName;
    }

    public void InstantiateCharacter(CharacterJSON character,  GameObject session)
    {
        GameObject newCharacter = new GameObject();
        newCharacter.AddComponent<Character>();
        newCharacter.transform.SetParent(session.transform);
        newCharacter.name = "[ "+character.id+" ] "+character.name+character.surname;
        Character newCharacterObject = newCharacter.GetComponent<Character>();
        newCharacterObject.id = character.id;
        newCharacterObject.ownerId = character.fk_owner_id;
        newCharacterObject.creationDate = character.creation_date;
        newCharacterObject.rarity = stringToRarity(character.rarity);
        newCharacterObject.gender = stringToGender(character.gender);
        newCharacterObject.name = character.name;
        newCharacterObject.surname = character.surname;
        newCharacterObject.race = stringToRaceName(character.race);
        newCharacterObject.job = stringToJobName(character.job);
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

        newCharacterObject.head = character.head;
        newCharacterObject.eyebrow = character.eyebrow;
        newCharacterObject.facialHair = character.facialHair;
        newCharacterObject.torso = character.torso;
        newCharacterObject.arm_Upper_Right = character.arm_Upper_Right;
        newCharacterObject.arm_Upper_Left = character.arm_Upper_Left;
        newCharacterObject.arm_Lower_Right = character.arm_Lower_Right;
        newCharacterObject.arm_Lower_Left = character.arm_Lower_Left;
        newCharacterObject.hand_Right = character.hand_Right;
        newCharacterObject.hand_Left = character.hand_Left;
        newCharacterObject.hips = character.hips;
        newCharacterObject.leg_Right = character.leg_Right;
        newCharacterObject.leg_Left = character.leg_Left;
        newCharacterObject.headCoverings = character.headCoverings;
        newCharacterObject.hair = character.hair;
        newCharacterObject.head_Attachment = character.head_Attachment;
        newCharacterObject.back_Attachment = character.back_Attachment;
        newCharacterObject.shoulder_Attachment_Right = character.shoulder_Attachment_Right;
        newCharacterObject.shoulder_Attachment_Left = character.shoulder_Attachment_Left;
        newCharacterObject.elbow_Attachment_Right = character.elbow_Attachment_Left;
        newCharacterObject.hips_Attachment = character.hips_Attachment;
        newCharacterObject.knee_Attachement_Right = character.knee_Attachement_Right;
        newCharacterObject.knee_Attachement_Left = character.knee_Attachement_Left;
        newCharacterObject.elf_Ear = character.elf_Ear;

        session.GetComponent<SessionManager>().characters.Add(newCharacterObject);
    }
}
