//using Griff.Definitions;
//using Griff.Definitions.Fast_Maps;
//using Griff.Maps;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace Griff.Board_Representation
//{
//    sealed class BoardPosition
//    {
//        #region Members

//        /// <summary>
//        /// Arrays, holding the board position of the pieces, both for white and black
//        /// </summary>
//        ulong[][] Pieces = new ulong[2][]
//        {
//            new ulong[PieceType.List.Length -1],
//            new ulong[PieceType.List.Length -1]
//        };

//        /// <summary>
//        /// Indicates the player to move
//        /// </summary>
//        int PlayerToMove;

//        /// <summary>
//        /// Holds flags indicating which castling rights are available
//        /// for both white and black
//        /// </summary>
//        bool[] CanCastleQueenSide = new bool[2];
//        bool[] CanCastleKingSide = new bool[2];

//        /// <summary>
//        /// Indicates the en-passant target square (i.e. e3, e6)
//        /// </summary>
//        ulong EnPassantTargetSquare;

//        /// <summary>
//        /// Number of half-moves (plies) since the last pawn move
//        /// </summary>
//        uint HalfMovesSinceLastPawnMove;

//        /// <summary>
//        /// This field stores the previous value of the HalfMovesSinceLastPawnMove field
//        /// when we make a move on the board
//        /// This will help us to restore the correct value for the HalfMovesSinceLastPawnMove field
//        /// when we un-make the move
//        /// </summary>
//        uint PreviousHalfMovesSinceLastPawnMove;

//        /// <summary>
//        /// Idicates the current move number
//        /// </summary>
//        uint MoveNumber;

//        /// <summary>
//        /// Contains the Zobrist hash value of the current position
//        /// </summary>
//        internal ulong PositionHash;

//        /// <summary>
//        /// Represents the bitboards containing all white pieces and all black pieces 
//        /// </summary>
//        ulong[] AllPiecesBitBoard = new ulong[2];

//        #endregion

//        /// <summary>
//        /// Parses a FEN string and creates a BoardPosition object based on it
//        /// </summary>
//        internal static BoardPosition ParseFEN(string fen)
//        {
//            if (string.IsNullOrEmpty(fen)) { throw new ArgumentNullException("fen"); }

//            string[] fenMembers = fen.Split();

//            if (fenMembers.Length < 6) { throw new ArgumentException("Invalid FEN string!", "fen"); }

//            // Construct a new BoardPosition object
//            BoardPosition position = new BoardPosition();

//            #region Parse pieces

//            // Piece placement along the 8 ranks
//            string[] rankInfo = fenMembers[0].Split('/');

//            if (rankInfo.Length != 8) { throw new ArgumentException("Invalid FEN string!", "fen"); }

//            // Contains the board
//            Piece[,] board = new Piece[8, 8];

//            // Fill board with empty squares
//            for (int i = 0; i < 8; i++)
//            {
//                for (int j = 0; j < 8; j++)
//                {
//                    board[i, j] = Piece.None;
//                }
//            }

//            // Fill the board with the pieces
//            for (int i = 0; i < 8; i++)
//            {
//                int x = i;
//                int y = 0;
//                for (int j = 0; j < rankInfo[i].Length; j++)
//                {
//                    char currentPiece = rankInfo[i][j];
//                    if (char.IsDigit(currentPiece))
//                    {
//                        int number = (int)char.GetNumericValue(currentPiece);
//                        y += number;
//                    }
//                    else
//                    {
//                        board[x, y] = PieceMap.CharToPiece[currentPiece];
//                        y++;
//                    }
//                }
//            }

//            // Construct now a BoardPosition object based on the board array

//            // Object holding the list of pieces
//            List<ulong>[][] pieces = new List<ulong>[2][]
//            {
//                new List<ulong>[6]
//                {
//                    new List<ulong>(),
//                    new List<ulong>(),
//                    new List<ulong>(),
//                    new List<ulong>(),
//                    new List<ulong>(),
//                    new List<ulong>()
//                },
//                new List<ulong>[6]
//                                {
//                    new List<ulong>(),
//                    new List<ulong>(),
//                    new List<ulong>(),
//                    new List<ulong>(),
//                    new List<ulong>(),
//                    new List<ulong>()
//                }
//            };

