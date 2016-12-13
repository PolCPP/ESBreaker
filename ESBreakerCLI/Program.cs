using System;
using Contents;
using System.Reflection;

namespace ESBreakerCLI
{
	class MainClass
	{
		static readonly string edition = "Honest Investigation";

		public static void Main(string[] args)
		{
			Console.WriteLine(String.Format("ESBreaker: PSO2es Fan translation Patch tool | {0} edition", edition));
			Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
			Console.WriteLine("THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.");
			Console.WriteLine("Phantasy Star Online 2 ES is copyright from SEGA\n\n");

			Database d = new Database(true);
			d.InitFromFile();
			d.Parse();
			d.SaveToFile();
		}
	}
}
