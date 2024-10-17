using Hexen.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexen.View.Animation
{
    public class PieceAnimation : MonoBehaviour
    {
        //reference to the pieceView on the same gameobject
        private PieceView _pieceView;

        void Start()
        {
            //Set the pieceView and sunb to the event.
            _pieceView = GetComponent<PieceView>();
            _pieceView.Piece.ColourChanged += Piece_ColourChanged;
        }

        /// <summary>
        /// start annimation when the piece color has been changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Piece_ColourChanged(object sender, PieceEventArgs e)
        {
            StopAllCoroutines();
            if ( e.Piece.PlayerColor == PlayerColor._black)
            {
                StartCoroutine(ColorChange(_pieceView._colorRotation));
            }
            else
            {
                StartCoroutine(ColorChange(_pieceView._defaultRotation));
            }
        }

        IEnumerator ColorChange(Quaternion rot)
        {
            yield return StartCoroutine(Jump());

            yield return StartCoroutine(Rotate(rot));

            yield return StartCoroutine(Fall());

            Vector3 vector3 = transform.position;
            vector3.y = 0.05f;
            transform.position = vector3;
        }

        IEnumerator Jump()
        {
            Vector3 targetPos = transform.position + Vector3.up * 0.5f;

            while (transform.position.y < targetPos.y)
            {
                transform.position += Vector3.up * Time.deltaTime * 4;
                yield return null;
            }
            transform.position = targetPos;
        }
        IEnumerator Rotate(Quaternion rot)
        {
            float t = 0f;
            while (t < 0.1f)
            {
                t += Time.deltaTime;

                transform.rotation = Quaternion.Lerp(transform.rotation, rot, t);
                yield return null;
            }
            transform.rotation = rot;

        }
        IEnumerator Fall()
        {
            Vector3 targetPos = transform.position - Vector3.up * 0.5f;

            while (transform.position.y > targetPos.y)
            {
                transform.position -= Vector3.up * Time.deltaTime * 4;
                yield return null;
            }
            transform.position = targetPos;
        }
    }
}
