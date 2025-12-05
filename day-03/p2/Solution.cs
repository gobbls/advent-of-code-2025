using System.Linq;
using aoclib;

namespace day_03_p2;

public class Solution {
	public const int CHARS = 12;

	public List<string> Input { get; private set; }
	public long Output { get; private set; }

	public Solution(bool? isTest) {
		Input = ReadInput.GetInput(isTest: isTest).ToList();
		run();
	}

	private void run() {
		List<long> largestJoltages = new List<long>();

		foreach (string bank in Input) {
			long largest = getLargestJoltage(bank);
			largestJoltages.Add(largest);
		}

		Output = largestJoltages.Sum();
	}

	private long getLargestJoltage(string bank) {
		string shrinkSliced = bank;
		string collected = "";
		int sliceFrom = 0;

		for (int i = 0; i < CHARS; i++) {
			shrinkSliced = shrinkSliced.Substring(sliceFrom);

			int sliceTo = shrinkSliced.Length - (CHARS - collected.Length) + 1;
			string chunk = shrinkSliced.Substring(0, sliceTo);
			Largest largest = getLargestIntInStr(chunk);

			collected += largest.Integer;
			sliceFrom = largest.Index + 1;

			if (collected.Length == CHARS) break;
		}

		return Int64.Parse(collected);
	}

	private Largest getLargestIntInStr(string str) {
		Largest largest = new Largest ('0', 0);

		for (int i = 0; i < str.Length; i++) {
			char battery = str[i];

			if ((int)battery > largest.Integer) {
				largest.Integer = battery;
				largest.Index = i;
			}
		}

		return largest;
	}
}

public class Largest {
	public char Integer { get; set; }
	public int Index { get; set; }

	public Largest(char largest, int index) {
		Integer = largest;
		Index = index;
	}
}
