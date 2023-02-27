using System.Buffers;
using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace VerticalBitBoard
{
    /// <summary>
    /// 縦形ビットボード
    /// 最右列の下がbitの最下位、最左列の上がbitの最上位(56番目)に対応する。
    /// TODO: 一列につき1bitパディングした8x8のビットボードを作り、パフォーマンスを比較する。
    /// </summary>
    public struct VBitBoard
    {
        public ulong[] Board;
        const int width = 8;
        const int height = 7;

        static ulong[] ColFillOne;
        static VBitBoard()
        {
            ColFillOne = CreateColFillOne();

            static ulong[] CreateColFillOne()
            {
                var res = new ulong[8];
                for (int i = 0; i < res.Length; i++)
                {
                    res[i] = (1UL << i) - 1;
                    // Console.WriteLine($"{i}: {Convert.ToString((long)res[i], 2)}");
                }
                return res;
            }
        }

        public VBitBoard(int[][] b)
        {
            Board = new ulong[3];

            for (int i = 0; i < b.Length; i++)
            {
                for (int k = 0; k < b[i].Length; k++)
                {
                    var (x, y, z) = b[i][k] switch
                    {
                        1 => (1UL, 0UL, 0UL),
                        2 => (0UL, 1UL, 0UL),
                        3 => (1UL, 1UL, 0UL),
                        4 => (0UL, 0UL, 1UL),
                        5 => (1UL, 0UL, 1UL),
                        6 => (0UL, 1UL, 1UL),
                        7 => (1UL, 1UL, 1UL),
                        _ => (0UL, 0UL, 0UL),
                    };

                    var shift = 55 - 7 * k - i;
                    Board[0] |= x << shift;
                    Board[1] |= y << shift;
                    Board[2] |= z << shift;
                }
            }
        }

        public VBitBoard(int[][] b, bool a)
        {
            ulong o = 0UL;
            ulong p = 0UL;
            ulong q = 0UL;

            for (int i = 0; i < b.Length; i++)
            {
                var shift = 55 - i;
                for (int k = 0; k < b[i].Length; k++)
                {
                    var value = b[i][k];

                    var x = (ulong)(value & 1);
                    var y = (ulong)((value >> 1) & 1);
                    var z = (ulong)((value >> 2) & 1);

                    o |= x << shift;
                    p |= y << shift;
                    q |= z << shift;

                    shift -= width;
                }
            }

            Board = new ulong[3] { o, p, q };
        }

        public VBitBoard(ulong[] bitBoard)
        {
            Board = new ulong[3];
            bitBoard.CopyTo(Board, 0);
        }

        public ulong Merged => Board[0] | Board[1] | Board[2];

        public void FallAll()
        {
            // Console.WriteLine(ToString());
            var exist = Merged;
            ulong a = exist & 0b_1111111UL;
            ulong b = exist & (0b_1111111UL << height);
            var c = exist & (0b_1111111UL << (height * 2));
            var d = exist & (0b_1111111UL << (height * 3));
            var e = exist & (0b_1111111UL << (height * 4));
            var f = exist & (0b_1111111UL << (height * 5));
            var g = exist & (0b_1111111UL << (height * 6));
            var h = exist & (0b_1111111UL << (height * 7));

            for (int i = 0; i < Board.Length; i++)
            {
                ulong next = 0;
                ulong n = 0;
                var board = Board[i];
                n = Bmi2.X64.ParallelBitExtract(board, a);
                next |= n;

                // Console.WriteLine(Convert.ToString((long)next, 2).PadLeft(56));
                n = Bmi2.X64.ParallelBitExtract(board, b);
                next |= n << height;

                // Console.WriteLine(Convert.ToString((long)n, 2).PadLeft(56));
                //  Console.WriteLine(Convert.ToString((long)next, 2).PadLeft(56));

                n = Bmi2.X64.ParallelBitExtract(board, c);
                next |= n << (height * 2);

                // Console.WriteLine(Convert.ToString((long)next, 2).PadLeft(56));
                n = Bmi2.X64.ParallelBitExtract(board, d);
                next |= n << (height * 3);

                // Console.WriteLine(Convert.ToString((long)next, 2).PadLeft(56));
                n = Bmi2.X64.ParallelBitExtract(board, e);
                next |= n << (height * 4);

                // Console.WriteLine(Convert.ToString((long)next, 2).PadLeft(56));
                n = Bmi2.X64.ParallelBitExtract(board, f);
                next |= n << (height * 5);

                //  Console.WriteLine(Convert.ToString((long)next, 2).PadLeft(56));
                n = Bmi2.X64.ParallelBitExtract(board, g);
                next |= n << (height * 6);

                // Console.WriteLine(Convert.ToString((long)next, 2).PadLeft(56));
                n = Bmi2.X64.ParallelBitExtract(board, h);
                next |= n << (height * 7);

                // Console.WriteLine(Convert.ToString((long)next, 2).PadLeft(56));
                Board[i] = next;
            }
        }

        public void Fall2All()
        {
            var exist = Merged;

            var restore = 0UL;
            restore |= (ColFillOne[BitOperations.PopCount(exist & (0b_1111111UL))]);
            restore |= (ColFillOne[BitOperations.PopCount(exist & (0b_1111111UL << 7))] << 7);
            restore |= (ColFillOne[BitOperations.PopCount(exist & (0b_1111111UL << 14))] << 14);
            restore |= (ColFillOne[BitOperations.PopCount(exist & (0b_1111111UL << 21))] << 21);
            restore |= (ColFillOne[BitOperations.PopCount(exist & (0b_1111111UL << 28))] << 28);
            restore |= (ColFillOne[BitOperations.PopCount(exist & (0b_1111111UL << 35))] << 35);
            restore |= (ColFillOne[BitOperations.PopCount(exist & (0b_1111111UL << 42))] << 42);
            restore |= (ColFillOne[BitOperations.PopCount(exist & (0b_1111111UL << 49))] << 49);


            // Console.WriteLine(Convert.ToString((long)restore, 2).PadLeft(56));
            for (int i = 0; i < Board.Length; i++)
            {
                var all = Bmi2.X64.ParallelBitExtract(Board[i], exist);
                ulong next = Bmi2.X64.ParallelBitDeposit(all, restore);
                Board[i] = next;
            }
        }

        public void Fall3All()
        {
            // Console.WriteLine(ToString());
            var existing = Merged;

            var restore = 0UL;
            restore |= (1UL << BitOperations.PopCount(existing & (0b_1111111UL))) - 1;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height)))) - 1) << height;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height * 2)))) - 1) << height * 2;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height * 3)))) - 1) << height * 3;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height * 4)))) - 1) << height * 4;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height * 5)))) - 1) << height * 5;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height * 6)))) - 1) << height * 6;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height * 7)))) - 1) << height * 7;

            // Console.WriteLine(Convert.ToString((long)restore, 2).PadLeft(56));
            for (int i = 0; i < Board.Length; i++)
            {
                var all = Bmi2.X64.ParallelBitExtract(Board[i], existing);
                ulong next = Bmi2.X64.ParallelBitDeposit(all, restore);
                Board[i] = next;
            }
        }

        public void Fall3()
        {
            // Console.WriteLine(ToString());
            var existing = Merged & 0b_0111111_0111111_0111111_0111111_0111111_0111111_0111111_0111111UL;

            var restore = 0UL;
            restore |= (1UL << BitOperations.PopCount(existing & (0b_1111111UL))) - 1;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height)))) - 1) << height;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height * 2)))) - 1) << height * 2;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height * 3)))) - 1) << height * 3;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height * 4)))) - 1) << height * 4;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height * 5)))) - 1) << height * 5;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height * 6)))) - 1) << height * 6;
            restore |= ((1UL << (BitOperations.PopCount(existing & (0b_1111111UL << height * 7)))) - 1) << height * 7;

            // Console.WriteLine(Convert.ToString((long)restore, 2).PadLeft(56));
            for (int i = 0; i < Board.Length; i++)
            {
                var all = Bmi2.X64.ParallelBitExtract(Board[i], existing);
                ulong next = Bmi2.X64.ParallelBitDeposit(all, restore);
                Board[i] = next | (Board[i] & 0b_1000000_1000000_1000000_1000000_1000000_1000000_1000000_1000000UL);
            }
        }

        public int DeleteConnected()
        {
            var red = Board[0] & ~Board[1] & ~Board[2]; // 1        100
            var blue = ~Board[0] & Board[1] & ~Board[2]; // 2       010
            var green = Board[0] & Board[1] & ~Board[2]; // 3       110
            var yellow = ~Board[0] & ~Board[1] & Board[2]; // 4     001
            var purple = Board[0] & ~Board[1] & Board[2]; // 5      101
            var heart = ~Board[0] & Board[1] & Board[2]; // 6       011
            var ojama = Board[0] & Board[1] & Board[2]; // 7        111

            var desappear = DisappearColor(red);
            desappear |= DisappearColor(blue);
            desappear |= DisappearColor(green);
            desappear |= DisappearColor(yellow);
            desappear |= DisappearColor(purple);

            var vanishingHeartAndOjama = Expand(desappear, heart | ojama);

            desappear |= vanishingHeartAndOjama;

            Board[0] &= ~desappear;
            Board[1] &= ~desappear;
            Board[2] &= ~desappear;

            return BitOperations.PopCount(desappear);
        }

        public int DeleteConnected2()
        {
            const ulong mask = 0b_0111111_0111111_0111111_0111111_0111111_0111111_0111111_0111111UL;
            var board0 = Board[0];
            var board1 = Board[1];
            var board2 = Board[2];

            var not_board0 = ~board0;
            var not_board1 = ~board1;
            var not_board2 = ~board2;

            var red = board0 & not_board1 & not_board2 & mask; // 1        100
            var blue = not_board0 & board1 & not_board2 & mask; // 2       010
            var green = board0 & board1 & not_board2 & mask; // 3       110
            var yellow = not_board0 & not_board1 & board2 & mask; // 4     001
            var purple = board0 & not_board1 & board2 & mask; // 5      101
            var heart = not_board0 & board1 & board2 & mask; // 6       011
            var ojama = board0 & board1 & board2 & mask; // 7        111

            var disappear = DisappearColor(red);
            disappear |= DisappearColor(blue);
            disappear |= DisappearColor(green);
            disappear |= DisappearColor(yellow);
            disappear |= DisappearColor(purple);

            var vanishingHeartAndOjama = Expand(disappear, heart | ojama);

            disappear |= vanishingHeartAndOjama;

            Board[0] &= ~disappear;
            Board[1] &= ~disappear;
            Board[2] &= ~disappear;

            return BitOperations.PopCount(disappear);
        }

        private static ulong DisappearColor(ulong red)
        {
            // redで試す
            var u = ShiftUp(red) & red;
            var d = ShiftDown(red) & red;
            var l = ShiftLeft(red) & red;
            var r = ShiftRight(red) & red;

            //Console.WriteLine(d.ToBinaryString());
            //Console.WriteLine(u.ToBinaryString());

            var u_and_d = u & d;
            var l_and_r = l & r;
            var u_or_d = u | d;
            var l_or_r = l | r;

            var three = (u_and_d & l_or_r) | (l_and_r & u_or_d);
            var two = u_and_d | l_and_r | (u_or_d & l_or_r);


            //Console.WriteLine(two.ToBinaryString());

            var two_d = ShiftDown(two) & two;
            two_d |= ShiftUp(two_d);

            //Console.WriteLine(two_d.ToBinaryString());

            var two_l = ShiftLeft(two) & two;
            two_l |= ShiftRight(two_l);
            var vanishing = three | two_d | two_l;

            //Console.WriteLine(vanishing.ToBinaryString());

            if (vanishing != 0)
            {
                vanishing = Expand(vanishing, red);
            }

            return vanishing;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ulong Expand(ulong vanishing, ulong mask)
        {
            var l = ShiftLeft(vanishing);
            var r = ShiftRight(vanishing);
            var u = ShiftUp(vanishing);
            var d = ShiftDown(vanishing);

            return (vanishing | l | r | u | d) & mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ulong ShiftLeft(ulong board) => board << height;
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ulong ShiftRight(ulong board) => board >> height;
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ulong ShiftUp(ulong board) => (board << 1) & 0b_1111110_1111110_1111110_1111110_1111110_1111110_1111110_1111110UL;
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ulong ShiftDown(ulong board) => (board >> 1) & 0b_0111111_0111111_0111111_0111111_0111111_0111111_0111111_0111111UL;

        public (int renasa, int deletedCount) CalcRensa(ulong delNew)
        {
            int rensa = 0;
            int deletedCount = BitOperations.PopCount(delNew);
            Delete(delNew);

            Fall3();

            bool fallAll = false;
            bool fall = true;
            for (; ; )
            {
                var deleted = DeleteConnected2();
                //Console.WriteLine(rensa);
                //Console.WriteLine(this.ToString());
                if (deleted > 0)
                {
                    fall = false;
                    deletedCount += deleted;
                    rensa++;
                }
                else if (fall is false)
                {
                    Fall3();
                    fall = true;
                    fallAll = false;
                }
                else if (fallAll is false)
                {
                    Fall3All();
                    fallAll = true;
                }
                else
                {
                    break;
                }
            }

            return (rensa, deletedCount);
        }

        public void Delete(ulong deletePositions)
        {
            deletePositions = ~deletePositions;
            for (int i = 0; i < Board.Length; i++)
            {
                Board[i] &= deletePositions;
            }
        }

        public override string ToString()
        {
            int[][] number = new int[height][];
            for (int i = 0; i < number.Length; i++)
            {
                number[i] = new int[width];
            }

            for (int i = 0; i < number.Length; i++)
            {
                for (int k = 0; k < number[i].Length; k++)
                {
                    number[i][k] = ToDropType(Board, 55 - 7 * k - i);
                }
            }

            static int ToDropType(ulong[] Board, int pos)
            {
                int res = 0;

                for (int i = 0; i < Board.Length; i++)
                {
                    int value = (int)((Board[i] >> pos) & 1) << i;
                    res |= value;
                }

                return res;
            }

            return number.Aggregate(new StringBuilder().AppendLine(), (sb, row) => sb.AppendLine(string.Join(' ', row))).ToString();
        }
    }

    public static class UInt64Extensions
    {
        public static string ToBinaryString(this ulong value)
        {
            return Convert.ToString((long)value, 2).PadLeft(64);
        }
    }
}