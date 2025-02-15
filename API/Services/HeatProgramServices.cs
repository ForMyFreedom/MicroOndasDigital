using API.Domain;
using API.Exceptions;
using API.Interfaces;

namespace API.Services
{
    public class HeatProgramServices
    {
        private readonly List<HeatProgram> AllHeatPrograms = [];
        private readonly IHeatProgramRepository repository;

        public HeatProgramServices(IHeatProgramRepository _repository)
        {
            repository = _repository;
            AllHeatPrograms = HeatProgram.GetStandartPrograms();
            repository.Initialization(AllHeatPrograms);
        }

        public List<HeatProgram> All()
        {
            return AllHeatPrograms;
        }

        public async Task<HeatProgram> Register(HeatProgramDto dto)
        {
            if (HeatProgram.ALL_TOKENS.Contains(dto.HeatToken))
            {
                throw new HeatException("Não se pode repetir string de aquecimento");
            }

            if(dto.HeatToken == ".")
            {
                throw new HeatException("A string de aquecimento padrão não pode ser selecionada");
            }

            HeatProgram program = await repository.Register(dto);

            AllHeatPrograms.Add(program);
            return program;
        }
    }
}
