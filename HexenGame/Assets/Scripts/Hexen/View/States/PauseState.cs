using Hexen.View.UI;
using StatePattern;
using UnityEngine;


namespace Hexen.View.States
{
    public class PauseState : IState
    {
        private readonly GameStateSystem _stateSystem;
        private PauseUI _pauseUI;

        public PauseState(GameStateSystem stateSystem)
        {
            _stateSystem = stateSystem;
        }

        public void OnEnter()
        {
            _pauseUI = GameObject.FindObjectOfType<PauseUI>();
            _pauseUI.NewGameClicked += _pauseUI_NewGameClicked;
            _pauseUI.MenuClicked += _pauseUI_MenuClicked;
            _pauseUI.Show();
        }

        private void _pauseUI_MenuClicked()
        {
            _stateSystem.StateMachine.MoveToState(_stateSystem.MenuState);
        }

        private void _pauseUI_NewGameClicked()
        {
            _stateSystem.StateMachine.MoveToState(_stateSystem.NewGameState);
        }

        public void OnExit()
        {
            _pauseUI.MenuClicked -= _pauseUI_MenuClicked;
            _pauseUI.NewGameClicked -= _pauseUI_NewGameClicked;
            _pauseUI?.Hide();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _stateSystem.StateMachine.MoveToState(_stateSystem.GameState);
            }
        }
    }
}
