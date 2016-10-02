namespace Griff.Definitions
{
    sealed class Move
    {
        internal int SourcePieceType;
        internal int TargetPieceType;
        internal ulong SourceSquare;
        internal ulong TargetSquare;
        internal bool IsShortCastle;
        internal bool IsLongCastle;
    }
}
