using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hexen.Model;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

namespace Hexen.View
{
    public class BoardView : MonoBehaviour
    {
        //Sets the PieceView Script in the unity editor.
        [SerializeField] private PieceView _piecePrefab;

        //reference to the BoardModel
        public BoardModel Board { get; private set; }

        private void Awake()
        {
            //Creating a new BoardModel
            Board = new BoardModel();

            //Subscribing to the PieceSpawned Event
            Board.PieceSpawned += BoardModel_PieceSpawned;

            //Get an aray of all Tileviews in the unity editor.
            TileView[] childTiles = GetComponentsInChildren<TileView>();

            //check all tileview in the aray
            foreach (TileView tileView in childTiles)
            {
                //get the hexgridpos based on the world position.
                HexGridPos hexGridPos = PositionHelper.WorldToGrid(tileView.transform.position);

                //add tile model to the dictionary in the board based on the HexGridPosition
                TileModel tileModel = Board.AddTile(hexGridPos);

                tileView.SetModel(tileModel);
            }
        }

        /// <summary>
        /// Instantiate a piece with the given PieceModel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoardModel_PieceSpawned(object sender, PieceEventArgs e)
        {
            PieceModel piece = e.Piece;

            PieceView prefab = _piecePrefab;

            GameObject spawnedPieceObject = GameObject.Instantiate(prefab.gameObject, PositionHelper.GridToWorld(piece.GridPosition), Quaternion.identity);
            
            spawnedPieceObject.GetComponent<PieceView>().SetModel(piece);
        }
    }
}
