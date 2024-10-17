using System;
using System.Collections.Generic;
using System.Net;
using CommandPattern;
using Hexen.Model.Engine.Commands;

namespace Hexen.Model.Engine
{
    public class HexenEngine
    {
        #region Variables
        // Event send when the PlayersColor Has changed, has PlayerColor argument.
        public event EventHandler<PlayerColorEventArgs> PlayerColorChanged;

        // Event send when the Score is updated, has a Score argument.
        public event EventHandler<ScoresEventArgs> UpdateScore;

        //Refernece to board, set in the construct, only readable.
        private readonly BoardModel _board;

        //Reference to a MovementHelper, created on construction.
        private readonly MovementHelper _movementHelper;

        //Reference to a CommandInvoker, Created on construction.
        private readonly CommandInvoker _commandInvoker;

        //Current player color.
        public PlayerColor _currentPlayer {  get; private set; }

        //List for ValidPositions and CapturePositions, based on HexGridPos.
        private List<HexGridPos> _validPositions, _capturePositions;

        //List for Commands, based on Icommands.
        private List<Icommand> _commands;

        private PlayerModel _playerWhite;
        private PlayerModel _playerBlack;
        #endregion

        /// <summary>
        /// Constructor: sets and creates references, initializes board, find first ValidTiles.
        /// </summary>
        /// <param name="board"> reference to boardModel</param>
        /// <param name="startingPlayer"> Color of the Starting Player</param>
        public HexenEngine(BoardModel board, PlayerColor startingPlayer)
        {
            //Setting references on construction.
            _board = board;
            _currentPlayer = startingPlayer;

            //Creating new references on construction.
            _movementHelper = new MovementHelper(_board);
            _commandInvoker = new CommandInvoker();

            //Subscribing to the events of the board.
            _board.PositionClicked += _board_PositionClicked;
            _board.PositionHovered += _board_PositionHovered;

            //Creating new Lists.
            _validPositions = new List<HexGridPos>();
            _capturePositions = new List<HexGridPos> ();

            InitializeBoard();
            FindValidTiles();
            ScoreUpdate();
        }

        /// <summary>
        /// Invokes commandInvoker.Undo, Updates Score.
        /// </summary>
        public void Undo()
        {
            _commandInvoker.Undo();
            ScoreUpdate();
        }

        /// <summary>
        /// Invokes commandInvoker.Redo, Updates Score.
        /// </summary>
        public void Redo()
        {
            _commandInvoker.Redo();
            ScoreUpdate();
        }

        public void SetPlayerWhite(PlayerModel playerWhite)
        {
            _playerWhite = playerWhite;
            ScoreUpdate();
        }

        public void SetPlayerBlack(PlayerModel playerBlack)
        {
            _playerBlack = playerBlack;
            ScoreUpdate();
        }

        /// <summary>
        /// After a piece has been placed switch player (player color).
        /// </summary>
        public void SwitchPlayer()
        {
            //switch current player color
            if (_currentPlayer == PlayerColor._white)
            {
                _currentPlayer = PlayerColor._black;
            }
            else _currentPlayer = PlayerColor._white;

            //invoke PlayerColorChanged Event.
            PlayerColorChanged?.Invoke(this, new PlayerColorEventArgs(_currentPlayer));

            //Set visuals for everytile back to Normal.
            foreach (var pos in _board._tiles.Keys)
            {
                _board.SetTileHighlight(pos, TileVisual.None);
            }

            //clear all lists.
            _validPositions.Clear();
            _capturePositions.Clear();
        }

        /// <summary>
        /// Logic For when Board.PositionHovered Event is called.
        /// </summary>
        /// <param name="sender"> The sender of the event, BoardModel </param>
        /// <param name="e"> The position of tile that is hovered </param>
        private void _board_PositionHovered(object sender, PositionEventArgs e)
        {
            //Set all tiles back to base Visuals.
            foreach (var pos in _board._tiles.Keys)
            {
                _board.SetTileHighlight(pos, TileVisual.None);
            }

            //Check of the hovered tile is part of the validTiles.
            if (_validPositions.Contains(e.Gridposition))
            {
                HighlightCapturePieces(e.Gridposition);
            }
            else if (_board.GetPieceOnPosition(e.Gridposition) != null)
            {
                switch (_currentPlayer)
                {
                    case PlayerColor._white:
                        if (_board.HasWhiteFliped) { _board.SetTileHighlight(e.Gridposition, TileVisual.PieceFlip); }
                        break;
                    case PlayerColor._black:
                        if (_board.HasBlackFliped) { _board.SetTileHighlight(e.Gridposition, TileVisual.PieceFlip); }
                        break;
                }
            }
            //if not part af validTiles, search for Valid tiles again.
            else
            {
                FindValidTiles();
            }
        }

