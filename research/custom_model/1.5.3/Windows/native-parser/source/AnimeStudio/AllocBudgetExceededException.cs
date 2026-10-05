using System;

namespace AnimeStudio
{
    /// <summary>
    /// 当单个文件解析的线程分配量超过 AssetsManager.PerFileAllocBudgetBytes 时抛出。
    /// 此异常**不会被 LoadGameBlockFile 的 catch-all 吞掉**——它会穿透到调用方，
    /// 让 Program.cs 能在 LoadFiles() 内部就中断，而不是等解析完 15 GB 后才发现。
    /// </summary>
    public class AllocBudgetExceededException : Exception
    {
        public long AllocDeltaBytes { get; }
        public long BudgetBytes { get; }
        public string FileName { get; }

        public AllocBudgetExceededException(long allocDelta, long budget, string fileName)
            : base($"Alloc budget exceeded: {allocDelta / 1024 / 1024}MB > {budget / 1024 / 1024}MB"
                   + $" (file: {fileName}, data likely corrupted/wrong-key)")
        {
            AllocDeltaBytes = allocDelta;
            BudgetBytes = budget;
            FileName = fileName;
        }
    }
}
