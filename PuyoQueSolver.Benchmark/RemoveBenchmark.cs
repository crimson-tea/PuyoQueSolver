using BenchmarkDotNet.Attributes;
using VerticalBitBoard;

namespace PuyoQueSolver.Benchmark
{

    // |      Method |     Mean |    Error |   StdDev |
    // |------------ |---------:|---------:|---------:|
    // | OldBitBoard | 74.96 ms | 0.721 ms | 0.675 ms |
    // | NewBitBoard | 22.27 ms | 0.090 ms | 0.079 ms |

    [SimpleJob]
    public class RemoveBenchmark
    {
        [Benchmark]
        public void OldBitBoard()
        {
            var x = new PuyoQueBitBoard();
            x.SetBoard(colorBoard2);
            for (int i = 0; i < n; i++)
            {
                var c = x.Clone();
                c.CalcDelete(out int count, 4);
            }
        }

        [Benchmark]
        public void NewBitBoard()
        {
            var x = new VBitBoard(colorBoard2);
            for (int i = 0; i < n; i++)
            {
                var c = new VBitBoard(x.Board);
                var count = c.DeleteConnected();
            }
        }

        [Benchmark]
        public void NewBitBoard2()
        {
            var x = new VBitBoard(colorBoard2);
            for (int i = 0; i < n; i++)
            {
                var c = new VBitBoard(x.Board);
                var count = c.DeleteConnected2();
            }
        }

        int[][] colorBoard2 = new int[][]
        {
            new int[] { 2,2,3,2,2,2,0,2, },
            new int[] { 2,2,2,2,2,0,0,0, },
            new int[] { 2, 2, 1, 1, 0, 2, 0, 2, },
            new int[] { 1, 2, 2, 0, 0, 2, 0, 2, },
            new int[] { 3, 3, 0, 2, 0, 2, 0, 1, },
            new int[] { 2, 0, 2, 2, 0, 2, 0, 1, },
            new int[] { 0, 2, 2, 1, 0, 3, 0, 2, },
        };

        const int n = 1000000;

    }
}