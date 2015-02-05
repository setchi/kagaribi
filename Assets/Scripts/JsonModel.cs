public class JsonModel {

	public class LocalData {
		public string bestScore;
		public PlayerInfo playerInfo;
	}

	public class Record {
		public string id;
		public string name;
		public string score;
	}

	public class PlayerInfo {
		public string id;
		public string name;
	}

	public class CheckRecord {
		public bool is_new_record;
	}
}
