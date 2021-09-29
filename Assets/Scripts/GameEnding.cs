using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup playerCaughtBackgroundImageCanvasGroup;
    public float exitImageDisplayDuration = 2f;
    public AudioSource gameEndingAudioSource;
    public AudioSource gameWinAudioSource;

    bool m_IsPlayerAtExit = false;
    bool m_IsPlayerCaught = false;
    float m_Timer;
    bool m_HasAudioPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // trigger game end
            m_IsPlayerAtExit = true;
        }
    }

    private void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(playerCaughtBackgroundImageCanvasGroup, true);
        }
    }

    private void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart)
    {
        if (!m_HasAudioPlayed)
        {
            if (doRestart)
            {
                gameEndingAudioSource.Play();
            }
            else
            {
                gameWinAudioSource.Play();
            }

            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + exitImageDisplayDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                Application.Quit();
            }

        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }
}
