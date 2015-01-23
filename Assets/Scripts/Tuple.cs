public class Tuple<T1, T2> {
	T1 item1_;
	T2 item2_;
	
	public T1 Item1 { get { return item1_; } }
	public T2 Item2 { get { return item2_; } }

	public Tuple(T1 item1, T2 item2) {
		item1_ = item1;
		item2_ = item2;
	}
}
