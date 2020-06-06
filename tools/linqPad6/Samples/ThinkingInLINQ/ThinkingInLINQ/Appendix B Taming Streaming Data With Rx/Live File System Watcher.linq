<Query Kind="Statements">
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>System.Reactive.Linq</Namespace>
</Query>

System.IO.FileSystemWatcher w = new
//Set the folder you want to monitor.
System.IO.FileSystemWatcher("C:\\Apress");
//start the File System Watcher to watch events
w.EnableRaisingEvents = true;
//Find the files that have been created
var fileCreated = Observable
							.FromEventPattern<FileSystemEventArgs>(w, "Created")
							.Select(
								z =>
									{
										var file = new FileInfo(z.EventArgs.FullPath);
										return new
										{
											FullPath = z.EventArgs.FullPath,
											Created = z.EventArgs.ChangeType,
											Name = z.EventArgs.Name,
											DirectoryName = file.DirectoryName
										};
								});
//Find the files that have been changed.
var fileChanged = Observable.FromEventPattern<FileSystemEventArgs>(w, "Changed")
							.Select(z =>
								new 
								{
									FullPath = z.EventArgs.FullPath,
									ChangeType = z.EventArgs.ChangeType
								}
							);
//Find the files that have been renamed
var fileRenamed = Observable.FromEventPattern<RenamedEventArgs>(w, "Renamed")
.Select
  (z => 
	new
	{
		OldFullPath = z.EventArgs.OldFullPath,
		NewPath = z.EventArgs.FullPath,
		ChangeType = z.EventArgs.ChangeType
	}
  );
fileCreated.Dump("Created");
fileRenamed.Dump("Renamed");
fileChanged.Dump("Changed");