using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class HighlightPlayerMoves : MonoBehaviour
{
    Dictionary<GameObject, string> coordinatesofSpawnObject;
    IStoreChessPiece storeChessPiece;
    public GameObject[] highlightsymbol;
    List<GameObject> storeSpawnHighlight = new List<GameObject>();
    List<Vector3> storeChessPieceCurrentPos = new List<Vector3>();
    List<Vector3> posStore = new List<Vector3>();
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

    void HighlightSteps(string piece, GameObject pieceCurrentPosition)
    {
        ClearSpawnedHighlights();


        switch (piece)
        {
            case "Pawn":
                AddPawnMoves(pieceCurrentPosition.transform.position);
            break;

            case "Elephant":
                AddStraightMoves(pieceCurrentPosition.transform.position);
            break;

            case "Camel":
                AddDiagonalMoves(pieceCurrentPosition.transform.position);
            break;

            case "Queen":
                AddStraightMoves(pieceCurrentPosition.transform.position);
                AddDiagonalMoves(pieceCurrentPosition.transform.position);
             break;

            case "King":
                AddKingMoves(pieceCurrentPosition.transform.position);
             break;

            case "Horse":
                AddHorseMoves(pieceCurrentPosition.transform.position);
             break;
        }

        Vector3 pos = new Vector3(pieceCurrentPosition.transform.position.x,pieceCurrentPosition.transform.position.y,0);
        SpawnHighlight(pos);
        
    }
    void AddPawnMoves(Vector3 position)
    {
        Vector3 pos1 = new Vector3(position.x, position.y + 1, 0);
        posStore.Add(pos1);
    }

    void AddStraightMoves(Vector3 position)
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

    void AddDiagonalMoves(Vector3 position)
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


    void AddKingMoves(Vector3 position)
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


    void AddHorseMoves(Vector3 position)
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

   void SpawnHighlight(Vector3 piecePosition)
{
        storeChessPieceCurrentPos.Add(piecePosition);

        for (int i = 0; i < storeChessPieceCurrentPos.Count; i++)
        {
            if (posStore.Contains(storeChessPieceCurrentPos[i]))
            {
                int index = posStore.IndexOf(storeChessPieceCurrentPos[i]);
              //  Debug.Log("Position found in posStore: " + storeChessPieceCurrentPos[i].ToString() + ", Index in posStore: " + index);
            }
        }

        for (int i = 0; i < posStore.Count; i++)
        {
            if ((posStore[i].x >= 0 && posStore[i].x < 8) && (posStore[i].y >= 0 && posStore[i].y < 8))
            {

                GameObject spawn = Instantiate(highlightsymbol[0], posStore[i], Quaternion.identity);
                storeSpawnHighlight.Add(spawn);
            }

        }  
        
   }


    void ClearSpawnedHighlights()
    {
        foreach (GameObject highlight in storeSpawnHighlight)
        {
            Destroy(highlight);
        }
        if (storeSpawnHighlight != null && posStore != null)
        {
            storeSpawnHighlight.Clear();
            posStore.Clear();
        }
    }
}
