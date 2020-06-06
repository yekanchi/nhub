<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{	
	new Thread (() => UserCache.GetUserAsync (1).Dump()).Start();
	new Thread (() => UserCache.GetUserAsync (1).Dump()).Start();
	new Thread (() => UserCache.GetUserAsync (1).Dump()).Start();
	
	// You can also await this method:
	User user = await UserCache.GetUserAsync (1);
	user.Dump();
}

static class UserCache
{
	static Dictionary <int, Task<User>> _userTasks = 
		 new Dictionary <int, Task<User>>();
	
	internal static Task<User> GetUserAsync (int id)
	{
		lock (_userTasks)
			if (_userTasks.TryGetValue (id, out var userTask))
				return userTask;
			else
				return _userTasks [id] = Task.Run (() => RetrieveUser (id));
	}

	static User RetrieveUser (int id)
	{
		Thread.Sleep(1000);  // simulate a time-consuming operation
		return new User { ID = id };
	}

}

class User { public int ID; }