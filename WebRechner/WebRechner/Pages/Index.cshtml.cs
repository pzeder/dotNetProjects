using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebRechner.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string? InputText { get; set; } = "";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (InputText != null) HttpContext.Session.SetString("textInput", InputText);
        }

        public IActionResult OnPost([FromForm] string inputText)
        {
            InputText = "" + ComputeResult(inputText);
            return Page(); 
        }

        private int ComputeResult(string input)
        {
            if (input == null || input.Length == 0) return -1;
            Stack<int> numbers = new Stack<int>();
            string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                switch (word)
                {
                    case "POP":
                        if (numbers.Count == 0) return -1;
                        numbers.Pop();
                        break;
                    case "DUP":
                        if (numbers.Count == 0) return -1;
                        int number = numbers.Pop();
                        numbers.Push(number);
                        numbers.Push(number);
                        break;
                    case "+":
                        if (numbers.Count < 2) return -1;
                        int summe = numbers.Pop() + numbers.Pop();
                        numbers.Push(summe);
                        break;
                    case "-":
                        if (numbers.Count < 2) return -1;
                        int diff = numbers.Pop() - numbers.Pop();
                        numbers.Push(diff);
                        break;
                    default:
                        int newNumber = -1;
                        if (int.TryParse(word, out newNumber))
                        {
                            numbers.Push((int) newNumber);
                        }
                        else
                        {
                            return -1;
                        }
                        break;
                }
            }
            if (numbers.Count == 0) return -1;    
            return numbers.Peek();
        }
    }
}
