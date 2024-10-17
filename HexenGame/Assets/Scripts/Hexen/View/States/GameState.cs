using Hexen.Model;
using Hexen.Model.Engine;
using Hexen.View.UI;
using PlasticPipe.PlasticProtocol.Messages;
using StatePattern;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Hexen.View.States
{

    public class GameState : IState
    {
        private readonly GameStateSystem _stateSystem;
        private BoardView _boardView;

        public GameState(GameStateSystem stateSystem) 
        {
            _stateSystem = stateSystem;
        }

        public void OnEnter()
        {
            _boardView = GameObject.FindObjectOfType<BoardView>();
            _boardView.Board.GameOver += Board_GameOver;
            if (SceneManager.GetActiveScene().name != "GameScene")
            {
                AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene");
            }


        }

        private void Board_GameOver(object sender, System.EventArgs e)
        {
            _stateSystem.StateMachine.MoveToState(_stateSystem.GameOverState);
        }

        public void OnExit()
        {
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _stateSystem.StateMachine.MoveToState(_stateSystem.PauseState);
            }
        }
    }
}
