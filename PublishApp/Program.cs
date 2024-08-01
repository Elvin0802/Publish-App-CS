namespace PublishApp;

public class Program
{
	static void Main(string[] args)
	{

		var uc = new UpdateService();

		Console.WriteLine();
		Console.WriteLine();
		Console.WriteLine(uc.CheckForUpdatesAsync(uc.UpdateUrl, @"1.0"));
		Console.WriteLine();
		Console.WriteLine();

	}
}

public class UpdateService
{
	public string UpdateUrl =
			@"https://github.com/Elvin0802/Publish-App-CS/blob/master/PublishApp/appversion.txt";

	public async Task<string> CheckForUpdatesAsync(string? dUrl, string? curVer)
	{
		try
		{
			using var client = new HttpClient();

			Version? vLast = null;
			string? filePath =
				DownloadAndInstallUpdateAsync(dUrl, @"D:/Games", @"appversion.txt")
				.Result;

			if (string.IsNullOrEmpty(filePath))
			{
				vLast = ParseVersionFromTxt(filePath);
			}

			//   Compare latestVersion with your app's current version

			var currentVersion = new Version(curVer);

			//var currentVersion = new Version("1.0");

			if (vLast > currentVersion)
				return "Update Aviable.";
			else
				return "Update Not Aviable.";
		}
		catch (Exception ex)
		{
			// Handle exceptions (e.g., network errors)

			Console.WriteLine($"Error checking for updates: {ex.Message}");

			return "Update Not Aviable.";
		}
	}

	public async Task<string> DownloadAndInstallUpdateAsync(string? cUrl, string? path, string? pName)
	{
		try
		{
			using var client = new HttpClient();

			var updatePackageBytes = await client.GetByteArrayAsync(cUrl);

			//@"https://github.com/Elvin0802/Publish-App-CS/releases/download/v1.0/net8.rar");

			// Save the update package to a temporary location

			var tempPath = Path.Combine(path, pName);

			Console.WriteLine($"\n\tLast path : {tempPath}\n");

			//var tempPath = Path.Combine("D:/Games", "net8.rar");


			File.WriteAllBytes(tempPath, updatePackageBytes);

			// Install the update (platform-specific code required)
			// For Windows, use Windows.ApplicationModel.Package APIs
			// For Android and iOS, use platform-specific mechanisms

			// Example: Windows
			// var packageUri = new Uri($"ms-appx:///MyAppUpdate.msix");
			// await Windows.ApplicationModel.Package.Current.InstallPackageAsync(packageUri);

			// Clean up the temporary file
			//File.Delete(tempPath);

			return tempPath;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error downloading/installing update: {ex.Message}");

			return @"D:\Games";
		}
	}

	private Version? ParseVersionFromTxt(string? txtPath)
	{
		var vText = File.ReadAllText(txtPath); // Version Text.

		Console.WriteLine(vText);

		Thread.Sleep(400);

		return new Version(vText);
	}
}