//            // Add the bitboards for the pieces
//            for (int i = 0; i < 8; i++)
//            {
//                for (int j = 0; j < 8; j++)
//                {
//                    Piece currentPiece = board[i, j];
//                    ulong currentPiecePosition = SquareMap.CoordinateToLong[i + 1][j + 1];

//                    if (currentPiece != Piece.None)
//                    {
//                        // Add current piece to the pieces list
//                        pieces[currentPiece.Color][currentPiece.PieceType].Add(currentPiecePosition);

//                        //Update the Zobrist hash with the current piece
//                        position.PositionHash ^= ZobristMap.PieceHashMap[currentPiece.Color][currentPiece.PieceType][currentPiecePosition].Value;

//                        // Update the bitboard containing all the pieces of the current player(color) 
//                        position.AllPiecesBitBoard[currentPiece.Color] |= currentPiecePosition;
//                    }
//                }
//            }

//            // Update the arrays containing the pieces
//            // We use arrays instead of generic lists to improve performance
//            for (int i = 0; i < 2; i++)
//            {
//                for (int j = 1; j < PieceType.List.Length; j++)
//                {
//                    if (pieces[i][PieceType.List[j]] != null)
//                    {
//                        //position.Pieces[i][PieceType.List[j]] = pieces[i][PieceType.List[j]].ToArray();
//                        for (int k = 0; k < pieces[i][PieceType.List[j]].Count; k++)
//                        {
//                            position.Pieces[i][PieceType.List[j]] |= pieces[i][PieceType.List[j]][k];
//                        }
//                    }
//                }
//            }

//            #endregion

//            // WhiteToMove
//            if (fenMembers[1].Equals("w", StringComparison.OrdinalIgnoreCase))
//            {
//                position.PlayerToMove = Player.White;
//                position.PositionHash ^= ZobristMap.PlayerToMoveHash[Player.White];
//            }
//            else
//            {
//                position.PlayerToMove = Player.Black;
//                position.PositionHash ^= ZobristMap.PlayerToMoveHash[Player.Black];
//            }

//            #region Castling rights

//            // Set no castling rights first, then enable those that are avaialable
//            position.PositionHash ^= ZobristMap.CannotCastleKingSideHash[Player.White];
//            position.PositionHash ^= ZobristMap.CannotCastleKingSideHash[Player.Black];
//            position.PositionHash ^= ZobristMap.CannotCastleQueenSideHash[Player.White];
//            position.PositionHash ^= ZobristMap.CannotCastleQueenSideHash[Player.Black];

//            if (fenMembers[2] != "-")
//            {
//                foreach (char flag in fenMembers[2].ToCharArray())
//                {
//                    switch (flag)
//                    {
//                        case 'K':
//                            {
//                                position.CanCastleKingSide[Player.White] = true;

//                                // Unset the no castling right bitmask
//                                position.PositionHash ^= ZobristMap.CannotCastleKingSideHash[Player.White];

//                                // Set the castling right bitmask
//                                position.PositionHash ^= ZobristMap.CanCastleKingSideHash[Player.White];

//                                break;
//                            }
//                        case 'Q':
//                            {
//                                position.CanCastleQueenSide[Player.White] = true;

//                                position.PositionHash ^= ZobristMap.CannotCastleQueenSideHash[Player.White];
//                                position.PositionHash ^= ZobristMap.CanCastleQueenSideHash[Player.White];

//                                break;
//                            }
//                        case 'k':
//                            {
//                                position.CanCastleKingSide[Player.Black] = true;

//                                position.PositionHash ^= ZobristMap.CannotCastleKingSideHash[Player.Black];
//                                position.PositionHash ^= ZobristMap.CanCastleKingSideHash[Player.Black];

//                                break;
//                            }
//                        case 'q':
//                            {
//                                position.CanCastleQueenSide[Player.Black] = true;

//                                position.PositionHash ^= ZobristMap.CannotCastleQueenSideHash[Player.Black];
//                                position.PositionHash ^= ZobristMap.CanCastleQueenSideHash[Player.Black];

//                                break;
//                            }
//                    }
//                }
//            }

