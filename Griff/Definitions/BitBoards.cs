namespace Griff.Definitions
{
    static class BitBoards
    {
        #region Squares

        internal const ulong Square_A1 = 72057594037927936;
        internal const ulong Square_A2 = 281474976710656;
        internal const ulong Square_A3 = 1099511627776;
        internal const ulong Square_A4 = 4294967296;
        internal const ulong Square_A5 = 16777216;
        internal const ulong Square_A6 = 65536;
        internal const ulong Square_A7 = 256;
        internal const ulong Square_A8 = 1;

        internal const ulong Square_B1 = 144115188075855872;
        internal const ulong Square_B2 = 562949953421312;
        internal const ulong Square_B3 = 2199023255552;
        internal const ulong Square_B4 = 8589934592;
        internal const ulong Square_B5 = 33554432;
        internal const ulong Square_B6 = 131072;
        internal const ulong Square_B7 = 512;
        internal const ulong Square_B8 = 2;

        internal const ulong Square_C1 = 288230376151711744;
        internal const ulong Square_C2 = 1125899906842624;
        internal const ulong Square_C3 = 4398046511104;
        internal const ulong Square_C4 = 17179869184;
        internal const ulong Square_C5 = 67108864;
        internal const ulong Square_C6 = 262144;
        internal const ulong Square_C7 = 1024;
        internal const ulong Square_C8 = 4;

        internal const ulong Square_D1 = 576460752303423488;
        internal const ulong Square_D2 = 2251799813685248;
        internal const ulong Square_D3 = 8796093022208;
        internal const ulong Square_D4 = 34359738368;
        internal const ulong Square_D5 = 134217728;
        internal const ulong Square_D6 = 524288;
        internal const ulong Square_D7 = 2048;
        internal const ulong Square_D8 = 8;

        internal const ulong Square_E1 = 1152921504606846976;
        internal const ulong Square_E2 = 4503599627370496;
        internal const ulong Square_E3 = 17592186044416;
        internal const ulong Square_E4 = 68719476736;
        internal const ulong Square_E5 = 268435456;
        internal const ulong Square_E6 = 1048576;
        internal const ulong Square_E7 = 4096;
        internal const ulong Square_E8 = 16;

        internal const ulong Square_F1 = 2305843009213693952;
        internal const ulong Square_F2 = 9007199254740992;
        internal const ulong Square_F3 = 35184372088832;
        internal const ulong Square_F4 = 137438953472;
        internal const ulong Square_F5 = 536870912;
        internal const ulong Square_F6 = 2097152;
        internal const ulong Square_F7 = 8192;
        internal const ulong Square_F8 = 32;

        internal const ulong Square_G1 = 4611686018427387904;
        internal const ulong Square_G2 = 18014398509481984;
        internal const ulong Square_G3 = 70368744177664;
        internal const ulong Square_G4 = 274877906944;
        internal const ulong Square_G5 = 1073741824;
        internal const ulong Square_G6 = 4194304;
        internal const ulong Square_G7 = 16384;
        internal const ulong Square_G8 = 64;

        internal const ulong Square_H1 = 9223372036854775808;
        internal const ulong Square_H2 = 36028797018963968;
        internal const ulong Square_H3 = 140737488355328;
        internal const ulong Square_H4 = 549755813888;
        internal const ulong Square_H5 = 2147483648;
        internal const ulong Square_H6 = 8388608;
        internal const ulong Square_H7 = 32768;
        internal const ulong Square_H8 = 128;

        #endregion

        #region Regions

        internal const ulong Region_B1H1B6H6 = 0xfefefefefefe0000;
        internal const ulong Region_A1G1A6G6 = 0x7f7f7f7f7f7f0000;
        internal const ulong Region_C1H1C7H7 = 0xfcfcfcfcfcfcfc00;
        internal const ulong Region_A1F1A7F7 = 0x3f3f3f3f3f3f3f00;
        internal const ulong Region_C2H2C8H8 = 0xfcfcfcfcfcfcfc;
        internal const ulong Region_A2F2A8F8 = 0x3f3f3f3f3f3f3f;
        internal const ulong Region_B3H3B8H8 = 0xfefefefefefe;
        internal const ulong Region_A3G3A8G8 = 0x7f7f7f7f7f7f;

        internal const ulong Region_B1H1B7H7 = 0xfefefefefefefe00;
        internal const ulong Region_A1H1A7H7 = 0xffffffffffffff00;
        internal const ulong Region_A1G1A7G7 = 0x7f7f7f7f7f7f7f00;
        internal const ulong Region_B1H1B8H8 = 0xfefefefefefefefe;
        internal const ulong Region_A1G1A8G8 = 0x7f7f7f7f7f7f7f7f;
        internal const ulong Region_B2H2B8H8 = 0xfefefefefefefe;
        internal const ulong Region_A2H2A8H8 = 0xffffffffffffff;
        internal const ulong Region_A2G2A8G8 = 0x7f7f7f7f7f7f7f;

        internal static readonly ulong[] Square_ShortCastle_Rook_Source = new ulong[] 
        {
            BitBoards.Square_H1,
            BitBoards.Square_H8
        };

        internal static readonly ulong[] Square_LongCastle_Rook_Source = new ulong[] 
        {
            BitBoards.Square_A1,
            BitBoards.Square_A8
        };

        internal static readonly ulong[] Square_Castle_King_Source = new ulong[] 
        {
            BitBoards.Square_E1,
            BitBoards.Square_E8
        };

        internal static readonly ulong[] Square_ShortCastle_King_Target = new ulong[] 
        {
            BitBoards.Square_G1,
            BitBoards.Square_G8
        };

        internal static readonly ulong[] Square_LongCastle_King_Target = new ulong[] 
        {
            BitBoards.Square_C1,
            BitBoards.Square_C8
        };

        internal static readonly ulong[] Region_ShortCastleMask = new ulong[] 
        {
            0x9000000000000000,
            0x90
        };

        internal static readonly ulong[] Region_LongCastleMask = new ulong[] 
        {
            0x1100000000000000,
            0x11
        };

        #endregion
    }
}
