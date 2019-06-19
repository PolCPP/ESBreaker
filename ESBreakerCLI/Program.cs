using System;
using CommandLine;
using CommandLine.Text;
using System.Globalization;

[assembly: CLSCompliant(false)]

namespace ESBreakerCLI
{
	class Options
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[Option(
			'j', "skip-json",
			DefaultValue = false, Required = false,
			HelpText = "Don't Generate/Update JSON. This way it only generates, or update the files with new patch data"
		)]
		public bool SkipJson { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[Option(
			'm', "minify-json",
			DefaultValue = false, Required = false,
			HelpText = "Don't prettyprint the JSON output"
		)]

		public bool MinifyJson { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[Option(
			's', "skip-save",
			DefaultValue = false, Required = false,
			HelpText = "Don't save the database"
		)]

		public bool SkipSave { get; set; }


		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[HelpOption]
		public string GetUsage()
		{
			var help = new HelpText
			{
				AdditionalNewLineAfterOption = true,
				AddDashesToOption = true
			};
			help.AddPreOptionsLine("Example Usage: ESBreakerCLI -j");
			help.AddOptions(this);
			return help;
		}
	}


	class MainClass
	{
		const string edition = "The Photoners Strike Back";

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ESBreaker")]
		public static void Main(string[] args)
		{
			Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "ESBreaker: PSO2es Fan translation Patch tool | {0} edition", edition));
			Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
			Console.WriteLine("THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND," +
				" EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, " +
				"FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS " +
				"OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, " +
				"WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF" +
				" OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.");
			Console.WriteLine("Phantasy Star Online 2 ES is copyright from SEGA\n");

			var options = new Options();
			if (CommandLine.Parser.Default.ParseArguments(args, options))
			{
				Database d = new Database(!options.SkipJson, !options.MinifyJson, !options.SkipSave);
				d.Process();
			}
		}
	}
}
