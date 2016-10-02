using Griff.Fen;
using System.Text;
using Griff.Definitions;

namespace Griff.Board_Representation
{
    sealed partial class BoardPosition
    {
        /// <summary>
        /// Displays at the console the current position as a string
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        internal static string DumpPosition(BoardPosition position)
        {
            StringBuilder output = new StringBuilder();

            // Holds the pieces of the board in chars
            char[,] board = new char[8, 8];

            // Fill board with empty squares
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = ' ';
                }
            }

            // Fill in the pieces
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < PieceType.List.Length - 1; j++)
                {
                    // Bitboard that holds all pieces of one kind
                    ulong piecesBitBoard = position.Pieces[i][j];
                    while (piecesBitBoard != 0)
                    {
                        // Retrieve one piece position (least significant bit)                        
                        ulong currentPiecePosition = piecesBitBoard & (~piecesBitBoard + 1);

                        // Get coordinate of current piece position
                        Pair pair = SquareMap.LongToCoordinate[currentPiecePosition];
                        board[pair.First - 1, pair.Second - 1] = PieceMap.PieceToChar[i][j];

                        // Remove the current piece from the pieces bitboard                        
                        piecesBitBoard = piecesBitBoard & (piecesBitBoard - 1);
                    }
                }
            }

            // Construct a string containing a formatted board position
            output.AppendLine("   A|B|C|D|E|F|G|H");
            output.AppendLine("   ----------------");
            for (int i = 7; i >= 0; i--)
            {
                output.Append(i + 1);
                output.Append(" |");
                for (int j = 0; j < 8; j++)
                {
                    output.Append(board[j, i]);
                    output.Append('|');
                }
                output.Append(" ");
                output.Append(i + 1);
                output.AppendLine("\r\n   ----------------");
            }
            output.AppendLine("   A|B|C|D|E|F|G|H");

            return output.ToString();
        }     
    }
}
