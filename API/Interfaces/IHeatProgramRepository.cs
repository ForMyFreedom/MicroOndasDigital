using API.Domain;

namespace API.Interfaces
{
    public interface IHeatProgramRepository
    {
        Task Initialization(List<HeatProgram> initialHeatPrograms);
        Task<HeatProgram> Register(HeatProgramDto dto);
    }
}
