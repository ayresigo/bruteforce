using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Indentity")]
    public int id;
    public int ownerId;
    public string name;
    public string surname;
    public string creationDate;
    public Rarity rarity;
    public Gender gender;

    [Header("Inheritment")]
    public Race.RaceName race;
    public Job.JobName job;

    [Header("Attributes")]
    public int health;
    public int attack;
    public int magic;
    public int defense;
    public int resistance;
    public int critchance;
    public int accuracy;
    public int evade;
    public int critdmg;
    public int pureness;

    [Header("Skillset")]
    public Skill.SkillName basicAttack;
    public Skill.SkillName activeSkill;

    [Header("Visuals")]
    public string head;
    public string eyebrow;
    public string facialHair;
    public string torso;
    public string arm_Upper_Right;
    public string arm_Upper_Left;
    public string arm_Lower_Right;
    public string arm_Lower_Left;
    public string hand_Right;
    public string hand_Left;
    public string hips;
    public string leg_Right;
    public string leg_Left;
    public string headCoverings;
    public string hair;
    public string head_Attachment;
    public string back_Attachment;
    public string shoulder_Attachment_Right;
    public string shoulder_Attachment_Left;
    public string elbow_Attachment_Right;
    public string elbow_Attachment_Left;
    public string hips_Attachment;
    public string knee_Attachement_Right;
    public string knee_Attachement_Left;
    public string elf_Ear;

    public enum Rarity
    {   
        Default,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythic,
        Eternal
    }

    public enum Gender
    {
        Male,
        Female
    }

    public void passData(Character target)
    {
        target.id = id;
        target.ownerId = ownerId;
        target.name = name;
        target.surname = surname;
        target.creationDate = creationDate;
        target.rarity = rarity;
        target.rarity = rarity;
        target.job = job;
        target.health = health;
        target.attack = attack;
        target.defense = defense;
        target.resistance = resistance;
        target.critchance = critchance;
        target.accuracy = accuracy;
        target.evade = evade;
        target.critdmg = critdmg;
        target.pureness = pureness;
        target.basicAttack = basicAttack;
        target.activeSkill = activeSkill;
        target.head = head;
        target.eyebrow = eyebrow;
        target.facialHair = facialHair;
        target.torso = torso;
        target.arm_Upper_Right = arm_Upper_Right;
        target.arm_Upper_Left = arm_Upper_Left;
        target.arm_Lower_Right = arm_Lower_Right;
        target.arm_Lower_Left = arm_Lower_Left;
        target.hand_Right = hand_Right;
        target.hand_Left = hand_Left;
        target.hips = hips;
        target.leg_Right = leg_Right;
        target.leg_Left = leg_Left;
        target.headCoverings = headCoverings;
        target.hair = hair;
        target.head_Attachment = head_Attachment;
        target.back_Attachment = back_Attachment;
        target.shoulder_Attachment_Right = shoulder_Attachment_Right;
        target.shoulder_Attachment_Left = shoulder_Attachment_Left;
        target.elbow_Attachment_Right = elbow_Attachment_Left;
        target.hips_Attachment = hips_Attachment;
        target.knee_Attachement_Right = knee_Attachement_Right;
        target.knee_Attachement_Left = knee_Attachement_Left;
        target.elf_Ear = elf_Ear;
        /*target.shapeMesh = shapeMesh;
        target.shapeMaterial = shapeMaterial;
        target.shapeColor = shapeColor;*/
    }
}
