using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private Transform _cardGridParent;
    [SerializeField] private List<Transform> _cardPrefabList;
    [SerializeField] private int numberOfEachPairToSpawn;
    private List<Transform> _cardsToSpawnList;

    private void Awake() {
        _cardsToSpawnList = new List<Transform>();
    }

     private void PopulateCardSpawnList(){
        foreach (Transform cardPrefab in _cardPrefabList)
        {
            for (int i = 0; i < numberOfEachPairToSpawn; i++){
                _cardsToSpawnList.Add(cardPrefab);
                _cardsToSpawnList.Add(cardPrefab);
            }
        }
    }

    private void PopulateCardGrid(){
        int xPos = 0;
        int zPos = 0;
        int gridWidth = 4;

        for (int i = _cardsToSpawnList.Count; i > 0; i--){
            int pickedCard = Random.Range(0, _cardsToSpawnList.Count);
            Transform cardTransform = Instantiate(_cardsToSpawnList[pickedCard], _cardGridParent);
            cardTransform.localPosition = new Vector3 (xPos * 0.6f, 0, zPos * 0.6f);
            xPos ++;

            if(xPos % gridWidth == 0){
                zPos ++;
                xPos = 0;
            }

            _cardsToSpawnList.RemoveAt(pickedCard);
        }       
    }

    public void Spawn() {
        PopulateCardSpawnList();
        PopulateCardGrid();
    }
}
