using MessagePack;
using System.Numerics;

namespace PuyoQueSolver
{
    // public enum DropType { None = 0, Red, Blue, Green, Yellow, Purple, Heart, Ojama }

    public enum Mode { Normal, DairensaChance }

    public class PuyoQueSolver
    {
        public PuyoQueSolver()
        {
            _combinationsUInt64 = InitCombinations();
            MaxCombinationLength = InitMaxCombinationLength();

            int InitMaxCombinationLength() => BitOperations.PopCount(_combinationsUInt64.Last());
        }


        public int MaxCombinationLength { get; init; }

        private readonly ulong[] _combinationsUInt64;

        /// <summary>
        /// なぞり方を読み込みます
        /// </summary>
        private static ulong[] InitCombinations()
        {
            const string pathFormat = @".\patterns_1-{0}.msgpack";

            var path = SearchPath();
            if (path is null)
            {
                throw new FileNotFoundException("patterns_1-x.msgpack をリポジトリのReleaseからダウンロードしてこのアプリケーションの実行ファイルと同じ場所に入れてください。");
            }

            string? SearchPath()
            {
                return Enumerable.Range(5, 10).Reverse().Select(x => string.Format(pathFormat, x)).FirstOrDefault(x => File.Exists(x));
            }

            var bytes = File.ReadAllBytes(path);
            var combinationsUInt64 = MessagePackSerializer.Deserialize<ulong[]>(bytes);

            return combinationsUInt64;
        }

        private readonly ulong[][] bitCombinations = new ulong[13][];
        private readonly ulong _hideUnOperableMask = ~0b_11111111UL;

        ulong[] GetDeleteCombination(int delPuyoLimit)
        {
            if (bitCombinations[delPuyoLimit] is null)
            {
                bitCombinations[delPuyoLimit] = _combinationsUInt64.Where(x => BitOperations.PopCount(x) <= delPuyoLimit).ToArray();
            }

            return bitCombinations[delPuyoLimit];
        }

        public (ulong conbination, int combo, int delCount) SolveBitBoard(PuyoQueBitBoard board, int rule = 4, int maxDelLength = 5, Mode mode = Mode.Normal)
        {
            if (mode == Mode.DairensaChance)
            {
                maxDelLength = 5;
            }
            int maxCombo = 0;
            int delCount = 0;

            ulong combination = 0;
            ulong existPuyo = board.MergeBoard;
            ulong operablePuyo = board.MergeBoard & _hideUnOperableMask;
            int existPuyoCount = BitOperations.PopCount(existPuyo);

            var lockObj = new object();

            ParallelOptions option = new()
            {
                MaxDegreeOfParallelism = 20
            };

            Parallel.ForEach(GetDeleteCombination(maxDelLength), option, del =>
            {
                if (mode == Mode.DairensaChance)
                {
                    // 盤面上に存在しないぷよを消すなぞり方はskip
                    // if (BitOperations.PopCount(existPuyo & delPattern) != del.Length)
                    if ((operablePuyo & del) != del)
                    {
                        return;
                    }
                }

                // おじゃまぷよはなぞって消すことができない
                if ((board.Board[6] & del) > 0)
                {
                    return;
                }

                var (combo, deleted) = CalcComboBitBoard(board, del, rule);
                if (maxCombo < combo)
                {
                    lock (lockObj)
                    {
                        combination = del;
                        maxCombo = combo;
                        delCount = deleted;
                    }
                }
                else if (maxCombo == combo && delCount < deleted)
                {
                    lock (lockObj)
                    {
                        combination = del;
                        delCount = deleted;
                    }
                }
            });

            return (combination, maxCombo, delCount);
        }

        /// <summary>
        /// BitBoardを使用し、コンボ数を計算します。
        /// </summary>
        /// <param name="board"></param>
        /// <param name="del"></param>
        /// <returns></returns>
        public static (int combo, int deletedCount) CalcComboBitBoard(PuyoQueBitBoard board, ulong del, int rule)
        {
            // クローンする
            var b = board.Clone();

            int combo = 0;
            int delCount = BitOperations.PopCount(del);// del.Length;

            b.Remove(del);
            b.FallAvx2();
            bool fell1 = false;
            bool fell2 = true;
            for (; ; )
            {
                if (b.CalcDelete(out int deleted, rule))
                {
                    fell2 = false;
                    delCount += deleted;
                    combo++;
                }
                else if (fell2 is false)
                {
                    b.FallAvx2();
                    fell2 = true;
                    fell1 = false;
                }
                else if (fell1 is false)
                {
                    b.FallAllAvx2();
                    fell1 = true;
                }
                else
                {
                    break;
                }
            }
            return (combo, delCount);
        }

        /// <summary>
        /// 消えなくなるところまで消す。
        /// ランダム盤面生成用。
        /// </summary>
        /// <param name="board"></param>
        /// <param name="del"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static bool RemoveConnect(PuyoQueBitBoard board, int rule)
        {
            board.FallAvx2();
            bool deleted = false;

            for (; ; )
            {
                if (board.CalcDelete(out int _, rule))
                {
                    deleted = true;
                    board.FallAllAvx2();
                }
                else
                {
                    break;
                }
            }
            return deleted;
        }
    }
}
