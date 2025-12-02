using System.Linq;
using aoclib;

namespace day_01_p2;

public class Solution {
	public List<string> Input { get; private set; }
	public int Output { get; private set; } = 0;

	public Solution(bool? isTest) {
		Input = ReadInput.GetInput(isTest: isTest).ToList();
		run();
	}

	private void run() {
		int dialPos = 50; // the start position of the dial
		foreach (string rotation in Input) {

			char direction = rotation[0]; // get the R or L char from the string
			int distance = int.Parse(rotation.Substring(1)); // get the int from the string

			int curDialPos;
			for (int i = 0; i <= distance; i++) {

				curDialPos = direction == 'R'
					? (dialPos + i) % 100  // if the dial rotates R
					: (dialPos - i) % 100; // if the dial rotates L

				// adjust if the nuber is negative
				curDialPos = curDialPos < 0 ? curDialPos + 100 : curDialPos;

				// Make sure to not count the start position if the prev iteration ended at position 0
				if (curDialPos == 0 && i != 0) {
					Output++;
				}
			}

			dialPos = direction == 'R'
				? (dialPos + distance) % 100  // if the dial rotates R
				: (dialPos - distance) % 100; // if the dial rotates L
		}
	}
}
