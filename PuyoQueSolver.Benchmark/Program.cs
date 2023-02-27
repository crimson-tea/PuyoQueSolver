using BenchmarkDotNet.Running;

namespace PuyoQueSolver.Benchmark
{
    class Program
    {
        static void Main()
        {
            //_ = BenchmarkRunner.Run<BitBoardBenchmark>();
            //_ = BenchmarkRunner.Run<SolverBenchmark>();
            _ = BenchmarkRunner.Run<Avx2Benchmark>();
            //_ = BenchmarkRunner.Run<ConstructBitBoard>();
            //_ = BenchmarkRunner.Run<RemoveBenchmark>();
        }
    }
}
