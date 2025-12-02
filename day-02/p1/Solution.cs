using System.Linq;
using aoclib;

namespace day_02_p1;

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

		// convert the start and the end of the range to integers
		long start = Int64.Parse(ranges[0]);
		long end = Int64.Parse(ranges[1]);

		List<long> sequences = new List<long>();

		for (long i = start; i <= end; i++) {

			string istr = i.ToString();
			string p1 = istr.Substring(0, istr.Length / 2);
			string p2 = istr.Substring(istr.Length / 2);

			if (istr.Length % 2 > 0 || p1[0] == '0' || p2[0] == '0') continue;

			if (p1 == p2) sequences.Add(i);
		}

		return sequences;
	}
}
