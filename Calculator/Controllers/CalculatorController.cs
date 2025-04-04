using Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Calculator.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ExpectedValue = 15; // Для части II

            // Часть IV: Проверяем, нужно ли показать обработанный результат
            if (TempData.ContainsKey("ProcessedResult"))
            {
                ViewBag.ShowProcessedResult = true;
                ViewBag.ProcessedResult = TempData["ProcessedResult"].ToString();
            }

            return View(new CalculateModel());
        }

        [HttpPost]
        public IActionResult Calculate(CalculateModel model, string action)
        {
            if (action == "clear")
            {
                ModelState.Clear();
                return View("Index", new CalculateModel());
            }

            if (ModelState.IsValid)
            {
                switch (model.Operation)
                {
                    case "+": model.Result = model.Operand1 + model.Operand2; break;
                    case "-": model.Result = model.Operand1 - model.Operand2; break;
                    case "*": model.Result = model.Operand1 * model.Operand2; break;
                    case "/":
                        model.Result = model.Operand2 != 0
                            ? (decimal)model.Operand1 / model.Operand2
                            : decimal.MaxValue;
                        break;
                }

                // Часть II: Сравнение с ожидаемым значением
                ViewBag.ComparisonMessage = model.Result == ViewBag.ExpectedValue
                    ? "Результат совпадает с ожидаемым значением 15"
                    : "Результат не совпадает с ожидаемым значением 15";

                // Часть IV: Обработка и сохранение результата
                string operationResult = $"{model.Operand1} {model.Operation} {model.Operand2} = {model.Result}";

                // Обработка строки (вариант Б2)
                int equalSignIndex = operationResult.IndexOf('=');
                if (equalSignIndex != -1)
                {
                    operationResult = operationResult.Remove(equalSignIndex, 1)
                               .Insert(equalSignIndex, " равно ");
                }

                TempData["ProcessedResult"] = operationResult;
                TempData["ShowProcessButton"] = true;
            }

            return View("Index", model);
        }
    }
}