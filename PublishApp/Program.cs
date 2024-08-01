using System.Text.Json;

namespace PublishApp;

public class Program
{
	static void Main(string[] args)
	{/*
		Console.WriteLine("This App Publishing on GitHub.com");
		Thread.Sleep(100);

		var u = new UpdateService();

		Console.WriteLine("CheckForUpdatesAsync Result : ");
		var rt = u.CheckForUpdatesAsync().Result;

		bool ki = false;

		if (rt)
			ki = u.DownloadAndInstallUpdateAsync().Result;

        Console.WriteLine();
        Console.WriteLine("Last Result : ");
        Console.WriteLine(ki);
        Console.WriteLine();

        Console.WriteLine("");
		Thread.Sleep(100);
		*/
		var ttttt = 
			@"https://github.com/Elvin0802/Publish-App-CS/blob/master/PublishApp/appversion.txt";


		using var client = new HttpClient();
		var response = client.GetStringAsync(ttttt);

		var tpl = response.Result.Split('{','}');
     
		foreach (var item in tpl)
        {
			Console.WriteLine();
			Console.WriteLine("Result : ");
			Console.WriteLine();
            Console.WriteLine(item);
            Console.WriteLine();
			Console.WriteLine();
		}



    }
}

public class UpdateService
{
	private const string UpdateUrl =
			@"";

	public async Task<bool> CheckForUpdatesAsync()
	{
		try
		{
			using var client = new HttpClient();
			var response = await client.GetStringAsync(UpdateUrl);
			var latestVersion = ParseVersionFromJson(response);

			//   Compare latestVersion with your app's current version
			var currentVersion = new Version(1, 0, 1);

			var ttt = JsonSerializer.Serialize<Version>(currentVersion);

			File.WriteAllText("testv.json", ttt);

			Console.WriteLine();
			Console.WriteLine("Currrent Version : ");
			Console.WriteLine(ttt);
			Console.WriteLine();
			Console.WriteLine("Latest Version : ");
			Console.WriteLine(latestVersion);
			Console.WriteLine();


			return latestVersion > currentVersion;
		}
		catch (Exception ex)
		{
			// Handle exceptions (e.g., network errors)
			Console.WriteLine($"Error checking for updates: {ex.Message}");
			return false;
		}
	}

	public async Task<bool> DownloadAndInstallUpdateAsync()
	{
		try
		{
			using var client = new HttpClient();
			var updatePackageBytes = await client.GetByteArrayAsync(
					@"https://github.com/Elvin0802/Publish-App-CS/releases/download/v1.0/net8.rar");

			// Save the update package to a temporary location
			var tempPath = Path.Combine("D:/Games", "net8.rar");
			File.WriteAllBytes(tempPath, updatePackageBytes);

			// Install the update (platform-specific code required)
			// For Windows, use Windows.ApplicationModel.Package APIs
			// For Android and iOS, use platform-specific mechanisms

			// Example: Windows
			// var packageUri = new Uri($"ms-appx:///MyAppUpdate.msix");
			// await Windows.ApplicationModel.Package.Current.InstallPackageAsync(packageUri);

			// Clean up the temporary file
			//File.Delete(tempPath);
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error downloading/installing update: {ex.Message}");
			return false;
		}
	}

	private Version? ParseVersionFromJson(string? json)
	{
		// Parse the JSON response to extract the version
		// Example: { "version": "2.0.1" }
		// Implement your own logic here
		// For simplicity, assume the version is a string
		//Console.WriteLine();
		//Console.WriteLine("json in web url : ");
		//Console.WriteLine(json);
		//Console.WriteLine();

		//Thread.Sleep(2000);

		return new Version(1, 0, 1);


		var oop = JsonSerializer.Deserialize<Version>(json);

		var versionString = "2.0.1"; // Replace with actual parsing
		return new Version(versionString);
	}
}