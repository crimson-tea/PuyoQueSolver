using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerticalBitBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PuyoQueSolver;

namespace VerticalBitBoard.Tests
{
    [TestClass()]
    public class VBitBoardTests
    {
        [TestMethod()]
        public void FallAllTest()
        {
            int[][] boardArray = new int[][] {
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,1 },
                new int[] { 0,0,0,0,0,0,0,0 },
            };

            int[][] expect = new int[][] {
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,1 },
            };


            var board = new VBitBoard(boardArray);
            board.FallAll();
            var exp = new VBitBoard(expect);

            Console.WriteLine(exp.ToString());
            Assert.AreEqual(exp.ToString(), board.ToString());
        }

        [TestMethod()]
        public void FallAllTest2()
        {
            int[][] boardArray = new int[][] {
                new int[] { 1,2,3,4,5,6,0,0 },
                new int[] { 1,2,3,4,5,0,7,1 },
                new int[] { 1,2,3,4,0,6,7,0 },
                new int[] { 1,2,3,0,5,6,7,0 },
                new int[] { 1,2,0,4,5,6,7,0 },
                new int[] { 1,0,3,4,5,6,7,0 },
                new int[] { 0,2,3,4,5,6,7,0 },
            };

            int[][] expect = new int[][] {
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,1 },
            };


            var board = new VBitBoard(boardArray);
            board.FallAll();
            var exp = new VBitBoard(expect);

            Console.WriteLine(exp.ToString());
            Assert.AreEqual(exp.ToString(), board.ToString());
        }

        [TestMethod()]
        public void FallAll2_Test()
        {
            int[][] boardArray = new int[][] {
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,1 },
                new int[] { 0,0,0,0,0,0,0,0 },
            };

            int[][] expect = new int[][] {
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,1 },
            };


            var board = new VBitBoard(boardArray);
            board.Fall2All();
            var exp = new VBitBoard(expect);

            Console.WriteLine(exp.ToString());
            Assert.AreEqual(exp.ToString(), board.ToString());
        }

        [TestMethod()]
        public void FallAll2_Test2()
        {
            int[][] boardArray = new int[][] {
                new int[] { 1,2,3,4,5,6,0,0 },
                new int[] { 1,2,3,4,5,0,7,1 },
                new int[] { 1,2,3,4,0,6,7,0 },
                new int[] { 1,2,3,0,5,6,7,0 },
                new int[] { 1,2,0,4,5,6,7,0 },
                new int[] { 1,0,3,4,5,6,7,0 },
                new int[] { 0,2,3,4,5,6,7,0 },
            };

            int[][] expect = new int[][] {
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,1 },
            };


            var board = new VBitBoard(boardArray);
            board.Fall2All();
            var exp = new VBitBoard(expect);

            Console.WriteLine(exp.ToString());
            Assert.AreEqual(exp.ToString(), board.ToString());
        }

        [TestMethod()]
        public void FallAll3_Test()
        {
            int[][] boardArray = new int[][] {
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,1 },
                new int[] { 0,0,0,0,0,0,0,0 },
            };

            int[][] expect = new int[][] {
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,1 },
            };


            var board = new VBitBoard(boardArray);
            board.Fall3All();
            var exp = new VBitBoard(expect);

            Console.WriteLine(exp.ToString());
            Assert.AreEqual(exp.ToString(), board.ToString());
        }

        [TestMethod()]
        public void FallAll3_Test2()
        {
            int[][] boardArray = new int[][] {
                new int[] { 1,2,3,4,5,6,0,0 },
                new int[] { 1,2,3,4,5,0,7,1 },
                new int[] { 1,2,3,4,0,6,7,0 },
                new int[] { 1,2,3,0,5,6,7,0 },
                new int[] { 1,2,0,4,5,6,7,0 },
                new int[] { 1,0,3,4,5,6,7,0 },
                new int[] { 0,2,3,4,5,6,7,0 },
            };

            int[][] expect = new int[][] {
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,1 },
            };


            var board = new VBitBoard(boardArray);
            board.Fall3All();
            var exp = new VBitBoard(expect);

            Console.WriteLine(exp.ToString());
            Assert.AreEqual(exp.ToString(), board.ToString());
        }

        [TestMethod()]
        public void Fall3_Test2()
        {
            int[][] boardArray = new int[][] {
                new int[] { 1,2,3,4,5,6,0,0 },
                new int[] { 1,2,3,4,5,0,7,1 },
                new int[] { 1,2,3,4,0,6,7,0 },
                new int[] { 1,2,3,0,5,6,7,0 },
                new int[] { 1,2,0,4,5,6,7,0 },
                new int[] { 1,0,3,4,5,6,7,0 },
                new int[] { 0,2,3,4,5,6,7,0 },
            };

            int[][] expect = new int[][] {
                new int[] { 1,2,3,4,5,6,0,0 },
                new int[] { 0,0,0,0,0,0,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,0 },
                new int[] { 1,2,3,4,5,6,7,1 },
            };


            var board = new VBitBoard(boardArray);
            board.Fall3();
            var exp = new VBitBoard(expect);

            Console.WriteLine(exp.ToString());
            Assert.AreEqual(exp.ToString(), board.ToString());
        }


        //[TestMethod()]
        //public void RemoveConnected_Red_Test()
        //{
        //    int[][] boardArray = new int[][] {
        //        new int[] { 0,0,0,0,0,0,0,0 },
        //        new int[] { 1,2,3,4,5,6,7,0 },
        //        new int[] { 1,2,3,4,5,6,7,0 },
        //        new int[] { 1,2,3,4,5,6,7,0 },
        //        new int[] { 1,2,3,4,5,6,7,0 },
        //        new int[] { 1,2,3,4,5,6,7,0 },
        //        new int[] { 1,2,3,4,5,6,7,1 },
        //    };

        //    int[][] expect = new int[][] {
        //        new int[] { 0,0,0,0,0,0,0,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,3,4,5,6,7,1 },
        //    };


        //    var board = new BitBoard(boardArray);
        //    board.RemoveConnected();
        //    var exp = new BitBoard(expect);

        //    Console.WriteLine(exp.ToString());
        //    Assert.AreEqual(exp.ToString(), board.ToString());
        //}

        //[TestMethod()]
        //public void RemoveConnected_Red_Test2()
        //{
        //    int[][] boardArray = new int[][] {
        //        new int[] { 1,1,1,2,2,1,1,1 },
        //        new int[] { 1,2,3,4,5,6,7,1 },
        //        new int[] { 1,2,3,4,5,6,7,1 },
        //        new int[] { 1,2,3,4,5,6,7,1 },
        //        new int[] { 1,2,3,4,5,6,7,1 },
        //        new int[] { 1,2,3,4,5,6,7,1 },
        //        new int[] { 1,2,3,4,5,6,7,1 },
        //    };

        //    int[][] expect = new int[][] {
        //        new int[] { 0,0,0,2,2,0,0,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //    };


        //    var board = new BitBoard(boardArray);
        //    board.RemoveConnected();
        //    var exp = new BitBoard(expect);

        //    Console.WriteLine(exp.ToString());
        //    Assert.AreEqual(exp.ToString(), board.ToString());
        //}
        //[TestMethod()]
        //public void RemoveConnected_Red_Test3()
        //{
        //    int[][] boardArray = new int[][] {
        //        new int[] { 1,1,1,1,0,0,0,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,1,4,5,6,7,1 },
        //        new int[] { 1,2,1,1,5,6,1,1 },
        //        new int[] { 1,2,1,4,5,6,1,0 },
        //        new int[] { 1,2,3,4,1,6,7,0 },
        //        new int[] { 1,2,3,1,1,1,7,0 },
        //    };

        //    int[][] expect = new int[][] {
        //        new int[] { 0,0,0,0,0,0,0,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,0,4,5,6,7,0 },
        //        new int[] { 0,2,0,0,5,6,0,0 },
        //        new int[] { 0,2,0,4,5,6,0,0 },
        //        new int[] { 0,2,3,4,0,6,7,0 },
        //        new int[] { 0,2,3,0,0,0,7,0 },
        //    };

        //    var board = new BitBoard(boardArray);
        //    board.RemoveConnected();
        //    var exp = new BitBoard(expect);

        //    Console.WriteLine(exp.ToString());
        //    Assert.AreEqual(exp.ToString(), board.ToString());
        //}
        //[TestMethod()]
        //public void RemoveConnected_Red_Test4()
        //{
        //    int[][] boardArray = new int[][] {
        //        new int[] { 1,1,1,0,0,0,0,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,0,4,5,6,7,1 },
        //        new int[] { 1,2,1,1,5,6,1,1 },
        //        new int[] { 1,2,1,4,5,6,0,2 },
        //        new int[] { 1,2,3,4,1,6,7,1 },
        //        new int[] { 0,2,3,0,1,1,7,1 },
        //    };

        //    int[][] expect = new int[][] {
        //        new int[] { 1,1,1,0,0,0,0,0 },
        //        new int[] { 0,2,3,4,5,6,7,0 },
        //        new int[] { 0,2,0,4,5,6,7,1 },
        //        new int[] { 1,2,1,1,5,6,1,1 },
        //        new int[] { 1,2,1,4,5,6,0,2 },
        //        new int[] { 1,2,3,4,1,6,7,1 },
        //        new int[] { 0,2,3,0,1,1,7,1 },
        //    };

        //    var board = new BitBoard(boardArray);
        //    board.RemoveConnected();
        //    var exp = new BitBoard(expect);

        //    Console.WriteLine(exp.ToString());
        //    Assert.AreEqual(exp.ToString(), board.ToString());
        //}

        [TestMethod()]
        public void RemoveConnected_Test()
        {
            int[][] boardArray = new int[][] {
                new int[] { 1,1,1,0,0,0,0,0 },
                new int[] { 0,2,3,4,5,6,7,0 },
                new int[] { 0,2,0,4,5,6,7,1 },
                new int[] { 1,2,1,1,5,6,1,1 },
                new int[] { 1,2,1,4,5,6,0,2 },
                new int[] { 1,2,3,4,1,6,7,1 },
                new int[] { 0,2,3,0,1,1,7,1 },
            };

            int[][] expect = new int[][] {
                new int[] { 1,1,1,0,0,0,0,0 },
                new int[] { 0,0,3,4,0,0,7,0 },
                new int[] { 0,0,0,4,0,0,7,1 },
                new int[] { 1,0,1,1,0,0,1,1 },
                new int[] { 1,0,1,4,0,0,0,2 },
                new int[] { 1,0,3,4,1,6,7,1 },
                new int[] { 0,0,3,0,1,1,7,1 },
            };

            var board = new VBitBoard(boardArray);
            board.DeleteConnected();
            var exp = new VBitBoard(expect);

            Console.WriteLine(exp.ToString());
            Assert.AreEqual(exp.ToString(), board.ToString());
        }

        [TestMethod()]
        public void RemoveConnected2_Test()
        {
            int[][] boardArray = new int[][] {
                new int[] { 1,1,1,0,0,0,0,0 },
                new int[] { 0,2,3,4,5,6,7,0 },
                new int[] { 0,2,0,4,5,6,7,1 },
                new int[] { 1,2,1,1,5,6,1,1 },
                new int[] { 1,2,1,4,5,6,0,2 },
                new int[] { 1,2,3,4,1,6,7,1 },
                new int[] { 0,2,3,0,1,1,7,1 },
            };

            int[][] expect = new int[][] {
                new int[] { 1,1,1,0,0,0,0,0 },
                new int[] { 0,0,3,4,0,0,7,0 },
                new int[] { 0,0,0,4,0,0,7,1 },
                new int[] { 1,0,1,1,0,0,1,1 },
                new int[] { 1,0,1,4,0,0,0,2 },
                new int[] { 1,0,3,4,1,6,7,1 },
                new int[] { 0,0,3,0,1,1,7,1 },
            };

            var board = new VBitBoard(boardArray);
            board.DeleteConnected2();
            var exp = new VBitBoard(expect);

            Console.WriteLine(exp.ToString());
            Assert.AreEqual(exp.ToString(), board.ToString());
        }

        [TestMethod()]
        public void CalcCombo_Test()
        {
            int[][] boardArray = new int[][] {
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,2,0,0,0 },
                new int[] { 0,0,0,2,1,2,0,0 },
                new int[] { 0,0,0,0,1,0,0,0 },
                new int[] { 0,0,0,0,1,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,1,2,0,0 },
            };
            int[][] expect = new int[][] {
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
            };


            var newImpl = new VBitBoard(boardArray);
            var (rensa, _) = newImpl.CalcRensa(0);

            var exp = new VBitBoard(expect);

            Assert.AreEqual(2, rensa);
            Assert.AreEqual(newImpl.ToString(), exp.ToString());
        }

        [TestMethod()]
        public void Delete_Test()
        {
            int[][] boardArray = new int[][] {
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 1,2,3,4,5,6,7,1 },
            };
            int[][] expect = new int[][] {
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
                new int[] { 0,0,0,0,0,0,0,0 },
            };

            var newImpl = new VBitBoard(boardArray);
            newImpl.Delete(0b_0000001_0000001_0000001_0000001_0000001_0000001_0000001_0000001UL);

            var exp = new VBitBoard(expect);

            Assert.AreEqual(newImpl.ToString(), exp.ToString());
        }

        [TestMethod()]
        public void CalcRensa_Test()
        {
            const ulong delNew = 0b_0011000_0001000_0010100_0000000UL;
            const ulong del = 0b100000000110000010100000000000000000000UL;

            // 5連鎖
            int[][] boardArray = new int[][]
            {
                new int []{ 5, 5, 5, 3, 2, 3, 4, 3, },
                new int []{ 4, 5, 4, 4, 3, 4, 5, 4, },
                new int []{ 3, 1, 2, 5, 1, 3, 3, 3, },
                new int []{ 1, 2, 2, 5, 1, 2, 5, 1, },
                new int []{ 5, 3, 3, 3, 5, 5, 2, 5, },
                new int []{ 3, 1, 2, 5, 4, 3, 5, 3, },
                new int []{ 4, 1, 2, 3, 3, 5, 1, 3, },
            };

            var newImpl = new VBitBoard(boardArray);
            Console.WriteLine(newImpl.ToString());
            var(rensa, deletedCount) = newImpl.CalcRensa(delNew);

            Console.WriteLine(newImpl.ToString());

            var board = new PuyoQueBitBoard();
            board.SetBoard(boardArray);
            board.Remove(del);
            Console.WriteLine(board.ToString());
            
            var (expectedRensa, expectedDeletedCount) = PuyoQueSolver.PuyoQueSolver.CalcComboBitBoard(board, del, rule: 4);


            Assert.AreEqual(expectedRensa, rensa);
            Assert.AreEqual(expectedDeletedCount, deletedCount);
        }
    }
}
