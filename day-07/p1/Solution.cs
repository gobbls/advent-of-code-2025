using System.Linq;
using aoclib;

namespace day_07_p1;

public class Solution {
	public List<string> Input { get; private set; }
	public long Output { get; private set; } = 0;

	private const char C = '^';
	private const char S = 'S';

	public Solution(bool? isTest) {
		Input = ReadInput.GetInput(isTest: isTest).ToList();
		run();
	}

	private void run() {
		List<int> beamIndices = new List<int>();
		char c;

		foreach (string line in Input) {
			for (int i = 0; i < line.Length; i++) {
				c = line[i];

				if (c == S) {
					beamIndices.Add(i);
				} else if (c == C && beamIndices.Contains(i)) {
					// beam stops before splitting
					beamIndices.Remove(i);

					Output += 1;

					int left = i - 1;
					int right = i + 1;

					if (!beamIndices.Contains(left)) {
						beamIndices.Add(left);
					}

					if (!beamIndices.Contains(right)) {
						beamIndices.Add(right);
					}
				}
			}
		}
	}
}
