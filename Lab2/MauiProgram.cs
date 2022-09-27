namespace Lab2;

/**
 * Names: Brady Braun and Wil LaLonde
 * Description: Lab 2
 * Date: 9/27/2022
 * Bugs: 
 *   1. When adding an entry that is too long, the text goes out of the table.
 *		Tried adding line breaks but that didn't seem to work inside the grid layout.
 *	 2. When adding, deleting, or editing an entry, sometimes other entries will just
 *		vanish. The entries still exist, but they fail to show in the ListView.
 *		Tried adding different styling to see if that was the issue, nothing seemed
 *		to fix this random bug. 
 * Reflection: 
 *   Brady: I feel that this assignment was a little challenging because not only 
 *          were we struggling with trying to run Visual Studio and Android Studio 
 *          at the same time, but with .Net Maui being a newer framework, there isn't 
 *          a lot of information on it besides the documentation and the GitHub page. 
 *          This caused some problems when we had run into errors. Trial by fire. 
 *          Despite the struggles, this is enhancing our researching and problem solving 
 *          skills when errors do occur, which, in the end, is making us better programmers.
 *          
 *   Wil: I thought this assignment was very beneficial learning more about .NET
 *	      MAUI. It was a struggle sometimes figuring out why certain XAML elements 
 *		  didn't work. Eventually, the solution came about. It was really odd how
 *		  the ListView elements would just vanish from time to time. After doing some
 *		  research, it seems like it is a known issue with .NET MAUI. One complaint is 
 *		  that my personal computer really struggles running VS and an Andriod Emulator
 *		  device at the same time. I most likely will have to start using the lab 
 *		  machines to complete the labs. 
 *		  
 * GitHub link: https://github.com/Wil-LaLonde/CS341-Lab2
 */
public static class MauiProgram {
	public static MauiApp CreateMauiApp() {
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}
