using System.Linq;
using aoclib;

namespace day_03_p1;

public class LargestWithIndex
{
	public char Largest { get; set; }
	public int Index { get; set; }

	public LargestWithIndex(char largest, int index) {
		Largest = largest;
		Index = index;
	}
}

public class Solution {
	public List<string> Input { get; private set; }
	public long Output { get; private set; }

	public Solution(bool? isTest) {
		Input = ReadInput.GetInput(isTest: isTest).ToList();
		run();
	}

	/**
	 * get the 'joltage' of each battery array.
	 * TASK: find the larges combination of two numbers
	 * without re-arranging the numbers
	 *
	 * 1. First, get the largest number in the string.
	 * 2. Find the index, and combind with the largest number behind it in the string.
	 * 3. Make sure that the number is NOT last in the string, since ther wont be a number to combine with it.
	 */
	private void run() {
		List<int> largestJoltages = new List<int>();

		foreach (string bank in Input) {
			largestJoltages.Add(getLargestJoltage(bank));
		}

		Output = largestJoltages.Sum();
	}

	private int getLargestJoltage(string bank) {
		LargestWithIndex largest = getLargestIntInStr(bank, secondSeek: false);

		// slice the string from the position of the biggest int,
		// and find the next largest
		LargestWithIndex secondLargest = getLargestIntInStr(
				bank
				.Substring(
					largest.Index + 1), secondSeek: true
				);

		return int.Parse(string.Concat(
					largest.Largest,
					secondLargest.Largest
					));
	}

	private LargestWithIndex getLargestIntInStr(string str, bool secondSeek) {
		LargestWithIndex largest = new LargestWithIndex('0', 0);

		// get the largest number in the string
		for (int i = 0; i < str.Length; i++) {
			char battery = str[i];

			// ignore the last character in the string if enabled
			if (!secondSeek && i == str.Length - 1) continue; 

			if ((int)battery > largest.Largest) {
				largest.Largest = battery;
				largest.Index = i;
			}
		}

		return largest;
	}
}
