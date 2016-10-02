using System.Collections.Generic;
using Griff.Definitions;

namespace Griff.Fen
{
    static class SquareMap
    {
        /// <summary>
        /// Table to match "e1" to Square_E1
        /// </summary>
        internal static Dictionary<string, ulong> StringToLong = new Dictionary<string, ulong>()
        {
            {"a1", BitBoards.Square_A1},
            {"a2", BitBoards.Square_A2},
            {"a3", BitBoards.Square_A3},
            {"a4", BitBoards.Square_A4},
            {"a5", BitBoards.Square_A5},
            {"a6", BitBoards.Square_A6},
            {"a7", BitBoards.Square_A7},
            {"a8", BitBoards.Square_A8},

            {"b1", BitBoards.Square_B1},
            {"b2", BitBoards.Square_B2},
            {"b3", BitBoards.Square_B3},
            {"b4", BitBoards.Square_B4},
            {"b5", BitBoards.Square_B5},
            {"b6", BitBoards.Square_B6},
            {"b7", BitBoards.Square_B7},
            {"b8", BitBoards.Square_B8},

            {"c1", BitBoards.Square_C1},
            {"c2", BitBoards.Square_C2},
            {"c3", BitBoards.Square_C3},
            {"c4", BitBoards.Square_C4},
            {"c5", BitBoards.Square_C5},
            {"c6", BitBoards.Square_C6},
            {"c7", BitBoards.Square_C7},
            {"c8", BitBoards.Square_C8},
            
            {"d1", BitBoards.Square_D1},
            {"d2", BitBoards.Square_D2},
            {"d3", BitBoards.Square_D3},
            {"d4", BitBoards.Square_D4},
            {"d5", BitBoards.Square_D5},
            {"d6", BitBoards.Square_D6},
            {"d7", BitBoards.Square_D7},
            {"d8", BitBoards.Square_D8},

            {"e1", BitBoards.Square_E1},
            {"e2", BitBoards.Square_E2},
            {"e3", BitBoards.Square_E3},
            {"e4", BitBoards.Square_E4},
            {"e5", BitBoards.Square_E5},
            {"e6", BitBoards.Square_E6},
            {"e7", BitBoards.Square_E7},
            {"e8", BitBoards.Square_E8},

            {"f1", BitBoards.Square_F1},
            {"f2", BitBoards.Square_F2},
            {"f3", BitBoards.Square_F3},
            {"f4", BitBoards.Square_F4},
            {"f5", BitBoards.Square_F5},
            {"f6", BitBoards.Square_F6},
            {"f7", BitBoards.Square_F7},
            {"f8", BitBoards.Square_F8},

            {"g1", BitBoards.Square_G1},
            {"g2", BitBoards.Square_G2},
            {"g3", BitBoards.Square_G3},
            {"g4", BitBoards.Square_G4},
            {"g5", BitBoards.Square_G5},
            {"g6", BitBoards.Square_G6},
            {"g7", BitBoards.Square_G7},
            {"g8", BitBoards.Square_G8},

            {"h1", BitBoards.Square_H1},
            {"h2", BitBoards.Square_H2},
            {"h3", BitBoards.Square_H3},
            {"h4", BitBoards.Square_H4},
            {"h5", BitBoards.Square_H5},
            {"h6", BitBoards.Square_H6},
            {"h7", BitBoards.Square_H7},
            {"h8", BitBoards.Square_H8}
        };

