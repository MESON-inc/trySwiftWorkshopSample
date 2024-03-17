using TMPro;
using UnityEngine;

namespace PolySpatial.Samples
{
    public class BalloonGalleryManager : MonoBehaviour
    {
        [SerializeField] TMP_Text m_ScoreText;
        [SerializeField] GameObject m_WinConfetti;

        private BalloonBehavior[] m_Balloons;

        private int m_CurrentScore = 0;
        private int m_NumberOfBalloonsPopped;
        private int m_NumberOfGoodBalloons;
        private const int k_NumberOfBalloons = 36;

        private GameObject _poppedWinConfetti;

        private void Awake()
        {
            m_CurrentScore = 0;
            m_Balloons = FindObjectsOfType<BalloonBehavior>();

            foreach (var balloon in m_Balloons)
            {
                balloon.m_Manager = this;
            }

            StartGame();
        }

        public void BalloonPopped(int scoreValue)
        {
            m_CurrentScore += scoreValue;
            m_ScoreText.text = m_CurrentScore.ToString();

            // good balloon
            if (scoreValue > 0)
            {
                m_NumberOfBalloonsPopped++;
            }

            // all balloons popped, Show confetti
            if (m_NumberOfBalloonsPopped == k_NumberOfBalloons)
            {
                FinishGame();
            }
        }

        public void GoodBalloonAdded()
        {
            m_NumberOfGoodBalloons++;
        }

        private void StartGame()
        {
            foreach (BalloonBehavior balloon in m_Balloons)
            {
                balloon.Initialize();
            }
        }

        private void FinishGame()
        {
            _poppedWinConfetti = Instantiate(m_WinConfetti);
        }

        private void ResetGame()
        {
            m_CurrentScore = 0;
            m_ScoreText.text = m_CurrentScore.ToString();
            m_NumberOfBalloonsPopped = 0;

            if (_poppedWinConfetti != null)
            {
                Destroy(_poppedWinConfetti);
            }
            
            StartGame();
        }

#if UNITY_EDITOR
        [UnityEditor.CustomEditor(typeof(BalloonGalleryManager))]
        private class BalloonGalleryManagerEditor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (!Application.isPlaying) return;
                if (target is not BalloonGalleryManager manager) return;

                if (GUILayout.Button("Reset Game"))
                {
                    manager.ResetGame();
                }
            }
        }
#endif
    }
}