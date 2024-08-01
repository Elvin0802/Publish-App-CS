

using System;
using System.Text.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace PublishApp;

public class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("This App Publishing on GitHub.com");
		Thread.Sleep(9000);









	}
}

public class UpdateService
{
	private const string UpdateUrl = @"https://your-update-server.com/latest-version.json";

	public async Task<bool> CheckForUpdatesAsync()
	{
		try
		{
			using var client = new HttpClient();
			var response = await client.GetStringAsync(UpdateUrl);
			var latestVersion = ParseVersionFromJson(response);

		    //   Compare latestVersion with your app's current version
		    var currentVersion = new Version(1,0,1);
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
			var updatePackageBytes = await client.GetByteArrayAsync("https://your-update-server.com/MyAppUpdate.msix");

			// Save the update package to a temporary location
			var tempPath = Path.Combine("D:/Games", "MyAppUpdate.msix");
			File.WriteAllBytes(tempPath, updatePackageBytes);

			// Install the update (platform-specific code required)
			// For Windows, use Windows.ApplicationModel.Package APIs
			// For Android and iOS, use platform-specific mechanisms

			// Example: Windows
			// var packageUri = new Uri($"ms-appx:///MyAppUpdate.msix");
			// await Windows.ApplicationModel.Package.Current.InstallPackageAsync(packageUri);

			// Clean up the temporary file
			File.Delete(tempPath);
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error downloading/installing update: {ex.Message}");
			return false;
		}
	}

	private Version ParseVersionFromJson(string json)
	{
		// Parse the JSON response to extract the version
		// Example: { "version": "2.0.1" }
		// Implement your own logic here
		// For simplicity, assume the version is a string

		var oop = JsonSerializer.Deserialize<Version>(json);

		var versionString = "2.0.1"; // Replace with actual parsing
		return new Version(versionString);
	}
}