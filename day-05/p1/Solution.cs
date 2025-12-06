using System.Linq;
using aoclib;

namespace day_05_p1;

public class Solution {
	public List<string> Input { get; private set; }
	public int Output { get; private set; } = 0;

	public List<long> LeftRanges = new List<long>();
	public List<long> RightRanges = new List<long>();
	public List<long> IDs = new List<long>();

	public Solution(bool? isTest) {
		Input = ReadInput.GetInput(isTest: isTest).ToList();
		run();
	}

	private void run() {
		formatInput();

		// match the IDs with the ranges, same index at a time
		foreach (long id in IDs) {
			for (int i = 0; i < LeftRanges.Count; i++) {
				if (id >= LeftRanges[i] && id <= RightRanges[i]) {
					Console.WriteLine($"[DEBUG] Valid! {id}");
					Output += 1;
					break;
				}
			}
		}
	}

	// split the input between ranges and IDs
	private void formatInput() {
		foreach (string line in Input) {
			if (line.Contains("-")) {
				string[] split = line.Split("-");

				LeftRanges.Add(Int64.Parse(split[0]));
				RightRanges.Add(Int64.Parse(split[1]));

			} else if (!string.IsNullOrWhiteSpace(line)) {
				IDs.Add(Int64.Parse(line));
			}
		}
	}
}
