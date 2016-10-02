using Griff.Definitions;
using System;
using System.Collections.Generic;

namespace Griff.Board_Representation
{
    sealed partial class BoardPosition
    {
        /// <summary>
        /// Generates legal moves
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        internal static Move[] GeneratePseudoLegalMoves(BoardPosition position)
        {
            List<Move> nonCaptureMoves = new List<Move>();
            List<Move> captureMoves = new List<Move>();
            List<Move> legalMoves = new List<Move>();

            #region Knight moves

            // Knight moves

            // Bitboard that holds the position of all knights
            ulong knightsBoard = position.Pieces[position.PlayerToMove][PieceType.Knight];
            while (knightsBoard != 0)
            {
                // Retrieve one piece position (least significant 1 bit)                
                ulong currentKnightPosition = knightsBoard & (~knightsBoard + 1);

                ulong possibleKnightMoves = 0;

                if ((currentKnightPosition & BitBoards.Region_B1H1B6H6) != 0) { possibleKnightMoves |= currentKnightPosition >> 17; }
                if ((currentKnightPosition & BitBoards.Region_A1G1A6G6) != 0) { possibleKnightMoves |= currentKnightPosition >> 15; }
                if ((currentKnightPosition & BitBoards.Region_C1H1C7H7) != 0) { possibleKnightMoves |= currentKnightPosition >> 10; }
                if ((currentKnightPosition & BitBoards.Region_A1F1A7F7) != 0) { possibleKnightMoves |= currentKnightPosition >> 6; }
                if ((currentKnightPosition & BitBoards.Region_C2H2C8H8) != 0) { possibleKnightMoves |= currentKnightPosition << 6; }
                if ((currentKnightPosition & BitBoards.Region_A2F2A8F8) != 0) { possibleKnightMoves |= currentKnightPosition << 10; }
                if ((currentKnightPosition & BitBoards.Region_B3H3B8H8) != 0) { possibleKnightMoves |= currentKnightPosition << 15; }
                if ((currentKnightPosition & BitBoards.Region_A3G3A8G8) != 0) { possibleKnightMoves |= currentKnightPosition << 17; }
                
                // Filter out 'illegal' moves (moves where you would move on top of your own piece; doesn't verify if you leave your king in check)               
                possibleKnightMoves &= ~position.AllPiecesOfAColorBitBoard[position.PlayerToMove];

                while (possibleKnightMoves != 0)
                {
                    // Retrieve one piece position (least significant 1 bit)                    
                    ulong candidateKnightMove = possibleKnightMoves & (~possibleKnightMoves + 1);

                    // Check candidate move, and add generated moves to the move lists
                    InspectCandidate(candidateKnightMove, currentKnightPosition, PieceType.Knight, position, captureMoves, nonCaptureMoves, false, false);                                     

                    // Remove the current position from the possible candidate moves                        
                    possibleKnightMoves = possibleKnightMoves & (possibleKnightMoves - 1);
                }             

                // Remove the current piece from the pieces bitboard                        
                knightsBoard = knightsBoard & (knightsBoard - 1);
            }       

            #endregion

            #region King moves

            // King moves

            // Bitboard that holds the position of all kings
            ulong currentKingPosition = position.Pieces[position.PlayerToMove][PieceType.King];

            ulong possibleKingMoves = 0;

            // Non-castling moves
            if ((currentKingPosition & BitBoards.Region_B1H1B7H7) != 0) { possibleKingMoves |= currentKingPosition >> 9; }
            if ((currentKingPosition & BitBoards.Region_A1H1A7H7) != 0) { possibleKingMoves |= currentKingPosition >> 8; }
            if ((currentKingPosition & BitBoards.Region_A1G1A7G7) != 0) { possibleKingMoves |= currentKingPosition >> 7; }
            if ((currentKingPosition & BitBoards.Region_B1H1B8H8) != 0) { possibleKingMoves |= currentKingPosition >> 1; }
            if ((currentKingPosition & BitBoards.Region_A1G1A8G8) != 0) { possibleKingMoves |= currentKingPosition << 1; }
            if ((currentKingPosition & BitBoards.Region_B2H2B8H8) != 0) { possibleKingMoves |= currentKingPosition << 7; }
            if ((currentKingPosition & BitBoards.Region_A2H2A8H8) != 0) { possibleKingMoves |= currentKingPosition << 8; }
            if ((currentKingPosition & BitBoards.Region_A2G2A8G8) != 0) { possibleKingMoves |= currentKingPosition << 9; }

            // Filter out illegal moves                
            possibleKingMoves &= ~position.AllPiecesOfAColorBitBoard[position.PlayerToMove];

            while (possibleKingMoves != 0)
            {
                // Retrieve one piece position (least significant 1 bit)                    
                ulong candidateKingMove = possibleKingMoves & (~possibleKingMoves + 1);

                // Check candidate move, and add generated moves to the move lists
                InspectCandidate(candidateKingMove, currentKingPosition, PieceType.King, position, captureMoves, nonCaptureMoves, false, false);

                // Remove the current position from the possible candidate moves                        
                possibleKingMoves = possibleKingMoves & (possibleKingMoves - 1);
            }

            #region Castling moves
            // Castling moves

            // Short Castle
            // Check if player to move can castle kingside and if the king and rook are in place 
            if (position.CanCastleKingSide[position.PlayerToMove] &&
                currentKingPosition == BitBoards.Region_ShortCastleMask[position.PlayerToMove] &&
                (position.Pieces[position.PlayerToMove][PieceType.Rook] & BitBoards.Square_ShortCastle_Rook_Source[position.PlayerToMove]) > 0)
            {
                // Contains a bitboard with all pieces on the board
                ulong currentBoard = position.AllPiecesOfAColorBitBoard[Player.White] | position.AllPiecesOfAColorBitBoard[Player.Black];

                // Gets a mask that helps us decide if there are no pieces between the king and rook
                ulong currentPlayerShortCastleMask = BitBoards.Region_ShortCastleMask[position.PlayerToMove];

                // Check that there are no pieces between them
                // This is still just a pseudo-legal move as we didn't check if the king or the squares it needs to cross are in check or not
                if ((currentBoard & currentPlayerShortCastleMask) == currentPlayerShortCastleMask)
                {
                    // Player to move can possibly castle short, so add this move to the non-captures
                    InspectCandidate(BitBoards.Square_ShortCastle_King_Target[position.PlayerToMove], currentKingPosition, PieceType.King,
                        position, captureMoves, nonCaptureMoves,
                        true,false);
                }
            }

            // Long Castle
            // Check if player to move can castle queenside and if the king and rook are in place 
            if (position.CanCastleQueenSide[position.PlayerToMove] &&
                currentKingPosition == BitBoards.Region_LongCastleMask[position.PlayerToMove] &&
                (position.Pieces[position.PlayerToMove][PieceType.Rook] & BitBoards.Square_LongCastle_Rook_Source[position.PlayerToMove]) > 0)
            {
                // Contains a bitboard with all pieces on the board
                ulong currentBoard = position.AllPiecesOfAColorBitBoard[Player.White] | position.AllPiecesOfAColorBitBoard[Player.Black];

                // Gets a mask that helps us decide if there are no pieces between the king and rook
                ulong currentPlayerLongCastleMask = BitBoards.Region_LongCastleMask[position.PlayerToMove];

                // Check that there are no pieces between them
                // This is still just a pseudo-legal move as we didn't check if the king or the squares it needs to cross are in check or not
                if ((currentBoard & currentPlayerLongCastleMask) == currentPlayerLongCastleMask)
                {
                    // Player to move can possibly castle long, so add this move to the non-captures
                    InspectCandidate(BitBoards.Square_LongCastle_King_Target[position.PlayerToMove], currentKingPosition, PieceType.King,
                    position, captureMoves, nonCaptureMoves,
                    false, true);
                }
            }
            #endregion        

            #endregion

            legalMoves.AddRange(captureMoves);
            legalMoves.AddRange(nonCaptureMoves);
            return legalMoves.ToArray();
        }

