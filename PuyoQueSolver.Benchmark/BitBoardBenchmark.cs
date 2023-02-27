using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace PuyoQueSolver.Benchmark
{
    [SimpleJob]
    public class BitBoardBenchmark
    {

        int[][] _board;
        public BitBoardBenchmark()
        {
            PuyoQueBitBoard board = new PuyoQueBitBoard();
            _board = new int[][]
            {
                new int []{ 5,5,3,4,4,5,0,4, },
                new int []{ 2,5,4,4,5,0,1,0, },
                new int []{ 2,2,1,1,0,3,5,4, },
                new int []{ 1,3,2,0,5,4,5,5, },
                new int []{ 3,3,0,5,3,4,4,1, },
                new int []{ 2,0,5,4,0,0,0,0, },
                new int []{ 0,5,2,1,5,3,3,2, },
            };
            board.SetBoard(_board);
        }

        const int n = 100000;
        //
        //|         Method |     Mean |    Error |   StdDev |
        //|--------------- |---------:|---------:|---------:|
        //|   UseMaskCache | 18.46 ms | 0.258 ms | 0.241 ms |
        //| UnuseMaskCache | 18.71 ms | 0.093 ms | 0.083 ms |

        //[Benchmark]
        //public void UseMaskCache()
        //{
        //    for (int i = 0; i < n; i++)
        //    {
        //        PuyoQueBitBoard board = new PuyoQueBitBoard();
        //        board.SetBoard(_board);
        //        board.Fall();
        //    }
        //}

        //[Benchmark]
        //public void UnuseMaskCache()
        //{
        //    for (int i = 0; i < n; i++)
        //    {
        //        PuyoQueBitBoard board = new PuyoQueBitBoard();
        //        board.SetBoard(_board);
        //        board.FallUnuseCache();
        //    }
        //}

    }
}
