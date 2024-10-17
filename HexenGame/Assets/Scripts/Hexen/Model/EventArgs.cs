using System;

namespace Hexen.Model
{
    /// <summary>
    /// Event arguments that hold a pieceModel.
    /// </summary>
    public class PieceEventArgs: EventArgs
    {
        public PieceModel Piece { get; }
        public PieceEventArgs(PieceModel piece) 
        {
            Piece = piece;
        }
    }

    /// <summary>
    /// Event arguments that hold a HexGridPos.
    /// </summary>
    public class PositionEventArgs : EventArgs
    {
        public HexGridPos Gridposition { get; }

        public PositionEventArgs(HexGridPos gridposition)
        {
            Gridposition = gridposition;
        }
    }

    /// <summary>
    /// Event arguments that hold a PlayerColor. 
    /// </summary>
    public class PlayerColorEventArgs : EventArgs
    {
        public PlayerColor Color { get; }

        public PlayerColorEventArgs(PlayerColor color)
        {
            Color = color;
        }
    }

    /// <summary>
    /// Event that holds 2 ints for score of white and black. 
    /// </summary>
    public class ScoresEventArgs: EventArgs
    {
        public int ScoreWhite { get; }
        public int ScoreBlack { get; }

        public ScoresEventArgs(int scoreWhite, int scoreBlack)
        {
            ScoreWhite = scoreWhite;
            ScoreBlack = scoreBlack;
        }
    }

    public class ScoreViewerEventArg : EventArgs
    {
        public ScoreViewerEventArg(float score)
        {
            Score = score;
        }

        public float Score { get; }
    }

}
