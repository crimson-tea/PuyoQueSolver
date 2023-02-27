using BenchmarkDotNet.Attributes;
using VerticalBitBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuyoQueSolver.Benchmark
{
    [SimpleJob]
    public class Avx2Benchmark
    {

        int[][] colorBoard;
        int[][] colorBoard2;
        int[][] colorBoard3;
        public Avx2Benchmark()
        {
            colorBoard = new int[][]
            {
                new int []{ 2,2,3,2,2,2,0,2, },
                new int []{ 2,2,2,2,2,0,1,0, },
                new int []{ 2,2,1,1,0,2,2,2, },
                new int []{ 1,2,2,0,2,2,2,2, },
                new int []{ 3,3,0,2,3,2,2,1, },
                new int []{ 2,0,2,2,1,2,2,1, },
                new int []{ 0,2,2,1,2,3,3,2, },
            };
            colorBoard2 = new int[][]
            {
                new int []{ 2,2,3,2,2,2,0,2, },
                new int []{ 2,2,2,2,2,0,0,0, },
                new int []{ 2,2,1,1,0,2,0,2, },
                new int []{ 1,2,2,0,0,2,0,2, },
                new int []{ 3,3,0,2,0,2,0,1, },
                new int []{ 2,0,2,2,0,2,0,1, },
                new int []{ 0,2,2,1,0,3,0,2, },
            };
            colorBoard3 = new int[][]
            {
                new int []{ 2,2,3,2,2,2,0,2, },
                new int []{ 2,2,2,2,2,1,0,1, },
                new int []{ 2,2,1,1,1,2,0,2, },
                new int []{ 1,2,2,1,1,2,0,2, },
                new int []{ 3,3,1,2,1,2,0,1, },
                new int []{ 2,1,2,2,1,2,0,1, },
                new int []{ 1,2,2,1,1,3,0,2, },
            };

            // colorBoard2 = colorBoard;
            // colorBoard2 = colorBoard3;
        }

        //|             Method |      Mean |     Error |    StdDev |
        //|------------------- |----------:|----------:|----------:|
        //|     MergeBenchmark |  18.80 ms |  0.369 ms |  0.616 ms |
        //| MergeAvx2Benchmark |  29.90 ms |  0.590 ms |  1.245 ms |
        //[Benchmark]
        //public void MergeBenchmark()
        //{
        //    var board = new PuyoQueBitBoard();
        //    board.Set(colorBoard2);

        //    for (int i = 0; i < n; i++)
        //    {
        //        var b = board.MergeBoard;
        //    }
        //}

        //[Benchmark]
        //public void MergeAvx2Benchmark()
        //{
        //    var board = new PuyoQueBitBoard();
        //    board.Set(colorBoard2);

        //    for (int i = 0; i < n; i++)
        //    {
        //        var b = board.MergeBoardAvx2;
        //    }
        //}


        //|      Method |     Mean |    Error |   StdDev |
        //|------------ |---------:|---------:|---------:|
        //|        Fall | 709.9 ms | 13.51 ms | 13.27 ms |
        //|    FallAvx2 | 362.1 ms |  2.54 ms |  2.25 ms |
        //|     FallAll | 896.1 ms | 10.83 ms | 10.13 ms |
        //| FallAllAvx2 | 423.1 ms |  6.54 ms |  6.12 ms |

        const int n = 1000000;
        //[Benchmark]
        //public void Fall()
        //{
        //    var board = new PuyoQueBitBoard();
        //    board.Set(colorBoard2);

        //    for (int i = 0; i < n; i++)
        //    {
        //        var b = board.Clone();
        //        b.Fall();
        //    }
        //}

        //[Benchmark]
        //public void FallPack()
        //{
        //    var board = new PuyoQueBitBoard();
        //    board.Set(colorBoard2);

        //    for (int i = 0; i < n; i++)
        //    {
        //        var b = board.Clone();
        //        b.FallPackedBit();
        //    }
        //}

        [Benchmark]
        public void FallAvx2()
        {
            var board = new PuyoQueBitBoard();
            board.SetBoard(colorBoard2);

            for (int i = 0; i < n; i++)
            {
                var b = board.Clone();
                b.FallAvx2();
            }
        }

        //[Benchmark]
        //public void FallAvx2_2()
        //{
        //    var board = new PuyoQueBitBoard();
        //    board.SetBoard(colorBoard2);

        //    for (int i = 0; i < n; i++)
        //    {
        //        var b = board.Clone();
        //        b.FallAvx2_2();
        //    }
        //}

        //|              Method |     Mean |    Error |   StdDev |
        //|-------------------- |---------:|---------:|---------:|
        //|            FallAvx2 | 45.84 ms | 0.751 ms | 0.702 ms |
        //|   FallBitBoard_Pext | 13.99 ms | 0.257 ms | 0.240 ms |
        //| FallBitBoard_Pext_2 | 11.72 ms | 0.218 ms | 0.214 ms |
        //| FallBitBoard_Pext_3 | 11.07 ms | 0.217 ms | 0.274 ms |
        [Benchmark]
        public void FallBitBoard_Pext()
        {
            var board = new VBitBoard(colorBoard2);

            for (int i = 0; i < n; i++)
            {
                var b = new VBitBoard(board.Board);
                b.FallAll();
            }
        }

        //[Benchmark]
        //public void FallBitBoard_Pext_2()
        //{
        //    var board = new VBitBoard(colorBoard2);

        //    for (int i = 0; i < n; i++)
        //    {
        //        var b = new VBitBoard(board.Board);
        //        b.Fall2All();
        //    }
        //}

        [Benchmark]
        public void FallBitBoard_Pext_3()
        {
            var board = new VBitBoard(colorBoard2);

            for (int i = 0; i < n; i++)
            {
                var b = new VBitBoard(board.Board);
                b.Fall3All();
            }
        }

        //[Benchmark]
        //public void FallPackAvx2()
        //{
        //    var board = new PuyoQueBitBoard();
        //    board.Set(colorBoard2);

        //    for (int i = 0; i < n; i++)
        //    {
        //        var b = board.Clone();
        //        b.FallAllPackedBitAvx2();
        //    }
        //}

        //[Benchmark]
        //public void FallPackAvx2_2()
        //{
        //    var board = new PuyoQueBitBoard();
        //    board.Set(colorBoard2);

        //    for (int i = 0; i < n; i++)
        //    {
        //        var b = board.Clone();
        //        b.FallAllPackedBitAvx2_2();
        //    }
        //}

        //[Benchmark]
        //public void FallAll()
        //{
        //    var board = new PuyoQueBitBoard();
        //    board.Set(colorBoard2);

        //    for (int i = 0; i < n; i++)
        //    {
        //        var b = board.Clone();
        //        b.FallAll();
        //    }
        //}

        //[Benchmark]
        //public void FallAllPack()
        //{
        //    var board = new PuyoQueBitBoard();
        //    board.Set(colorBoard2);

        //    for (int i = 0; i < n; i++)
        //    {
        //        var b = board.Clone();
        //        b.FallAllPackedBit();
        //    }
        //}

        //[Benchmark]
        //public void FallAllAvx2()
        //{
        //    var board = new PuyoQueBitBoard();
        //    board.Set(colorBoard2);

        //    for (int i = 0; i < n; i++)
        //    {
        //        var b = board.Clone();
        //        b.FallAllAvx2();
        //    }
        //}


        //[Benchmark]
        //public void FallAllAvx2_2()
        //{
        //    var board = new PuyoQueBitBoard();
        //    board.Set(colorBoard2);

        //    for (int i = 0; i < n; i++)
        //    {
        //        var b = board.Clone();
        //        b.FallAllAvx2_2();
        //    }
        //}

        //// colorBoard
        //|      Method |     Mean |    Error |   StdDev |
        //|------------ |---------:|---------:|---------:|
        //|        Fall | 707.6 ms | 12.70 ms | 14.12 ms |
        //|    FallPack | 383.7 ms |  2.58 ms |  2.29 ms |
        //|     FallAll | 888.9 ms |  8.19 ms |  7.26 ms |
        //| FallAllPack | 381.2 ms |  3.13 ms |  2.77 ms |

        //// colorBoard2
        //|      Method |    Mean |    Error |   StdDev |
        //|------------ |--------:|---------:|---------:|
        //|        Fall | 1.545 s | 0.0158 s | 0.0148 s |
        //|    FallPack | 1.084 s | 0.0062 s | 0.0052 s |
        //|     FallAll | 1.810 s | 0.0058 s | 0.0049 s |
        //| FallAllPack | 1.257 s | 0.0039 s | 0.0036 s |

        //// worst case for pack
        //|      Method |       Mean |    Error |   StdDev |
        //|------------ |-----------:|---------:|---------:|
        //|        Fall |   716.0 ms |  8.81 ms |  8.24 ms |
        //|    FallPack | 1,096.9 ms | 11.62 ms | 10.30 ms |
        //|     FallAll |   804.0 ms |  4.91 ms |  4.10 ms |
        //| FallAllPack | 1,266.8 ms |  5.90 ms |  4.93 ms |

        //// best case for pack
        //|      Method |       Mean |    Error |   StdDev |
        //|------------ |-----------:|---------:|---------:|
        //|        Fall |   792.0 ms | 12.35 ms | 10.94 ms |
        //|    FallPack |   447.2 ms |  8.76 ms |  9.37 ms |
        //|    FallAvx2 |   421.0 ms |  8.22 ms |  9.14 ms |
        //|     FallAll | 1,002.9 ms | 19.93 ms | 18.64 ms |
        //| FallAllPack |   433.2 ms |  5.37 ms |  4.49 ms |
        //| FallAllAvx2 |   485.3 ms |  6.19 ms |  5.49 ms |

        //        // best case for pack
        //|       Method |     Mean |    Error |   StdDev |
        //|------------- |---------:|---------:|---------:|
        //|         Fall | 799.2 ms | 13.94 ms | 12.36 ms |
        //|     FallPack | 440.2 ms |  8.69 ms |  9.66 ms |
        //|     FallAvx2 | 428.6 ms |  8.47 ms |  8.32 ms |
        //|   FallAvx2_2 | 366.7 ms |  5.68 ms |  5.83 ms |
        //| FallPackAvx2 | 447.5 ms |  5.29 ms |  4.94 ms |

        //// board1
        //|         Method |     Mean |   Error |  StdDev |
        //|--------------- |---------:|--------:|--------:|
        //|           Fall | 787.2 ms | 8.16 ms | 6.82 ms |
        //|       FallPack | 468.2 ms | 8.62 ms | 8.06 ms |
        //|       FallAvx2 | 423.3 ms | 8.38 ms | 9.98 ms |
        //|     FallAvx2_2 | 359.4 ms | 4.04 ms | 3.58 ms |
        //|   FallPackAvx2 | 371.3 ms | 4.97 ms | 4.15 ms |
        //| FallPackAvx2_2 | 392.2 ms | 7.65 ms | 8.51 ms |

        ////board1
        //|         Method |       Mean |    Error |   StdDev |
        //|--------------- |-----------:|---------:|---------:|
        //|           Fall | 1,726.6 ms | 33.81 ms | 36.17 ms |
        //|       FallPack | 1,189.1 ms |  7.14 ms |  6.33 ms |
        //|       FallAvx2 |   693.8 ms |  9.06 ms |  8.48 ms |
        //|     FallAvx2_2 |   540.7 ms | 10.64 ms | 27.28 ms |
        //|   FallPackAvx2 |   854.6 ms | 14.26 ms | 12.64 ms |
        //| FallPackAvx2_2 |   804.1 ms | 11.34 ms | 10.05 ms |

        //// b2
        //|-------------- |---------:|--------:|--------:|
        //|      FallAvx2 | 586.0 ms | 7.06 ms | 6.26 ms |
        //|    FallAvx2_2 | 517.4 ms | 5.46 ms | 4.26 ms |
        //|   FallAllAvx2 | 654.0 ms | 5.28 ms | 4.41 ms |
        //| FallAllAvx2_2 | 571.7 ms | 8.86 ms | 7.85 ms |
    }
}
