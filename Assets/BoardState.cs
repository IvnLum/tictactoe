using System;
public class BoardState
{
    protected int[,] state = new int[4, 4] {
                                {0, 0, 0, 0},
                                {0, 0, 0, 0},
                                {0, 0, 0, 0},
                                {0, 0, 0, 0}
                             };

    public int GetPos(int x, int y)
    {
        return state[x, y];
    }

    public void SetPos(int x, int y, int value)
    {
        state[x, y] = value;
    }

    public int EvaluateWin()
    {
        //Horizontal test.
        for (int i = 0; i < 4; i++)
        {
            bool victory = state[i, 0] == state[i, 1] && state[i, 1] == state[i, 2] && state[i, 2] == state[i, 3];
            if (victory && state[i, 0] != 0)
                return state[i, 0];
        }
        //Vertical test.
        for (int i = 0; i < 4; i++)
        {
            bool victory = state[0, i] == state[1, i] && state[1, i] == state[2, i] && state[2, i] == state[3, i];
            if (victory && state[0, i] != 0)
                return state[0, i];
        }

        //Right diagonal
        bool victoryDR = state[0, 0] == state[1, 1] && state[1, 1] == state[2, 2]  && state[2, 2] == state[3, 3];
        if (victoryDR && state[0, 0] != 0)
            return state[1, 1];

        //Left diagonal
        bool victoryLR = state[0, 3] == state[1, 2] && state[1, 2] == state[2, 1] && state[2, 1] == state[3, 0];
        if (victoryLR && state[0, 3] != 0)
            return state[0, 3];

        return 0;
    }

    public BoardState CreateCopy()
    {
        BoardState bs = new BoardState();
        Array.Copy(state, bs.state, state.Length);
        return bs;
    }

}