//            #endregion

//            // En passant target square
//            if (fenMembers[3] != "-")
//            {
//                position.EnPassantTargetSquare = SquareMap.StringToLong[fenMembers[3]];

//                position.PositionHash ^= ZobristMap.EnPassantHashMap[position.EnPassantTargetSquare];
//            }
//            else
//            {
//                position.PositionHash ^= ZobristMap.NoEnPassantSquareHash;
//            }

//            //HalfMovesSinceLastPawnMove
//            position.HalfMovesSinceLastPawnMove = uint.Parse(fenMembers[4], CultureInfo.InvariantCulture);

//            //PreviousHalfMovesSinceLastPawnMove
//            position.PreviousHalfMovesSinceLastPawnMove = position.HalfMovesSinceLastPawnMove;

//            //Move number
//            position.MoveNumber = uint.Parse(fenMembers[5], CultureInfo.InvariantCulture);

//            return position;
//        }

//        /// <summary>
//        /// Displays at the console the current position as a string
//        /// </summary>
//        /// <param name="position"></param>
//        /// <returns></returns>
//        internal static string DumpPosition(BoardPosition position)
//        {
//            StringBuilder output = new StringBuilder();

//            // Holds the pieces of the board in chars
//            char[,] board = new char[8, 8];

//            // Fill board with empty squares
//            for (int i = 0; i < 8; i++)
//            {
//                for (int j = 0; j < 8; j++)
//                {
//                    board[i, j] = ' ';
//                }
//            }

//            // Fill in the pieces
//            for (int i = 0; i < 2; i++)
//            {
//                for (int j = 0; j < PieceType.List.Length - 1; j++)
//                {
//                    // Bitboard that holds all pieces of one kind
//                    ulong piecesBitBoard = position.Pieces[i][j];
//                    while (piecesBitBoard != 0)
//                    {
//                        // Retrieve one piece position (least significant bit)                        
//                        ulong currentPiecePosition = piecesBitBoard & (~piecesBitBoard + 1);

//                        // Get coordinate of current piece position
//                        Pair pair = SquareMap.LongToCoordinate[currentPiecePosition];
//                        board[pair.First - 1, pair.Second - 1] = PieceMap.PieceToChar[i][j];

//                        // Remove the current piece from the pieces bitboard                        
//                        piecesBitBoard = piecesBitBoard & (piecesBitBoard - 1);
//                    }
//                }
//            }

//            // Construct a string containing a formatted board position
//            output.AppendLine("   A|B|C|D|E|F|G|H");
//            output.AppendLine("   ----------------");
//            for (int i = 7; i >= 0; i--)
//            {
//                output.Append(i + 1);
//                output.Append(" |");
//                for (int j = 0; j < 8; j++)
//                {
//                    output.Append(board[j, i]);
//                    output.Append('|');
//                }
//                output.Append(" ");
//                output.Append(i + 1);
//                output.AppendLine("\r\n   ----------------");
//            }
//            output.AppendLine("   A|B|C|D|E|F|G|H");

//            return output.ToString();
//        }

//        /// <summary>
//        /// Generates legal moves
//        /// </summary>
//        /// <param name="position"></param>
//        /// <returns></returns>
//        internal static Move[] GenerateLegalMoves(BoardPosition position)
//        {
//            List<Move> nonCaptureMoves = new List<Move>();
//            List<Move> captureMoves = new List<Move>();
//            List<Move> legalMoves = new List<Move>();

//            #region Knight moves

//            // Knight moves

//            // Bitboard that holds the position of all knights
//            ulong knightsBoard = position.Pieces[position.PlayerToMove][PieceType.Knight];
//            while (knightsBoard != 0)
//            {
//                // Retrieve one piece position (least significant 1 bit)                
//                ulong currentKnightPosition = knightsBoard & (~knightsBoard + 1);

//                ulong possibleKnightMoves = 0;

