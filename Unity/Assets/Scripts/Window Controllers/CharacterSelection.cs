using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject characterCardPrefab;
    public GameObject scrollViewContent;
    public GameObject newCard;
    public GameObject session;
    public List<Character> characters;

    private void Awake()
    {
        session = GameObject.Find("PlayerSession");
        characters = session.GetComponent<SessionManager>().characters;
        listCharacters();
    }

    public void listCharacters ()
    {
        foreach (Character character in characters) 
        {
            bool hasRarity = false;
            newCard = Instantiate(characterCardPrefab);
            Character newCardCharacterComponent = newCard.GetComponent<Character>();
            CharacterCard cardComponent = newCard.GetComponent<CharacterCard>();
            newCard.transform.parent = scrollViewContent.transform;
            character.passData(newCardCharacterComponent);
            newCardCharacterComponent.rarity = Character.Rarity.epic;

            switch(newCardCharacterComponent.rarity)
            {
                case Character.Rarity.common:
                    cardComponent.frame = cardComponent.commonFrame;
                    cardComponent.label = cardComponent.commonLabelPrefab;
                    hasRarity = true;
                    break;
                case Character.Rarity.rare:
                    cardComponent.frame = cardComponent.rareFrame;
                    cardComponent.label = cardComponent.rareLabelPrefab;
                    hasRarity = true;
                    break;
                case Character.Rarity.epic:
                    cardComponent.frame = cardComponent.epicFrame;
                    cardComponent.label = cardComponent.epicLabelPrefab;
                    hasRarity = true;
                    break;
                case Character.Rarity.legendary:
                    cardComponent.frame = cardComponent.legendaryFrame;
                    cardComponent.label = cardComponent.legendaryLabelPrefab;
                    hasRarity = true;
                    break;
                default:
                    cardComponent.frame = cardComponent.defaultFrame;
                    hasRarity = false;
                    break;
            }

            if (hasRarity)
            {
                cardComponent.addIcon.SetActive(false);
                cardComponent.addText.SetActive(false);
                cardComponent.label.SetActive(true);
                cardComponent.stars.SetActive(true);
                cardComponent.characterName.SetActive(true);
                cardComponent.characterName.GetComponent<TMPro.TMP_Text>().text = character.name + " " + character.surname;
                cardComponent.thisFrame.sprite = cardComponent.frame;
            } else
            {
                cardComponent.addIcon.SetActive(true);
                cardComponent.addText.SetActive(true);
                cardComponent.label.SetActive(false);
                cardComponent.stars.SetActive(false);
                cardComponent.characterName.SetActive(false);

            }
        }
    }
}
