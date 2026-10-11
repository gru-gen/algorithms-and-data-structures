using System.Runtime.ExceptionServices;

namespace Algorithms.Tests.Helpers;

/// <summary>
/// Runs a test body on its own thread with a 64 MB stack and a time limit.
///
/// Use it for tests on big inputs:
///   - the time limit catches a slow solution: O(n^2) on n = 10^5 does not finish in 3 seconds;
///   - the big stack lets deep recursion (a chain of 10^4..10^5 nested calls) work on every OS.
///     xUnit runs tests on normal threads, and a normal thread has a small stack: about 1 MB on Windows.
///     Such a recursion would crash the whole test process with "Stack overflow".
///
/// Example:
///     [Fact]
///     public void BigInputIsFast() => Limits.Run(() =>
///     {
///         int[] nums = ... 10^5 numbers ...;
///         Assert.Equal(expected, MySolution.Solve(nums));
///     });
/// </summary>
public static class Limits
{
    /// <summary>
    /// Stack size of the test thread: enough for about 10^5..10^6 nested calls.
    /// </summary>
    public const int StackSize = 64 * 1024 * 1024;

    /// <summary>
    /// Time limit used when the test does not give its own.
    /// </summary>
    public const int DefaultSeconds = 3;

    /// <summary>
    /// Runs body on a big-stack thread; fails the test if it takes more than the limit.
    /// </summary>
    public static void Run(Action action, int seconds = DefaultSeconds)
    {
        ExceptionDispatchInfo? error = null;
        var thread = new Thread(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                error = ExceptionDispatchInfo.Capture(ex);
            }
        }, StackSize);
        thread.IsBackground = true; // a test that never ends must not keep the process alive
        thread.Start();

        if (!thread.Join(TimeSpan.FromSeconds(seconds)))
            Assert.Fail($"Too slow: more than {seconds} s (slow algorithm or endless loop?)");

        error?.Throw(); // the test failed inside the thread: rethrow with the original stack trace
    }
}
