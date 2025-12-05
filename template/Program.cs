namespace template;

class Program
{
    static void Main(string[] args)
    {
		bool testing = args.Length > 0 && args.Contains("test");
		Console.WriteLine($"[DEBUG] running: {(testing ? "TEST INPUT" : "REAL INPUT")}");

		var solution = new Solution(isTest: testing);
		Console.WriteLine($"Solution: {solution.Output}");
    }
}
