using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public GameObject characterCardPrefab;
    public GameObject scrollViewContent;
    public GameObject newCard;
    public GameObject session;
    public GameObject modularCharacter;
    public List<Character> characters;
    public Canvas canvas;
    public GameObject AUXCanvas, AUXContent;
    public Animator AnimationController;
    private Color[] glowColor = { new Color(0, 0.6980392f, 0.4431373f), new Color (0.04313726f, 0.454902f, 0.8078432f), new Color(0.6156863f, 0.3058824f, 1), new Color (1, 0.5019608f, 0.09803922f)  };
    private float rectsDiff = 0.94f;
    public Vector3 scrollRectA, scrollRectB;

    private void Awake()
    {
        Debug.Log("Awakening Character Selection...");
        session = GameObject.Find("PlayerSession");
        characters = session.GetComponent<SessionManager>().characters;
        listCharacters();
    }
    private void OnEnable()
    {
        AUXCanvas.SetActive(true);        
    }
    private void OnDisable()
    {
        AUXCanvas.SetActive(false);
    }

    private void Update()
    {
        if (characters.Count != 0)
        {
            if (rectsDiff != 0)
            {
                //scroll rect do auxcontent recebe a normalized position do scroll rect principal
                scrollViewContent.transform.localPosition = AUXContent.transform.localPosition / rectsDiff;
                scrollRectA = scrollViewContent.transform.localPosition;
                scrollRectB = AUXContent.transform.localPosition;
            }            
        }
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
            newCard.transform.localPosition = new Vector3(0, 0, 0);
            newCard.transform.localScale = new Vector3(1, 1, 1);
            character.passData(newCardCharacterComponent);
            Vector3 labelPosition = new Vector3(0, 0, 0);

            switch(newCardCharacterComponent.rarity)
            {
                case Character.Rarity.Common:
                    cardComponent.frame = cardComponent.commonFrame;
                    cardComponent.label = cardComponent.commonLabelPrefab;
                    cardComponent.glow.GetComponent<Image>().color = glowColor[0];
                    labelPosition = new Vector3(38f, 19f, 0f);
                    hasRarity = true;
                    break;
                case Character.Rarity.Rare:
                    cardComponent.frame = cardComponent.rareFrame;
                    cardComponent.label = cardComponent.rareLabelPrefab;
                    cardComponent.glow.GetComponent<Image>().color = glowColor[1];
                    labelPosition = new Vector3(10f, 19f, 0f);
                    hasRarity = true;
                    break;
                case Character.Rarity.Epic:
                    cardComponent.frame = cardComponent.epicFrame;
                    cardComponent.label = cardComponent.epicLabelPrefab;
                    cardComponent.glow.GetComponent<Image>().color = glowColor[2];
                    labelPosition = new Vector3(10f, 19f, 0f);
                    hasRarity = true;
                    break;
                case Character.Rarity.Legendary:
                    cardComponent.frame = cardComponent.legendaryFrame;
                    cardComponent.label = cardComponent.legendaryLabelPrefab;
                    cardComponent.glow.GetComponent<Image>().color = glowColor[3];
                    labelPosition = new Vector3(120f, 19f, 0f);
                    hasRarity = true;
                    break;
                default:
                    cardComponent.frame = cardComponent.defaultFrame;
                    hasRarity = false;
                    break;
            }

            /*
             * Hi everyone! So I think I figured out a pretty easy, and I think elegant solution for touch screen devices 
             * for this (at least until unity supports this which I hope they will in the future!). I have a simple touch 
             * gesture script that sends out gesture events, so if you have a vertical scroll rect and a nested horizontal 
             * scroll rect like above, figure out in the OnGesture or whatever you have listening for the gesture if the user 
             * is swiping in a vertical motion. If they are, disable the ScrollRect script on the nested scroll rects. It 
             * works perfectly for anything that scrolls the opposite of the parent, at least. I tried using IDrag interfaces 
             * but there were too many issues I encountered, and I really wanted a way to not recreate what unity built with 
             * the scroll rects in the first place. If anyone has an even better solution, I'm all ears 
             */

            if (hasRarity)
            {
                cardComponent.addIcon.SetActive(false);
                cardComponent.addText.SetActive(false);
                cardComponent.label.SetActive(true);
                cardComponent.stars.SetActive(true);
                cardComponent.characterName.SetActive(true);
                cardComponent.glow.SetActive(true);
                cardComponent.characterName.GetComponent<TMPro.TMP_Text>().text = character.name + " " + character.surname;
                cardComponent.thisFrame.sprite = cardComponent.frame;

                //manda alguns componentes do card para o canvas auxiliar para ficar na frente do modelo 3d.
                GameObject auxCardComponent = new GameObject();
                auxCardComponent.transform.parent = AUXContent.transform;
                auxCardComponent.name = "Auxiliar Card Component";
                auxCardComponent.AddComponent<RectTransform>();
                auxCardComponent.transform.localPosition = new Vector3(0, 0, 0);
                auxCardComponent.transform.localScale = new Vector3(1, 1, 1);
                cardComponent.glow.transform.parent = auxCardComponent.transform;
                cardComponent.glow.transform.localPosition = new Vector3(0.6806461f, -97f, 0);
                cardComponent.glow.transform.localScale = new Vector3(0.98f, 1, 1);
                cardComponent.stars.transform.parent = auxCardComponent.transform;
                cardComponent.stars.transform.localPosition = new Vector3(1.261575f, -147f, 0);
                cardComponent.characterName.transform.parent = auxCardComponent.transform;
                cardComponent.characterName.transform.localPosition = new Vector3(1.261576f, -196f, 0);
                cardComponent.label.transform.parent = auxCardComponent.transform;
                cardComponent.label.transform.localPosition = labelPosition;

                //renderiza o personagem na scene
                cardComponent.character = modularCharacter.GetComponent<ModularCharacterRenderer>().RenderCharacter(character);
                    //seta a card como parent e seta a posição do personagem
                    cardComponent.character.transform.parent = cardComponent.transform;
                    cardComponent.character.transform.localPosition = new Vector3(0, -7.3f, 116.7f);
                    cardComponent.character.transform.localRotation = Quaternion.identity;
                    cardComponent.character.transform.Rotate(0, 190, 0);
                    cardComponent.character.transform.localScale = new Vector3(5, 5, 5);

                //cria render texture com a resolução do canvas
                //cardComponent.renderTexture = new RenderTexture((int)canvas.GetComponent<RectTransform>().rect.width, (int)canvas.GetComponent<RectTransform>().rect.height, 1, RenderTextureFormat.ARGB32);
                cardComponent.renderTexture = new RenderTexture(340, 430, 1, RenderTextureFormat.ARGB32);
                cardComponent.renderTexture.name = character.name + "RenderTexture";

                //cria o material da render texture
                cardComponent.renderMaterial = new Material(cardComponent.renderShader);
                cardComponent.renderMaterial.name = character.name + "Material";
                //seta as propriedades do material para o render texture
                    cardComponent.renderMaterial.SetTexture("_MainTex", cardComponent.renderTexture);
                //seta o Rendering type para cutout
                    cardComponent.renderMaterial.SetFloat("_Mode", 1);
                    cardComponent.renderMaterial.SetOverrideTag("RenderType", "TransparentCutout");
                    cardComponent.renderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    cardComponent.renderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    cardComponent.renderMaterial.SetInt("_ZWrite", 1);
                    cardComponent.renderMaterial.EnableKeyword("_ALPHATEST_ON");
                    cardComponent.renderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    cardComponent.renderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    cardComponent.renderMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;

                //adiciona o material ao plane
                cardComponent.characterFrame.SetActive(true);
                cardComponent.characterFrame.GetComponent<MeshRenderer>().material =  cardComponent.renderMaterial;

                //cria a camera seta suas propriedades
                cardComponent.renderCamera = new GameObject();
                cardComponent.renderCamera.name = character.name + "Camera";
                cardComponent.renderCamera.AddComponent<Camera>();
                Camera renderCamera = cardComponent.renderCamera.GetComponent<Camera>();
                renderCamera.transform.parent = cardComponent.transform;
                renderCamera.transform.localPosition = new Vector3(0, -1.2f , 111);
                renderCamera.transform.Rotate(new Vector3(0, 0, 0));
                renderCamera.clearFlags = CameraClearFlags.SolidColor;
                renderCamera.backgroundColor = new Color(0, 0, 0, 0);
                renderCamera.targetTexture = cardComponent.renderTexture;
                renderCamera.usePhysicalProperties = true;
                renderCamera.fieldOfView = 40;
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
