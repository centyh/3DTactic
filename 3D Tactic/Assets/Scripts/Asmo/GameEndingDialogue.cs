using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndingDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    public GameObject playerText;
    public GameObject npcText;
    public GameObject sceneTransition;

    private int index;

    void Start()
    {
        textComponent.text = string.Empty;

        StartDialogue(); 
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }

        if (index == 0 || index == 2 || index == 4 || index == 6)
        {
            npcText.SetActive(true);
            playerText.SetActive(false);
        }
        else
        {
            playerText.SetActive(true);
            npcText.SetActive(false);
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }


    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            sceneTransition.SetActive(true);
            gameObject.SetActive(false);

        }
    }
}
