using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RankRecord : MonoBehaviour {
	public Text rankTextUI;
	public Text nameTextUI;
	public Text socreTextUI;
	
	public void Set(int rank, JsonModel.Record record, string selfPlayerId) {
		rankTextUI.text = rank.ToString();
		nameTextUI.text = record.name;
		socreTextUI.text = record.score;
		
		if (record.id == selfPlayerId) {
			var selfColor = Color.yellow;
			
			rankTextUI.color = selfColor;
			nameTextUI.color = selfColor;
			socreTextUI.color = selfColor;
		}
	}
}
