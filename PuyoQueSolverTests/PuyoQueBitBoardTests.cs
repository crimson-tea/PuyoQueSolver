using Microsoft.VisualStudio.TestTools.UnitTesting;
using PuyoQueSolver;
using System;

namespace PuyoQueSolverTests
{
    [TestClass]
    public class PuyoQueBitBoardTests
    {
        [TestMethod()]
        public void FallAvx2_2Test()
        {
            PuyoQueBitBoard board = new();
            var initBoard = new int[][]
            {
                new int []{ 5,5,3,4,4,5,0,4, },
                new int []{ 2,5,4,4,5,0,1,0, },
                new int []{ 2,2,1,1,0,3,5,4, },
                new int []{ 1,3,2,0,5,4,5,5, },
                new int []{ 3,3,0,5,3,4,4,1, },
                new int []{ 2,0,5,4,1,2,2,1, },
                new int []{ 0,5,2,1,5,3,3,2, },
            };

            var expectBoard = new int[][]
            {
                new int []{ 0,0,0,0,0,0,0,0, },
                new int []{ 5,5,3,4,4,5,1,4, },
                new int []{ 2,5,4,4,5,3,5,4, },
                new int []{ 2,2,1,1,5,4,5,5, },
                new int []{ 1,3,2,5,3,4,4,1, },
                new int []{ 3,3,5,4,1,2,2,1, },
                new int []{ 2,5,2,1,5,3,3,2, },
            };

            board.SetBoard(initBoard);
            board.FallAllAvx2();

            PuyoQueBitBoard expect = new();
            expect.SetBoard(expectBoard);

            Console.WriteLine(board);
            Console.WriteLine(expect);

            Assert.AreEqual(expect.ToString(), board.ToString());
        }

        int[][] board = new int[][]
        {
            new int[]{ 5,5,3,4,4,5,4,4, },
            new int[]{ 2,5,4,4,5,1,1,1 },
            new int[] { 2, 2, 1, 1, 3, 3, 5, 4 },
            new int[] { 1, 3, 2, 3, 5, 4, 5, 5, },
            new int[] { 3, 3, 4, 5, 3, 4, 4, 1, },
            new int[] { 2, 4, 5, 4, 1, 2, 2, 1 },
            new int[] { 3, 5, 2, 1, 5, 3, 3, 2 },
        };
    }
}
