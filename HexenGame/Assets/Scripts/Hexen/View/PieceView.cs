using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hexen.Model;
using System;
using UnityEngine.EventSystems;

namespace Hexen.View
{
    public class PieceView : MonoBehaviour, IPointerClickHandler
    {
        public Quaternion _defaultRotation {  get; private set; }
        public Quaternion _colorRotation { get; private set; }
        public PieceModel Piece { get; private set; }
        public PlayerColor PlayerColor {  get; private set; }

        private void Awake()
        {
            _defaultRotation = transform.rotation;
            _colorRotation = Quaternion.Euler(0, 0, 180);
        }

        /// <summary>
        /// Sets the PieceModel of the PieceView
        /// </summary>
        /// <param name="piece"></param>
        public void SetModel(PieceModel piece)
        {
            Piece = piece;
            piece.Removed += Piece_Removed;

            if(Piece.PlayerColor == PlayerColor._black)
            {
                transform.rotation = _colorRotation;
            }
            else transform.rotation = _defaultRotation;
        }

        /// <summary>
        /// Remove the piece
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Piece_Removed(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Piece.Click();
        }
    }
}
