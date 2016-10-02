using Griff.Fen;
using System;
using System.Collections.Generic;
using System.Globalization;
using Griff.Definitions;

namespace Griff.Board_Representation
{
    sealed partial class BoardPosition
    {
         /// <summary>
        /// Parses a FEN string and creates a BoardPosition object based on it
        /// </summary>
        internal static BoardPosition ParseFEN(string fen)
        {
            if (string.IsNullOrEmpty(fen)) { throw new ArgumentNullException("fen"); }

            string[] fenMembers = fen.Split();

            if (fenMembers.Length < 6) { throw new ArgumentException("Invalid FEN string!", "fen"); }

            // Construct a new BoardPosition object
            BoardPosition position = new BoardPosition();

            #region Parse pieces

            // Piece placement along the 8 ranks
            string[] rankInfo = fenMembers[0].Split('/');

            if (rankInfo.Length != 8) { throw new ArgumentException("Invalid FEN string!", "fen"); }

            // Contains the board
            Piece[,] board = new Piece[8, 8];

            // Fill board with empty squares
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = Piece.None;
                }
            }

            // Fill the board with the pieces
            for (int i = 0; i < 8; i++)
            {
                int x = i;
                int y = 0;
                for (int j = 0; j < rankInfo[i].Length; j++)
                {
                    char currentPiece = rankInfo[i][j];
                    if (char.IsDigit(currentPiece))
                    {
                        int number = (int)char.GetNumericValue(currentPiece);
                        y += number;
                    }
                    else
                    {
                        board[x, y] = PieceMap.CharToPiece[currentPiece];
                        y++;
                    }
                }
            }

            // Construct now a BoardPosition object based on the board array

            // Object holding the list of pieces
            List<ulong>[][] pieces = new List<ulong>[2][]
            {
                new List<ulong>[6]
                {
                    new List<ulong>(),
                    new List<ulong>(),
                    new List<ulong>(),
                    new List<ulong>(),
                    new List<ulong>(),
                    new List<ulong>()
                },
                new List<ulong>[6]
                                {
                    new List<ulong>(),
                    new List<ulong>(),
                    new List<ulong>(),
                    new List<ulong>(),
                    new List<ulong>(),
                    new List<ulong>()
                }
            };

            // Add the bitboards for the pieces
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece currentPiece = board[i, j];
                    ulong currentPiecePosition = SquareMap.CoordinateToLong[i + 1][j + 1];

                    if (currentPiece != Piece.None)
                    {
                        // Add current piece to the pieces list
                        pieces[currentPiece.Color][currentPiece.Type].Add(currentPiecePosition);

                        // Update the bitboard containing all the pieces of the current player(color) 
                        position.AllPiecesOfAColorBitBoard[currentPiece.Color] |= currentPiecePosition;
                    }
                }
            }

            // Update the arrays containing the pieces
            // We use arrays instead of generic lists to improve performance
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j < PieceType.List.Length; j++)
                {
                    if (pieces[i][PieceType.List[j]] != null)
                    {
                        //position.Pieces[i][PieceType.List[j]] = pieces[i][PieceType.List[j]].ToArray();
                        for (int k = 0; k < pieces[i][PieceType.List[j]].Count; k++)
                        {
                            position.Pieces[i][PieceType.List[j]] |= pieces[i][PieceType.List[j]][k];
                        }
                    }
                }
            }

            #endregion

            // WhiteToMove
            if (fenMembers[1].Equals("w", StringComparison.OrdinalIgnoreCase))
            {
                position.PlayerToMove = Player.White;
            }
            else
            {
                position.PlayerToMove = Player.Black;
            }

            #region Castling rights

            if (fenMembers[2] != "-")
            {
                foreach (char flag in fenMembers[2].ToCharArray())
                {
                    switch (flag)
                    {
                        case 'K':
                            {
                                position.CanCastleKingSide[Player.White] = true;
                                break;
                            }
                        case 'Q':
                            {
                                position.CanCastleQueenSide[Player.White] = true;
                                break;
                            }
                        case 'k':
                            {
                                position.CanCastleKingSide[Player.Black] = true;
                                break;
                            }
                        case 'q':
                            {
                                position.CanCastleQueenSide[Player.Black] = true;
                                break;
                            }
                    }
                }
            }

            #endregion

            // En passant target square
            if (fenMembers[3] != "-")
            {
                position.EnPassantTargetSquare = SquareMap.StringToLong[fenMembers[3]];
            }

            //HalfMovesSinceLastPawnMove
            position.HalfMovesSinceLastPawnMove = uint.Parse(fenMembers[4], CultureInfo.InvariantCulture);

            //PreviousHalfMovesSinceLastPawnMove
            position.PreviousHalfMovesSinceLastPawnMove = position.HalfMovesSinceLastPawnMove;

            //Move number
            position.MoveNumber = uint.Parse(fenMembers[5], CultureInfo.InvariantCulture);

            return position;
        }
    }
}
