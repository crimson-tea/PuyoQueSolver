using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Windows.Forms;

namespace PuyoQueSolver
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private PuyoQueSolver _solver;

        private void Form1_Load(object sender, EventArgs e)
        {
            _solver = new PuyoQueSolver();
        }

        /// <summary>
        /// OPの実装で yield する。
        /// </summary>
        /// <param name="board"></param>
        private void SolveRoutine(int[][] board)
        {
            BoardPictureBox.BackgroundImage?.Dispose();
            BoardPictureBox.Image?.Dispose();
            BoardPictureBox.Image = null;
            BoardPictureBox.BackgroundImage = BoardToImage(board);
            Application.DoEvents();

            Stopwatch stopwatch = Stopwatch.StartNew();

            var bitBoard = new PuyoQueBitBoard();
            bitBoard.SetBoard(board);
            var (combinationBit, combo, delCount) = _solver.SolveBitBoard(bitBoard, _rule, (int)RouteLengthNumericUpDown.Value, Mode.Normal);
            stopwatch.Stop();

            ElapsedTextBox.Text = stopwatch.Elapsed.ToString();
            ComboTextBox.Text = combo.ToString();
            DelPuyosCountTextBox.Text = delCount.ToString();

            var combination = PuyoQueBitBoard.ToCombinationArray(combinationBit);
            BoardPictureBox.Image = FillDeletePuyoImage(combination);

            Debug.WriteLine($"コンボ: {combo}");
            Debug.WriteLine($"消えたぷよの数: {delCount}");
        }

        // Disposeしない。
        private readonly Brush[] _brushes = new Brush[] { Brushes.White, Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.Orange, Brushes.Purple, Brushes.Pink, Brushes.DarkGray };

        private static Bitmap FillDeletePuyoImage(int[] combination)
        {
            const int width = 8;
            const int height = 7;
            const int cellSize = 50;
            Bitmap bitmap = new(cellSize * width, cellSize * (height - 1) + cellSize / 2, PixelFormat.Format32bppArgb);

            using Graphics g = Graphics.FromImage(bitmap);

            foreach (var pos in combination)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(64, 0, 0, 0)),
                    new Rectangle(cellSize * (pos % width), cellSize * (pos / width) + cellSize / 2, cellSize, cellSize));
            }

            return bitmap;
        }

        private Image BoardToImage(int[][] board)
        {
            int width = 8;
            int height = 7;
            int cellSize = 50;

            Bitmap bitmap = new(cellSize * width, cellSize * (height - 1) + cellSize / 2, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.FillRectangle(Brushes.LightGray, new Rectangle(Point.Empty, bitmap.Size));

                FillTop(g, board[0]);
                Fill(g, board[1..]);

                void FillTop(Graphics g, int[] topRow)
                {
                    for (int k = 0; k < topRow.Length; k++)
                    {
                        g.FillRectangle(_brushes[topRow[k]], new Rectangle(cellSize * k, 0, cellSize, cellSize / 2));
                    }
                }

                void Fill(Graphics g, int[][] board)
                {
                    for (int i = 0; i < board.Length; i++)
                    {
                        for (int k = 0; k < board[i].Length; k++)
                        {
                            g.FillEllipse(_brushes[board[i][k]], new Rectangle(cellSize * k, cellSize * i + cellSize / 2, cellSize, cellSize));
                        }
                    }
                }
            }
            return bitmap;
        }

        private int[][] _board;

        private int _rule = 4;

        private void Set3Button_Click(object sender, EventArgs e)
        {
            var b = sender as Button;
            (_rule, b.Text) = _rule switch
            {
                3 => (4, "Connect 4"),
                4 => (3, "Connect 3"),
                _ => (4, "Connect 4"),
            };
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            if (_board is null)
            {
                return;
            }

            SolveRoutine(_board);
        }

        private void RandomBoardButton_Click(object sender, EventArgs e)
        {
            _board = PuyoQueBitBoard.GenerateRandomBoard(_solver, _rule, (int)ColorCountNumericUpDown.Value);
            if (_board is null)
            {
                MessageBox.Show("条件を変えて盤面の生成を再試行してください。");
                return;
            }

            BoardPictureBox.Image?.Dispose();
            BoardPictureBox.Image = null;
            BoardPictureBox.BackgroundImage?.Dispose();
            BoardPictureBox.BackgroundImage = BoardToImage(_board);
        }
    }
}
