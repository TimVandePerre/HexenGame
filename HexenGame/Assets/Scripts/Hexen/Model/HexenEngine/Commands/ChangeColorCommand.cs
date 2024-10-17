using CommandPattern;

namespace Hexen.Model.Engine.Commands
{
    public class ChangeColorCommand : Icommand
    {
        //Reference to the boardModel
        private readonly BoardModel _board;

        //Reference to a Grid Position
        private readonly HexGridPos _pos;

        //Reference to a PlayerColor
        private readonly PlayerColor _color;


        /// <summary>
        /// Constructor: gets a board Model, HexGridPos of a piece, PlayerColor to change the piece color to.
        /// </summary>
        /// <param name="board"> The boardModel</param>
        /// <param name="pos"> The Grid Position of the piece</param>
        /// <param name="color"> The color to change to piece to</param>
        public ChangeColorCommand(BoardModel board,HexGridPos pos, PlayerColor color)
        {
            _board = board;
            _pos = pos;
            _color = color;
        }

        /// <summary>
        /// Will change the Color of the piece on _pos to _color.
        /// </summary>
        public void Execute()
        {
            PieceModel piece = _board.GetPieceOnPosition(_pos);
            if (piece != null)
            {
                piece.PlayerColor = _color;
            }
        }

        /// <summary>
        /// Will change the color of piece on Grid position _pos back to the other color.
        /// </summary>
        public void Undo()
        {
            PieceModel piece = _board.GetPieceOnPosition(_pos);

            switch (_color)
            {
                case PlayerColor._white:
                    piece.PlayerColor = PlayerColor._black;
                    break;
                case PlayerColor._black:
                    piece.PlayerColor = PlayerColor._white;
                    break;
            }
            
        }
    }
}
