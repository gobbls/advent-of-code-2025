namespace aoclib;

public static class ReadInput {
	public const string INPUT_PATH = "./input.txt";
	public const string INPUT_PATH_TEST = "./test-input.txt";

	public static string[] GetInput(bool? isTest) {
		string path = isTest is null || isTest is false
			? INPUT_PATH
			: INPUT_PATH_TEST;

		try {
			string[] input = File.ReadAllLines(path);
			return input;
		} catch (FileNotFoundException e) {
			Console.WriteLine($"[ERROR] While trying to read file; Got error: {e}");
			Environment.Exit(2);
		}

		return new string[1] { "WAS NOT ABLE TO READ FILE" };
	}
}
