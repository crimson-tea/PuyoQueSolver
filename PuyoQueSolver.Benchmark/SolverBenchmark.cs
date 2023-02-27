using BenchmarkDotNet.Attributes;
using VerticalBitBoard;

namespace PuyoQueSolver.Benchmark
{
   [SimpleJob]
    public class SolverBenchmark
    {
        int[][] board1 = new int[][]
        {
            new int []{ 5 ,5, 3, 4, 4, 5, 0, 4, },
            new int []{ 2 ,5, 4, 4, 5, 0, 1, 0, },
            new int []{ 2 ,2, 1, 1, 0, 3, 5, 4, },
            new int []{ 1 ,3, 2, 0, 5, 4, 5, 5, },
            new int []{ 3 ,3, 0, 5, 3, 4, 4, 1, },
            new int []{ 2 ,0, 5, 4, 1, 2, 2, 1, },
            new int []{ 0 ,5, 2, 1, 5, 3, 3, 2, },
        };

        int[][] board2 = new int[][]
        {
            new int []{ 5, 5, 5, 3, 2, 3, 4, 3, },
            new int []{ 4, 5, 4, 4, 3, 4, 5, 4, },
            new int []{ 3, 1, 2, 5, 1, 3, 3, 3, },
            new int []{ 1, 2, 2, 5, 1, 2, 5, 1, },
            new int []{ 5, 3, 3, 3, 5, 5, 2, 5, },
            new int []{ 3, 1, 2, 5, 4, 3, 5, 3, },
            new int []{ 4, 1, 2, 3, 3, 5, 1, 3, },
        };

        ulong del2 = 0b100000000110000010100000000000000000000UL;

        const int n = 1000000;

        ///// <summary>
        ///// 1combo
        ///// </summary>
        //[Benchmark]
        //public void SolverBench1()
        //{
        //    PuyoQueBitBoard board = new();
        //    board.SetBoard(board1);

        //    var del = 1U << 52;

        //    for (int i = 0; i < n; i++)
        //    {
        //        var (combo, deleted) = PuyoQueSolver.CalcComboBitBoard(board, del, rule: 4);
        //    }
        //}

        /// <summary>
        /// 5combo
        /// </summary>
        [Benchmark]
        public void SolverBench2()
        {
            PuyoQueBitBoard board = new();
            board.SetBoard(board2);

            for (int i = 0; i < n; i++)
            {
                var (combo, deleted) = PuyoQueSolver.CalcComboBitBoard(board, del2, rule: 4);
            }
        }

        /// <summary>
        /// 5combo
        /// </summary>
        [Benchmark]
        public void SolverBench2_NewImpl()
        {
            const ulong delNew = 0b_0011000_0001000_0010100_0000000UL;
            VBitBoard board = new(board2);

            for (int i = 0; i < n; i++)
            {
                var b = new VBitBoard(board.Board);
                var (rensa, deleted) = b.CalcRensa(delNew);
            }
        }

        //|               Method |       Mean |   Error |  StdDev |
        //|--------------------- |-----------:|--------:|--------:|
        //|         SolverBench2 | 1,087.7 ms | 5.14 ms | 4.81 ms |
        //| SolverBench2_NewImpl |   282.8 ms | 1.29 ms | 1.21 ms |
    }
}
