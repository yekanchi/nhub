<Query Kind="Statements" />

#LINQPad admin

string category = "Nutshell Monitoring";

// We'll create two counters in this category:
string eatenPerMin = "Macadamias eaten so far";
string tooHard = "Macadamias deemed too hard";

if (!PerformanceCounterCategory.Exists (category))
{
	CounterCreationDataCollection cd = new CounterCreationDataCollection();

	cd.Add (new CounterCreationData (eatenPerMin,
            "Number of macadamias consumed, including shelling time",
            PerformanceCounterType.NumberOfItems32));

	cd.Add (new CounterCreationData (tooHard,
            "Number of macadamias that will not crack, despite much effort",
            PerformanceCounterType.NumberOfItems32));

	// This line requires elevated permissions. Either run LINQPad as administrator (for this query only!) 
	// or create the category in a separate program you run as administrator.
	PerformanceCounterCategory.Create (category, "Test Category",
		PerformanceCounterCategoryType.SingleInstance, cd);
}

using (PerformanceCounter pc = new PerformanceCounter (category,
                                                       eatenPerMin, ""))
{
	pc.ReadOnly = false;
	pc.RawValue = 1000;
	pc.Increment();
	pc.IncrementBy (10);
	Console.WriteLine (pc.NextValue());    // 1011
}