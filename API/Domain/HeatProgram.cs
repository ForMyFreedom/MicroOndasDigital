
namespace API.Domain
{
    public class HeatProgramDto
    {
        public required string Name { get; set; }
        public required string Food { get; set; }
        public required int Time { get; set; }
        public required int Potency { get; set; }
        public required string HeatToken { get; set; }
        public string? Instructions { get; set; }
    }

    public class HeatProgram
    {
        private readonly static List<string> ALL_TOKENS = [];
        public string Name { get; private set; }
        public string Food { get; private set; }
        public int Time { get; private set; }
        public int Potency { get; private set; }
        public string HeatToken { get; private set; }
        public bool IsStandart { get; set; }
        public string Instructions { get; private set; }

        public HeatProgram(string name, string food, int time, int potency, string heatToken, bool isStandart, string? instructions)
        {
            if (ALL_TOKENS.Contains(heatToken))
            {
                throw new Exception("Não se pode repetir string de aquecimento");
            }
            
            if (heatToken == ".")
            {
                throw new Exception("A string de aquecimento padrão não pode ser selecionada");
            }

            this.Name = name;
            this.Food = food;
            this.Time = time;
            this.Potency = potency;
            this.HeatToken = heatToken;
            this.IsStandart = isStandart;
            this.Instructions = instructions ?? "";
            ALL_TOKENS.Add(heatToken);
        }

        public HeatProgram(HeatProgramDto dto) : this(dto.Name, dto.Food, dto.Time, dto.Potency, dto.HeatToken, false, dto.Instructions) { }

        public static List<HeatProgram> GetStandartPrograms()
        {
            List<HeatProgram> myList = [];

            myList.Add(new HeatProgram("Pipoca", "Pipoca (de micro-ondas)", 180, 7, "🌽", true, "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."));
            myList.Add(new HeatProgram("Leite", "Leite", 300, 5, "🥛", true, "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras."));
            myList.Add(new HeatProgram("Carnes de boi", "Carne em pedaço ou fatias", 840, 4, "🐮", true, "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."));
            myList.Add(new HeatProgram("Frango", "Frango (qualquer corte)", 480, 7, "🐔", true, "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."));
            myList.Add(new HeatProgram("Feijão", "Feijão congelado", 480, 9, "🥔", true, "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas."));

            return myList;
        }
    }
}
