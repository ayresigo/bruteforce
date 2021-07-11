using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class ModularCharacterRenderer : MonoBehaviour
{
    public GameObject session;
    public GameObject modularCharacter;
    public Character[] characters;
    public bool startLists = false;

    [Header("Material")]
    public Material mat;

    [Header("Gear Colors")]
    public Color[] primary = { new Color(0.2862745f, 0.4f, 0.4941177f), new Color(0.4392157f, 0.1960784f, 0.172549f), new Color(0.3529412f, 0.3803922f, 0.2705882f), new Color(0.682353f, 0.4392157f, 0.2196079f), new Color(0.4313726f, 0.2313726f, 0.2705882f), new Color(0.5921569f, 0.4941177f, 0.2588235f), new Color(0.482353f, 0.4156863f, 0.3529412f), new Color(0.2352941f, 0.2352941f, 0.2352941f), new Color(0.2313726f, 0.4313726f, 0.4156863f) };
    public Color[] secondary = { new Color(0.7019608f, 0.6235294f, 0.4666667f), new Color(0.7372549f, 0.7372549f, 0.7372549f), new Color(0.1647059f, 0.1647059f, 0.1647059f), new Color(0.2392157f, 0.2509804f, 0.1882353f) };

    [Header("Metal Colors")]
    public Color[] metalPrimary = { new Color(0.6705883f, 0.6705883f, 0.6705883f), new Color(0.5568628f, 0.5960785f, 0.6392157f), new Color(0.5568628f, 0.6235294f, 0.6f), new Color(0.6313726f, 0.6196079f, 0.5568628f), new Color(0.6980392f, 0.6509804f, 0.6196079f) };
    public Color[] metalSecondary = { new Color(0.3921569f, 0.4039216f, 0.4117647f), new Color(0.4784314f, 0.5176471f, 0.5450981f), new Color(0.3764706f, 0.3607843f, 0.3372549f), new Color(0.3254902f, 0.3764706f, 0.3372549f), new Color(0.4f, 0.4039216f, 0.3568628f) };

    [Header("Leather Colors")]
    public Color[] leatherPrimary;
    public Color[] leatherSecondary;

    [Header("Skin Colors")]
    public Color[] whiteSkin = { new Color(1f, 0.8000001f, 0.682353f) };
    public Color[] brownSkin = { new Color(0.8196079f, 0.6352941f, 0.4588236f) };
    public Color[] blackSkin = { new Color(0.5647059f, 0.4078432f, 0.3137255f) };
    public Color[] elfSkin = { new Color(0.9607844f, 0.7843138f, 0.7294118f) };

    [Header("Hair Colors")]
    public Color[] whiteHair = { new Color(0.3098039f, 0.254902f, 0.1764706f), new Color(0.2196079f, 0.2196079f, 0.2196079f), new Color(0.8313726f, 0.6235294f, 0.3607843f), new Color(0.8901961f, 0.7803922f, 0.5490196f), new Color(0.8000001f, 0.8196079f, 0.8078432f), new Color(0.6862745f, 0.4f, 0.2352941f), new Color(0.5450981f, 0.427451f, 0.2156863f), new Color(0.8470589f, 0.4666667f, 0.2470588f) };
    public Color whiteStubble = new Color(0.8039216f, 0.7019608f, 0.6313726f);
    public Color[] brownHair = { new Color(0.3098039f, 0.254902f, 0.1764706f), new Color(0.1764706f, 0.1686275f, 0.1686275f), new Color(0.3843138f, 0.2352941f, 0.0509804f), new Color(0.6196079f, 0.6196079f, 0.6196079f), new Color(0.6196079f, 0.6196079f, 0.6196079f) };
    public Color brownStubble = new Color(0.6588235f, 0.572549f, 0.4627451f);
    public Color[] blackHair = { new Color(0.2431373f, 0.2039216f, 0.145098f), new Color(0.1764706f, 0.1686275f, 0.1686275f), new Color(0.1764706f, 0.1686275f, 0.1686275f) };
    public Color blackStubble = new Color(0.3882353f, 0.2901961f, 0.2470588f);
    public Color[] elfHair = { new Color(0.9764706f, 0.9686275f, 0.9568628f), new Color(0.1764706f, 0.1686275f, 0.1686275f), new Color(0.8980393f, 0.7764707f, 0.6196079f) };
    public Color elfStubble = new Color(0.8627452f, 0.7294118f, 0.6862745f);

    [Header("Scar Colors")]
    public Color whiteScar = new Color(0.9294118f, 0.6862745f, 0.5921569f);
    public Color brownScar = new Color(0.6980392f, 0.5450981f, 0.4f);
    public Color blackScar = new Color(0.4235294f, 0.3176471f, 0.282353f);
    public Color elfScar = new Color(0.8745099f, 0.6588235f, 0.6313726f);

    [Header("Body Art Colors")]
    public Color[] bodyArt = { new Color(0.0509804f, 0.6745098f, 0.9843138f), new Color(0.7215686f, 0.2666667f, 0.2666667f), new Color(0.3058824f, 0.7215686f, 0.6862745f), new Color(0.9254903f, 0.882353f, 0.8509805f), new Color(0.3098039f, 0.7058824f, 0.3137255f), new Color(0.5294118f, 0.3098039f, 0.6470588f), new Color(0.8666667f, 0.7764707f, 0.254902f), new Color(0.2392157f, 0.4588236f, 0.8156863f) };

    // list of enabed objects on character
    public List<GameObject> enabledObjects = new List<GameObject>();

    // character object lists
    // male list
    public CharacterObjectGroups male;

    // female list
    public CharacterObjectGroups female;

    // universal list
    public CharacterObjectListsAllGender allGender;

    private void Start()
    {
        Debug.Log("Starting Modular Character Renderer...");
    }
    private void Update()
    {
        if (startLists)
        {
            Debug.LogWarning("Building lists");
            BuildLists();
            characters = session.GetComponentsInChildren<Character>();
            startLists = false;
        }
        /* if (enabledObjects.Count != 0)
        {
            foreach (GameObject g in enabledObjects)
            {
                g.SetActive(false);
            }
        }

        // clear enabled objects list
        enabledObjects.Clear();
        Debug.Log("Cleaning List...");*/

        //session = GameObject.Find("PlayerSession");        
    }

    GameObject GetParts (GameObject instanciatedCharacter, string part)
    {
        Character character = instanciatedCharacter.GetComponent<Character>();
        switch(part)
        {
            case "head":
                if (character.gender == Character.Gender.Male)
                {
                    for (int i = 0; i < male.headAllElements.Count; i++)
                    {
                        if (character.head == male.headAllElements[i].name)
                        {
                            return male.headAllElements[i];
                        }
                    }
                    for (int i = 0; i < male.headNoElements.Count; i++)
                    {
                        if (character.head == male.headNoElements[i].name)
                        {
                            return male.headNoElements[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < female.headAllElements.Count; i++)
                    {
                        if (character.head == female.headAllElements[i].name)
                        {
                            return female.headAllElements[i];
                        }
                    }
                    for (int i = 0; i < female.headNoElements.Count; i++)
                    {
                        if (character.head == female.headNoElements[i].name)
                        {
                            return female.headNoElements[i];
                        }
                    }
                }
                return null;

            case "eyebrow":
                if (character.gender == Character.Gender.Male)
                {
                    for (int i = 0; i < male.eyebrow.Count; i++)
                    {
                        if (character.eyebrow == male.eyebrow[i].name)
                        {
                            return male.eyebrow[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < female.eyebrow.Count; i++)
                    {
                        if (character.eyebrow == female.eyebrow[i].name)
                        {
                            return female.eyebrow[i];
                        }
                    }
                }
                return null;

            case "facialHair":
                if (character.gender == Character.Gender.Male)
                {
                    for (int i = 0; i < male.facialHair.Count; i++)
                    {
                        if (character.facialHair == male.facialHair[i].name)
                        {
                            return male.facialHair[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < female.facialHair.Count; i++)
                    {
                        if (character.facialHair == female.facialHair[i].name)
                        {
                            return female.facialHair[i];
                        }
                    }
                }
                return null;

            case "torso":
                if (character.gender == Character.Gender.Male)
                {
                    for (int i = 0; i < male.torso.Count; i++)
                    {
                        if (character.torso == male.torso[i].name)
                        {
                            return male.torso[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < female.torso.Count; i++)
                    {
                        if (character.torso == female.torso[i].name)
                        {
                            return female.torso[i];
                        }
                    }
                }
                return null;

            case "arm_Upper_Right":
                if (character.gender == Character.Gender.Male)
                {
                    for (int i = 0; i < male.arm_Upper_Right.Count; i++)
                    {
                        if (character.arm_Upper_Right == male.arm_Upper_Right[i].name)
                        {
                            return male.arm_Upper_Right[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < female.arm_Upper_Right.Count; i++)
                    {
                        if (character.arm_Upper_Right == female.arm_Upper_Right[i].name)
                        {
                            return female.arm_Upper_Right[i];
                        }
                    }
                }
                return null;

            case "arm_Upper_Left":
                if (character.gender == Character.Gender.Male)
                {
                    for (int i = 0; i < male.arm_Upper_Left.Count; i++)
                    {
                        if (character.arm_Upper_Left == male.arm_Upper_Left[i].name)
                        {
                            return male.arm_Upper_Left[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < female.arm_Upper_Left.Count; i++)
                    {
                        if (character.arm_Upper_Left == female.arm_Upper_Left[i].name)
                        {
                            return female.arm_Upper_Left[i];
                        }
                    }
                }
                return null;

            case "arm_Lower_Right":
                if (character.gender == Character.Gender.Male)
                {
                    for (int i = 0; i < male.arm_Lower_Right.Count; i++)
                    {
                        if (character.arm_Lower_Right == male.arm_Lower_Right[i].name)
                        {
                            return male.arm_Lower_Right[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < female.arm_Lower_Right.Count; i++)
                    {
                        if (character.arm_Lower_Right == female.arm_Lower_Right[i].name)
                        {
                            return female.arm_Lower_Right[i];
                        }
                    }
                }
                return null;

            case "arm_Lower_Left":
                if (character.gender == Character.Gender.Male)
                {
                    for (int i = 0; i < male.arm_Lower_Left.Count; i++)
                    {
                        if (character.arm_Lower_Left == male.arm_Lower_Left[i].name)
                        {
                            return male.arm_Lower_Left[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < female.arm_Lower_Left.Count; i++)
                    {
                        if (character.arm_Lower_Left == female.arm_Lower_Left[i].name)
                        {
                            return female.arm_Lower_Left[i];
                        }
                    }
                }
                return null;

            case "hand_Right":
                if (character.gender == Character.Gender.Male)
                {
                    for (int i = 0; i < male.hand_Right.Count; i++)
                    {
                        if (character.hand_Right == male.hand_Right[i].name)
                        {
                            return male.hand_Right[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < female.hand_Right.Count; i++)
                    {
                        if (character.hand_Right == female.hand_Right[i].name)
                        {
                            return female.hand_Right[i];
                        }
                    }
                }
                return null;

            case "hand_Left":
                if (character.gender == Character.Gender.Male)
                {
                    for (int i = 0; i < male.hand_Left.Count; i++)
                    {
                        if (character.hand_Left == male.hand_Left[i].name)
                        {
                            return male.hand_Left[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < female.hand_Left.Count; i++)
                    {
                        if (character.hand_Left == female.hand_Left[i].name)
                        {
                            return female.hand_Left[i];
                        }
                    }
                }
                return null;

            case "hips":
                if (character.gender == Character.Gender.Male)
                {
                    for (int i = 0; i < male.hips.Count; i++)
                    {
                        if (character.hips == male.hips[i].name)
                        {
                            return male.hips[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < female.hips.Count; i++)
                    {
                        if (character.hips == female.hips[i].name)
                        {
                            return female.hips[i];
                        }
                    }
                }
                return null;

            case "leg_Right":
                if (character.gender == Character.Gender.Male)
                {
                    for (int i = 0; i < male.leg_Right.Count; i++)
                    {
                        if (character.leg_Right == male.leg_Right[i].name)
                        {
                            return male.leg_Right[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < female.leg_Right.Count; i++)
                    {
                        if (character.leg_Right == female.leg_Right[i].name)
                        {
                            return female.leg_Right[i];
                        }
                    }
                }
                return null;

            case "leg_Left":
                if (character.gender == Character.Gender.Male)
                {
                    for (int i = 0; i < male.leg_Left.Count; i++)
                    {
                        if (character.leg_Left == male.leg_Left[i].name)
                        {
                            return male.leg_Left[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < female.leg_Left.Count; i++)
                    {
                        if (character.leg_Left == female.leg_Left[i].name)
                        {
                            return female.leg_Left[i];
                        }
                    }
                }
                return null;

            case "headCoverings_Base_Hair":
                for (int i = 0; i < allGender.headCoverings_Base_Hair.Count; i++)
                {
                    if(character.headCoverings == allGender.headCoverings_Base_Hair[i].name)
                    {
                        return allGender.headCoverings_Base_Hair[i];
                    }
                }
                for (int i = 0; i < allGender.headCoverings_No_FacialHair.Count; i++)
                {
                    if (character.headCoverings == allGender.headCoverings_No_FacialHair[i].name)
                    {
                        return allGender.headCoverings_No_FacialHair[i];
                    }
                }
                for (int i = 0; i < allGender.headCoverings_No_Hair.Count; i++)
                {
                    if (character.headCoverings == allGender.headCoverings_No_Hair[i].name)
                    {
                        return allGender.headCoverings_No_Hair[i];
                    }
                }
                return null;

            case "all_Hair":
                for(int i = 0; i < allGender.all_Hair.Count; i++)
                {
                    if(character.hair == allGender.all_Hair[i].name)
                    {
                        return allGender.all_Hair[i];
                    }
                }
                return null;

            case "all_Head_Attachment":
                for (int i = 0; i < allGender.all_Head_Attachment.Count; i++)
                {
                    if (character.head_Attachment == allGender.all_Head_Attachment[i].name)
                    {
                        return allGender.all_Head_Attachment[i];
                    }
                }
                return null;

            case "back_Attachment":
                for (int i = 0; i < allGender.back_Attachment.Count; i++)
                {
                    if (character.back_Attachment == allGender.back_Attachment[i].name)
                    {
                        return allGender.back_Attachment[i];
                    }
                }
                return null;

            case "shoulder_Attachment_Right":
                for (int i = 0; i < allGender.shoulder_Attachment_Right.Count; i++)
                {
                    if (character.shoulder_Attachment_Right == allGender.shoulder_Attachment_Right[i].name)
                    {
                        return allGender.shoulder_Attachment_Right[i];
                    }
                }
                return null;

            case "shoulder_Attachment_Left":
                for (int i = 0; i < allGender.shoulder_Attachment_Left.Count; i++)
                {
                    if (character.shoulder_Attachment_Left == allGender.shoulder_Attachment_Left[i].name)
                    {
                        return allGender.shoulder_Attachment_Left[i];
                    }
                }
                return null;

            case "elbow_Attachment_Right":
                for (int i = 0; i < allGender.elbow_Attachment_Right.Count; i++)
                {
                    if (character.elbow_Attachment_Right == allGender.elbow_Attachment_Right[i].name)
                    {
                        return allGender.elbow_Attachment_Right[i];
                    }
                }
                return null;

            case "elbow_Attachment_Left":
                for (int i = 0; i < allGender.elbow_Attachment_Left.Count; i++)
                {
                    if (character.elbow_Attachment_Left == allGender.elbow_Attachment_Left[i].name)
                    {
                        return allGender.elbow_Attachment_Left[i];
                    }
                }
                return null;

            case "hips_Attachment":
                for (int i = 0; i < allGender.hips_Attachment.Count; i++)
                {
                    if (character.hips_Attachment == allGender.hips_Attachment[i].name)
                    {
                        return allGender.hips_Attachment[i];
                    }
                }
                return null;

            case "knee_Attachement_Right":
                for (int i = 0; i < allGender.knee_Attachement_Right.Count; i++)
                {
                    if (character.knee_Attachement_Right == allGender.knee_Attachement_Right[i].name)
                    {
                        return allGender.knee_Attachement_Right[i];
                    }
                }
                return null;

            case "knee_Attachement_Left":
                for (int i = 0; i < allGender.knee_Attachement_Left.Count; i++)
                {
                    if (character.knee_Attachement_Left == allGender.knee_Attachement_Left[i].name)
                    {
                        return allGender.knee_Attachement_Left[i];
                    }
                }
                return null;

            case "elf_Ear":
                for (int i = 0; i < allGender.elf_Ear.Count; i++)
                {
                    if (character.elf_Ear == allGender.elf_Ear[i].name)
                    {
                        return allGender.elf_Ear[i];
                    }
                }
                return null;

            default:
                return null;
        }
    }

    public GameObject RenderCharacter(Character character)
    {
        string[] parts = { "head", "eyebrow", "facialHair", "torso",
            "arm_Upper_Right", "arm_Upper_Left", "arm_Lower_Right", 
            "arm_Lower_Left", "hand_Right", "hand_Left", "hips",
            "leg_Right", "leg_Left", "headCoverings_Base_Hair",
            "headCoverings_No_FacialHair", "headCoverings_No_Hair",
            "all_Hair", "all_Head_Attachment", "chest_Attachment", 
            "back_Attachment", "shoulder_Attachment_Right", 
            "shoulder_Attachment_Left", "elbow_Attachment_Right", 
            "elbow_Attachment_Left", "hips_Attachment", 
            "knee_Attachement_Right", "knee_Attachement_Left", 
            "all_12_Extra", "elf_Ear" };

        modularCharacter.AddComponent<Character>();
        character.passData(modularCharacter.GetComponent<Character>());
        modularCharacter.GetComponent<ModularCharacterRenderer>().enabledObjects = new List<GameObject>();
        for (int i = 0; i < parts.Length; i++)
        {
            GameObject part = GetParts(modularCharacter, parts[i]);
            if (part == null)
            {
                Debug.LogWarning(parts[i]+": null object");
            } else
            { 
                part.SetActive(true);
                modularCharacter.GetComponent<ModularCharacterRenderer>().enabledObjects.Add(part);
            }
        }
        GameObject newCharacter = Instantiate(modularCharacter) as GameObject;
        newCharacter.SetActive(true);
        newCharacter.AddComponent<Character>();        
        newCharacter.name = "[ " + character.id + " ] " + character.name + character.surname;

        if (modularCharacter.GetComponent<ModularCharacterRenderer>().enabledObjects.Count != 0)
        {
            foreach (GameObject g in enabledObjects)
            {
                g.SetActive(false);
            }
        }

        // clear enabled objects list
        enabledObjects.Clear();
        Destroy(modularCharacter.GetComponent<Character>());
        return newCharacter;
    }

    void ActivateItem(GameObject go)
    {
        if (go != null)
        {
            // enable item
            go.SetActive(true);

            // add item to the enabled items list
            enabledObjects.Add(go);
        }
        else
            Debug.LogWarning("null Game Object");

    }

    private void BuildLists()
    {
        //build out male lists
        BuildList(male.headAllElements, "Male_Head_All_Elements");
        BuildList(male.headNoElements, "Male_Head_No_Elements");
        BuildList(male.eyebrow, "Male_01_Eyebrows");
        BuildList(male.facialHair, "Male_02_FacialHair");
        BuildList(male.torso, "Male_03_Torso");
        BuildList(male.arm_Upper_Right, "Male_04_Arm_Upper_Right");
        BuildList(male.arm_Upper_Left, "Male_05_Arm_Upper_Left");
        BuildList(male.arm_Lower_Right, "Male_06_Arm_Lower_Right");
        BuildList(male.arm_Lower_Left, "Male_07_Arm_Lower_Left");
        BuildList(male.hand_Right, "Male_08_Hand_Right");
        BuildList(male.hand_Left, "Male_09_Hand_Left");
        BuildList(male.hips, "Male_10_Hips");
        BuildList(male.leg_Right, "Male_11_Leg_Right");
        BuildList(male.leg_Left, "Male_12_Leg_Left");

        //build out female lists
        BuildList(female.headAllElements, "Female_Head_All_Elements");
        BuildList(female.headNoElements, "Female_Head_No_Elements");
        BuildList(female.eyebrow, "Female_01_Eyebrows");
        BuildList(female.facialHair, "Female_02_FacialHair");
        BuildList(female.torso, "Female_03_Torso");
        BuildList(female.arm_Upper_Right, "Female_04_Arm_Upper_Right");
        BuildList(female.arm_Upper_Left, "Female_05_Arm_Upper_Left");
        BuildList(female.arm_Lower_Right, "Female_06_Arm_Lower_Right");
        BuildList(female.arm_Lower_Left, "Female_07_Arm_Lower_Left");
        BuildList(female.hand_Right, "Female_08_Hand_Right");
        BuildList(female.hand_Left, "Female_09_Hand_Left");
        BuildList(female.hips, "Female_10_Hips");
        BuildList(female.leg_Right, "Female_11_Leg_Right");
        BuildList(female.leg_Left, "Female_12_Leg_Left");

        // build out all gender lists
        BuildList(allGender.all_Hair, "All_01_Hair");
        BuildList(allGender.all_Head_Attachment, "All_02_Head_Attachment");
        BuildList(allGender.headCoverings_Base_Hair, "HeadCoverings_Base_Hair");
        BuildList(allGender.headCoverings_No_FacialHair, "HeadCoverings_No_FacialHair");
        BuildList(allGender.headCoverings_No_Hair, "HeadCoverings_No_Hair");
        BuildList(allGender.chest_Attachment, "All_03_Chest_Attachment");
        BuildList(allGender.back_Attachment, "All_04_Back_Attachment");
        BuildList(allGender.shoulder_Attachment_Right, "All_05_Shoulder_Attachment_Right");
        BuildList(allGender.shoulder_Attachment_Left, "All_06_Shoulder_Attachment_Left");
        BuildList(allGender.elbow_Attachment_Right, "All_07_Elbow_Attachment_Right");
        BuildList(allGender.elbow_Attachment_Left, "All_08_Elbow_Attachment_Left");
        BuildList(allGender.hips_Attachment, "All_09_Hips_Attachment");
        BuildList(allGender.knee_Attachement_Right, "All_10_Knee_Attachement_Right");
        BuildList(allGender.knee_Attachement_Left, "All_11_Knee_Attachement_Left");
        BuildList(allGender.elf_Ear, "Elf_Ear");
    }

    // called from the BuildLists method
    void BuildList(List<GameObject> targetList, string characterPart)
    {
        Transform[] rootTransform = gameObject.GetComponentsInChildren<Transform>();

        // declare target root transform
        Transform targetRoot = null;

        // find character parts parent object in the scene
        foreach (Transform t in rootTransform)
        {
            if (t.gameObject.name == characterPart)
            {
                targetRoot = t;
                break;
            }
        }

        // clears targeted list of all objects
        targetList.Clear();

        // cycle through all child objects of the parent object
        for (int i = 0; i < targetRoot.childCount; i++)
        {
            // get child gameobject index i
            GameObject go = targetRoot.GetChild(i).gameObject;

            // disable child object
            go.SetActive(false);

            // add object to the targeted object list
            targetList.Add(go);

            // collect the material for the random character, only if null in the inspector;
            if (!mat)
            {
                if (go.GetComponent<SkinnedMeshRenderer>())
                    mat = go.GetComponent<SkinnedMeshRenderer>().material;
            }
        }
    }
    
    [Serializable]
    public class CharacterObjectGroups
    {
        public List<GameObject> headAllElements;
        public List<GameObject> headNoElements;
        public List<GameObject> eyebrow;
        public List<GameObject> facialHair;
        public List<GameObject> torso;
        public List<GameObject> arm_Upper_Right;
        public List<GameObject> arm_Upper_Left;
        public List<GameObject> arm_Lower_Right;
        public List<GameObject> arm_Lower_Left;
        public List<GameObject> hand_Right;
        public List<GameObject> hand_Left;
        public List<GameObject> hips;
        public List<GameObject> leg_Right;
        public List<GameObject> leg_Left;
    }

    [Serializable]
    public class CharacterObjectListsAllGender
    {
        public List<GameObject> headCoverings_Base_Hair;
        public List<GameObject> headCoverings_No_FacialHair;
        public List<GameObject> headCoverings_No_Hair;
        public List<GameObject> all_Hair;
        public List<GameObject> all_Head_Attachment;
        public List<GameObject> chest_Attachment;
        public List<GameObject> back_Attachment;
        public List<GameObject> shoulder_Attachment_Right;
        public List<GameObject> shoulder_Attachment_Left;
        public List<GameObject> elbow_Attachment_Right;
        public List<GameObject> elbow_Attachment_Left;
        public List<GameObject> hips_Attachment;
        public List<GameObject> knee_Attachement_Right;
        public List<GameObject> knee_Attachement_Left;
        public List<GameObject> all_12_Extra;
        public List<GameObject> elf_Ear;
    }
}
