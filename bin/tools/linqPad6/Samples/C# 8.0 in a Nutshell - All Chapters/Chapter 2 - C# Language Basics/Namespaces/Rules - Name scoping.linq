<Query Kind="Program" />

namespace Outer
{
	class Class1 { }

	namespace Inner
	{
		class Class2 : Class1 { }
	}
}

namespace MyTradingCompany
{
	namespace Common
	{
		class ReportBase { }
	}
	namespace ManagementReporting
	{
		class SalesReport : Common.ReportBase { }
	}
}