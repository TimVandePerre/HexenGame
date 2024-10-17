using UnityEngine;
using Hexen.View.UI;
using StatePattern;
using UnityEngine.SceneManagement;


namespace Hexen.View.States
{
    public class MenuState : IState
    {
        private readonly GameStateSystem _stateSystem;

        public MenuState(GameStateSystem stateSystem)
        {
            _stateSystem = stateSystem;
        }

        public void OnEnter()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("MenuScene");
            loadOperation.completed += LoadOperation_completed;

        }

        private void LoadOperation_completed(AsyncOperation obj)
        {
            MenuUI menuUI = GameObject.FindObjectOfType<MenuUI>();
            menuUI.NewGameClicked += MenuUI_NewGameClicked;
        }

        private void MenuUI_NewGameClicked()
        {
            _stateSystem.StateMachine.MoveToState(_stateSystem.NewGameState);
        }

        public void OnExit()
        {
            MenuUI menuUI = GameObject.FindObjectOfType<MenuUI>();
            menuUI.NewGameClicked -= MenuUI_NewGameClicked;
        }

        public void Update()
        {
        }
    }
}
