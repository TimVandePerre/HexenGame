using System;
using System.Collections.Generic;

namespace Hexen.Model
{
    public class BoardModel
    {
        //Event send when a piece is spawned, it sends the spawned piece as an argument. (Subs: BoardView)
        public event EventHandler<PieceEventArgs> PieceSpawned;

        //Event send when a tile is clicked it sends the position of the tile. (Subs: HexenEngin)
        public event EventHandler<PositionEventArgs> PositionClicked;

        //Event send when a tile is hovered over it send the position of the tile. (Subs: HexenEngin)
        public event EventHandler<PositionEventArgs> PositionHovered;

        //event send when the game is over.
        public event EventHandler<EventArgs> GameOver;

        //Dictionary giving each Tile a gridpos, you can search for tile based on the gridpos.
        public Dictionary<HexGridPos, TileModel> _tiles = new Dictionary<HexGridPos, TileModel>();

        //Dictionary giving each piece a gridpos, you can find a piece based on the gridpos.
        public Dictionary<HexGridPos, PieceModel> _pieces = new Dictionary<HexGridPos, PieceModel>();

        public bool HasWhiteFliped = true;

        public bool HasBlackFliped = true;

        /// <summary>
        /// Adds Tilemodel of tile to dictionary based on gridpos.
        /// </summary>
        /// <param name="pos"> The grid pos that the tile is on</param>
        /// <returns> TileModel </returns>
        public TileModel AddTile(HexGridPos pos)
        {
            TileModel tile = new TileModel(this, pos);

            _tiles.Add(pos, tile);

            return tile;
        }

        /// <summary>
        /// Sets the visual of the Tile.
        /// </summary>
        /// <param name="pos">Gridpos of Tile</param>
        /// <param name="visual">Visual the tile must have</param>
        public void SetTileHighlight(HexGridPos pos, TileVisual visual)
        {
            TileModel tile = GetTileOnPosition(pos);
            switch (visual)
            {
                case TileVisual.None:
                    tile.VisualTile = TileVisual.None;
                    break;
                case TileVisual.Highlight:
                    tile.VisualTile = TileVisual.Highlight;
                    break;
                case TileVisual.Capture:
                    tile.VisualTile = TileVisual.Capture;
                    break;
                case TileVisual.PieceFlip:
                    tile.VisualTile = TileVisual.PieceFlip;
                    break;
            }
        }

        /// <summary>
        /// Invoke the positionClicked event when tile is clicked.
        /// </summary>
        /// <param name="tile"> Clikced Tile</param>
        public void TileClicked(TileModel tile)
        {
            //Check if there is a piece on tile.
            if (GetPieceOnTile(tile) != null)
            {
                return;
            }
            //Choot the event when tile is empty
            else PositionClicked?.Invoke(this, new PositionEventArgs(tile.GridPosition));
        }

        /// <summary>
        /// Invoke the positionHovered event when tile is hovered.
        /// </summary>
        /// <param name="tile">Hovered tile</param>
        public void TileHover(TileModel tile)
        {
            PositionHovered?.Invoke(this, new PositionEventArgs(tile.GridPosition));
        }

        /// <summary>
        /// Returns the TileModel on the given GridPos.
        /// </summary>
        /// <param name="pos"> GridPos to search for tile</param>
        /// <returns> TileModel</returns>
        public TileModel GetTileOnPosition(HexGridPos pos)
        {
            return _tiles.GetValueOrDefault(pos);
        }

        /// <summary>
        /// Return PieceModel on a given TileModel.
        /// </summary>
        /// <param name="tile"> Tile to get Piece from</param>
        /// <returns>PieceModel on Tile</returns>
        public PieceModel GetPieceOnTile(TileModel tile)
        {
            return _pieces.GetValueOrDefault(tile.GridPosition);
        }

        /// <summary> 
        /// Returns the PieceModel on Position. 
        /// </summary>
        /// <param name="pos"> the position of the piece</param>
        /// <returns> PieceModel</returns>
        public PieceModel GetPieceOnPosition(HexGridPos pos)
        {
            return _pieces.GetValueOrDefault(pos);
        }

        /// <summary>
        /// Place a piece on pos with Color.
        /// </summary>
        /// <param name="pos">HexPos to place piece on</param>
        /// <param name="player">PlayerColor for piece</param>
        /// <returns>PieceModel</returns>
        public PieceModel PlacePiece(HexGridPos pos, PlayerColor player)
        {
            PieceModel piece = new PieceModel(this, pos, player);

            _pieces.Add(pos, piece);

            PieceSpawned?.Invoke(piece, new PieceEventArgs(piece));
            return piece;
        }

        /// <summary>
        /// Remove a piece on pos.
        /// </summary>
        /// <param name="pos"> HexPos to remove piece from</param>
        public void RemovePiece(HexGridPos pos)
        {
            if (_pieces.ContainsKey(pos))
            {
                _pieces[pos].Remove();
                _pieces.Remove(pos);
            }
        }

        /// <summary>
        /// Invoke GameOver event. 
        /// </summary>
        public void GameOverFunc()
        {
            GameOver?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Logic for when a piece is clicked. 
        /// </summary>
        /// <param name="piece"></param>
        public void PieceClicked(PieceModel piece)
        {
            PositionClicked?.Invoke(this, new PositionEventArgs(piece.GridPosition));
        }

        /// <summary>
        /// Logic for when a tile is hovered.
        /// </summary>
        /// <param name="piece"></param>
        public void PieceHovered(PieceModel piece)
        {
            PositionHovered?.Invoke(this, new PositionEventArgs(piece.GridPosition));
        }
    }
}