        internal static Dictionary<ulong, string> LongToString = new Dictionary<ulong, string>() 
        {
            {BitBoards.Square_A1, "a1"},
            {BitBoards.Square_A2, "a2"},
            {BitBoards.Square_A3, "a3"},
            {BitBoards.Square_A4, "a4"},
            {BitBoards.Square_A5, "a5"},
            {BitBoards.Square_A6, "a6"},
            {BitBoards.Square_A7, "a7"},
            {BitBoards.Square_A8, "a8"},

            {BitBoards.Square_B1, "b1"},
            {BitBoards.Square_B2, "b2"},
            {BitBoards.Square_B3, "b3"},
            {BitBoards.Square_B4, "b4"},
            {BitBoards.Square_B5, "b5"},
            {BitBoards.Square_B6, "b6"},
            {BitBoards.Square_B7, "b7"},
            {BitBoards.Square_B8, "b8"},

            {BitBoards.Square_C1, "c1"},
            {BitBoards.Square_C2, "c2"},
            {BitBoards.Square_C3, "c3"},
            {BitBoards.Square_C4, "c4"},
            {BitBoards.Square_C5, "c5"},
            {BitBoards.Square_C6, "c6"},
            {BitBoards.Square_C7, "c7"},
            {BitBoards.Square_C8, "c8"},

            {BitBoards.Square_D1, "d1"},
            {BitBoards.Square_D2, "d2"},
            {BitBoards.Square_D3, "d3"},
            {BitBoards.Square_D4, "d4"},
            {BitBoards.Square_D5, "d5"},
            {BitBoards.Square_D6, "d6"},
            {BitBoards.Square_D7, "d7"},
            {BitBoards.Square_D8, "d8"},

            {BitBoards.Square_E1, "e1"},
            {BitBoards.Square_E2, "e2"},
            {BitBoards.Square_E3, "e3"},
            {BitBoards.Square_E4, "e4"},
            {BitBoards.Square_E5, "e5"},
            {BitBoards.Square_E6, "e6"},
            {BitBoards.Square_E7, "e7"},
            {BitBoards.Square_E8, "e8"},

            {BitBoards.Square_F1, "f1"},
            {BitBoards.Square_F2, "f2"},
            {BitBoards.Square_F3, "f3"},
            {BitBoards.Square_F4, "f4"},
            {BitBoards.Square_F5, "f5"},
            {BitBoards.Square_F6, "f6"},
            {BitBoards.Square_F7, "f7"},
            {BitBoards.Square_F8, "f8"},

            {BitBoards.Square_G1, "g1"},
            {BitBoards.Square_G2, "g2"},
            {BitBoards.Square_G3, "g3"},
            {BitBoards.Square_G4, "g4"},
            {BitBoards.Square_G5, "g5"},
            {BitBoards.Square_G6, "g6"},
            {BitBoards.Square_G7, "g7"},
            {BitBoards.Square_G8, "g8"},

            {BitBoards.Square_H1, "h1"},
            {BitBoards.Square_H2, "h2"},
            {BitBoards.Square_H3, "h3"},
            {BitBoards.Square_H4, "h4"},
            {BitBoards.Square_H5, "h5"},
            {BitBoards.Square_H6, "h6"},
            {BitBoards.Square_H7, "h7"},
            {BitBoards.Square_H8, "h8"}
        };

        internal static Dictionary<int, Dictionary<int, ulong>> CoordinateToLong = new Dictionary<int, Dictionary<int, ulong>>() 
        {
            {1, new Dictionary<int,ulong>()
                {
                    {1, BitBoards.Square_A8},
                    {2, BitBoards.Square_B8},
                    {3, BitBoards.Square_C8},
                    {4, BitBoards.Square_D8},
                    {5, BitBoards.Square_E8},
                    {6, BitBoards.Square_F8},
                    {7, BitBoards.Square_G8},
                    {8, BitBoards.Square_H8}
                }
            },
            {2, new Dictionary<int,ulong>()
                {
                    {1, BitBoards.Square_A7},
                    {2, BitBoards.Square_B7},
                    {3, BitBoards.Square_C7},
                    {4, BitBoards.Square_D7},
                    {5, BitBoards.Square_E7},
                    {6, BitBoards.Square_F7},
                    {7, BitBoards.Square_G7},
                    {8, BitBoards.Square_H7}
                }
            },
            {3, new Dictionary<int,ulong>()
                {
                    {1, BitBoards.Square_A6},
                    {2, BitBoards.Square_B6},
                    {3, BitBoards.Square_C6},
                    {4, BitBoards.Square_D6},
                    {5, BitBoards.Square_E6},
                    {6, BitBoards.Square_F6},
                    {7, BitBoards.Square_G6},
                    {8, BitBoards.Square_H6}
                }
            },
            {4, new Dictionary<int,ulong>()
                {
                    {1, BitBoards.Square_A5},
                    {2, BitBoards.Square_B5},
                    {3, BitBoards.Square_C5},
                    {4, BitBoards.Square_D5},
                    {5, BitBoards.Square_E5},
                    {6, BitBoards.Square_F5},
                    {7, BitBoards.Square_G5},
                    {8, BitBoards.Square_H5}
                }
            },
            {5, new Dictionary<int,ulong>()
                {
                    {1, BitBoards.Square_A4},
                    {2, BitBoards.Square_B4},
                    {3, BitBoards.Square_C4},
                    {4, BitBoards.Square_D4},
                    {5, BitBoards.Square_E4},
                    {6, BitBoards.Square_F4},
                    {7, BitBoards.Square_G4},
                    {8, BitBoards.Square_H4}
                }
            },
            {6, new Dictionary<int,ulong>()
                {
                    {1, BitBoards.Square_A3},
                    {2, BitBoards.Square_B3},
                    {3, BitBoards.Square_C3},
                    {4, BitBoards.Square_D3},
                    {5, BitBoards.Square_E3},
                    {6, BitBoards.Square_F3},
                    {7, BitBoards.Square_G3},
                    {8, BitBoards.Square_H3}
                }
            },
            {7, new Dictionary<int,ulong>()
                {
                    {1, BitBoards.Square_A2},
                    {2, BitBoards.Square_B2},
                    {3, BitBoards.Square_C2},
                    {4, BitBoards.Square_D2},
                    {5, BitBoards.Square_E2},
                    {6, BitBoards.Square_F2},
                    {7, BitBoards.Square_G2},
                    {8, BitBoards.Square_H2}
                }
            },
            {8, new Dictionary<int,ulong>()
                {
                    {1, BitBoards.Square_A1},
                    {2, BitBoards.Square_B1},
                    {3, BitBoards.Square_C1},
                    {4, BitBoards.Square_D1},
                    {5, BitBoards.Square_E1},
                    {6, BitBoards.Square_F1},
                    {7, BitBoards.Square_G1},
                    {8, BitBoards.Square_H1}
                }
            }
        };

