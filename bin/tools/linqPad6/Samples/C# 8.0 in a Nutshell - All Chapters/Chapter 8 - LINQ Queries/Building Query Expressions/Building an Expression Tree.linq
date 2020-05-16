<Query Kind="Statements">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c394</ID>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AttachFile>true</AttachFile>
    <Server>(localdb)\MSSQLLocalDB</Server>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
    <DisplayName>Nutshell</DisplayName>
    <Persist>true</Persist>
    <DriverData>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

ParameterExpression p = Expression.Parameter (typeof (string), "s");

MemberExpression stringLength = Expression.Property (p, "Length");
ConstantExpression five = Expression.Constant (5);

BinaryExpression comparison = Expression.LessThan (stringLength, five);

Expression<Func<string, bool>> lambda = Expression.Lambda<Func<string, bool>> (comparison, p);

Func<string, bool> runnable = lambda.Compile();

runnable ("kangaroo")  .Dump ("kangaroo is less than 5 characters");
runnable ("dog")       .Dump ("dog is less than 5 characters");