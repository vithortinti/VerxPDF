namespace VerxPDF.Core.Utils
{
    public static class ProgressBar
    {
        public static void Update(int currentStep, int total, string message = null)
        {
            Console.CursorLeft = 0; // Define a posição do cursor para a coluna 0
            int progressBarWidth = 20; // Largura da barra de progresso

            // Calcula o número de caracteres a serem preenchidos na barra de progresso
            int progressWidth = (int)Math.Floor((double)currentStep / total * progressBarWidth);

            // Cria a representação da barra de progresso
            string progressBar = new string('#', progressWidth) + new string('-', progressBarWidth - progressWidth);

            // Exibe a barra de progresso
            Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
            Console.Write($"[{progressBar}] {currentStep}/{total} {(message != null ? "- " + message : "")}");
        }
    }
}
