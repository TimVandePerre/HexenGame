using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hexen.Model;
using Hexen.View;

namespace Hexen.Test
{
    public class BoardTester : MonoBehaviour
    {
        BoardModel boardModel;
        // Start is called before the first frame update
        void Start()
        {
            BoardView boardView = FindObjectOfType<BoardView>();
            boardModel = boardView.Board;
        }
    }
}
