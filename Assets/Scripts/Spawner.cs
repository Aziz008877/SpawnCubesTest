using System;
using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _moveDuration, _moveDistance, _cubeSpawnCoolDown;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Ease _easeType;

    private void Awake()
    {
        StartCoroutine(SpawnCube());
    }
    
    private IEnumerator SpawnCube()
    {
        yield return new WaitForSeconds(_cubeSpawnCoolDown);
        var spawnedCube = Instantiate(_cubePrefab, _spawnPosition.position, Quaternion.identity);
        MoveSpawnedCube(spawnedCube);
    }

    private void MoveSpawnedCube(GameObject moveCube)
    {
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(
        moveCube.transform
            .DOMoveZ(_moveDistance, _moveDuration)
            .SetEase(_easeType));
        sequence.OnComplete(() => Destroy(moveCube));
        
        StartCoroutine(SpawnCube());
    }
    
    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}
