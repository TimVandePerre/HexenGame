using System.Collections;
using CommandPattern;

namespace Hexen.Model.Engine.Commands
{
    public class PlacePieceCommand : Icommand
    {
        //Reference to the BoardModel
        private readonly BoardModel _board;

        //Reference to a HexGridPos where a piece has to be placed
        private readonly HexGridPos _pos;

        //Reference to a PlayerColor of the placed piece.
        private readonly PlayerColor _color;


        /// <summary>
        /// Constructor: Takes reference of: The BoardModel, HexGridPos where to place a piece, PlayerColor of the placed piece
        /// </summary>
        /// <param name="board"></param>
        /// <param name="pos"></param>
        /// <param name="color"></param>
        public PlacePieceCommand(BoardModel board, HexGridPos pos, PlayerColor color)
        {
            _board = board;
            _pos = pos;
            _color = color;
        }


        /// <summary>
        /// Executes the PlacePiece method from the BoardModel, with the HexGridPos and PlayerColor as its arguments.
        /// </summary>
        public void Execute()
        {
            _board.PlacePiece(_pos, _color);
        }

        /// <summary>
        /// Executes the RemovePiece method from the BoardModel, with the HexGridPos as its argument
        /// </summary>
        public void Undo()
        {
            _board.RemovePiece(_pos);
        }
    }
}
