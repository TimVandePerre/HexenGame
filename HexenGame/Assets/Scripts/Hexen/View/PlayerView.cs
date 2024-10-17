using Hexen.Model;
using Hexen.View.Animation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexen.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private GameObject _scoreblock;
        public PlayerModel Model {  get; private set; }
        private PlayerAnimation _playerAnimation;

        private void Awake()
        {
            _playerAnimation = GetComponentInChildren<PlayerAnimation>();
        }
        public void SetModel(PlayerModel model)
        {
            Model = model;
            model.ScoresChanged += Model_ScoresChanged;
        }
        private void Model_ScoresChanged(object sender, ScoreViewerEventArg e)
        {
            _playerAnimation.Animation(e.Score);
        }
    }
}
