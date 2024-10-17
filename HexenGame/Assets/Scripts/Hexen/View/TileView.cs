using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using Hexen.Model;

namespace Hexen.View
{
    public class TileView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        //TEMP: variables for highlight
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Material _placeableMaterial;
        [SerializeField] private Material _captureMaterial;
        [SerializeField] private Material _pieceFlipMaterial;

        private Material _defaultMaterial;


        public TileModel Tile { get; private set; }

        private void Awake()
        {
            //TEMP: 
            Assert.IsNotNull(_placeableMaterial); Assert.IsNotNull(_renderer);

            _defaultMaterial = _renderer.material;
        }

        public void SetModel(TileModel model)
        {
            Tile = model;
            model.TileVisualChanged += Model_TileVisualChanged;
        }

        private void Model_TileVisualChanged(object sender, EventArgs e)
        {
            switch(Tile.VisualTile)
            {
                case TileVisual.None:
                    _renderer.sharedMaterial = _defaultMaterial;
                    break;
                case TileVisual.Highlight:
                    _renderer.sharedMaterial = _placeableMaterial;
                    break;
                case TileVisual.Capture:
                    _renderer.sharedMaterial = _captureMaterial;
                    break;
                case TileVisual.PieceFlip:
                    _renderer.sharedMaterial = _pieceFlipMaterial;
                    break;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Tile.Click();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Tile.Hover();
        }
    }
}
