using System.Collections.Generic;

namespace Hexen.Model.Engine
{
    public class MovementHelper
    {
        //Reference to the BoardModel, set in the Constructor
        private readonly BoardModel _board;

        //List with Directions set in the constructor.
        public List<HexGridPos> Directions = new List<HexGridPos>();

        /// <summary>
        /// Constructor: Sets reference to board and creates list.
        /// </summary>
        /// <param name="board">refernce to the boardModel</param>
        public MovementHelper(BoardModel board)
        {
            _board = board;

            Directions = new List<HexGridPos>
            {
                new HexGridPos(1,-1,0),
                new HexGridPos(-1,1,0),
                new HexGridPos(0,1,-1),
                new HexGridPos(0,-1,1),
                new HexGridPos(-1,0,1),
                new HexGridPos(1,0,-1)
            };
        }

        /// <summary>
        /// Finding all pos where a piece can be placed depending on the playerColor.
        /// </summary>
        /// <param name="currentPlayer"> currentPlayerColor</param>
        /// <returns>List with positions of tiles.</returns>
        public List<HexGridPos> GetValidPositions(PlayerColor currentPlayer)
        {
            //creating new list for the Valid Tiles positions.
            List<HexGridPos> validPositions = new List<HexGridPos>();

            //List with all Player Tiles positions.
            List<HexGridPos> playerPieces = GetPlayerTiles(currentPlayer);

            //Check all positions in list of players tiles positions.
            foreach(HexGridPos pos in playerPieces)
            {
                //Check all directions in list of directions
                foreach (HexGridPos direction in Directions)
                {
                    //The pos of tile we need to chech: the pos of PlayerList + the direction of direction List
                    HexGridPos enemyPlayer = pos + direction;

                    //check whether the Tile on this pos exists.
                    if (!_board._tiles.ContainsKey(enemyPlayer)) continue;

                    //get the piece on this pos.
                    PieceModel piece = _board.GetPieceOnPosition(enemyPlayer);

                    //if this piece does not exist or is the of the same color go to next direction
                    if (piece == null || piece.PlayerColor == currentPlayer) continue;


                    else
                    {
                        for (int i = 1; i < 7; i++)
                        {
                            HexGridPos hexGridPos = pos + direction * i;
                            if (_board._tiles.ContainsKey(hexGridPos))
                            {
                                if (!_board._pieces.ContainsKey(hexGridPos))
                                {
                                    validPositions.Add(hexGridPos);
                                    break;
                                }
                                else
                                {
                                    PieceModel enemypiece = _board.GetPieceOnPosition(hexGridPos);
                                    if (enemypiece.PlayerColor == currentPlayer) break;
                                }
                            }
                        }
                    }
                }
            }

            return validPositions;
        }

        /// <summary>
        /// Find all pos that have a piece that will be captured.
        /// </summary>
        /// <param name="currentPlayer"> the current player color</param>
        /// <param name="validtile"> The clicked tile.</param>
        /// <returns></returns>
        public List<HexGridPos> GetCapturePositions(PlayerColor currentPlayer, HexGridPos validtile)
        {
            //creat new list for the capture tile pos.
            List<HexGridPos> captureposition = new List<HexGridPos>();

            //go over all directions
            foreach(HexGridPos direction in Directions)
            {
                //check for 7 steps (max size of board.)
                for (int i = 1; i < 7; i++)
                {
                    //the tile 1 further from the starting tile along the direction
                    HexGridPos checktile = validtile + direction * i;

                    //check if tile exists
                    if (!_board._tiles.ContainsKey(checktile)) break;
                    
                    //get the piece on that pos
                    PieceModel piece = _board.GetPieceOnPosition(checktile);
                    //check if that piece exists
                    if (piece == null) break;

                    //check if the first tile along the direction is the same color
                    if(i==1 && piece.PlayerColor == currentPlayer) break;

                    //if it is the same color
                    if(piece.PlayerColor == currentPlayer)
                    {
                        // get all pos in between the starting pos and piece pos.
                        for (int j = 1; j <= i-1 ; j++)
                        {
                            //get tilepos
                            HexGridPos capturetile = checktile + direction * j * -1;
                            
                            //add tile pos to list
                            captureposition.Add(capturetile);
                        }
                        break;
                    }
                }
            }
            //return the list with tile pos.
            return captureposition;
        }

        /// <summary>
        /// Find all pos that need to change to capture visuals.
        /// </summary>
        /// <param name="currentPlayer"> current PlayerColor</param>
        /// <param name="validtile"> Starting Tile</param>
        /// <returns>List with HexGridPos</returns>
        public List<HexGridPos> HighlightCapturePositions(PlayerColor currentPlayer, HexGridPos validtile)
        {
            //creat new list for the capture tile pos.
            List<HexGridPos> captureposition = new List<HexGridPos>();

            //go over all directions
            foreach (HexGridPos direction in Directions)
            {
                //check for 7 steps (max size of board.)
                for (int i = 1; i < 7; i++)
                {
                    //the tile 1 further from the starting tile along the direction
                    HexGridPos checktile = validtile + direction * i;

                    //check if tile exists
                    if (!_board._tiles.ContainsKey(checktile)) break;

                    //get the piece on that pos
                    PieceModel piece = _board.GetPieceOnPosition(checktile);
                    //check if that piece exists
                    if (piece == null) break;

                    //check if the first tile along the direction is the same color
                    if (i == 1 && piece.PlayerColor == currentPlayer) break;

                    //if it is the same color
                    if (piece.PlayerColor == currentPlayer)
                    {
                        // go back to te starting pos
                        for (int j = 0; j <= i; j++)
                        {
                            //get tilepos
                            HexGridPos capturetile = checktile + direction * j * -1;

                            //add tile pos to list
                            captureposition.Add(capturetile);
                        }
                        break;
                    }
                }
            }
            //return the list with tile pos.
            return captureposition;
        }

        /// <summary>
        /// Get all playerColored pieces positions.
        /// </summary>
        /// <param name="currentPlayer"> current PlayerColer</param>
        /// <returns>List with HexGridPos</returns>
        private List<HexGridPos> GetPlayerTiles(PlayerColor currentPlayer)
        {
            //creating list with hexgrid pos
            List<HexGridPos> result = new List<HexGridPos>();

            //check all pieces 
            foreach (PieceModel piece in _board._pieces.Values)
            {
                //check if the piece is the same color as the player.
                if(piece.PlayerColor == currentPlayer)
                {
                    //add the piece pos to the list.
                    result.Add(piece.GridPosition);
                }
            }
            return result;
        }
    }
}
