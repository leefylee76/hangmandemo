namespace HangMan.Models
{
    public class GameViewModel : IGameViewModel
    {
        public string DisguisedWord { get; set; }
        public string ActualWord { get; set; }
        public int Lives { get; set; }
        public string GuessedLetters { get; set; }
        public StateOfPlay StateOfPlay { get; set; }
    }
}
