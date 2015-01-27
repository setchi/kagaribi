using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PatternRoutines : MonoBehaviour {
	public ParticleController particleController;
	public AbstractSquareManager squareManager;

	Pattern pattern_;
	Pattern pattern { get { return pattern_; } }

	List<Func<Tuple<Color, Color>, List<IEnumerator>>> patternRoutineList_;
	List<Func<Tuple<Color, Color>, List<IEnumerator>>> patternRoutineList { get { return patternRoutineList_; } }

	void Awake() {
		pattern_ = new Pattern(squareManager);
		patternRoutineList_ = new List<Func<Tuple<Color, Color>, List<IEnumerator>>>();

		// Multiple Cross Circle
		patternRoutineList_.Add(colorPair => {
			var routines = new List<IEnumerator>();
			var multiplicity = UnityEngine.Random.Range(2, 6);
			var rotateDir = PickAtRandom<int>(1, -1);
			
			routines.Add(pattern.background.Circular(10, 4f, 330, rotateDir, multiplicity, colorPair));
			routines.Add(pattern.background.Circular(10, 4f, 330, rotateDir * -1, multiplicity, colorPair));
			routines.Add(pattern.target.Circular(3f, 7.5f, 5, PickAtRandom<int>(1, -1)));
			routines.Add(particleController.ChangeParticleColorAfter5sec(colorPair.Item1));
			return routines;
		});

		// Multiple Circle
		patternRoutineList_.Add(colorPair => new List<IEnumerator>() { 
			pattern.background.Circular(10, 4f, 330, PickAtRandom<int>(1, -1), UnityEngine.Random.Range(2, 6), colorPair),
			pattern.target.Circular(3f, 7.5f, 5, PickAtRandom<int>(1, -1)),
			particleController.ChangeParticleColorAfter5sec(colorPair.Item1),
		});

		// Random Position
		patternRoutineList_.Add(colorPair => new List<IEnumerator>() {
			pattern.background.RandomPositionAndScale(-30, 30, colorPair),
			pattern.target.RandomPosition(3, 1.5f, 0),
			particleController.ChangeParticleColorAfter5sec(colorPair.Item1)
		});

		// Multiple Cross Polygon
		patternRoutineList_.Add(colorPair => new List<IEnumerator>() { /**
				pattern.Background.Polygon(15, 1.5f, 70, 3, colorPair),
				pattern.Target.RandomPosition(3, 1.5f, 0),
				ChangeParticleColor(colorPair.Item1)
			
			// Cross Square
			}, colorPair => new List<IEnumerator>() { /**/
			pattern.background.Polygon(20, 1.7f, 70, 4, colorPair),
			pattern.target.RandomPosition(5, 3f, 4),
			particleController.ChangeParticleColorAfter5sec(colorPair.Item1)
		});
	}
	
	public Func<Tuple<Color, Color>, List<IEnumerator>> Get(int index) {
		return patternRoutineList[index];
	}
	
	public int Count {
		get { return patternRoutineList.Count; }
	}
	
	T PickAtRandom<T>(params T[] items) {
		return items[UnityEngine.Random.Range(0, items.Length)];
	}
}