//                if ((currentKnightPosition & BitBoards.Region_B1H1B6H6) != 0) { possibleKnightMoves |= currentKnightPosition >> 17; }
//                if ((currentKnightPosition & BitBoards.Region_A1G1A6G6) != 0) { possibleKnightMoves |= currentKnightPosition >> 15; }
//                if ((currentKnightPosition & BitBoards.Region_C1H1C7H7) != 0) { possibleKnightMoves |= currentKnightPosition >> 10; }
//                if ((currentKnightPosition & BitBoards.Region_A1F1A7F7) != 0) { possibleKnightMoves |= currentKnightPosition >> 6; }
//                if ((currentKnightPosition & BitBoards.Region_C2H2C8H8) != 0) { possibleKnightMoves |= currentKnightPosition << 6; }
//                if ((currentKnightPosition & BitBoards.Region_A2F2A8F8) != 0) { possibleKnightMoves |= currentKnightPosition << 10; }
//                if ((currentKnightPosition & BitBoards.Region_B3H3B8H8) != 0) { possibleKnightMoves |= currentKnightPosition << 15; }
//                if ((currentKnightPosition & BitBoards.Region_A3G3A8G8) != 0) { possibleKnightMoves |= currentKnightPosition << 17; }
                
//                // Filter out illegal moves                
//                possibleKnightMoves &= ~position.AllPiecesBitBoard[position.PlayerToMove];

//                while (possibleKnightMoves != 0)
//                {
//                    // Retrieve one piece position (least significant 1 bit)                    
//                    ulong candidateKnightMove = possibleKnightMoves & (~possibleKnightMoves + 1);

//                    // Check candidate move, and add generated moves to the move lists
//                    InspectCandidate(candidateKnightMove, currentKnightPosition, PieceType.Knight, position, captureMoves, nonCaptureMoves, false, false);                                     

//                    // Remove the current position from the possible candidate moves                        
//                    possibleKnightMoves = possibleKnightMoves & (possibleKnightMoves - 1);
//                }             

//                // Remove the current piece from the pieces bitboard                        
//                knightsBoard = knightsBoard & (knightsBoard - 1);
//            }       

//            #endregion

//            #region King moves

//            // King moves

//            // Bitboard that holds the position of all kings
//            ulong currentKingPosition = position.Pieces[position.PlayerToMove][PieceType.King];

//            ulong possibleKingMoves = 0;

//            // Non-castling moves
//            if ((currentKingPosition & BitBoards.Region_B1H1B7H7) != 0) { possibleKingMoves |= currentKingPosition >> 9; }
//            if ((currentKingPosition & BitBoards.Region_A1H1A7H7) != 0) { possibleKingMoves |= currentKingPosition >> 8; }
//            if ((currentKingPosition & BitBoards.Region_A1G1A7G7) != 0) { possibleKingMoves |= currentKingPosition >> 7; }
//            if ((currentKingPosition & BitBoards.Region_B1H1B8H8) != 0) { possibleKingMoves |= currentKingPosition >> 1; }
//            if ((currentKingPosition & BitBoards.Region_A1G1A8G8) != 0) { possibleKingMoves |= currentKingPosition << 1; }
//            if ((currentKingPosition & BitBoards.Region_B2H2B8H8) != 0) { possibleKingMoves |= currentKingPosition << 7; }
//            if ((currentKingPosition & BitBoards.Region_A2H2A8H8) != 0) { possibleKingMoves |= currentKingPosition << 8; }
//            if ((currentKingPosition & BitBoards.Region_A2G2A8G8) != 0) { possibleKingMoves |= currentKingPosition << 9; }

//            // Filter out illegal moves                
//            possibleKingMoves &= ~position.AllPiecesBitBoard[position.PlayerToMove];

//            while (possibleKingMoves != 0)
//            {
//                // Retrieve one piece position (least significant 1 bit)                    
//                ulong candidateKingMove = possibleKingMoves & (~possibleKingMoves + 1);

//                // Check candidate move, and add generated moves to the move lists
//                InspectCandidate(candidateKingMove, currentKingPosition, PieceType.King, position, captureMoves, nonCaptureMoves, false, false);

//                // Remove the current position from the possible candidate moves                        
//                possibleKingMoves = possibleKingMoves & (possibleKingMoves - 1);
//            }

//            #region Castling moves
//            // Castling moves

