using System.Collections.Generic;
using Griff.Definitions;

namespace Griff.Fen
{
    /// <summary>
    /// Used for parsing or creating a FEN string
    /// </summary>
    static class PieceMap
    {
        internal static Dictionary<char, Piece> CharToPiece = new Dictionary<char, Piece>() 
        {
            {' ', Piece.None},
            {'r', new Piece(){ Color = Player.Black, Type = PieceType.Rook}},
            {'n', new Piece(){ Color = Player.Black, Type = PieceType.Knight}},
            {'b', new Piece(){ Color = Player.Black, Type = PieceType.Bishop}},
            {'q', new Piece(){ Color = Player.Black, Type = PieceType.Queen}},
            {'k', new Piece(){ Color = Player.Black, Type = PieceType.King}},
            {'p', new Piece(){ Color = Player.Black, Type = PieceType.Pawn}},
            {'R', new Piece(){ Color = Player.White, Type = PieceType.Rook}},
            {'N', new Piece(){ Color = Player.White, Type = PieceType.Knight}},
            {'B', new Piece(){ Color = Player.White, Type = PieceType.Bishop}},
            {'Q', new Piece(){ Color = Player.White, Type = PieceType.Queen}},
            {'K', new Piece(){ Color = Player.White, Type = PieceType.King}},
            {'P', new Piece(){ Color = Player.White, Type = PieceType.Pawn}}
        };

        internal static char[][] PieceToChar = new char[2][]
        {
            new char[6]
            {
                'R', 'N', 'B', 'Q', 'K', 'P'
            },
            new char[6]
            {
                'r', 'n', 'b', 'q', 'k', 'p'
            }
        };
    }
}
