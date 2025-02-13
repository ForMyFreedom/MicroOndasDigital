using Api.Domain;
using System.Text.Json;

namespace Api.Services
{
    public class HeatProgramServices
    {
        private static readonly List<HeatProgram> AllHeatPrograms = HeatProgram.GetStandartPrograms();
        private static readonly string FolderName = "dist/";

        public HeatProgramServices()
        {
            if (Directory.Exists(FolderName))
            {
                string[] files = Directory.GetFiles(FolderName);
                foreach(string filePath in files)
                {
                    string jsonData = File.ReadAllText(filePath);

                    HeatProgramDto? program = JsonSerializer.Deserialize<HeatProgramDto>(jsonData);
                    if (program != null)
                    {
                        AllHeatPrograms.Add(new HeatProgram(program));
                    }
                }
            }
        }

        public List<HeatProgram> All()
        {
            return AllHeatPrograms;
        }

        public HeatProgram Register(HeatProgramDto dto)
        {
            HeatProgram program = new(dto);
            AllHeatPrograms.Add(program);

            string jsonFormat = JsonSerializer.Serialize(program);
            File.WriteAllText(FolderName + program.Name+".json", jsonFormat);

            return program;
        }

    }
}
