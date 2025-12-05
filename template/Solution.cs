using System.Linq;
using aoclib;

namespace template;

public class Solution {
	public List<string> Input { get; private set; }
	public long Output { get; private set; }

	public Solution(bool? isTest) {
		Input = ReadInput.GetInput(isTest: isTest).ToList();
		run();
	}

	private void run() {
		//
	}
}
