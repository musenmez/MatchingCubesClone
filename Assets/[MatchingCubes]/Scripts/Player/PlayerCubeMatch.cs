using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCubeMatch : MonoBehaviour
{
    private PlayerStack _playerStack;
    private PlayerStack PlayerStack => _playerStack == null ? _playerStack = GetComponentInParent<PlayerStack>() : _playerStack;

    private const float MATCH_DELAY = 0.3f;
    private const int MATCH_THRESHOLD = 3;

    private Coroutine _matchCoroutine = null;

    private void OnEnable()
    {
        PlayerStack.OnStackUpdated.AddListener(StartMatchCoroutine);
    }

    private void OnDisable()
    {
        PlayerStack.OnStackUpdated.RemoveListener(StartMatchCoroutine);
    }

    private void StartMatchCoroutine() 
    {
        if (_matchCoroutine != null)
        {
            StopCoroutine(_matchCoroutine);
        }

        _matchCoroutine = StartCoroutine(MatchCoroutine());
    }

    private IEnumerator MatchCoroutine() 
    {
        yield return new WaitForSeconds(MATCH_DELAY);
        CheckMatch();
    }

    private void CheckMatch() 
    {
        if (PlayerStack.Cubes.Count <= 1)
            return;

        int matchCount = 1;
        CubeType previousType = PlayerStack.Cubes[0].CubeType;
        List<Cube> matchedCubes = new List<Cube> {PlayerStack.Cubes[0]};            

        for (int i = 1; i < PlayerStack.Cubes.Count; i++)
        {
            Cube cube = PlayerStack.Cubes[i];           

            if (previousType == cube.CubeType && !cube.IsMatched)
            {
                matchCount++;
                matchedCubes.Add(cube);
            }

            else
            {
                matchCount = 1;
                matchedCubes = new List<Cube> {cube};
                previousType = cube.CubeType;                
            }

            if (matchCount >= MATCH_THRESHOLD)
            {
                MatchCubes(matchedCubes);
                break;
            }
        }
    }

    private void MatchCubes(List<Cube> cubes) 
    {
        foreach (var cube in cubes)
        {
            cube.Match();
        }
    }
}
