using System;
using System.Collections;
using System.Collections.Generic;

namespace Hexen.Model
{
    public class PlayerModel
    {
        public event EventHandler<ScoreViewerEventArg> ScoresChanged;
        public float Score
        {
            get => _score;

            set
            {
                if (_score != value)
                {
                    _score = value;
                    ScoresChanged?.Invoke(this, new ScoreViewerEventArg(value));
                }
            }
        }

        public bool IsPlayerActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                }
            }
        }

        private float _score;
        private bool _isActive;

        public PlayerModel(bool isActive)
        {
            _isActive = isActive;
        }
    }
}
