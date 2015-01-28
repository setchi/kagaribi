using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class Pattern {
	static float popDepth = 200;

	Target target_;
	public Target target { get { return target_; } }

	Background background_;
	public Background background { get { return background_; } }
	
	public Pattern(AbstractSquareGenerator squareGenerator) {
		target_ = new Target(squareGenerator);
		background_ = new Background(squareGenerator);
	}

	
	static Vector3 RandomVectorFromRange(float min, float max) {
		return new Vector3(
			UnityEngine.Random.Range(min, max),
			UnityEngine.Random.Range(min, max),
			UnityEngine.Random.Range(min, max)
		);
	}

	static Vector3 DirectionFromDegree(float degree) {
		return Quaternion.AngleAxis(degree, Vector3.forward) * Vector3.up;
	}

	static void Repeat(int max, Action<int> action) {
		for (int i = 0; i < max; ++i)
			action(i);
	}


	public class Target {
		AbstractSquareGenerator squareGenerator;
		float switchDelay = 1f;

		public Target(AbstractSquareGenerator squareGenerator) {
			this.squareGenerator = squareGenerator;
		}
		
		public IEnumerator Circular(float r, float time, int resolution, int rotateDir) {
			yield return new WaitForSeconds(switchDelay);
			var counter = 0;
			var maxChain = 2;

			while (true) {
				Repeat (maxChain, chain => {
					var degree = 360f * ((float)counter / resolution);
					var direction = DirectionFromDegree(degree + chain * 2 * rotateDir);
					var basePos = direction * r;
					var pos = basePos + RandomVectorFromRange(-3, 3) / 100;
					pos.z = popDepth + chain * 1.5f;
					squareGenerator.PopTarget(pos, Quaternion.identity);
				});
				
				counter = ++counter % resolution;
				
				yield return new WaitForSeconds((time * (1 / Time.timeScale)) / resolution);
			}
		}
		
		public IEnumerator RandomPosition(float range, float interval, float ignoreRange = 0) {
			yield return new WaitForSeconds(switchDelay);
			int maxChain = 3;
			
			while (true) {
				Vector3 basePos;
				do {
					basePos = RandomVectorFromRange(-range, range);
				} while (Mathf.Abs(basePos.x) < ignoreRange || Mathf.Abs(basePos.y) < ignoreRange);
				
				Repeat (maxChain, chain => {
					var pos = basePos + RandomVectorFromRange(-3, 3) / 100;
					pos.z = popDepth + chain * 2f;

					squareGenerator.PopTarget(pos, Quaternion.identity);
				});
				
				yield return new WaitForSeconds(interval);
			}
		}
	}


	public class Background {
		AbstractSquareGenerator squareGenerator;
		float switchDelay = 0.75f;

		public Background(AbstractSquareGenerator squareGenerator) {
			this.squareGenerator = squareGenerator;
		}
		
		public IEnumerator Circular(float r, float interval, int depthOfRound, int rotateDir, int multiplicity, Tuple<Color, Color> colorPair) {
			yield return new WaitForSeconds(switchDelay);
			GameObject tailEnd = null;
			var counter = 0;
			var resolution = Mathf.RoundToInt(depthOfRound / interval);
			
			while (true) {
				var tailEndZ = tailEnd == null ? popDepth - interval * 2 : tailEnd.transform.position.z;

				while ((tailEndZ += interval) < popDepth) {
					var percentage = (float)counter / resolution;
					var degree = 360f * percentage * rotateDir;
					
					Repeat (multiplicity, i => {
						var color = Color.Lerp(
							colorPair.Item1,
							colorPair.Item2,
							Mathf.PingPong(percentage * 4, 1)
						);

						var direction = DirectionFromDegree(degree + i * (360f / multiplicity));
						var pos = direction * r;
						pos.z = tailEndZ;
						tailEnd = squareGenerator.PopBackground(pos, Quaternion.identity, color);
					});

					counter = ++counter % resolution;
				}
				
				yield return new WaitForEndOfFrame();
			}
		}
		
		public IEnumerator RandomPositionAndScale(float min, float max, Tuple<Color, Color> colorPair) {
			yield return new WaitForSeconds(switchDelay);
			bool collerToggle = false;
			
			while (true) {
				Vector3 pos;
				do {
					pos = RandomVectorFromRange(min, max);
				} while (Mathf.Abs(pos.x) < 4 && Mathf.Abs(pos.y) < 4);
				
				pos.z = popDepth;
				var color = (collerToggle = !collerToggle) ? colorPair.Item1 : colorPair.Item2;
				var square = squareGenerator.PopBackground(pos, Quaternion.identity, color);
				var scale = RandomVectorFromRange(0.5f, 2);
				square.transform.localScale = scale;
				
				yield return new WaitForSeconds(0.001f);
			}
		}
		
		public IEnumerator Polygon(float r, float interval, int length, int multiplicity, Tuple<Color, Color> colorPair) {
			yield return new WaitForSeconds(switchDelay);
			GameObject tailEnd = null;
			var counter = 0;
			
			while (true) {
				var tailEndZ = tailEnd == null ? popDepth - interval * 2 : tailEnd.transform.position.z;

				while ((tailEndZ += interval) < popDepth) {
					var percentage = (float)counter / length;

					Repeat (multiplicity, i => {
						var color = Color.Lerp(
							colorPair.Item1,
							colorPair.Item2,
							Mathf.PingPong(percentage * multiplicity, 1)
						);

						var pos = Vector3.Lerp(
							DirectionFromDegree(i * (360f / multiplicity)),
							DirectionFromDegree((i + 2) * (360f / multiplicity)),
							percentage
						) * r;

						pos.z = tailEndZ;

						tailEnd = squareGenerator.PopBackground(pos, Quaternion.identity, color);
						tailEnd.transform.localScale = Vector3.one * 0.5f;
					});

					counter = ++counter % length;
				}

				yield return new WaitForEndOfFrame();
			}
		}
	}
}
