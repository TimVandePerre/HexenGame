using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Hexen.View.UI
{
    public class GameOverUI : MonoBehaviour
    {
        public event Action NewGameClicked;
        public event Action MenuClicked;
        private UIDocument _doc;

        private void Awake()
        {
            UIDocument document = GetComponent<UIDocument>();

            Button NewGameButton = document.rootVisualElement.Q<Button>("NewGameButton");
            Button MenuButton = document.rootVisualElement.Q<Button>("Menu");
            NewGameButton.clicked += NewGameButton_clicked;
            MenuButton.clicked += MenuButton_clicked;

            _doc = document;
            Hide();
        }

        private void MenuButton_clicked()
        {
            MenuClicked?.Invoke();
        }

        private void NewGameButton_clicked()
        {
            NewGameClicked?.Invoke();
        }

        public void Show()
        {
            _doc.rootVisualElement.visible = true;
        }

        public void Hide()
        {
            _doc.rootVisualElement.visible = false;
        }
    }
}
