using System;
using Griff.Definitions;

namespace Griff.Board_Representation
{
    sealed partial class BoardPosition
    {   
        /// <summary>
        /// Makes the specified move on the board
        /// </summary>
        internal static void MakeMove(Move move, BoardPosition position)
        {
            int opponentPlayer = position.PlayerToMove ^ 1;

            if ((move.SourceSquare & position.Pieces[position.PlayerToMove][move.SourcePieceType]) == 0)
            {
                throw new Exception("Invalid move!");
            }

            #region Update Pieces list

            // Delete target piece
            if (move.TargetPieceType != PieceType.None)
            {
                // Delete the captured piece from Pieces list
                position.Pieces[opponentPlayer][move.TargetPieceType] ^= move.TargetSquare;
            }

            // Relocate source piece
            position.Pieces[position.PlayerToMove][move.SourcePieceType] ^= move.SourceSquare;
            position.Pieces[position.PlayerToMove][move.SourcePieceType] ^= move.TargetSquare;

            if (move.IsShortCastle)
            { 
                // move the rook
            }
            else if (move.IsLongCastle)
            {
 
            }

            #endregion

            #region Update AllPiecesBitboard

            // Remove (XOR out) the target piece
            if (move.TargetPieceType != PieceType.None)
            {
                position.AllPiecesOfAColorBitBoard[opponentPlayer] ^= move.TargetSquare;
            }

            // Relocate source piece
            position.AllPiecesOfAColorBitBoard[position.PlayerToMove] ^= move.SourceSquare;
            position.AllPiecesOfAColorBitBoard[position.PlayerToMove] ^= move.TargetSquare;

            #endregion

            #region Update HalfMovesSinceLastPawnMove, PreviousHalfMovesSinceLastPawnMove and MoveNumber

            // PreviousHalfMovesSinceLastPawnMove
            position.PreviousHalfMovesSinceLastPawnMove = position.HalfMovesSinceLastPawnMove;

            // HalfMovesSinceLastPawnMove
            if (move.SourcePieceType == PieceType.Pawn ||
                move.TargetPieceType == PieceType.Pawn)
            {
                position.HalfMovesSinceLastPawnMove = 0;
            }
            else
            {
                position.HalfMovesSinceLastPawnMove++;
            }

            // MoveNumber
            if (position.PlayerToMove == Player.Black)
            {
                position.MoveNumber++;
            }

            #endregion

            #region Update PlayerToMove

            position.PlayerToMove = opponentPlayer;

            #endregion
        }

        /// <summary>
        /// Un-makes the specified move on the board
        /// </summary>
        internal static void UnmakeMove(Move move, BoardPosition position)
        {
            var opponentPlayer = position.PlayerToMove ^ 1;

            if ((move.TargetSquare & position.Pieces[opponentPlayer][move.SourcePieceType]) == 0)
            {
                throw new Exception("Invalid move!");
            }

            #region Update Pieces list

            // Move source piece back to it's original position
            position.Pieces[opponentPlayer][move.SourcePieceType] ^= move.TargetSquare;
            position.Pieces[opponentPlayer][move.SourcePieceType] ^= move.SourceSquare;

            // Put back captured target piece
            if (move.TargetPieceType != PieceType.None)
            {
                // Delete the captured piece from Pieces list
                position.Pieces[position.PlayerToMove][move.TargetPieceType] ^= move.TargetSquare;
            }

            #endregion          

            #region Update AllPiecesBitboard

            // Relocate source piece
            position.AllPiecesOfAColorBitBoard[opponentPlayer] ^= move.TargetSquare;
            position.AllPiecesOfAColorBitBoard[opponentPlayer] ^= move.SourceSquare;

            // Add (XOR in) the target piece
            if (move.TargetPieceType != PieceType.None)
            {
                position.AllPiecesOfAColorBitBoard[position.PlayerToMove] ^= move.TargetSquare;
            }

            #endregion

            #region Update HalfMovesSinceLastPawnMove, PreviousHalfMovesSinceLastPawnMove and MoveNumber

            // HalfMovesSinceLastPawnMove
            position.HalfMovesSinceLastPawnMove = position.PreviousHalfMovesSinceLastPawnMove;

            // MoveNumber
            if (position.PlayerToMove == Player.White)
            {
                position.MoveNumber--;
            }

            #endregion

            #region Update PlayerToMove

            position.PlayerToMove = opponentPlayer;

            #endregion
        }                 
    }
}
