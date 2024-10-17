using UnityEngine;
using Hexen.Model;
using Hexen.Model.Engine;
using Hexen.View;
using Hexen.View.UI;

public class HexenGame : MonoBehaviour
{
    public HexenEngine _engine { get; private set; }
    private HexenUI _hexenUI;
    
    private void Start()
    {
        BoardView board = FindObjectOfType<BoardView>();
        _engine = new HexenEngine(board.Board, PlayerColor._white);
        _hexenUI = GameObject.Find("HexenUI").GetComponent<HexenUI>();

        _engine.PlayerColorChanged += _engine_PlayerColorChanged;
        _engine.UpdateScore += _engine_UpdateScore;

        GameObject playerWhite = GameObject.Find("PlayerWhite");
        GameObject playerBlack = GameObject.Find("PlayerBlack");

        CheckAndSetPlayers(playerWhite, playerBlack);
    }

    private void CheckAndSetPlayers(GameObject playerWhite, GameObject playerBlack)
    {
        if (playerWhite != null)
        {
            PlayerModel playerModelWhite = new PlayerModel(true);
            playerWhite.GetComponent<PlayerView>().SetModel(playerModelWhite);
            _engine.SetPlayerWhite(playerModelWhite);
            _hexenUI.PlayerWhite = playerWhite.GetComponent<PlayerView>();
        }

        if (playerBlack != null)
        {
            PlayerModel playerModelBlack = new PlayerModel(true);
            playerBlack.GetComponent<PlayerView>().SetModel(playerModelBlack);
            _engine.SetPlayerBlack(playerModelBlack);
            _hexenUI.PlayerBlack = playerBlack.GetComponent<PlayerView>();
        }
    }


    /// <summary>
    /// On the event ScoreChanged, visually update the score.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void _engine_UpdateScore(object sender, ScoresEventArgs e)
    {
        _hexenUI.ScoreUpdate(e.ScoreWhite, e.ScoreBlack);
    }

    /// <summary>
    /// On the event PlayerColorChanged, visually change the highlight of the active player.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void _engine_PlayerColorChanged(object sender, PlayerColorEventArgs e)
    {
        _hexenUI.OnPlayerColorChanged(e.Color);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            _engine.Undo();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            _engine.Redo();
        }
    }
}
