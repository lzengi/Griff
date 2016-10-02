namespace Griff.Board_Representation
{
    sealed partial class BoardPosition
    {
        internal static BoardPosition ClonePosition(BoardPosition position)
        {
            BoardPosition clone = new BoardPosition();

            // Pieces
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    clone.Pieces[i][j] = position.Pieces[i][j];
                }
            }

            return clone;
        }       
    }
}
