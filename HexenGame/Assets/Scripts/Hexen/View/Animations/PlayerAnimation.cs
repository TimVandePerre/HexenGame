using System.Collections;
using UnityEngine;

namespace Hexen.View.Animation
{
    public class PlayerAnimation : MonoBehaviour
    {
        private float _score;

        public void Animation(float score)
        {
            StopAllCoroutines();
            _score = score;
            StartCoroutine(SizeUpdate());
        }

        IEnumerator SizeUpdate()
        {
            Vector3 targetsize = new Vector3(8*_score, 0.5f, 0.5f);
            while (transform.localScale.x < targetsize.x)
            {
                transform.localScale += Vector3.right * Time.deltaTime * 8;
                yield return null;
            }

            while (transform.localScale.x > targetsize.x)
            {
                transform.localScale -= Vector3.right * Time.deltaTime * 8;
                yield return null;
            }
            transform.localScale = targetsize;
        }
    }
}
