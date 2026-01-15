namespace VoroFit.Domain.Enums
{
    public enum WorkoutExecutionStatusEnum
    {
        Unspecified = 0,
        Completed = 100,   // Treino concluído
        Partial = 200,     // Não finalizou tudo
        Skipped = 300      // Pulou o treino

    }
}
