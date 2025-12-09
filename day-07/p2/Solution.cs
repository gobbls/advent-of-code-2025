using System.Linq;
using aoclib;

namespace day_07_p2;

public class Solution {
	public string[] Input { get; private set; }
	public long Output { get; private set; } = 0;

	private const char C = '^';
	private const char S = 'S';

	public Solution(bool? isTest) {
		Input = ReadInput.GetInput(isTest: isTest);
		run();
	}

	private void run() {
		void traverse(int x, int y) {
			//Console.WriteLine($"[DEBUG] x = {x} y = {y}, Output = {Output}\r");

			if (y == Input.Length - 1) {
				Output++;
				return;
			} else {
				if (Input[y][x] == C) {
					traverse(x - 1, y + 1);
					traverse(x + 1, y + 1);
				} else {
					traverse(x, y + 1);
				}
			}
		}

		traverse(Input[0].IndexOf(S), 1);
	}
}
