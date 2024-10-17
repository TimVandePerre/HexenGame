using Hexen.Model;
using UnityEngine;
using UnityEngine.UIElements;

namespace Hexen.View.UI
{
    public class HexenUI : MonoBehaviour
    {
        private PlayerPanel _white, _black;

        public PlayerView PlayerWhite;
        public PlayerView PlayerBlack;

        private void Awake()
        {
            UIDocument document = GetComponent<UIDocument>();
            VisualElement rootVisual = document.rootVisualElement;

            _white = new PlayerPanel(PlayerColor._white, rootVisual.Q("PlayerPanelWhite"));
            _black = new PlayerPanel(PlayerColor._black, rootVisual.Q("PlayerPanelBlack"));
        }

        public void OnPlayerColorChanged(PlayerColor player)
        {
            switch (player)
            {
                case PlayerColor._white:
                    _white.SetHighlighted(true); 
                    _black.SetHighlighted(false);
                    break;
                case PlayerColor._black:
                    _black.SetHighlighted(true);
                    _white.SetHighlighted(false);
                    break;
            }
        }

        public void ScoreUpdate(int scoreWhite, int scoreBlack)
        {
            _white.UpdateScore(scoreWhite);
            _black.UpdateScore(scoreBlack);
        }
    }
}
