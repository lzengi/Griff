namespace Griff.Definitions
{
    class PieceType
    {
        internal const int None = -1;
        internal const int Rook = 0;
        internal const int Knight = 1;
        internal const int Bishop = 2;
        internal const int Queen = 3;
        internal const int King = 4;
        internal const int Pawn = 5;
        

        internal static int[] List = new int[7] 
        {
            PieceType.None,
            PieceType.Rook,
            PieceType.Knight,
            PieceType.Bishop,
            PieceType.Queen,
            PieceType.King,
            PieceType.Pawn            
        };
    }
}
