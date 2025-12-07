Using System.Linq;
using aoclib;

namespace day_06_p1;

public class Solution {
	public List<string> Input { get; private set; }
	public long Output { get; private set; } = 0;

	private List<string[]> Problems = new List<string[]>();

	public Solution(bool? isTest) {
		Input = ReadInput.GetInput(isTest: isTest).ToList();
		run();
	}

	private void run() {
		formatInput();

		long math(long a, long b, char o) {
			if (o == '+') return a + b;
			if (o == '*') return a * b;
			return 0;
		}

		for (int i = 0; i < Problems[0].Length; i++) {
			long accum = 0;

			char op = Problems[Problems.Count - 1][i][0];

			// don't include the last row (since they're operators)
			for (int j = 0; j < Problems.Count - 2; j++) {
				long a = j == 0 ? Int64.Parse(Problems[j][i]) : accum;
				long b = Int64.Parse(Problems[j + 1][i]);

				accum = math(a, b, op);
			}

			Output += accum;
		}
	}

	private void formatInput() {
		foreach (string line in Input) {
			List<string> nums = new List<string>();
			string numStr = "";

			for (int i = 0; i < line.Length; i++) {
				char c = line[i];

				if (!Char.IsWhiteSpace(c)) {
					numStr += c;
				} else {
					if (numStr.Length == 0) continue;
					nums.Add(numStr);
					numStr = "";
				}

				if (i == line.Length - 1) {
					if (numStr.Length == 0) continue;
					nums.Add(numStr);
					numStr = "";
				}
			}

			if (nums.Count > 0) {
				Problems.Add(nums.ToArray());
			}
		}
	}
}
