using StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexen.View.States
{
    public class GameStateSystem : MonoBehaviour
    {
        public StateMachine StateMachine { get; private set; }
        public MenuState MenuState { get; private set; }
        public NewGameState NewGameState { get; private set; }
        public GameState GameState { get; private set; }
        public PauseState PauseState { get; private set; }
        public GameOverState GameOverState { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            MenuState = new MenuState(this);
            NewGameState = new NewGameState(this);
            GameState = new GameState(this);
            PauseState = new PauseState(this);
            GameOverState = new GameOverState(this);

            StateMachine = new StateMachine(MenuState);
        }

        private void Update()
        {
            StateMachine.Update();
        }
    }
}
