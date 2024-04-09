using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class HighlightPlayerMoves : MonoBehaviour
{
    Dictionary<GameObject, string> coordinatesofSpawnObject;
    IStoreChessPiece storeChessPiece;
    public GameObject[] highlightsymbol;
    List<GameObject> storeSpawnHighlight = new List<GameObject>();
    List<GameObject> piecesChess = new List<GameObject>();
    private void Start()
    {
        storeChessPiece = GetComponent<IStoreChessPiece>();
    }

    private void Update()
    {
        FindoutPieces();
    }

    void FindoutPieces()
    {
        if (Input.GetMouseButtonDown(0))
        {
            coordinatesofSpawnObject = storeChessPiece.storeObject();
            if (coordinatesofSpawnObject != null)
            {
                foreach (KeyValuePair<GameObject, string> pair in coordinatesofSpawnObject)
                {
                    GameObject pieceObject = pair.Key;
                    string pieceName = pair.Value;
                    if (pieceObject.transform.position == ChessPieces.instance.highlight.transform.position)
                    {
                        HighlightSteps(pieceName, pieceObject);
                        Debug.Log(pieceName);
                    }
                }
            }
        }
    }

    void HighlightSteps(string piece, GameObject piecePosition)
    {
        ClearSpawnedHighlights();
        List<Vector3> posStore = new List<Vector3>();

        switch (piece)
        {
            case "Pawn":
                AddPawnMoves(piecePosition.transform.position, posStore);
                break;

            case "Elephant":
                AddStraightMoves(piecePosition.transform.position, posStore);
                break;

            case "Camel":
                AddDiagonalMoves(piecePosition.transform.position, posStore);
                break;

            case "Queen":
                AddStraightMoves(piecePosition.transform.position, posStore);
                AddDiagonalMoves(piecePosition.transform.position, posStore);
                break;

            case "King":
                AddKingMoves(piecePosition.transform.position, posStore);
                break;

            case "Horse":
                AddHorseMoves(piecePosition.transform.position, posStore);
                break;
        }

        SpawnHighlight(piecePosition, posStore);
    }

    void AddPawnMoves(Vector3 position, List<Vector3> posStore)
    {
        Vector3 pos1 = new Vector3(position.x, position.y + 1, 0);
        posStore.Add(pos1);
    }

    void AddStraightMoves(Vector3 position, List<Vector3> posStore)
    {
        for (int i = 1; i <= 8; i++)
        {
            
            Dictionary<int, List<int>> moves = new Dictionary<int, List<int>>()
            {
                { 0, new List<int> { i, -i } },
                { i,  new List<int> { 0 } },
                { -i,  new List<int> { 0 } }
            };

            foreach (var kvp in moves)
            {
                int offsetX = kvp.Key;
                foreach (int offsetY in kvp.Value)
                {
                    posStore.Add(new Vector3(position.x + offsetX, position.y + offsetY, 0));
                }
            }
        }
    }

    void AddDiagonalMoves(Vector3 position, List<Vector3> posStore)
    {
        int[] offsets = { 1, -1 };

        foreach (int offsetX in offsets)
        {
            foreach (int offsetY in offsets)
            {
                for (int i = 1; i <= 8; i++)
                {
                    posStore.Add(new Vector3(position.x + i * offsetX, position.y + i * offsetY, 0));
                }
            }
        }
    }


    void AddKingMoves(Vector3 position, List<Vector3> posStore)
    {
        int[] offsets = { -1, 0, 1 };

        foreach (int offsetX in offsets)
        {
            foreach (int offsetY in offsets)
            {
                if (offsetX == 0 && offsetY == 0) continue; 
                posStore.Add(new Vector3(position.x + offsetX, position.y + offsetY, 0));
            }
        }
    }


    void AddHorseMoves(Vector3 position, List<Vector3> posStore)
    {
        Dictionary<int, List<int>> moves = new Dictionary<int, List<int>>()
        {
            { 1, new List<int> { 2, -2 } },
            { -1, new List<int> { 2, -2 } },
            { 2, new List<int> { 1, -1 } },
            { -2, new List<int> { 1, -1 } }
        };

        foreach (var kvp in moves)
        {
            int offsetX = kvp.Key;
            foreach (int offsetY in kvp.Value)
            {
                posStore.Add(new Vector3(position.x + offsetX, position.y + offsetY, 0));
            }
        }
    }

    void SpawnHighlight(GameObject piecePosition, List<Vector3> posStore)
    {
        piecesChess.Add(piecePosition);
        posStore.Where(pos =>
            pos.x >= 0 && pos.x < 8 && pos.y >= 0 && pos.y < 8 && !piecesChess.Any(p => p.transform.position == pos)
        ).ToList().ForEach(pos =>
        {
            GameObject spawn = Instantiate(highlightsymbol[0], pos, Quaternion.identity);
            storeSpawnHighlight.Add(spawn);
        });
    }

    void ClearSpawnedHighlights()
    {
        foreach (GameObject highlight in storeSpawnHighlight)
        {
            Destroy(highlight);
        }
        storeSpawnHighlight.Clear();
    }
}
