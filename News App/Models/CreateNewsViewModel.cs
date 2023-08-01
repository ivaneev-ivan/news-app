using System.ComponentModel.DataAnnotations;

namespace News_App.Models;

public class CreateNewsViewModel
{
    [Required(ErrorMessage = "Введите название")]
    [MinLength(5, ErrorMessage = "Длина должна превышать 5 символов")]
    [MaxLength(150, ErrorMessage = "Длина не должна превышать 150 символов")]
    public string title { get; set; }

    [Required(ErrorMessage = "Введите Описание")]
    public string description { set; get; }

    public DateTime created_at { get; set; } = DateTime.Now;
}