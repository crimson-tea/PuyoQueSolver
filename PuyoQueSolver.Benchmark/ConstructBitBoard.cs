using BenchmarkDotNet.Attributes;
using VerticalBitBoard;

namespace PuyoQueSolver.Benchmark
{

    // |           Method |       Mean |     Error |    StdDev |
    // |----------------- |-----------:|----------:|----------:|
    // |      OldBitBoard |  46.744 ms | 0.2409 ms | 0.2012 ms |
    // |      NewBitBoard | 102.253 ms | 0.1989 ms | 0.1860 ms |
    // |     NewBitBoard2 |  71.016 ms | 0.1886 ms | 0.1671 ms |
    // | OldBitBoardClone |   9.159 ms | 0.0384 ms | 0.0359 ms |
    // | NewBitBoardClone |   5.254 ms | 0.0157 ms | 0.0139 ms |
    [SimpleJob]
    public class ConstructBitBoard
    {
        [Benchmark]
        public void OldBitBoard()
        {
            for (int i = 0; i < n; i++)
            {
                var x = new PuyoQueBitBoard();
                x.SetBoard(colorBoard2);
            }
        }

        [Benchmark]
        public void NewBitBoard()
        {
            for (int i = 0; i < n; i++)
            {
                var x = new VBitBoard(colorBoard2);
            }
        }

        [Benchmark]
        public void NewBitBoard2()
        {
            for (int i = 0; i < n; i++)
            {
                var x = new VBitBoard(colorBoard2, false);
            }
        }

        [Benchmark]
        public void OldBitBoardClone()
        {
            var orig = new PuyoQueBitBoard();
            orig.SetBoard(colorBoard2);

            for (int i = 0; i < n; i++)
            {
                var clone = orig.Clone();
            }
        }

        [Benchmark]
        public void NewBitBoardClone()
        {
            var orig = new VBitBoard(colorBoard2, false);
            for (int i = 0; i < n; i++)
            {
                var clone = new VBitBoard(orig.Board);
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