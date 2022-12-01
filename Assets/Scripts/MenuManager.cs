using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_decountText = null;
    [SerializeField] private Canvas m_instructionCanvas = null;
    [SerializeField] private Canvas m_decountCanvas = null;
    [SerializeField] private Canvas m_endGameCanvas = null; 
    [SerializeField] private BladeAnimation m_blade = null;
    [SerializeField] private AudioSource m_source = null;
    [SerializeField] private AudioClip m_clip = null;

    public static event Action OnLevelSucced;

    public static int enemies; 

    private bool m_begin;
    private bool m_end;

    private void Awake()
    {
        enemies = 15;
        m_begin = true;
        m_end = false;
        m_decountText.text = "";
        m_endGameCanvas.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        OnLevelSucced += ClapSound;
    }

    private void OnDisable()
    {
        OnLevelSucced -= ClapSound;
    }

    private void ClapSound()
    {
        m_source.PlayOneShot(m_clip);
    }

    public void ResetLvl()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Quit()
    {
        Application.Quit();
    }


    private IEnumerator DecountCoroutine()
    {
        float time = 3f; 
        do
        {
            m_decountText.text = time.ToString();
            time--;
            yield return new WaitForSeconds(1f);
        } while (time > 0f);

        m_decountCanvas.gameObject.SetActive(false);
    }

    private IEnumerator EndGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        m_endGameCanvas.gameObject.SetActive(true);
    }

    void Update()
    {
        if (m_blade.IsOn && m_begin)
        {
            m_begin = false;
            m_instructionCanvas.gameObject.SetActive(false);
            m_decountCanvas.gameObject.SetActive(true);
            StartCoroutine(DecountCoroutine());
        }


        if (enemies == 0 && !m_end )
        {
            m_end = true;
            StartCoroutine(EndGameCoroutine());
            OnLevelSucced?.Invoke();
        }
            
    }
}
