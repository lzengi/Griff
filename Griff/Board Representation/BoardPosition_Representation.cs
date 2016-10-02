using Griff.Definitions;

namespace Griff.Board_Representation
{
    sealed partial class BoardPosition
    {
        /// <summary>
        /// Arrays, holding the board position of the pieces, both for white and black
        /// </summary>
        ulong[][] Pieces = new ulong[2][]
        {
            new ulong[PieceType.List.Length -1],
            new ulong[PieceType.List.Length -1]
        };

        /// <summary>
        /// Indicates the player to move
        /// </summary>
        int PlayerToMove;

        /// <summary>
        /// Holds flags indicating which castling rights are available
        /// for both white and black
        /// </summary>
        bool[] CanCastleQueenSide = new bool[2];
        bool[] CanCastleKingSide = new bool[2];

        /// <summary>
        /// Indicates the en-passant target square (i.e. e3, e6)
        /// </summary>
        ulong EnPassantTargetSquare;

        /// <summary>
        /// Number of half-moves (plies) since the last pawn move
        /// </summary>
        uint HalfMovesSinceLastPawnMove;

        /// <summary>
        /// This field stores the previous value of the HalfMovesSinceLastPawnMove field
        /// when we make a move on the board
        /// This will help us to restore the correct value for the HalfMovesSinceLastPawnMove field
        /// when we un-make the move
        /// </summary>
        uint PreviousHalfMovesSinceLastPawnMove;

        /// <summary>
        /// Idicates the current move number
        /// </summary>
        uint MoveNumber;

        /// <summary>
        /// Represents the bitboards containing all white pieces and all black pieces 
        /// </summary>
        ulong[] AllPiecesOfAColorBitBoard = new ulong[2];      
    }
}
