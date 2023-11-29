using System.Reflection;
using System.Text.Json;

namespace CodingTest.Net.Infra.Data.Context
{
    ///  <summary>
    ///  Class for save in a file json the data
    /// </summary>
    public class ArchiveJsonContext
    {
        public readonly string dataText = String.Empty;
        private string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Customers.json");


        ///  <summary>
        ///  Constructor of the save for json
        /// </summary>
        public ArchiveJsonContext()
        {
            dataText = ReadFromFileAsync().Result;
        }


        ///  <summary>
        ///  Save the object information in the file
        /// </summary>
        public async Task SaveIntoFileAsync(object values)
        {
            await using (FileStream createStream = File.Create(filePath))
            {
                await JsonSerializer.SerializeAsync(createStream, values);
            }

        }


        ///  <summary>
        ///  Read the object information in the file
        /// </summary>
        private async Task<string> ReadFromFileAsync() 
        {
            string text = string.Empty;

            if (File.Exists(filePath))
            {
                text = await System.IO.File.ReadAllTextAsync(filePath);
                
            }
            return text; 
        
        }

    }
}