//            // Short Castle
//            // Check if player to move can castle kingside and if the king and rook are in place 
//            if (position.CanCastleKingSide[position.PlayerToMove] &&
//                currentKingPosition == BitBoards.Region_ShortCastleMask[position.PlayerToMove] &&
//                (position.Pieces[position.PlayerToMove][PieceType.Rook] & BitBoards.Square_ShortCastle_Rook_Source[position.PlayerToMove]) > 0)
//            {
//                // Contains a bitboard with all pieces on the board
//                ulong currentBoard = position.AllPiecesBitBoard[Player.White] | position.AllPiecesBitBoard[Player.Black];

//                // Gets a mask that helps us decide if there are no pieces between the king and rook
//                ulong currentPlayerShortCastleMask = BitBoards.Region_ShortCastleMask[position.PlayerToMove];

//                // Check that there are no pieces between them
//                // This is still just a pseudo-legal move as we didn't check if the king or the squares it needs to cross are in check or not
//                if ((currentBoard & currentPlayerShortCastleMask) == currentPlayerShortCastleMask)
//                {
//                    // Player to move can possibly castle short, so add this move to the non-captures
//                    InspectCandidate(BitBoards.Square_ShortCastle_King_Target[position.PlayerToMove], currentKingPosition, PieceType.King,
//                        position, captureMoves, nonCaptureMoves,
//                        true,false);
//                }
//            }

//            // Long Castle
//            // Check if player to move can castle queenside and if the king and rook are in place 
//            if (position.CanCastleQueenSide[position.PlayerToMove] &&
//                currentKingPosition == BitBoards.Region_LongCastleMask[position.PlayerToMove] &&
//                (position.Pieces[position.PlayerToMove][PieceType.Rook] & BitBoards.Square_LongCastle_Rook_Source[position.PlayerToMove]) > 0)
//            {
//                // Contains a bitboard with all pieces on the board
//                ulong currentBoard = position.AllPiecesBitBoard[Player.White] | position.AllPiecesBitBoard[Player.Black];

//                // Gets a mask that helps us decide if there are no pieces between the king and rook
//                ulong currentPlayerLongCastleMask = BitBoards.Region_LongCastleMask[position.PlayerToMove];

//                // Check that there are no pieces between them
//                // This is still just a pseudo-legal move as we didn't check if the king or the squares it needs to cross are in check or not
//                if ((currentBoard & currentPlayerLongCastleMask) == currentPlayerLongCastleMask)
//                {
//                    // Player to move can possibly castle long, so add this move to the non-captures
//                    InspectCandidate(BitBoards.Square_LongCastle_King_Target[position.PlayerToMove], currentKingPosition, PieceType.King,
//                    position, captureMoves, nonCaptureMoves,
//                    false, true);
//                }
//            }
//            #endregion        

//            #endregion

//            legalMoves.AddRange(captureMoves);
//            legalMoves.AddRange(nonCaptureMoves);
//            return legalMoves.ToArray();
//        }

//        /// <summary>
//        /// Makes the specified move on the board
//        /// </summary>
//        internal static void MakeMove(Move move, BoardPosition position)
//        {
//            int opponentPlayer = position.PlayerToMove ^ 1;

//            if ((move.SourceSquare & position.Pieces[position.PlayerToMove][move.SourcePieceType]) == 0)
//            {
//                throw new Exception("Invalid move!");
//            }

//            #region Update Pieces list

//            // Delete target piece
//            if (move.TargetPieceType != PieceType.None)
//            {
//                // Delete the captured piece from Pieces list
//                position.Pieces[opponentPlayer][move.TargetPieceType] ^= move.TargetSquare;
//            }

//            // Relocate source piece
//            position.Pieces[position.PlayerToMove][move.SourcePieceType] ^= move.SourceSquare;
//            position.Pieces[position.PlayerToMove][move.SourcePieceType] ^= move.TargetSquare;

//            if (move.IsShortCastle)
//            { 
//                // move the rook
//            }
//            else if (move.IsLongCastle)
//            {
 
//            }

//            #endregion

//            #region Update PositionHash

//            ulong hashValue;
//            if (move.TargetPieceType != PieceType.None)
//            {
//                // Delete the captured piece from the position hash
//                hashValue = ZobristMap.PieceHashMap[opponentPlayer][move.TargetPieceType][move.TargetSquare].Value;
//                position.PositionHash ^= hashValue;
//            }            

