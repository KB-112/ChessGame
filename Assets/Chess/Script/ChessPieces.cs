using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Flags]
public enum SelectPlayer
{
    None = 0,
    Pawn = 1 << 0,      
    Elephant = 1 << 1,  
    Horse = 1 << 2,     
    Camel = 1 << 3,    
    King = 1 << 4,     
    Queen = 1 << 5      
}



public class ChessPieces : MonoBehaviour ,IStoreChessPiece
{
    public SelectPlayer player;
    public SliderValue slider;
    public GameObject[] listofPieces;
    public Dictionary<string, GameObject> pieceInstances;
    public Button checkCorrdiantes;
    public GameObject highlight;
    Vector2 newPosition;
    public Dictionary<GameObject ,string> storeSpawn;
  

    public static ChessPieces instance;
    public List<string> chesspieceName = new List<string>()

    {
        "Pawn",
        "Elephant",
        "Horse",
        "Camel",
        "King",
        "Queen"
    };

   public List<Vector2> positions = new List<Vector2>();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {   storeSpawn = new Dictionary<GameObject ,string>();
        pieceInstances = new Dictionary<string, GameObject>();

        for (float i = 0.5f; i < 8; i++)
        {
            for (float j = 0.5f; j < 8; j++)
            {
                Vector2 newPosition = new Vector2(i, j);
                positions.Add(newPosition);
            }
        }


        checkCorrdiantes.onClick.AddListener(delegate { SelectedPieceName(); });
        InputPiecesName();

    }
    private void Update()
    {
        UpdatePosition();
        
    }


    void SelectedPieceName()
    {           
        if (pieceInstances.Any(p => p.Key == player.ToString()) )
        {
            GameObject piece = pieceInstances[player.ToString()];

            Vector2 highlightPosition = new Vector2(highlight.transform.position.x, highlight.transform.position.y);
            if (positions.Contains(highlightPosition))
            {
                GameObject g = Instantiate(piece, highlight.transform.position, Quaternion.identity);
                for (int i = 0; i < chesspieceName.Count; i++)
                {
                    if (piece.name == chesspieceName[i])
                    {
                        storeSpawn.Add(g, chesspieceName[i]);
                    }

                }
                positions.Remove(highlightPosition);
            }

        }

    }

    void InputPiecesName()
    {
        pieceInstances.Add("Queen", listofPieces[0]);
        pieceInstances.Add("King", listofPieces[1]);
        pieceInstances.Add("Elephant", listofPieces[2]);
        pieceInstances.Add("Camel", listofPieces[3]); 
        pieceInstances.Add("Horse", listofPieces[4]);
        pieceInstances.Add("Pawn", listofPieces[5]);
    }

    void UpdatePosition()
    {  
        if (highlight != null)
       {

            float xCoord = slider.xCoordinates;
            float yCoord = slider.yCoordinates;
            newPosition = new Vector2(xCoord - 0.5f, yCoord - 0.5f);
            highlight.transform.position = newPosition;
          

       }
    }

    public Dictionary<GameObject, string> storeObject()
    {
        return storeSpawn;
    }


}
