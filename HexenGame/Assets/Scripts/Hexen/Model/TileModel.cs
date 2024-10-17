using System;
using System.Collections;
using System.Collections.Generic;

namespace Hexen.Model
{
    public class TileModel
    {
        //Event sent when the tile visual has been changed.
        public event EventHandler TileVisualChanged;

        //TileVisual when changed send TileVisualChanged Event.
        public TileVisual VisualTile 
        {
            get => _visual;
            set
            {
                if(_visual != value)
                {
                    _visual = value;
                    TileVisualChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        //The HexGridPos of the tile, can be read but not changed.
        public HexGridPos GridPosition { get; private set; }

        //Stored Tilevisual
        private TileVisual _visual;

        //reference to the Board, can be read but not changed.
        private readonly BoardModel _board;

        /// <summary>
        /// Constructor: takes reference to board and the HexGridPos.
        /// </summary>
        /// <param name="board"> Reference to Board</param>
        /// <param name="gridposition"> The HexGridPos of the Tile</param>
        public TileModel(BoardModel board,HexGridPos gridposition)
        {
            _board = board;
            GridPosition = gridposition;
        }

        /// <summary>
        /// Invoke Board.TileClicked
        /// </summary>
        public void Click()
        {
            _board.TileClicked(this);
        }

        /// <summary>
        /// Invoke Board.Hovered
        /// </summary>
        public void Hover()
        {
            _board.TileHover(this);
        }
    }
}
