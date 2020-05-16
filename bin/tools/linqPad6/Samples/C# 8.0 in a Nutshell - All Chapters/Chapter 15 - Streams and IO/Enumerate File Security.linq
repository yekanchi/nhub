<Query Kind="Statements">
  <Namespace>System.Security.AccessControl</Namespace>
  <Namespace>System.Security.Principal</Namespace>
</Query>

try
{
	File.WriteAllText("sectest.txt", "File for testing security.");

	FileSecurity fSecurity = new FileSecurity("sectest.txt", AccessControlSections.Owner |
                                                             AccessControlSections.Group |
                                                             AccessControlSections.Access);

	Console.WriteLine (string.Join (Environment.NewLine, 
        fSecurity.GetAccessRules (true, true, typeof (NTAccount))
        .Cast<AuthorizationRule>()
        .Select (r => r.IdentityReference.Value)));
}
catch (PlatformNotSupportedException ex)
{
	Console.WriteLine ($"Not supported: {ex.Message}");
}
finally
{
	File.Delete ("sectest.txt");
}