        private static void InspectCandidate(ulong candidate, ulong currentPiecePosition, int currentPieceType, BoardPosition position,
           List<Move> captureMoves, List<Move> nonCaptureMoves,
           bool isShortCastle,
           bool isLongCastle)
        {
            #region Check candidate

            int opponent = position.PlayerToMove ^ 1;
            ulong opponentBitMask = position.AllPiecesOfAColorBitBoard[opponent];

            if ((candidate & opponentBitMask) != 0)
            {
                // Capture, get captured piece type

                int targetPieceType = GetCapturedPieceType(candidate, position);

                Move move = new Move()
                {
                    SourcePieceType = currentPieceType,
                    SourceSquare = currentPiecePosition,
                    TargetSquare = candidate,
                    TargetPieceType = targetPieceType,
                    IsShortCastle = isShortCastle,
                    IsLongCastle = isLongCastle
                };

                captureMoves.Add(move);
            }
            else
            {
                // Move to an empty square
                Move move = new Move()
                {
                    SourcePieceType = currentPieceType,
                    SourceSquare = currentPiecePosition,
                    TargetSquare = candidate,
                    TargetPieceType = PieceType.None
                };

                nonCaptureMoves.Add(move);
            }
            #endregion
        }

        private static int GetCapturedPieceType(ulong candidate, BoardPosition position)
        {
            int targetPieceType = PieceType.None;
            int opponent = position.PlayerToMove ^ 1;

            if ((candidate & position.Pieces[opponent][PieceType.Pawn]) != 0) { targetPieceType = PieceType.Pawn; }
            else if ((candidate & position.Pieces[opponent][PieceType.Queen]) != 0) { targetPieceType = PieceType.Queen; }
            else if ((candidate & position.Pieces[opponent][PieceType.King]) != 0) { targetPieceType = PieceType.King; }
            else if ((candidate & position.Pieces[opponent][PieceType.Bishop]) != 0) { targetPieceType = PieceType.Bishop; }
            else if ((candidate & position.Pieces[opponent][PieceType.Knight]) != 0) { targetPieceType = PieceType.Knight; }
            else if ((candidate & position.Pieces[opponent][PieceType.Rook]) != 0) { targetPieceType = PieceType.Rook; }

            if (targetPieceType == PieceType.None)
            {
                throw new Exception("Unable to generate capture!");
            }

            return targetPieceType;
        }
    }
}
