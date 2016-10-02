using Griff.Board_Representation;
using Griff.Fen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Griff.Definitions;

namespace Griff
{
    class Program
    {
        static bool displayed = false;
        static ulong nodes = 0;

        static void Main(string[] args)
        {
            //BoardPosition currentPosition = BoardPosition.ParseFEN("rnbqkbnr/pppppppp/1p1P1p1P/4qQ2/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1");      
            //BoardPosition currentPosition = BoardPosition.ParseFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1");
            //BoardPosition currentPosition = BoardPosition.ParseFEN("1n4n1/8/8/8/8/8/8/1N4N1 b KQkq - 0 1");
            //BoardPosition currentPosition = BoardPosition.ParseFEN("NNNNNNNN/NnNnNnNn/nNnNnNnN/nnnnnnnn/NNNNNNNN/nNnNnNnN/NnNnNnNn/nnnnnnnn w - - 0 1");
            //BoardPosition currentPosition = BoardPosition.ParseFEN("NNNNNNNN/nnnnnnnn/8/8/8/8/NNNNNNNN/nnnnnnnn w - - 0 1");
            //BoardPosition currentPosition = BoardPosition.ParseFEN("NNNNNNNN/8/8/8/8/8/8/nnnnnnnn w - - 0 1");
            //BoardPosition currentPosition = BoardPosition.ParseFEN("5b2/p2r2pk/2n2q1p/1Qp1p1p1/3nP1P1/P2P1PN1/5BBP/1R4K1 w - - 12 40");

            BoardPosition currentPosition = BoardPosition.ParseFEN("8/8/8/8/8/8/8/4K3 w - - 0 1");

            //ulong nodes = 0;
            //Stopwatch sw = new Stopwatch();
            //BoardPosition.GenerateLegalMoves(currentPosition);
            //Random random = new Random(DateTime.Now.Millisecond);
            //Move[] moves = null;
            //sw.Start();
            //for (int i = 10000000; i > 0; i--)
            //{
            //    moves = BoardPosition.GenerateLegalMoves(currentPosition);
            //    if (moves.Length > 0)
            //    {
            //        //int index = random.Next(0, moves.Length);
            //        for (int j = moves.Length - 1; j >= 0; j--)
            //        {
            //            BoardPosition.MakeMove(moves[j], currentPosition);
            //            BoardPosition.UnmakeMove(moves[j], currentPosition);
            //            nodes++;
            //        }
            //    }
            //}
            //sw.Stop();
            //Console.WriteLine(BoardPosition.DumpPosition(currentPosition));
            //Console.WriteLine("Elapsed: " + sw.Elapsed.ToString());
            //Console.WriteLine("KN:" + nodes / 1000);
            //Console.WriteLine("KN/s: " + (nodes / 1000) / sw.Elapsed.TotalSeconds);
            //Console.ReadKey();
            //return;

            Console.WriteLine(BoardPosition.DumpPosition(currentPosition));

            BoardPosition clone = BoardPosition.ClonePosition(currentPosition);
            Console.WriteLine(BoardPosition.DumpPosition(currentPosition));

            Dictionary<ulong, BoardPosition> transpTable = new Dictionary<ulong, BoardPosition>();

            //Move[] legalMoves = BoardPosition.GenerateLegalMoves(currentPosition);

            //BoardPosition.MakeMove(legalMoves[0], currentPosition);

            //Console.WriteLine(BoardPosition.DumpPosition(currentPosition));

            Stopwatch stopper = new Stopwatch();
            stopper.Start();

            Play(currentPosition, 4, 0, transpTable);

            stopper.Stop();

            Console.WriteLine("Main\r\nKNodes:" + nodes / 1000);
            Console.WriteLine("Done: " + stopper.Elapsed.ToString() + "\r\nkN/s:" + (nodes / 1000) / stopper.Elapsed.TotalSeconds);

            Console.ReadKey();
        }

        static void Play(BoardPosition position, int maxDepth, int currentDepth, Dictionary<ulong, BoardPosition> transpTable)
        {
            if (currentDepth < maxDepth)
            {
                //if (!transpTable.ContainsKey(position.PositionHash))
                {
                    Move[] legalMoves = BoardPosition.GeneratePseudoLegalMoves(position);
                    for (int i = 0; i < legalMoves.Length; i++)
                    {
                        
                        BoardPosition.MakeMove(legalMoves[i], position);
                        //Console.WriteLine("\r\n" + counter + BoardPosition.DumpPosition(position));
                        Program.nodes++;
                        Play(position, maxDepth, currentDepth + 1, transpTable);
                        BoardPosition.UnmakeMove(legalMoves[i], position);

                        //transpTable[position.PositionHash] = position;

                        if (currentDepth == (maxDepth - 1) && Program.displayed == false)
                        {
                            Console.WriteLine("\r\n" + BoardPosition.DumpPosition(position));
                            displayed = true;
                        }
                    }
                }
                //else 
                //{
                //   // Program.nodes++;
                //}
            }
        }
    }
}