        internal static Dictionary<ulong, Pair> LongToCoordinate = new Dictionary<ulong, Pair>() 
        {
            { BitBoards.Square_A1, new Pair(1,1) },
            { BitBoards.Square_A2, new Pair(1,2) },
            { BitBoards.Square_A3, new Pair(1,3) },
            { BitBoards.Square_A4, new Pair(1,4) },
            { BitBoards.Square_A5, new Pair(1,5) },
            { BitBoards.Square_A6, new Pair(1,6) },
            { BitBoards.Square_A7, new Pair(1,7) },
            { BitBoards.Square_A8, new Pair(1,8) },

            { BitBoards.Square_B1, new Pair(2,1) },
            { BitBoards.Square_B2, new Pair(2,2) },
            { BitBoards.Square_B3, new Pair(2,3) },
            { BitBoards.Square_B4, new Pair(2,4) },
            { BitBoards.Square_B5, new Pair(2,5) },
            { BitBoards.Square_B6, new Pair(2,6) },
            { BitBoards.Square_B7, new Pair(2,7) },
            { BitBoards.Square_B8, new Pair(2,8) },

            { BitBoards.Square_C1, new Pair(3,1) },
            { BitBoards.Square_C2, new Pair(3,2) },
            { BitBoards.Square_C3, new Pair(3,3) },
            { BitBoards.Square_C4, new Pair(3,4) },
            { BitBoards.Square_C5, new Pair(3,5) },
            { BitBoards.Square_C6, new Pair(3,6) },
            { BitBoards.Square_C7, new Pair(3,7) },
            { BitBoards.Square_C8, new Pair(3,8) },

            { BitBoards.Square_D1, new Pair(4,1) },
            { BitBoards.Square_D2, new Pair(4,2) },
            { BitBoards.Square_D3, new Pair(4,3) },
            { BitBoards.Square_D4, new Pair(4,4) },
            { BitBoards.Square_D5, new Pair(4,5) },
            { BitBoards.Square_D6, new Pair(4,6) },
            { BitBoards.Square_D7, new Pair(4,7) },
            { BitBoards.Square_D8, new Pair(4,8) },

            { BitBoards.Square_E1, new Pair(5,1) },
            { BitBoards.Square_E2, new Pair(5,2) },
            { BitBoards.Square_E3, new Pair(5,3) },
            { BitBoards.Square_E4, new Pair(5,4) },
            { BitBoards.Square_E5, new Pair(5,5) },
            { BitBoards.Square_E6, new Pair(5,6) },
            { BitBoards.Square_E7, new Pair(5,7) },
            { BitBoards.Square_E8, new Pair(5,8) },

            { BitBoards.Square_F1, new Pair(6,1) },
            { BitBoards.Square_F2, new Pair(6,2) },
            { BitBoards.Square_F3, new Pair(6,3) },
            { BitBoards.Square_F4, new Pair(6,4) },
            { BitBoards.Square_F5, new Pair(6,5) },
            { BitBoards.Square_F6, new Pair(6,6) },
            { BitBoards.Square_F7, new Pair(6,7) },
            { BitBoards.Square_F8, new Pair(6,8) },

            { BitBoards.Square_G1, new Pair(7,1) },
            { BitBoards.Square_G2, new Pair(7,2) },
            { BitBoards.Square_G3, new Pair(7,3) },
            { BitBoards.Square_G4, new Pair(7,4) },
            { BitBoards.Square_G5, new Pair(7,5) },
            { BitBoards.Square_G6, new Pair(7,6) },
            { BitBoards.Square_G7, new Pair(7,7) },
            { BitBoards.Square_G8, new Pair(7,8) },

            { BitBoards.Square_H1, new Pair(8,1) },
            { BitBoards.Square_H2, new Pair(8,2) },
            { BitBoards.Square_H3, new Pair(8,3) },
            { BitBoards.Square_H4, new Pair(8,4) },
            { BitBoards.Square_H5, new Pair(8,5) },
            { BitBoards.Square_H6, new Pair(8,6) },
            { BitBoards.Square_H7, new Pair(8,7) },
            { BitBoards.Square_H8, new Pair(8,8) },
        };

    }
}
