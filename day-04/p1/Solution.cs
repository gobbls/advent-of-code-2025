using System.Linq;
using aoclib;

namespace day_04_p1;

public class Solution {
	public List<string> Input { get; private set; }
	public long Output { get; private set; }

	public const int MAX_ALLOWED_OBSTRUCTIONS = 3;
	public const char OBSTRUCTION = '@';

	public Solution(bool? isTest) {
		Input = ReadInput.GetInput(isTest: isTest).ToList();
		run();
	}

	private void run() {
		// input rows
		for (int i = 0; i < Input.Count; i++) {
			// input columns
			for (int j = 0; j < Input[i].Length; j++) {
				char cur = Input[i][j];
				if (cur == OBSTRUCTION) Output += isAccessible(i, j) ? 1 : 0;
			}
		}
	}

	private bool isAccessible(int y, int x) {
		int obstructions = 0;

		int windowStartPosX = x - 1;
		int windowStartPosY = y - 1;
		int windowEndPosX = x + 1;
		int windowEndPosY = y + 1;

		Func<int, int, bool> ignoreSelfAsObstruction = (_x, _y) =>
			_x == x && _y == y;

		// window height
		for (int i = windowStartPosY; i <= windowEndPosY; i++) {

			// pass if the position is outside the grid
			if (i < 0 || i == Input.Count) continue;

			// window width
			for (int j = windowStartPosX; j <= windowEndPosX; j++) {

				// pass if the start position is outside the grid or the
				// same position as the one we are checking around
				if (j < 0 || j == Input[i].Length ||
						ignoreSelfAsObstruction(j, i)) continue;

				obstructions += Input[i][j] == OBSTRUCTION ? 1 : 0;

				if (obstructions > MAX_ALLOWED_OBSTRUCTIONS) return false;
			}
		}

		return true;
	}
}