//            // Source piece
//            // Remove at source square
//            hashValue = ZobristMap.PieceHashMap[position.PlayerToMove][move.SourcePieceType][move.SourceSquare].Value;
//            position.PositionHash ^= hashValue;

//            // Add at the target square
//            hashValue = ZobristMap.PieceHashMap[position.PlayerToMove][move.SourcePieceType][move.TargetSquare].Value;
//            position.PositionHash ^= hashValue;

//            #endregion

//            #region Update AllPiecesBitboard

//            // Remove (XOR out) the target piece
//            if (move.TargetPieceType != PieceType.None)
//            {
//                position.AllPiecesBitBoard[opponentPlayer] ^= move.TargetSquare;
//            }

//            // Relocate source piece
//            position.AllPiecesBitBoard[position.PlayerToMove] ^= move.SourceSquare;
//            position.AllPiecesBitBoard[position.PlayerToMove] ^= move.TargetSquare;

//            #endregion

//            #region Update HalfMovesSinceLastPawnMove, PreviousHalfMovesSinceLastPawnMove and MoveNumber

//            // PreviousHalfMovesSinceLastPawnMove
//            position.PreviousHalfMovesSinceLastPawnMove = position.HalfMovesSinceLastPawnMove;

//            // HalfMovesSinceLastPawnMove
//            if (move.SourcePieceType == PieceType.Pawn ||
//                move.TargetPieceType == PieceType.Pawn)
//            {
//                position.HalfMovesSinceLastPawnMove = 0;
//            }
//            else
//            {
//                position.HalfMovesSinceLastPawnMove++;
//            }

//            // MoveNumber
//            if (position.PlayerToMove == Player.Black)
//            {
//                position.MoveNumber++;
//            }

//            #endregion

//            #region Update PlayerToMove and PositionHash

//            // XOR out previous player to move
//            position.PositionHash ^= ZobristMap.PlayerToMoveHash[position.PlayerToMove];

//            // XOR in next player to move
//            position.PositionHash ^= ZobristMap.PlayerToMoveHash[opponentPlayer];
//            position.PlayerToMove = opponentPlayer;

//            #endregion
//        }

//        /// <summary>
//        /// Un-makes the specified move on the board
//        /// </summary>
//        internal static void UnmakeMove(Move move, BoardPosition position)
//        {
//            var opponentPlayer = position.PlayerToMove ^ 1;

//            if ((move.TargetSquare & position.Pieces[opponentPlayer][move.SourcePieceType]) == 0)
//            {
//                throw new Exception("Invalid move!");
//            }

//            #region Update Pieces list

//            // Move source piece back to it's original position
//            position.Pieces[opponentPlayer][move.SourcePieceType] ^= move.TargetSquare;
//            position.Pieces[opponentPlayer][move.SourcePieceType] ^= move.SourceSquare;

//            // Put back captured target piece
//            if (move.TargetPieceType != PieceType.None)
//            {
//                // Delete the captured piece from Pieces list
//                position.Pieces[position.PlayerToMove][move.TargetPieceType] ^= move.TargetSquare;
//            }

//            #endregion

//            #region Update PositionHash

//            ulong hashValue;

//            // Source piece
//            // Remove at target square
//            hashValue = ZobristMap.PieceHashMap[opponentPlayer][move.SourcePieceType][move.TargetSquare].Value;
//            position.PositionHash ^= hashValue;

//            // Add at the source square
//            hashValue = ZobristMap.PieceHashMap[opponentPlayer][move.SourcePieceType][move.SourceSquare].Value;
//            position.PositionHash ^= hashValue;

//            // Place back the target piece if there is one
//            if (move.TargetPieceType != PieceType.None)
//            {
//                // XOR in the hash of the captured piece back into the position hash
//                hashValue = ZobristMap.PieceHashMap[position.PlayerToMove][move.TargetPieceType][move.TargetSquare].Value;
//                position.PositionHash ^= hashValue;
//            }

//            #endregion

//            #region Update AllPiecesBitboard

