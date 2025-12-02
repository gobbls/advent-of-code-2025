using System.Linq;
using aoclib;

namespace day_01_p1;

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

			// the dial goes from 0 to 99, do modulo 100 to compansate for the zero
			dialPos = direction == 'R'
				? (dialPos + distance) % 100  // if the dial rotates R
				: (dialPos - distance) % 100; // if the dial rotates L

			if (dialPos == 0) {
				Output++;
			}
		}
	}
}
