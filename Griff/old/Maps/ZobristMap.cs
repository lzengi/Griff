//using Griff.Definitions;
//using Griff.Definitions.Fast_Maps;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Griff.Maps
//{
//    static class ZobristMap
//    {
//        internal static FastMap[][] PieceHashMap = new FastMap[2][]
//            {
//                new FastMap[6],
//                new FastMap[6]
//            };

//        internal static ulong[] PlayerToMoveHash = new ulong[2];

//        internal static ulong[] CanCastleKingSideHash = new ulong[2];
//        internal static ulong[] CanCastleQueenSideHash = new ulong[2];
//        internal static ulong[] CannotCastleKingSideHash = new ulong[2];
//        internal static ulong[] CannotCastleQueenSideHash = new ulong[2];

//        internal static Dictionary<ulong, ulong> EnPassantHashMap = new Dictionary<ulong, ulong>();
//        internal static ulong NoEnPassantSquareHash;

//        internal static void Initialize()
//        {
//            Random randomizer = new Random(DateTime.Now.Millisecond);

//            // This array represents the squares of a chessboard.
//            // It specifies the order in which the squares should
//            // be put into the FastMap object's binary tree.       
//            ulong[] indexes = new ulong[64] 
//            {
//                32, 16, 48, 08, 24, 40, 56, 04,
//                12, 20, 28, 36, 44, 52, 60, 02,
//                06, 10, 14, 18, 22, 26, 30, 34,
//                38, 42, 46, 50, 54, 58, 62, 01,
//                03, 05, 07, 09, 11, 13, 15, 17,
//                19, 21, 23, 25, 27, 29, 31, 33, 
//                35, 37, 39, 41, 43, 45, 47, 49, 
//                51, 53, 55, 57, 59, 61, 63, 00
//            };

//            // Pieces-squares hash map
//            for (int i = 0; i < PieceType.List.Length; i++)
//            {
//                if (PieceType.List[i] == PieceType.None)
//                {
//                    continue;
//                }
//                int currentPieceType = PieceType.List[i];

//                PieceHashMap[Player.White][currentPieceType] = new FastMap();
//                PieceHashMap[Player.Black][currentPieceType] = new FastMap();

//                for (int j = 0; j < indexes.Length; j++)
//                {
//                    FastMapNode nodeWhite = new FastMapNode() 
//                    {
//                        Key = (ulong)Math.Pow(2, indexes[j]),
//                        Value = randomizer.NextULong()
//                    };

//                    FastMapNode nodeBlack = new FastMapNode()
//                    {
//                        Key = (ulong)Math.Pow(2, indexes[j]),
//                        Value = randomizer.NextULong()
//                    };

//                    PieceHashMap[Player.White][currentPieceType].Add(nodeWhite);
//                    PieceHashMap[Player.Black][currentPieceType].Add(nodeBlack);
//                }
//            }

//            // WhiteToMove hash map
//            PlayerToMoveHash[Player.White] = randomizer.NextULong();
//            PlayerToMoveHash[Player.Black] = randomizer.NextULong();

//            // Castling rights hash
//            CanCastleKingSideHash[Player.White] = randomizer.NextULong();
//            CanCastleKingSideHash[Player.Black] = randomizer.NextULong();
//            CannotCastleKingSideHash[Player.White] = randomizer.NextULong();
//            CannotCastleKingSideHash[Player.Black] = randomizer.NextULong();
//            CanCastleQueenSideHash[Player.White] = randomizer.NextULong();
//            CanCastleQueenSideHash[Player.Black] = randomizer.NextULong();
//            CannotCastleQueenSideHash[Player.White] = randomizer.NextULong();
//            CannotCastleQueenSideHash[Player.Black] = randomizer.NextULong();

//            // En Passant hash map
//            EnPassantHashMap = new Dictionary<ulong, ulong>() 
//            {
//                {BitBoards.Square_A3, randomizer.NextULong()},
//                {BitBoards.Square_B3, randomizer.NextULong()},
//                {BitBoards.Square_C3, randomizer.NextULong()},
//                {BitBoards.Square_D3, randomizer.NextULong()},
//                {BitBoards.Square_E3, randomizer.NextULong()},
//                {BitBoards.Square_F3, randomizer.NextULong()},
//                {BitBoards.Square_G3, randomizer.NextULong()},
//                {BitBoards.Square_H3, randomizer.NextULong()},

//                {BitBoards.Square_A6, randomizer.NextULong()},
//                {BitBoards.Square_B6, randomizer.NextULong()},
//                {BitBoards.Square_C6, randomizer.NextULong()},
//                {BitBoards.Square_D6, randomizer.NextULong()},
//                {BitBoards.Square_E6, randomizer.NextULong()},
//                {BitBoards.Square_F6, randomizer.NextULong()},
//                {BitBoards.Square_G6, randomizer.NextULong()},
//                {BitBoards.Square_H6, randomizer.NextULong()},
//            };

//            NoEnPassantSquareHash = randomizer.NextULong();

//        }

//        public static ulong NextULong(this Random randomizer)
//        {
//            byte[] buffer = new byte[sizeof(ulong)];
//            randomizer.NextBytes(buffer);
//            return BitConverter.ToUInt64(buffer, 0);
//        }
//    }
//}
