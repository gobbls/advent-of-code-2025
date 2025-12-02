using System.Linq;
using aoclib;

namespace day_02_p1;

public class Solution {
	public List<string> Input { get; private set; }
	public int Output { get; private set; }

	public Solution(bool? isTest) {
		Input = ReadInput.GetInput(isTest: isTest)[0].Split(',').ToList();
		run();
	}

	private void run() {
		List<int> sequences = new List<int>();
		
		foreach (string range in Input) {
			Console.WriteLine("\n\n\n\n----------------------------------------------------");
			Console.WriteLine($"[DEBUG] Getting sequences from: {range}");
			List<int> seq = getSequencesFromRange(range);
			sequences.AddRange(seq); // spread list onto another
		}

		foreach (int id in sequences) {
			Console.WriteLine($"\t[DEBUG] found id: {id}");
		}

		//Output = sequences.Sum();
	}

	private List<int> getSequencesFromRange(string range) {
		string[] ranges = range.Split('-');

		// convert the start and the end of the range to integers
		int start = int.Parse(ranges[0]);
		int end = int.Parse(ranges[1]);

		List<int> sequences = new List<int>();
		for (int i = start; i <= end; i++) {
			Console.WriteLine($"[DEBUG] Checking sequence: {i}");
			// filter out if the number length is odd
			bool notEven = i.ToString().Length % 2 > 0;
			if (notEven) {
				Console.WriteLine($"[DEBUG] Sequence {i} is NOT even!! Ignoring...");
			} else if (hasSequence(i)) {
				Console.WriteLine($"[DEBUG] Found sequence: {i} !!");
				sequences.Add(i);
			}
		}

		return sequences;
	}

	// look for sequences that matches the criteria:
	// 1. the ID is made up ONLY by repeating number sequences
	// 2. the ID does NOT have a leading "0" (zero)
	private bool hasSequence(int num) {
		string strNum = num.ToString();

		// return false immedieately if the number starts with zero
		if (strNum[0] == '0') {
			Console.WriteLine($"[DEBUG] Sequence begins with zero! Invalid!");
			return false;
		}

		// cut the number (string) in half, to avoid looking for sequences we will never find
		int halfStrLength = strNum.Length > 1 ? strNum.Substring(0, (strNum.Length / 2)).Length : 1;

		Console.WriteLine($"[DEBUG] Halved string length: {halfStrLength}");

		/* 
		 * cut the string in possible variations and match every chunk.
		 * return if all the cunks (items in the array) are the same.
		 */

		// variable chunk size until half (+1) is hit
		for (int chunkLen = 1; chunkLen <= halfStrLength; chunkLen++) {
			Console.WriteLine($"[DEBUG] trying chunkLength of: {chunkLen}");
			List<string> sequenceChunks = new List<string>();

			for (int j = 0; j < strNum.Length; j += chunkLen) {
				int length = Math.Min(chunkLen, strNum.Length - j);
				string strChunk = strNum.Substring(j, length);
				Console.WriteLine($"[DEBUG] Current chunk string: {strChunk}");
				sequenceChunks.Add(strChunk);
			}

			Console.Write("[DEBUG] checking chunks: ");
			foreach (string chunk in sequenceChunks) {
				Console.Write(chunk + ", ");
			}

			if (sequenceChunks.TrueForAll(chunk => chunk == sequenceChunks[0])) {
				return true;
			};
		}

		return false;
	}
}
