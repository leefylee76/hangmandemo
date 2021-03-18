namespace HangMan.Models
{
    public interface IGameViewModel
    {
        string GuessedLetters { get; set; }
        StateOfPlay StateOfPlay { get; set; }
        int Lives { get; set; }
        string DisguisedWord { get; set; }
    }
}