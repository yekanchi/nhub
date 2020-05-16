<Query Kind="Program" />

void Main()
{  
}

public class Foo
{
	WeakDelegate<EventHandler> _click = new WeakDelegate<EventHandler>();

	public event EventHandler Click
	{
		add { _click.Combine (value); }
		remove { _click.Remove (value); }
	}

	protected virtual void OnClick (EventArgs e)
		=> _click.Target?.Invoke (this, e);
}


public class WeakDelegate<TDelegate> where TDelegate : class
{
	class MethodTarget
	{
		public readonly WeakReference Reference;
		public readonly MethodInfo Method;

		public MethodTarget (Delegate d)
		{
			// d.Target will be null for static method targets:
			if (d.Target != null) Reference = new WeakReference (d.Target);
			Method = d.Method;
		}
	}

	List<MethodTarget> _targets = new List<MethodTarget>();

	public WeakDelegate()
	{
		if (!typeof (TDelegate).IsSubclassOf (typeof (Delegate)))
			throw new InvalidOperationException
				("TDelegate must be a delegate type");
	}

	public void Combine (TDelegate target)
	{
		if (target == null) return;

		foreach (Delegate d in (target as Delegate).GetInvocationList())
			_targets.Add (new MethodTarget (d));
	}

	public void Remove (TDelegate target)
	{
		if (target == null) return;
		foreach (Delegate d in (target as Delegate).GetInvocationList())
		{
			MethodTarget mt = _targets.Find (w =>
				Equals (d.Target, w.Reference?.Target) &&
				Equals (d.Method.MethodHandle, w.Method.MethodHandle));

			if (mt != null) _targets.Remove (mt);
		}
	}

	public TDelegate Target
	{
		get
		{
			Delegate combinedTarget = null;

			foreach (MethodTarget mt in _targets.ToArray())
			{
				WeakReference wr = mt.Reference;

				// Static target || alive instance target
				if (wr == null || wr.Target != null)
				{
					var newDelegate = Delegate.CreateDelegate (
						typeof (TDelegate), wr?.Target, mt.Method);
					combinedTarget = Delegate.Combine (combinedTarget, newDelegate);
				}
				else
					_targets.Remove (mt);
			}

			return combinedTarget as TDelegate;
		}
		set
		{
			_targets.Clear();
			Combine (value);
		}
	}
}