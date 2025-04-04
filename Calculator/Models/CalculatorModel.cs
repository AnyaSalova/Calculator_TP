using System.ComponentModel.DataAnnotations;

namespace Calculator.Models
{
    public class CalculateModel
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Первое число (0-65535)")]
        public ushort Operand1 { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Должно быть 1-5 цифр")]
        [Display(Name = "Второе число (0-65535)")]
        public string Operand2Str { get; set; }

        [Required(ErrorMessage = "Выберите операцию")]
        [Display(Name = "Операция")]
        public string Operation { get; set; }

        [Display(Name = "Результат")]
        public decimal? Result { get; set; }

        public ushort Operand2
        {
            get => ushort.TryParse(Operand2Str, out var num) ? num : (ushort)0;
            set => Operand2Str = value.ToString();
        }
    }
}