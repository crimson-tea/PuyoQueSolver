using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace PuyoQueSolver
{
    public class PuyoQueBitBoard
    {
        private readonly static byte _width = 8;

        public ulong[] Board = new ulong[8];

        public ulong MergeBoard => Board[0] | Board[1] | Board[2] | Board[3] | Board[4] | Board[5] | Board[6];

        public void SetBoard(int[][] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int k = 0; k < board[i].Length; k++)
                {
                    if (board[i][k] - 1 >= 0)
                    {
                        Board[board[i][k] - 1] ^= 1UL << (i * _width + k);
                    }
                }
            }
        }

        private static readonly ulong[] _shiftOperableMasks;
        private static readonly ulong[] _shiftAllMasks = new ulong[65];

        private static readonly ulong[] _colOperableMasks = new ulong[_width];
        private static readonly ulong[] _colAllMasks = new ulong[_width];

        private static readonly int _operaleHeight = 6;
        private static readonly int _height = 7;

        // 操作可能範囲内でつながっているかを判定するためのmask
        // 盤面全体に掛けるmaskとして作成するため Length == 56 である
        private static readonly ulong[] _operableMasks;

        static PuyoQueBitBoard()
        {
            _shiftOperableMasks = InitShiftMasks();
            _shiftAllMasks = InitShiftAllMasks();

            _colOperableMasks = InitColMasks();
            _colAllMasks = InitColAllMasks();

            _operableMasks = InitIndexToOperableMask();

            static ulong[] InitShiftMasks()
            {
                var shiftMasks = new ulong[65];
                for (int i = 16; i < shiftMasks.Length; i++)
                {
                    ulong m = 0;
                    for (int k = i - 8; k > 7; k -= 8)
                    {
                        m |= 1UL << k;
                    }
                    shiftMasks[i] = m;
                }

                shiftMasks[64] = 0;
                return shiftMasks;
            }

            static ulong[] InitShiftAllMasks()
            {
                var shiftAllMasks = new ulong[65];

                for (int i = 8; i < shiftAllMasks.Length; i++)
                {
                    ulong m = 0;
                    for (int k = i - 8; k >= 0; k -= 8)
                    {
                        m |= 1UL << k;
                    }
                    shiftAllMasks[i] = m;
                }

                shiftAllMasks[64] = 0;

                return shiftAllMasks;
            }

            static ulong[] InitColMasks()
            {
                var colMasks = new ulong[_width];
                for (int i = 0; i < colMasks.Length; i++)
                {
                    colMasks[i] = 0b_00000001_00000001_00000001_00000001_00000001_00000000_00000000UL << i;
                }
                return colMasks;
            }

            static ulong[] InitColAllMasks()
            {
                var colAllMasks = new ulong[_width];
                for (int i = 0; i < colAllMasks.Length; i++)
                {
                    colAllMasks[i] = 0b_00000001_00000001_00000001_00000001_00000001_00000001_00000000UL << i;
                }
                return colAllMasks;
            }
        }

        /// <summary>
        /// 操作可能範囲でぷよのつながりを判定するためのマスクを作成します。
        /// </summary>
        private static ulong[] InitIndexToOperableMask()
        {
            var operableMasks = new ulong[_width * _height];
            int[] direction4 = new int[] { -8, -1, 1, 8, };

            for (int i = 8; i < operableMasks.Length; i++)
            {
                ulong mask = 0;

                foreach (var direction in direction4)
                {
                    int x = i % _width;
                    int y = i / _width;


                    int DeltaIndexToXVector(int direction) => direction switch
                    {
                        -8 => 0,
                        -1 => -1,
                        1 => 1,
                        8 => 0,
                        _ => throw new ArgumentNullException()
                    };

                    int DeltaIndexToYVector(int direction) => direction switch
                    {
                        -8 => -1,
                        -1 => 0,
                        1 => 0,
                        8 => 1,
                        _ => throw new ArgumentNullException()
                    };

                    int xx = x + DeltaIndexToXVector(direction);
                    int yy = y + DeltaIndexToYVector(direction);


                    // 水平方向にはみ出ていないか。
                    if ((0 <= xx && xx < _width) is false)
                    {
                        continue;
                    }

                    // 垂直方向にはみ出ていないか。
                    if ((0 <= yy && yy < _height) is false)
                    {
                        continue;
                    }

                    int shift = i + direction;

                    // shiftにマイナスの値を入れると期待する動作にならないので左シフトと右シフトを使い分ける。
                    mask ^= shift >= 0
                        ? 1UL << shift
                        : 1UL >> Math.Abs(shift);
                }

                mask &= 0b_11111111__11111111__11111111__11111111__11111111__11111111__11111111__00000000UL;

                Debug.WriteLine($"i={i} {Convert.ToString((long)mask, 2).PadLeft(64, '0')}");

                operableMasks[i] = mask;
            }

            return operableMasks;
        }

        /// <summary>
        /// ドロップを下に詰める処理。
        /// ネクストぷよは含まない。
        /// Avx2を使用する。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]

        public unsafe void FallAvx2()
        {
            var exist = MergeBoard;
            var notExist = ~exist;

            fixed (ulong* pBoard = Board)
            {
                for (int w = 0; w < _colOperableMasks.Length; w++)
                {
                    ulong empty = notExist & _colOperableMasks[w];

                    int count = BitOperations.PopCount(empty);
                    for (int i = 0; i < count; i++)
                    {
                        int shift = BitOperations.TrailingZeroCount(empty);
                        // 以下の処理をSIMDでする。
                        // 4色のボードをまとめて処理する。

                        // color: 0-3
                        var boardVector = Avx2.LoadVector256(pBoard);

                        var shiftMaskVector = Vector256.Create(_shiftOperableMasks[shift]);

                        // 移動するドロップ
                        var toMoveDropVector = Avx2.And(boardVector, shiftMaskVector);

                        // ドロップの削除
                        boardVector = Avx2.Xor(boardVector, toMoveDropVector);

                        // ドロップの移動
                        toMoveDropVector = Avx2.ShiftLeftLogical(toMoveDropVector, _width);

                        // 移動したドロップを反映する
                        boardVector = Avx2.Xor(boardVector, toMoveDropVector);

                        // 元の配列に反映
                        Avx2.Store(pBoard, boardVector);


                        // color: 4-7
                        boardVector = Avx2.LoadVector256(pBoard + 4);

                        // 移動するドロップ
                        toMoveDropVector = Avx2.And(boardVector, shiftMaskVector);

                        // ドロップの削除
                        boardVector = Avx2.Xor(boardVector, toMoveDropVector);

                        // ドロップの移動
                        toMoveDropVector = Avx2.ShiftLeftLogical(toMoveDropVector, _width);

                        // 移動したドロップを反映する
                        boardVector = Avx2.Xor(boardVector, toMoveDropVector);

                        Avx2.Store(pBoard + 4, boardVector);

                        // 処理済みの空白を削除
                        empty ^= 1UL << shift;
                    }
                }
            }
        }

        /// <summary>
        /// ドロップを下に詰める処理。
        /// ネクストぷよは含まない。
        /// Avx2を使用する。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public unsafe void FallAllAvx2()
        {
            var exist = MergeBoard;
            var notExist = ~exist;


            fixed (ulong* pBoard = Board)
            fixed (ulong* pShiftMasks = _shiftAllMasks)
            {
                for (int w = 0; w < _colAllMasks.Length; w++)
                {
                    ulong empty = notExist & _colAllMasks[w];

                    int count = BitOperations.PopCount(empty);
                    for (int i = 0; i < count; i++)
                    {
                        int shift = BitOperations.TrailingZeroCount(empty);
                        // 以下の処理をSIMDでする。
                        // 各色のボードを最大4つまとめて処理する。
                        // 0-3
                        var boardVector = Avx2.LoadVector256(pBoard);
                        // Console.WriteLine("0-3");
                        //Console.WriteLine(boardVector);

                        var shiftMaskVector = Vector256.Create(pShiftMasks[shift]);

                        // 移動するドロップ
                        var toMoveDropVector = Avx2.And(boardVector, shiftMaskVector);

                        // ドロップの削除
                        boardVector = Avx2.Xor(boardVector, toMoveDropVector);

                        // ドロップの移動
                        toMoveDropVector = Avx2.ShiftLeftLogical(toMoveDropVector, _width);

                        // 移動したドロップを反映する
                        boardVector = Avx2.Xor(boardVector, toMoveDropVector);

                        Avx2.Store(pBoard, boardVector);


                        // 4-8
                        boardVector = Avx2.LoadVector256(pBoard + 4);
                        //Console.WriteLine("4-7");
                        //Console.WriteLine(boardVector);

                        // 移動するドロップ
                        toMoveDropVector = Avx2.And(boardVector, shiftMaskVector);

                        // ドロップの削除
                        boardVector = Avx2.Xor(boardVector, toMoveDropVector);

                        // ドロップの移動
                        toMoveDropVector = Avx2.ShiftLeftLogical(toMoveDropVector, _width);

                        // 移動したドロップを反映する
                        boardVector = Avx2.Xor(boardVector, toMoveDropVector);

                        Avx2.Store(pBoard + 4, boardVector);

                        // 処理済みの空白を削除
                        empty ^= 1UL << shift;
                    }
                }
            }
        }

        public PuyoQueBitBoard Clone()
        {
            var clone = new PuyoQueBitBoard();
            Board.CopyTo(clone.Board, 0);
            return clone;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void Remove(ulong del)
        {
            for (int i = 0; i < Board.Length; i++)
            {
                Board[i] &= ~del;
            }
        }

        /// <summary>
        /// 操作可能な場所のみ取り出すためのマスク
        /// </summary>
        private readonly ulong _extractOperableMask = ~0b_11111111UL;

        /// <summary>
        /// 消えるぷよがあるか判定する。
        /// 消えるぷよの数がoutで返される。
        /// </summary>
        /// <param name="board"></param>
        /// <param name="deletedCount"></param>
        /// <returns></returns>
        public bool CalcDelete(out int deletedCount, int rule)
        {
            var bitBoard = this;

            bool res = false;
            var board = bitBoard.Board;
            deletedCount = 0;

            ulong delAll = 0;


            // ハートとおじゃまはつながっても消えないため除外する。
            for (int b = 0; b < board.Length - 3; b++)
            {
                ulong restore = 0;
                int count = BitOperations.PopCount(board[b] & _extractOperableMask);

                while (count > 0)
                {
                    int pos = BitOperations.TrailingZeroCount(board[b] & _extractOperableMask);
                    ulong del = 0;

                    // 隣接しているぷよは隣接数にかかわらず消される
                    // 復元する必要があるので消すつもりのないぷよは変数に保存しておく
                    RemoveConnectedSameColor(ref board[b], pos, ref del);

                    int deleted = BitOperations.PopCount(del);
                    if (deleted >= rule)
                    {
                        res = true;
                        delAll |= del;
                        deletedCount += deleted;
                    }
                    else
                    {
                        restore |= del;
                    }

                    count -= deleted;
                    // Debug.WriteLine(bitBoard.ToString());
                    // Debug.WriteLine(Convert.ToString((long)board[b], 2).PadLeft(64, '0'));
                }

                board[b] |= restore;
            }


            ulong delMask = CreateDelMask(delAll);

            var five = board[5] & delMask;
            deletedCount += BitOperations.PopCount(five);
            board[5] ^= five;
            var six = board[6] & delMask;
            deletedCount += BitOperations.PopCount(six);
            board[6] ^= six;

            return res;
        }


        /// <summary>
        /// ハートやおじゃまなどの巻き込まれて消えるものを巻き込む範囲を作成します。
        /// </summary>
        /// <param name="delAll"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private ulong CreateDelMask(ulong delAll)
        {
            // 盤面で考えて右、左（シフトは逆方向）
            const ulong shiftMaskRight = ~0b_00000001_00000001_00000001_00000001_00000001_00000001_00000001_11111111UL;
            const ulong shiftMaskLeft = ~0b_10000000_10000000_10000000_10000000_10000000_10000000_10000000_11111111UL;
            ulong mask = 0;
            mask |= (delAll >> 1) & shiftMaskLeft;
            mask |= (delAll << 1) & shiftMaskRight;
            mask |= (delAll >> 8);
            mask |= (delAll << 8);
            return mask & _extractOperableMask;
        }

        /// <summary>
        /// 縦横につながっているぷよを再帰的に検出する。
        /// 隣接するぷよは全て消える。
        /// 復元する必要がある場合はdelを利用してください。
        /// </summary>
        /// <param name="oneColorBoard"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="drop"></param>
        /// <param name="del"></param>
        /// <param name="heartCount"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void RemoveConnectedSameColor(ref ulong oneColorBoard, int pos, ref ulong del)
        {
            ulong posBit = 1UL << pos;

            del |= posBit;

            oneColorBoard ^= posBit;
            ulong connect = oneColorBoard & _operableMasks[pos];

            //Debug.WriteLine(Convert.ToString((long)connect, 2));
            //Debug.WriteLine(Convert.ToString((long)_operableMasks[pos], 2));
            while (connect != 0)
            {
                int shift = BitOperations.TrailingZeroCount(connect);
                RemoveConnectedSameColor(ref oneColorBoard, shift, ref del);
                connect ^= 1UL << shift;
                connect &= (~del);
            }
        }

        /// <summary>
        /// ビット表現を二次元配列表現にします。
        /// </summary>
        /// <param name="bitBoard"></param>
        /// <returns></returns>
        public static int[][] BitBoardToArray2D(PuyoQueBitBoard bitBoard)
        {
            var board = bitBoard.Board;
            int[][] array = new int[_height][];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new int[_width];
            }

            for (int b = 0; b < board.Length; b++)
            {
                for (int i = 0; i < _width * _height; i++)
                {
                    if ((board[b] & (1UL << i)) > 0)
                    {
                        array[i / _width][i % _width] = b + 1;
                    }
                }
            }
            return array;
        }

        public override string ToString()
        {
            var board = BitBoardToArray2D(this);
            var builder = board.Aggregate(new StringBuilder().AppendLine(), (builder, row) => builder.AppendLine(string.Join(' ', row)));
            return builder.ToString();
        }

        public static int[][]? GenerateRandomBoard(PuyoQueSolver solver, int rule, int colorCount)
        {
            int max = colorCount + 1;

            int[][] RandomBoardArray() => Enumerable.Repeat(0, _height).Select(x => Enumerable.Repeat(0, _width).Select(x => Random.Shared.Next(1, max)).ToArray()).ToArray();
            var bitBoard = new PuyoQueBitBoard();
            bitBoard.SetBoard(RandomBoardArray());

            int retryCount = 0;
            while (retryCount < 100000)
            {
                retryCount++;
                var deleted = PuyoQueSolver.RemoveConnect(bitBoard, rule);
                if (deleted is false)
                {
                    // DebugOutputBitBoard(bitBoard);
                    return BitBoardToArray2D(bitBoard);
                }

                int[][] additional = Enumerable.Repeat(0, _height).Select(x => Enumerable.Repeat(0, _width).ToArray()).ToArray();

                for (int i = 0; i < _width * _height; i++)
                {
                    // 何もないところをランダムなドロップで埋める
                    if ((bitBoard.MergeBoard & (1UL << i)) == 0)
                    {
                        additional[i / _width][i % _width] = Random.Shared.Next(1, max);
                    }
                }

                bitBoard.SetBoard(additional);
            }

            static void DebugOutputBitBoard(PuyoQueBitBoard bitBoard)
            {
                Debug.WriteLine(bitBoard.ToString());

                foreach (var color in bitBoard.Board)
                {
                    Debug.WriteLine(Convert.ToString((long)color, 2).PadLeft(56));
                }
            }

            return null;
        }
    }
}
