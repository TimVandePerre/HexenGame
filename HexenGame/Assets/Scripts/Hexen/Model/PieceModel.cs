 using System;
using System.Collections;
using System.Collections.Generic;

namespace Hexen.Model
{

    public class PieceModel
    {
        //Event send when Color s changed, has PieceModel as argument.
        public event EventHandler<PieceEventArgs> ColourChanged;

        //Event send when tile is removed, has no arguments.
        public event EventHandler<EventArgs> Removed;

        //The HexGridPos set in the constructor, does not change.
        public HexGridPos GridPosition => _gridPosition;

        //The playerColor set in the constructor, when changed invoke ColourChanged Event.
        public PlayerColor PlayerColor
        {
            get => _playerColor;
            set
            {
                if (_playerColor == value) return;
                _playerColor = value;
                ColourChanged?.Invoke(this, new PieceEventArgs(this));
            }
        }

        //reference to board. Can only be read not changed.
        private readonly BoardModel _board;

        //HexGridPos of piece, based on GridPosition
        private HexGridPos _gridPosition;

        //PlayerColor of piece, Changes when PlayerColor changes.
        private PlayerColor _playerColor;

        /// <summary>
        /// Constructor: referencing board, its Grid pos and color.
        /// </summary>
        /// <param name="board"> reference to boardmodel.</param>
        /// <param name="pos">HexGridPos of Piece.</param>
        /// <param name="Color"> PlayerColor of Piece.</param>
        public PieceModel(BoardModel board, HexGridPos pos, PlayerColor Color)
        {
            _board = board;
            _gridPosition = pos;
            _playerColor = Color;
        }

        /// <summary>
        /// Invoke the Removed event.
        /// </summary>
        public void Remove()
        {
            Removed?.Invoke(this, EventArgs.Empty);
        }

        //Extra Logic: Handy to have. Will only need to be updated in Board.

        /// <summary>
        /// When Piece is clicked Go to board.PieceClicked.
        /// </summary>
        public void Click()
        {
            _board.PieceClicked(this);
        }

        /// <summary>
        /// When Piece is Hovered Go to board.PieceHovered.
        /// </summary>
        public void Hover()
        {
            _board.PieceHovered(this);
        }
    }
}
