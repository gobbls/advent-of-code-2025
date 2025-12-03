using System.Linq;
using aoclib;

namespace day_02_p2;

public class Solution {
	public List<string> Input { get; private set; }
	public long Output { get; private set; }

	public Solution(bool? isTest) {
		Input = ReadInput.GetInput(isTest: isTest)[0].Split(',').ToList();
		run();
	}

	private void run() {
		List<long> sequences = new List<long>();
		
		foreach (string range in Input) {
			List<long> seq = getSequencesFromRange(range);
			sequences.AddRange(seq); // spread list onto another
		}

		Output = sequences.Sum();
	}

	private List<long> getSequencesFromRange(string range) {
		string[] ranges = range.Split('-');
		long start = Int64.Parse(ranges[0]);
		long end = Int64.Parse(ranges[1]);

		List<long> sequences = new List<long>();

		for (long i = start; i <= end; i++) {
			if (hasSequence(i)) {
				sequences.Add(i);
			}
		}

		return sequences;
	}

	// look for sequences that matches the criteria:
	// 1. the ID is made up ONLY by repeating number sequences
	// 2. the ID does NOT have a leading "0" (zero)
	private bool hasSequence(long num) {
		string strNum = num.ToString();

		if (strNum[0] == '0') return false;

		/* 
		 * cut the string in possible variations and match every chunk.
		 * return if all the cunks (items in the array) are the same.
		 */

		for (int chunkLen = 1; chunkLen <= strNum.Length / 2; chunkLen++) {
			List<string> sequenceChunks = new List<string>();

			// skip if the string can't be evenly split into chunks
			if (strNum.Length % chunkLen > 0 || strNum.Length == chunkLen) continue;

			for (int j = 0; j < strNum.Length; j += chunkLen) {
				string strChunk = strNum.Substring(j, chunkLen);
				sequenceChunks.Add(strChunk);
			}

			if (sequenceChunks.TrueForAll(chunk => chunk == sequenceChunks[0])) {
				return true;
			};
		}

		return false;
	}
}
