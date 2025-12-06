using System.Linq;
using aoclib;

namespace day_05_p2;

public class Solution {
	public long Output { get; private set; } = 0;

	private List<string> Input;

	private List<long> LeftRanges = new List<long>();
	private List<long> RightRanges = new List<long>();

	// to avoid seeking bigger / smaller ranges for already processed ranges
	private List<int> utilizedRangeIndices = new List<int>();

	public Solution(bool? isTest) {
		Input = ReadInput.GetInput(isTest: isTest).ToList();
		run();
	}

	private void run() {
		formatInput();

		for (int i = 0; i < LeftRanges.Count; i++) {
			// ignore if the range is processed
			if (utilizedRangeIndices.Contains(i)) continue;

			setAmountOfUniqueIDs(LeftRanges[i], RightRanges[i], i);
		}
	}

	// find the smallest left range and the largest right range within an ID scope
	private void setAmountOfUniqueIDs(long left, long right, int index) {
		long smallestStartRange = left;
		long largestEndRange = right;

		// if a new overlapping range is found, that range might have another
		// overlapping range. Keep seeking until there are noe more overlapping ranges
		// while ignoring already processed ranges (pr indices).
		void recurToLargestRangeDiff(long left, long right) {
			for (int i = 0; i < LeftRanges.Count; i++) {
				long iterLeft = LeftRanges[i];
				long iterRight = RightRanges[i];

				if (i == index || right < iterLeft || left > iterRight ||
						utilizedRangeIndices.Contains(i)) continue;

				if (overlapping(left, iterLeft, right, iterRight) != "noOverlap") {
					smallestStartRange = Math.Min(Math.Min(left, iterLeft), smallestStartRange);
					largestEndRange = Math.Max(right, iterRight);

					utilizedRangeIndices.Add(i);
					recurToLargestRangeDiff(smallestStartRange, largestEndRange);
				}
			}
		}
		recurToLargestRangeDiff(left, right);

		utilizedRangeIndices.Add(index);

		// compansate for the deduction with + 1
		Output += (largestEndRange - smallestStartRange) + 1;
	}

	private string overlapping(long left, long iterLeft, long right, long iterRight) {
		if (left <= iterLeft && right <= iterRight && right >= iterLeft) return "lowerEndOverlapp";
		if (right >= iterRight && left >= iterLeft && left <= iterRight) return "higherEndOverlapp";
		if (left >= iterLeft && right <= iterRight) return "fitsInsideIterRange";
		if (iterLeft >= left && iterRight <= right) return "iterRangeFitsInside";

		return "noOverlap";
	}

	// Filter out none-range lines
	private void formatInput() {
		foreach (string line in Input) {
			if (line.Contains("-")) {
				string[] split = line.Split("-");

				LeftRanges.Add(Int64.Parse(split[0]));
				RightRanges.Add(Int64.Parse(split[1]));
			} else {
				break;
			}
		}
	}
}
