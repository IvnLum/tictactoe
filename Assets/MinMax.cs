using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class MinMax : MonoBehaviour
{
    public TextMeshProUGUI[] objRef = new TextMeshProUGUI[16];
    public TextMeshProUGUI statusMSG;
    BoardState state;
    int movementsLimit = 8;
    int movements = 0;
    bool ready = true;
    
    private void Start()
    {
        Restart();
    }
    public void Restart()
    {
        state = new BoardState();
        ready = true;
        movements = 0;
        statusMSG.color = new Color32(255, 255, 255, 255);
        statusMSG.text = "X player Starts!";

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                objRef[i*4 + j].text = " ";
            }
        }
        RandomSelect();
    }
    public void RandomSelect()
    {
        if (Random.Range(0, 2) == 0)
            return;
        statusMSG.text = "O player Starts";
        state.SetPos(Random.Range(0, 4), Random.Range(0, 4),  1);
        UpdateGrid();
    }
    private void UpdateGrid()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int val = state.GetPos(i, j);
                if (val  == -1)
                {
                    objRef[i*4 + j].color = new Color32(255, 255, 255, 255);
                    objRef[i*4 + j].text = "X";
                }
                if (val  == 1)
                {
                    objRef[i*4 + j].color = new Color32(255, 0, 0, 255);
                    objRef[i*4 + j].text = "O";
                }
            }
        }
    }

    public void ActivateButton(int idxX, int idxY)
    {
        if (movements == movementsLimit || !ready)
            return; // Awaiting for manual restart
        movements++;
        objRef[idxX*4 + idxY].text = "X";
        state.SetPos(idxX, idxY,  -1);
        
        if (state.EvaluateWin() == -1)
        {
            Debug.Log("You Won!");
            statusMSG.text = "X won!";
            ready = false;
            return;
        }

        state = CalculateMinMax(state, 4);
        UpdateGrid();

        if (state.EvaluateWin() == 1)
        {
            Debug.Log("You lose!");
            statusMSG.text = "O won!";
            ready = false;
            return;
        }
        if (movements == movementsLimit)
        {
            ready = false;
            Debug.Log("Tie");
            statusMSG.text = "It's a Tie!";
        }

    }
    public BoardState CalculateMinMax(BoardState newState, int depth)
    {
        return CalculateMinMax(newState, depth, int.MinValue, int.MaxValue).Item2;
    }

    (int, BoardState) CalculateMinMax(BoardState newState, int depth, int alpha, int beta, bool minMax = true)
    {
        int solutionValue = newState.EvaluateWin();
        if (solutionValue != 0 || depth == 0)
            return (solutionValue, newState);
        int? solVal = null;
        BoardState solState = newState;

        for (int i = 0; i < 4;  i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (newState.GetPos(i, j) != 0)
                    continue;
                BoardState testState = newState.CreateCopy();
                testState.SetPos(i, j, minMax?1:-1);
                int retVal;
                (retVal, _) = CalculateMinMax(testState, depth - 1, alpha, beta, !minMax);
                /*if (minMax == (retVal > solVal) || solVal == null)
                {
                    solVal = retVal;
                    solState = testState;
                }*/
                
                if (minMax && retVal > alpha)
                {
                    solVal = alpha = retVal;
                    solState = testState;
                }
                else if (retVal < beta && !minMax)
                {
                    solVal = beta = retVal;
                    solState = testState;
                }
                if (alpha >= beta)
                {
                    j = i = 4;
                }
            }

        }
        if (solVal != null)
        {
            solutionValue = (int)solVal;
        }
        return (solutionValue, solState);
    }
}
