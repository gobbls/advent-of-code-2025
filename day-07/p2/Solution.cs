using System.Linq;
using aoclib;

namespace day_07_p2;

public class Solution {
	public string[] Input { get; private set; }
	public long Output { get; private set; } = 0;

	private const char C = '^';
	private const char S = 'S';

	public Solution(bool? isTest) {
		string[] _input = ReadInput.GetInput(isTest: isTest);

		// filter await gap-lines
		Input = _input.Distinct().ToArray();

		run();
	}

	private void run() {
		Queue<(int y, int x)> q = new Queue<(int y, int x)>();

 		// append the start of the Input
		q.Enqueue((0, Input[0].IndexOf(S)));

		var beams = new Dictionary<(int y, int x), long>() {
			[(0, Input[0].IndexOf(S))] = 1
		};

		void EnqueueBeam((int, int) p, long paths) {
			if (!beams.ContainsKey(p)) {
				beams[p] = paths;
			} else {
				beams[p] += paths;
			}

			if (!q.Contains(p)) {
				q.Enqueue(p);
			}
		}

		while (q.Count > 0) {
			var beam = q.Dequeue(); 
			var paths = beams[beam];
			beams.Remove(beam);
			var (y, x) = beam;
			int by = y + 1;

			if (by >= Input.Length) {
				Output += paths;
				continue;
			}

			switch (Input[by][x]) {
				case '.':
					EnqueueBeam((by, x), paths);
					break;
				case '^':
					int left = x - 1;
					int right = x + 1;

					if (left >= 0 && Input[by][left] == '.') {
						EnqueueBeam((by, left), paths);
					}

					if (right < Input[0].Length && Input[by][right] == '.') {
						EnqueueBeam((by, right), paths);
					}

					break;
			}
		}
	}
}
