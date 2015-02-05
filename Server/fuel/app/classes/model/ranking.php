<?php

class Model_Ranking extends Model
{
	private static $prefix = 'kagaribi_';

	public static function exist($id) {
		$exist = DB::select()->from(self::$prefix.'ranking')
			->where('player_id', $id)
			->execute()->count();

		return $exist != 0;
	}

	public static function entry($id, $score) {
		$data = array(
			'player_id' => $id,
			'score' => $score,
			'time' => date('Y-m-d H:i:s')
		);

		if (self::exist($id)) {
			DB::update(self::$prefix.'ranking')->set($data)->where('player_id', $id)->execute();

		} else {
			DB::insert(self::$prefix.'ranking')->set($data)->execute();
		}
	}

	public static function get_score($id) {
		if (!self::exist($id))
			return 0;

		$record = DB::select('score')->from(self::$prefix.'ranking')
			->where('player_id', $id)
			->execute()->as_array();

		return $record[0]['score'];
	}

	public static function get($limit = 10) {
		return DB::select('id', 'name', 'score')->from(self::$prefix.'ranking')
			->join(self::$prefix.'player')
			->on('player_id', '=', 'id')
			->order_by('score', 'desc')
			->limit($limit)
			->execute()->as_array();
	}
}
