using UnityEngine;
using System.Linq;
using System.Collections;

public class BackgroundPattern {

	public static IEnumerator Circular(float r, float time, int resolution, int dir, int multiple, Tuple<Color, Color> colorPair) {
		yield return new WaitForSeconds(1f);
		var i = 0;
		
		for (;;) {
			var per = (float)i / resolution;
			var degree = 360f * per * dir;

			foreach (var j in Enumerable.Range(0, multiple)) {
				var pos = Quaternion.AngleAxis(degree + (j * (360f / multiple)), Vector3.forward) * Vector3.up * r;
				pos.z = SquareGenerator.popDepth;
				SquareGenerator.PopBackground(pos, Quaternion.identity, Color.Lerp(colorPair.Item1, colorPair.Item2, Mathf.PingPong(per * 5, 1)));
			}
			i = ++i % resolution;
			
			yield return new WaitForSeconds((time * (1 / Time.timeScale)) / resolution);
		}
	}
	
	public static IEnumerator RandomPositionAndScale(float min, float max, Tuple<Color, Color> colorPair) {
		yield return new WaitForSeconds(1f);
		for (;;) {
			Vector3 pos;
			do {
				pos = SquareGenerator.GenerateRandomVectorFromRange(min, max);
			} while (Mathf.Abs(pos.x) < 4 && Mathf.Abs(pos.y) < 4);
			
			pos.z = SquareGenerator.popDepth;
			var square = SquareGenerator.PopBackground(pos, Quaternion.identity, Random.Range(0, 2) == 0 ? colorPair.Item1 : colorPair.Item1);
			var scale = SquareGenerator.GenerateRandomVectorFromRange(0.5f, 2);
			square.transform.localScale = scale;
			
			yield return new WaitForSeconds(0.001f);
		}
	}

	public static IEnumerator Triangle(float r, float time, int resolution, int multiple, Tuple<Color, Color> colorPair) {
		yield return new WaitForSeconds(1f);
		var i = 0;

		time /= multiple;

		for (;;) {
			var per = (float)i / resolution;

			foreach (var j in Enumerable.Range(0, multiple)) {
				var from = Quaternion.AngleAxis(j * (360f / multiple), Vector3.forward) * Vector3.up * r;
				var to = Quaternion.AngleAxis((j + 2) * (360f / multiple), Vector3.forward) * Vector3.up * r;
				var pos = Vector3.Lerp(from, to, per);
				pos.z = SquareGenerator.popDepth;
				var square = SquareGenerator.PopBackground(pos, Quaternion.identity, Color.Lerp(colorPair.Item1, colorPair.Item2, Mathf.PingPong(per * multiple, 1)));
				square.transform.localScale = Vector3.one * 0.5f;
			}
			i = ++i % resolution;

			yield return new WaitForSeconds((time * (1 / Time.timeScale)) / resolution);
		}
	}

}