//            // Relocate source piece
//            position.AllPiecesBitBoard[opponentPlayer] ^= move.TargetSquare;
//            position.AllPiecesBitBoard[opponentPlayer] ^= move.SourceSquare;

//            // Add (XOR in) the target piece
//            if (move.TargetPieceType != PieceType.None)
//            {
//                position.AllPiecesBitBoard[position.PlayerToMove] ^= move.TargetSquare;
//            }

//            #endregion

//            #region Update HalfMovesSinceLastPawnMove, PreviousHalfMovesSinceLastPawnMove and MoveNumber

//            // HalfMovesSinceLastPawnMove
//            position.HalfMovesSinceLastPawnMove = position.PreviousHalfMovesSinceLastPawnMove;

//            // MoveNumber
//            if (position.PlayerToMove == Player.White)
//            {
//                position.MoveNumber--;
//            }

//            #endregion

//            #region Update PlayerToMove and PositionHash

//            // XOR out previous player to move
//            position.PositionHash ^= ZobristMap.PlayerToMoveHash[position.PlayerToMove];

//            // XOR in next player to move
//            position.PositionHash ^= ZobristMap.PlayerToMoveHash[opponentPlayer];
//            position.PlayerToMove = opponentPlayer;

//            #endregion
//        }

//        internal static BoardPosition ClonePosition(BoardPosition position)
//        {
//            BoardPosition clone = new BoardPosition();

//            // Pieces
//            for (int i = 0; i < 2; i++)
//            {
//                for (int j = 0; j < 6; j++)
//                {
//                    clone.Pieces[i][j] = position.Pieces[i][j];
//                }
//            }


//            return clone;
//        }

//        private static int GetCapturedPieceType(ulong candidate, BoardPosition position)
//        {
//            int targetPieceType = PieceType.None;
//            int opponent = position.PlayerToMove ^ 1;

//            if ((candidate & position.Pieces[opponent][PieceType.Pawn]) != 0) { targetPieceType = PieceType.Pawn; }
//            else if ((candidate & position.Pieces[opponent][PieceType.Queen]) != 0) { targetPieceType = PieceType.Queen; }
//            else if ((candidate & position.Pieces[opponent][PieceType.King]) != 0) { targetPieceType = PieceType.King; }
//            else if ((candidate & position.Pieces[opponent][PieceType.Bishop]) != 0) { targetPieceType = PieceType.Bishop; }
//            else if ((candidate & position.Pieces[opponent][PieceType.Knight]) != 0) { targetPieceType = PieceType.Knight; }
//            else if ((candidate & position.Pieces[opponent][PieceType.Rook]) != 0) { targetPieceType = PieceType.Rook; }

//            if (targetPieceType == PieceType.None)
//            {
//                throw new Exception("Unable to generate capture!");
//            }

//            return targetPieceType;
//        }

//        private static void InspectCandidate(ulong candidate, ulong currentPiecePosition, int currentPieceType, BoardPosition position, 
//            List<Move> captureMoves, List<Move> nonCaptureMoves,
//            bool isShortCastle,
//            bool isLongCastle)
//        {
//            #region Check candidate

//            int opponent = position.PlayerToMove ^ 1;
//            ulong opponentBitMask = position.AllPiecesBitBoard[opponent];

//            if ((candidate & opponentBitMask) != 0)
//            {
//                // Capture, get captured piece type

//                int targetPieceType = GetCapturedPieceType(candidate, position);

//                Move move = new Move()
//                {
//                    SourcePieceType = currentPieceType,
//                    SourceSquare = currentPiecePosition,
//                    TargetSquare = candidate,
//                    TargetPieceType = targetPieceType,
//                    IsShortCastle = isShortCastle,
//                    IsLongCastle = isLongCastle
//                };

//                captureMoves.Add(move);
//            }
//            else
//            {
//                // Move to an empty square
//                Move move = new Move()
//                {
//                    SourcePieceType = currentPieceType,
//                    SourceSquare = currentPiecePosition,
//                    TargetSquare = candidate,
//                    TargetPieceType = PieceType.None
//                };

//                nonCaptureMoves.Add(move);
//            }
//            #endregion
//        }
//    }
//}
