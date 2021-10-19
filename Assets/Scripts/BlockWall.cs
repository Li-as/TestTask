using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlockWall : MonoBehaviour
{
    [SerializeField] private List<Block> _blocks;
    [SerializeField] private float _resetDuration;

    private int _stoppedBlocksCount;

    public event UnityAction AllBlocksStopped;
    public event UnityAction BlockWallResetFinished;

    private void OnEnable()
    {
        foreach (Block block in _blocks)
        {
            block.BallHit += OnBallHit;
            block.BlockStopped += OnBlockStopped;
        }
    }

    private void OnDisable()
    {
        foreach (Block block in _blocks)
        {
            block.BallHit -= OnBallHit;
            block.BlockStopped -= OnBlockStopped;
        }
    }

    private void OnBallHit()
    {
        foreach (Block block in _blocks)
        {
            block.StartStateChecking();
        }

        _stoppedBlocksCount = 0;
    }

    private void OnBlockStopped()
    {
        _stoppedBlocksCount++;
        if (_stoppedBlocksCount == _blocks.Count)
        {
            AllBlocksStopped?.Invoke();
        }
    }

    public void Reset()
    {
        StartCoroutine(ResetRoutine(_resetDuration));
    }

    private IEnumerator ResetRoutine(float duration)
    {
        float passedTime = 0;

        foreach (Block block in _blocks)
        {
            block.Rigidbody.isKinematic = true;
        }

        while (passedTime < duration)
        {
            foreach (Block block in _blocks)
            {
                block.transform.position = Vector3.Lerp(block.AfterStopPosition, block.StartPosition, passedTime / duration);
                block.transform.rotation = Quaternion.Lerp(block.AfterStopRotation, block.StartRotation, passedTime / duration);
            }

            passedTime += Time.deltaTime;
            yield return null;
        }

        foreach (Block block in _blocks)
        {
            block.transform.position = block.StartPosition;
            block.transform.rotation = block.StartRotation;
            block.Rigidbody.isKinematic = false;
        }

        BlockWallResetFinished?.Invoke();
    }    
}
