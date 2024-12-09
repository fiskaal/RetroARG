using UnityEngine;
using DG.Tweening;
using System;

namespace Platformer
{
    public class PlatformMover : MonoBehaviour
    {
        [SerializeField] Vector3 moveTo = Vector3.zero;
        [SerializeField] float moveTime = 1f;
        [SerializeField] Ease ease = Ease.InOutQuad;

        Vector3 startPos;

        private void Start()
        {
            startPos = transform.position;
            Move();
        }

        private void Move()
        {
            transform.DOMove(startPos + moveTo, moveTime).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
            
        }
    }
}
