using UnityEngine;
using System.Linq;
using System.Collections;

public class TargetPattern {
	
	public static IEnumerator Circular(float r, float time, int resolution, int dir) {
		yield return new WaitForSeconds(1f);
		var i = 0;
		var maxChain = 2;

		for (;;) {
			foreach (var chain in Enumerable.Range(0, maxChain)) {
				var degree = 360f * ((float)i / resolution) * dir;
				var basePos = Quaternion.AngleAxis(degree + chain * 2 * dir, Vector3.forward) * Vector3.up * r;
				var pos = basePos + SquareGenerator.GenerateRandomVectorFromRange(-3, 3) / 100;
				pos.z = SquareGenerator.popDepth + chain * 1.5f;
				SquareGenerator.PopTarget(pos, Quaternion.identity);
			}
			
			i = ++i % resolution;
			
			yield return new WaitForSeconds((time * (1 / Time.timeScale)) / resolution);
		}
	}
	
	public static IEnumerator RandomPosition(float range, float interval, float ignoreRange = 0) {
		yield return new WaitForSeconds(1f);
		int maxChain = 3;
		
		for (;;) {
			Vector3 basePos;
			do {
				basePos = SquareGenerator.GenerateRandomVectorFromRange(-range, range);
			} while (Mathf.Abs(basePos.x) < ignoreRange || Mathf.Abs(basePos.y) < ignoreRange);
			// var basePos = SquareGenerator.GenerateRandomVectorFromRange(-range, range);
			
			foreach (var chain in Enumerable.Range(0, maxChain)) {
				var pos = basePos + SquareGenerator.GenerateRandomVectorFromRange(-3, 3) / 100;
				pos.z = SquareGenerator.popDepth + chain * 2f;
				SquareGenerator.PopTarget(pos, Quaternion.identity);
			}
			
			yield return new WaitForSeconds(interval);
		}
	}
}
