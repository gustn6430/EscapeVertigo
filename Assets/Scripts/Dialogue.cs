using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq.Expressions;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public AudioClip[] audioClips;
    public SpriteRenderer[] sprites;

    public float textSpeed;


    private int index;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        audioSource = gameObject.AddComponent<AudioSource>();
        HideAllSprites();

        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent != null && textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                PlayAudio();
                ShowSprite();

            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        PlayAudio();
        ShowSprite();


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
            gameObject.SetActive(false);
        }
    }

    void PlayAudio()
    {
        if (audioClips != null && audioClips.Length > index && audioClips[index] != null)
        {
            audioSource.Stop();
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
    }

    void ShowSprite()
    {
        HideAllSprites(); // ��� ��������Ʈ �����

        if (sprites != null && sprites.Length > index && sprites[index] != null)
        {
            sprites[index].enabled = true; // ���� ������ ��������Ʈ ���̱�
        }
    }


    void HideAllSprites()
    {
        foreach (var sprite in sprites)
        {
            if (sprite != null)
            {
                sprite.enabled = false;
            }
        }
    }

}    