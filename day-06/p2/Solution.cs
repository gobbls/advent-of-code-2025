using System.Linq;
using aoclib;

namespace day_06_p2;

public class Solution {
	public List<string> Input { get; private set; }
	public long Output { get; private set; } = 0;

	private List<string[]> ProblemsWithoutWhitespaces = new List<string[]>();
	private List<string[]> ProblemsPreservedWhitespaces = new List<string[]>();

	public Solution(bool? isTest) {
		Input = ReadInput.GetInput(isTest: isTest).ToList();

		formatInput();
		run();
	}

	private void run() {
		for (int i = 0; i < ProblemsPreservedWhitespaces.Count; i++) {

			int[] correctedNumberSeq = correctNumberSeq(ProblemsPreservedWhitespaces[i]);
			long result = 0;
			char op;

			for (int j = 0; j < correctedNumberSeq.Length - 1; j++) {
				op = ProblemsWithoutWhitespaces[ProblemsWithoutWhitespaces.Count - 1][i][0];
				result = math(j == 0 ? correctedNumberSeq[j] : result, correctedNumberSeq[j + 1], op);
			}

			Output += result;
		}
	}

	private long math(long a, long b, char o) {
		if (o == '+') return a + b;
		if (o == '*') return a * b;

		return 0;
	}

	private int[] correctNumberSeq(string[] input) {
		int columns = input[0].Length - 1;

		List<int> inputs = new List<int>();

		// go backwards in the number sequence
		string sorted = "";
		char c;

		for (int col = columns; col >= 0; col--) {
			for (int row = 0; row < input.Length; row++) {
				c = input[row][col];
				if (Char.IsWhiteSpace(c)) continue;
				sorted += c;
			}

			if (sorted.Length > 0) inputs.Add(int.Parse(sorted));

			sorted = "";
		}

		return inputs.ToArray();
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
				ProblemsWithoutWhitespaces.Add(nums.ToArray());
			}
		}

		splitProblemsPreserveWhitespaces();
	}

	private void splitProblemsPreserveWhitespaces() {
		int breakpoint = 0;

		for (int col = 0; col < ProblemsWithoutWhitespaces[0].Length; col++) {

			List<string> rows = new List<string>();

			for (int row = 0; row < ProblemsWithoutWhitespaces.Count - 1; row++) {
				rows.Add(ProblemsWithoutWhitespaces[row][col]);
			}

			int longestStr = rows.OrderByDescending(s => s.Length).FirstOrDefault().Length;
			List<string> rowsWithWhitespace = new List<string>();

			for (int row = 0; row < ProblemsWithoutWhitespaces.Count - 1; row++) {
				string chunk = Input[row].Substring(breakpoint, longestStr);
				rowsWithWhitespace.Add(chunk);
			}

			ProblemsPreservedWhitespaces.Add(rowsWithWhitespace.ToArray());
			breakpoint += longestStr + 1;
		}
	}
}