        /// <summary>
        /// Logic for when Board.PositionClicked Event is called.
        /// </summary>
        /// <param name="sender"> Sender of the event: Board.model</param>
        /// <param name="e"> The position of the Cliked Tile</param>
        private void _board_PositionClicked(object sender, PositionEventArgs e)
        {
            //make a new list for commands
            _commands = new List<Icommand>();


            if (_validPositions.Contains(e.Gridposition))
            {
                //add place tile command
                _commands.Add(new PlacePieceCommand(_board, e.Gridposition, _currentPlayer));

                //capture pieces from tile clicked
                CapturePieces(e.Gridposition);

                //add change player command
                _commands.Add(new ChangePlayerCommand(this));

                //find the valid tiles agian
                FindValidTiles();

                //invoke all commands
                _commandInvoker.ExecuteCommand(new CompositeCommand(_commands));

                //update the score
                ScoreUpdate();

                //Check if valid pos is empty
                if(_movementHelper.GetValidPositions(_currentPlayer).Count <= 0)
                {
                    //Invokes board.GameOverFunc.
                    _board.GameOverFunc();
                }
            }

            else if(_board.GetPieceOnPosition(e.Gridposition) != null )
            {
                switch (_currentPlayer)
                {
                    case PlayerColor._white:
                        if (_board.HasWhiteFliped) 
                        {
                            _commands.Add(new ChangePieceColorCommand(_board, e.Gridposition, this));
                            _commands.Add(new ChangePlayerCommand(this));
                            _commandInvoker.ExecuteCommand(new CompositeCommand(_commands));
                        }
                        break;
                    case PlayerColor._black:
                        if (_board.HasBlackFliped)
                        {
                            _commands.Add(new ChangePieceColorCommand(_board, e.Gridposition, this));
                            _commands.Add(new ChangePlayerCommand(this));
                            _commandInvoker.ExecuteCommand(new CompositeCommand(_commands));
                        }
                        break;
                }
                ScoreUpdate();
            }
        }

        /// <summary>
        /// Finds Valid tiles and Highlights them.
        /// </summary>
        private void FindValidTiles()
        {
            //Ask movementHelper for list with ValidPositions.
            _validPositions = _movementHelper.GetValidPositions(_currentPlayer);

            //Set Visual or eachtTile in ValidPos
            foreach (var pos in _validPositions)
            {
                _board.SetTileHighlight(pos, TileVisual.Highlight);
            }
        }

        /// <summary>
        /// Highlights the Tiles that will capture pieces. 
        /// </summary>
        /// <param name="pos"> Hovered Tile, used to as start for capture.</param>
        private void HighlightCapturePieces(HexGridPos pos)
        {
            //Ask movementHelper for a list with Tiles that can capture pieces.
            _capturePositions = _movementHelper.HighlightCapturePositions(_currentPlayer, pos);

            //Change visual for each Tile in CapturePositions
            foreach (var piece in _capturePositions)
            {
                _board.SetTileHighlight(piece, TileVisual.Capture);
            }
        }

        /// <summary>
        /// Capture pieces based on the capturePos.
        /// </summary>
        /// <param name="pos"> The clicked Tile used as a start</param>
        private void CapturePieces(HexGridPos pos)
        {
            //Ask movementHelper for list of tiles that have pieces that can be captured.
            _capturePositions = _movementHelper.GetCapturePositions(_currentPlayer, pos);

            //Capture each piece.
            foreach (HexGridPos capturepos in _capturePositions)
            {
                _commands.Add(new ChangeColorCommand(_board, capturepos, _currentPlayer));
            }
        }

        /// <summary>
        /// Invoke the UpdateScore Event with the score for white and black.
        /// </summary>
        private void ScoreUpdate()
        {
            int white = 0;
            int black = 0;

            //calculate score for white and black.
            foreach (PieceModel piece in _board._pieces.Values) 
            {
                switch (piece.PlayerColor)
                {
                    case PlayerColor._white:
                        white++;
                        break;
                    case PlayerColor._black: 
                        black++;
                        break;
                }
            }

            float scoreWhite = (float)white/ (float)_board._pieces.Count;
            float scoreBlack = (float)black / (float)_board._pieces.Count;

            if(_playerWhite != null)
            {
                _playerWhite.Score = scoreWhite;
            }
            if(_playerBlack != null)
            {
                _playerBlack.Score = scoreBlack;
            }
            //invoke the UpdateScore event
            UpdateScore?.Invoke(this, new ScoresEventArgs(white, black));
        }

        /// <summary>
        /// Initialise the pieces on the board.
        /// </summary>
        private void InitializeBoard()
        {
            //All black pieces on the board
            _board.PlacePiece(new HexGridPos(0,0,0), PlayerColor._black);
            _board.PlacePiece(new HexGridPos(1,-1,0), PlayerColor._black);
            _board.PlacePiece(new HexGridPos(0,1,-1), PlayerColor._black);
            _board.PlacePiece(new HexGridPos(-1,0,1), PlayerColor._black);

            //All white pieces on the board
            _board.PlacePiece(new HexGridPos(-1,1,0), PlayerColor._white);
            _board.PlacePiece(new HexGridPos(1,0,-1), PlayerColor._white);
            _board.PlacePiece(new HexGridPos(0,-1,1), PlayerColor._white);
        }
    }
}
