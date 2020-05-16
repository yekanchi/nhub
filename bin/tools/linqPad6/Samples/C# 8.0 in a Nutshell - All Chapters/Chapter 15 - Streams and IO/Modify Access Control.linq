<Query Kind="Statements">
  <Namespace>System.Security.AccessControl</Namespace>
  <Namespace>System.Security.Principal</Namespace>
</Query>

void ShowSecurity (FileSecurity sec)
{
	AuthorizationRuleCollection rules = sec.GetAccessRules (true, true,
                                                         typeof (NTAccount));
	foreach (FileSystemAccessRule r in rules.Cast<FileSystemAccessRule>()
		.OrderBy (rule => rule.IdentityReference.Value))
	{
		// e.g., MyDomain/Joe
		Console.WriteLine ($"  {r.IdentityReference.Value}");
		// Allow or Deny: e.g., FullControl
		Console.WriteLine ($"    {r.FileSystemRights}: {r.AccessControlType}");
	}
}

var file = "sectest.txt";
File.WriteAllText (file, "File security.");

var sid = new SecurityIdentifier (WellKnownSidType.BuiltinUsersSid, null);
string usersAccount = sid.Translate (typeof (NTAccount)).ToString();

Console.WriteLine ($"User: {usersAccount}");

FileSecurity sec = new FileSecurity (file,
                          AccessControlSections.Owner |
                          AccessControlSections.Group |
                          AccessControlSections.Access);

Console.WriteLine ("AFTER CREATE:");
ShowSecurity(sec);

sec.ModifyAccessRule (AccessControlModification.Add,
    new FileSystemAccessRule (usersAccount, FileSystemRights.Write, 
    													AccessControlType.Allow),
    out bool modified);

Console.WriteLine ("AFTER MODIFY:");
ShowSecurity (sec);