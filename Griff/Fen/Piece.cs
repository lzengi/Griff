using Griff.Definitions;

namespace Griff.Fen
{
    sealed class Piece
    {
        internal int Type;
        internal int Color;

        internal static readonly Piece None = new Piece(){ Color = Player.None, Type = PieceType.None};
    }
}
