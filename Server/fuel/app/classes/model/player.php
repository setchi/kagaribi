<?php

class Model_Player extends Model
{
	private static $prefix = 'kagaribi_';

	public static function exist($id) {
		$exist = DB::select()->from(self::$prefix.'player')
			->where('id', $id)
			->execute()->count();

		return $exist != 0;
	}

	public static function add($id, $name = null) {
		DB::insert(self::$prefix.'player')->set(array(
			'id' => $id,
			'name' => $name
		))->execute();
	}

	public static function update_name($id, $name) {

		if (!self::exist($id))
			return;

		DB::update(self::$prefix.'player')->set(array(
			'name' => $name
		))->where('id', $id)->execute();
	}
}
