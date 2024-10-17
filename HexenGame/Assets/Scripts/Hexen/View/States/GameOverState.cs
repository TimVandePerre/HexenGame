using Hexen.View.UI;
using StatePattern;
using UnityEngine;


namespace Hexen.View.States
{
    public class GameOverState : IState
    {
        private readonly GameStateSystem _stateSystem;
        private GameOverUI _gameOverUI;

        public GameOverState(GameStateSystem stateSystem)
        {
            _stateSystem = stateSystem;
        }
        public void OnEnter()
        {
            _gameOverUI = GameObject.FindAnyObjectByType<GameOverUI>();
            _gameOverUI.NewGameClicked += _gameOverUI_NewGameClicked;
            _gameOverUI.MenuClicked += _gameOverUI_MenuClicked;
            _gameOverUI.Show();
        }

        private void _gameOverUI_MenuClicked()
        {
            _stateSystem.StateMachine.MoveToState(_stateSystem.MenuState);
        }

        private void _gameOverUI_NewGameClicked()
        {
            _stateSystem.StateMachine.MoveToState(_stateSystem.NewGameState);
        }

        public void OnExit()
        {
            _gameOverUI.MenuClicked -= _gameOverUI_MenuClicked;
            _gameOverUI.NewGameClicked -= _gameOverUI_NewGameClicked;
            _gameOverUI.Hide();
        }

        public void Update()
        {
           
        }
    }
}
