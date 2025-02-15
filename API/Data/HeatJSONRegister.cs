using API.Domain;
using API.Interfaces;
using System.Text.Json;

namespace API.Data
{
    public class HeatJSONRegister : IHeatProgramRepository
    {
        private static readonly string FolderName = "dist/";

        public Task Initialization(List<HeatProgram> initialHeatPrograms)
        {
            if (Directory.Exists(FolderName))
            {
                string[] files = Directory.GetFiles(FolderName);
                foreach (string filePath in files)
                {
                    string jsonData = File.ReadAllText(filePath);

                    HeatProgramDto? program = JsonSerializer.Deserialize<HeatProgramDto>(jsonData);
                    if (program != null)
                    {
                        initialHeatPrograms.Add(new HeatProgram(program));
                    }
                }
            }
            return Task.CompletedTask;
        }

        public Task<HeatProgram> Register(HeatProgramDto dto)
        {
            HeatProgram program = new(dto);
            string jsonFormat = JsonSerializer.Serialize(program);
            File.WriteAllText(FolderName + program.Name + ".json", jsonFormat);
            return Task.Run(() => program);
        }
    }
}
