using Hexen.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Hexen.View.UI
{
    public class PlayerPanel
    {
        private readonly PlayerColor _player;
        private readonly VisualElement _playerPanel;

        private Label _score;

        public PlayerPanel(PlayerColor player, VisualElement playerPanel)
        {
            _player = player;
            _playerPanel = playerPanel;

            playerPanel.Q<Label>("PlayerColor").text = player.ToString();
            _score = playerPanel.Q<Label>("Score");
        }

        public void SetHighlighted(bool highlighted) 
        {
            string className = "activePlayer";

            if (highlighted)
            {
                _playerPanel.AddToClassList(className);
            }
            else _playerPanel.RemoveFromClassList(className);
        }

        public void UpdateScore(int score)
        {
            _score.text = $"Score: {score.ToString()}";
        }
    }
}
