using CommandPattern;

namespace Hexen.Model.Engine.Commands
{
    public class ChangePieceColorCommand : Icommand
    {
        private readonly BoardModel _board;
        private readonly HexGridPos _pos;
        private readonly HexenEngine _engine;

        public ChangePieceColorCommand(BoardModel board, HexGridPos pos, HexenEngine engine)
        {
            _board = board;
            _pos = pos;
            _engine = engine;
        }

        public void Execute()
        {
            PieceModel piece = _board.GetPieceOnPosition(_pos);
            switch (_engine._currentPlayer)
            {
                case PlayerColor._white:
                    if (_board.HasWhiteFliped)
                    {
                        if (piece != null)
                        {
                            switch (piece.PlayerColor)
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
                    _board.HasWhiteFliped = false;
                    break;
                case PlayerColor._black:
                    if (_board.HasBlackFliped)
                    {
                        if (piece != null)
                        {
                            switch (piece.PlayerColor)
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
                    _board.HasBlackFliped = false;
                    break;
            }

        }

        public void Undo()
        {
            PieceModel piece = _board.GetPieceOnPosition(_pos);
            if (piece != null)
            {
                switch (piece.PlayerColor)
                {
                    case PlayerColor._white:
                        piece.PlayerColor = PlayerColor._black;
                        break;
                    case PlayerColor._black:
                        piece.PlayerColor = PlayerColor._white;
                        break;
                }
                switch (_engine._currentPlayer)
                {
                    case PlayerColor._white:
                        _board.HasWhiteFliped = true;
                        break;
                    case PlayerColor._black:
                        _board.HasBlackFliped = true;
                        break;
                }
            }
        }
    }
}
